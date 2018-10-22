using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Async_Socket {

    public partial class ClientForm : Form {

        private Socket _clientSocket;

        public ClientForm() {

            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            try {
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientSocket.BeginConnect(new IPEndPoint(IPAddress.Loopback, 3333), new AsyncCallback(ConnectCallback), null);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConnect_Load(object sender, EventArgs e) {

        }
        private void ClientForm_Load(object sender, EventArgs e) {

        }


        private void ConnectCallback(IAsyncResult AR) {
            try {
                _clientSocket.EndConnect(AR);
                btnSend.Enabled = true;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSend_Click(object sender, EventArgs e) {
            try {
                byte[] buffer = Encoding.ASCII.GetBytes(textBox.Text);
                _clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (SocketException) {
                // Server closed, do nothing
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendCallback(IAsyncResult AR) {
            try {
                _clientSocket.EndSend(AR);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
