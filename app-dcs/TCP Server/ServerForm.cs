using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Server {

    public partial class ServerForm : Form {

        private Socket _serverSocket, _clientSocket;
        private byte[] _buffer;

        public ServerForm() {
            // string path = @"C:\Users\Fahim\Documents\Source\Simple Async Socket\Simple Async Socket\bin\Debug\TCP Client.exe";
            // Process.Start(path);
            InitializeComponent();
            StartServer();
        }

        private void StartServer() {
            try {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3333));
                _serverSocket.Listen(0);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AcceptCallback(IAsyncResult AR) {
            try {
                _clientSocket = _serverSocket.EndAccept(AR);
                _buffer = new byte[_clientSocket.ReceiveBufferSize];
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiveCallback(IAsyncResult AR) {
            try {
                int received = _clientSocket.EndReceive(AR);
                Array.Resize(ref _buffer, received);
                string text = Encoding.ASCII.GetString(_buffer);
                string pitch = text.Substring(0, 6);
                string bank = text.Substring(6, 6);
                AppendToTextBox(text + ", pitch: " + pitch + ", bank: " + bank);
                Array.Resize(ref _buffer, _clientSocket.ReceiveBufferSize);
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppendToTextBox(string text) {
            MethodInvoker invoker = new MethodInvoker(delegate {
                textBox1.Text = text;
            });
            this.Invoke(invoker);
        }
    }
}
