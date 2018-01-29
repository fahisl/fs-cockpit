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

namespace fs_cockpit_2 {

    public partial class Form1 : Form {

        const int WM_USER_SIMCONNECT = 0x0402;
        SimConnect simconnect = null;
        SerialPort portPitch = null;
        SerialPort portBank = null;

        private BackgroundWorker portScanner;

        enum DEFINITIONS {
            Struct1,
        };

        enum DATA_REQUESTS {
            REQUEST_1,
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct Struct1 {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String title;
            public double pitch;
            public double bank;
            public double altitude;
        };

        public Form1() {
            InitializeComponent();
            InitializeWorkers();
            setButtons(true, false, false);
        }

        private void InitializeWorkers() {
            portScanner.DoWork += new DoWorkEventHandler(scanPorts);
            portScanner.RunWorkerCompleted += new RunWorkerCompletedEventHandler(scanPortsComplete);
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

        protected override void DefWndProc(ref Message m) {
            if (m.Msg == WM_USER_SIMCONNECT) {
                if (simconnect != null) {
                    simconnect.ReceiveMessage();
                }
            }
            else {
                base.DefWndProc(ref m);
            }
        }

        private void setButtons(bool bConnect, bool bGet, bool bDisconnect) {
            buttonConnect.Enabled = bConnect;
            buttonDisconnect.Enabled = bDisconnect;
        }

        private void closeConnection() {
            if (simconnect != null) {
                simconnect.Dispose();  // Dispose same as SimConnect_Close()
                simconnect = null;
                displayText("Connection to Prepar3D is closed");
            }
        }

        // Set up all the SimConnect related data definitions and event handlers
        private void initDataRequest() {
            try {
                // listen to connect and quit msgs
                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen);
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit);

                // listen to exceptions
                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException);

                // define a data structure
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "title", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "PLANE PITCH DEGREES", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "PLANE BANK DEGREES", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                // IMPORTANT: register it with the simconnect managed wrapper marshaller
                // if you skip this step, you will only receive a uint in the .dwData field.
                simconnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.Struct1);

                // catch a simobject data request
                //simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(simconnect_OnRecvSimobjectDataBytype);
                simconnect.OnRecvSimobjectData += new SimConnect.RecvSimobjectDataEventHandler(simconnect_OnRecvSimobjectData);
            }
            catch (COMException ex) {
                displayText(ex.Message);
            }
        }

        void simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data) {
            displayText("Connected to Prepar3D");
        }

        // The case where the user closes Prepar3D
        void simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data) {
            displayText("Prepar3D has exited");
            closeConnection();
        }

        void simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data) {
            displayText("Exception received: " + data.dwException);
        }

        // The case where the user closes the client
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            closeConnection();
            portPitch.Close();
            portBank.Close();
        }

        void simconnect_OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data) {
            switch ((DATA_REQUESTS)data.dwRequestID) {
                case DATA_REQUESTS.REQUEST_1:
                    Struct1 s1 = (Struct1)data.dwData[0];

                    if (textAircraft.Text != s1.title) {
                        textAircraft.Text = s1.title;
                    }

                    int simPitch = (int)(s1.pitch * 100);
                    textSimPitch.Text = "Sim angle: " + (double)simPitch / 100;
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

                    int simBank = (int)(s1.bank * 100);
                    textSimBank.Text = "Sim angle: " + (double)simBank / 100;
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
                    
                    break;

                default:
                    displayText("Unknown request ID: " + data.dwRequestID);
                    break;
            }
        }

        private void buttonConnect_Click_1(object sender, EventArgs e) {
            if (simconnect == null) {
                try {
                    simconnect = new SimConnect("Managed Data Request", this.Handle, WM_USER_SIMCONNECT, null, 0);
                    setButtons(false, true, true);
                    initDataRequest();
                    simconnect.RequestDataOnSimObject(
                        DATA_REQUESTS.REQUEST_1,
                        DEFINITIONS.Struct1,
                        SimConnect.SIMCONNECT_OBJECT_ID_USER,
                        SIMCONNECT_PERIOD.SIM_FRAME,
                        SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT,
                        0,
                        6,
                        0
                    );
                }
                catch (COMException ex) {
                    displayText("Unable to connect to Prepar3D:\n\n" + ex.Message);
                }
            }
            else {
                displayText("Error - try again");
                closeConnection();
                setButtons(true, false, false);
            }
        }

        private void buttonDisconnect_Click_1(object sender, EventArgs e) {
            closeConnection();
            setButtons(true, false, false);
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
    }
}
