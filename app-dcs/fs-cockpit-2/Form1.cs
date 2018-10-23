using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LockheedMartin.Prepar3D.SimConnect;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;

namespace fs_cockpit_2 {

    public partial class Form1 : Form {

        SerialPort portPitch = null;
        SerialPort portBank = null;
        private Socket _serverSocket, _clientSocket;
        private byte[] _buffer;
        private BackgroundWorker portScanner;

        public Form1() {
            InitializeComponent();
            InitializeWorkers();
            StartServer();
        }

        private void InitializeWorkers() {
            portScanner.DoWork += new DoWorkEventHandler(scanPorts);
            portScanner.RunWorkerCompleted += new RunWorkerCompletedEventHandler(scanPortsComplete);
        }

        private void SetStatus(string status) {
            MethodInvoker invoker = new MethodInvoker(delegate {
                textAircraft.Text = status;
            });
            this.Invoke(invoker);
        }

        private void StartServer() {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3333));
            _serverSocket.Listen(0);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            textAircraft.Text = "Awaiting connection";
        }

        private void AcceptCallback(IAsyncResult AR) {
            _clientSocket = _serverSocket.EndAccept(AR);
            _buffer = new byte[_clientSocket.ReceiveBufferSize];
            _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            SetStatus("Connected");
        }

        private void ReceiveCallback(IAsyncResult AR) {
            int received = _clientSocket.EndReceive(AR);
            Array.Resize(ref _buffer, received);
            string text = Encoding.ASCII.GetString(_buffer);
            try {
                string pitch = text.Substring(0, 6);
                UpdatePitchController(pitch);
                SetSimPitch(pitch);
                string bank = text.Substring(6, 6);
                UpdateBankController(bank);
                SetSimBank(bank);
                
            }
            catch (System.ArgumentOutOfRangeException ex) {
                // do nothing
            }
            Array.Resize(ref _buffer, _clientSocket.ReceiveBufferSize);
            _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
        }

        private void SetSimPitch(string rawAngle) {
            MethodInvoker invoker = new MethodInvoker(delegate {
                textSimPitch.Text = "Sim: " + rawAngle;
            });
            this.Invoke(invoker);
        }

        private void SetSimBank(string rawAngle) {
            MethodInvoker invoker = new MethodInvoker(delegate {
                textSimBank.Text = "Sim: " + rawAngle;
            });
            this.Invoke(invoker);
        }

        private void UpdatePitchController(string simPitch) {
            MethodInvoker invoker = new MethodInvoker(delegate {
                if (portPitch != null && portPitch.IsOpen) {
                    portPitch.Write("D" + simPitch + ",");
                    try {
                        portPitch.Write("getTarget,");
                        string response = portPitch.ReadLine();
                        textControllerPitch.Text = response;

                        portPitch.Write("getPWM,");
                        textPitchControllerStatus.Text = portPitch.ReadLine();

                        portPitch.Write("getTargetPot,");
                        textControllerPitchStatus2.Text = portPitch.ReadLine();
                    }
                    catch (Exception) {
                        displayText("Could not read back from pitch controller");
                    }
                }
            });
            this.Invoke(invoker);
        }

        private void UpdateBankController(string simBank) {
            MethodInvoker invoker = new MethodInvoker(delegate {
                if (portBank != null && portBank.IsOpen) {
                    portBank.Write("D" + simBank + ",");
                    try {
                        portBank.Write("getTarget,");
                        string response = portBank.ReadLine();
                        textControllerBank.Text = response;

                        portBank.Write("getPWM,");
                        textControllerBankStatus.Text = portBank.ReadLine();

                        portBank.Write("getTargetPot,");
                        textControllerBankStatus2.Text = portBank.ReadLine();
                    }
                    catch (Exception) {
                        displayText("Could not read back from bank controller");
                    }
                }
            });
            this.Invoke(invoker);
        }

        private void scanPorts(object sender, DoWorkEventArgs e) {
            BackgroundWorker worker = sender as BackgroundWorker;

            getPortWithId();
            e.Result = true;
        }

        private void scanPortsComplete(object sender, RunWorkerCompletedEventArgs e) {
            if (portPitch != null) {
                portPitch.Open();
                textPitchConnStatus.Text = "Pitch: connected";
                buttonDisconnectAllPorts.Enabled = true;
                displayText("Connected to pitch axis controller");
            }
            else {
                textPitchConnStatus.Text = "Pitch: not found";
                displayText("Pitch axis controller was not detected");
            }
            if (portBank != null) {
                portBank.Open();
                textBankConnStatus.Text = "Bank: connected";
                buttonDisconnectAllPorts.Enabled = true;
                displayText("Connected to bank axis controller");
            }
            else {
                textBankConnStatus.Text = "Bank: not found";
                displayText("Bank axis controller was not detected");
            }
        }
        

        // The case where the user closes the client
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            portPitch.Close();
            portBank.Close();
        }

        int response = 1;
        string output = "\n\n\n\n\n\n\n\n\n\n\n\n";
        void displayText(string s) {
            output = output.Substring(output.IndexOf("\n") + 1);
            output += "\n" + response++ + ": " + s;
            richResponse.Text = output;
        }

        private void getPortWithId() {
            string[] portNames = SerialPort.GetPortNames();
            foreach (string portName in portNames) {
                if (portPitch != null && portBank != null) return;
                SerialPort tmpPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
                try {
                    tmpPort.ReadTimeout = 500;
                    tmpPort.Open();
                    System.Threading.Thread.Sleep(3000);
                    tmpPort.Write("ident,");
                    string boardId = tmpPort.ReadLine();
                    if (boardId.Contains("c5b6d4a4-09f2-4a3f-ae90-2a2b3c24d842")) {
                        tmpPort.Close();
                        portBank = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
                    }
                    else if (boardId.Contains("bcbc7cc9-f2be-4303-87f1-9ca2e0378e0b")) {
                        tmpPort.Close();
                        portPitch = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
                    }
                }
                catch (Exception) {}
                tmpPort.Close();
            }
        }

        private void buttonConnectAllPorts_Click(object sender, EventArgs e) {
            buttonConnectAllPorts.Enabled = false;
            if (portPitch == null || portBank == null) {
                textPitchConnStatus.Text = "Pitch: scanning";
                textBankConnStatus.Text = "Bank: scanning";
                displayText("Scanning for motion controllers");
                portScanner.RunWorkerAsync();
            }
            else {
                portPitch.Open();
                textPitchConnStatus.Text = "Pitch: connected";
                displayText("Connected to pitch axis controller");
                buttonDisconnectAllPorts.Enabled = true;

                portBank.Open();
                textBankConnStatus.Text = "Bank: connected";
                displayText("Connected to bank axis controller");
                buttonDisconnectAllPorts.Enabled = true;
            }
            
        }

        private void buttonDisconnectAllPorts_Click(object sender, EventArgs e) {
            buttonDisconnectAllPorts.Enabled = false;
            if (portPitch != null) {
                portPitch.Close();
                textPitchConnStatus.Text = "Pitch: disconnected";
                buttonConnectAllPorts.Enabled = true;
                displayText("Disconnected from pitch axis controller");
            }
            if (portBank != null) {
                portBank.Close();
                textBankConnStatus.Text = "Bank: disconnected";
                buttonConnectAllPorts.Enabled = true;
                displayText("Disconnected from bank axis controller");
            }
        }

        private void richResponse_TextChanged(object sender, EventArgs e) {

        }
    }
}
