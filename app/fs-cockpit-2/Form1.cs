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
            setButtons(true, false, false);
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
            buttonRequestData.Enabled = bGet;
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

                    textSimBank.Text = "Sim angle: " + (int)s1.bank;
                    if (portBank != null && portBank.IsOpen) {
                        portBank.Write("D" + (int)s1.bank + ",");
                        try
                        {
                            portBank.Write("getTarget,");
                            string response = portBank.ReadLine();
                            textControllerBank.Text = response;
                        }
                        catch (Exception)
                        {
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

        private void buttonRequestData_Click_1(object sender, EventArgs e) {
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
            displayText("Request sent...");
            buttonRequestData.Enabled = false;
        }

        // Response number 
        int response = 1;

        // Output text - display a maximum of 10 lines 
        string output = "\n\n\n\n\n\n\n\n\n\n";

        void displayText(string s) {
            // remove first string from output 
            output = output.Substring(output.IndexOf("\n") + 1);

            // add the new string 
            output += "\n" + response++ + ": " + s;

            // display it 
            richResponse.Text = output;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!portPitch.IsOpen) {
                displayText("Pitch axis COM port is not connected");
                return;
            }
            if (button1.Text == "Test ON") {
                portPitch.Write("on,");
                displayText("Verify LED is now ON on pitch Arduino controller");
                button1.Text = "Test OFF";
            }
            else {
                portPitch.Write("off,");
                displayText("Verify LED is now OFF on pitch Arduino controller");
                button1.Text = "Test ON";
            }
        }

        private void buttonPitchConnect_Click(object sender, EventArgs e) {
            if (comboPitchCom.Text == "") {
                displayText("Select pitch axis COM port first");
                return;
            }
            try {
                comboPitchCom.Enabled = false;
                portPitch = new SerialPort(comboPitchCom.Text, 115200, Parity.None, 8, StopBits.One);
                portPitch.Open();
                buttonPitchConnect.Enabled = false;
                buttonPitchDisconnect.Enabled = true;
                button1.Enabled = true;
                displayText("Connected to " + comboPitchCom.Text);
            }
            catch (Exception) {
                displayText("Error connecting to " + comboPitchCom.Text);
                comboPitchCom.Enabled = true;
                buttonPitchConnect.Enabled = true;
                buttonPitchDisconnect.Enabled = true;
            }
        }

        private void buttonPitchDisconnectClick(object sender, EventArgs e) {
            if (portPitch.IsOpen) {
                portPitch.Close();
                displayText("Disconnected from pitch axis COM port");
            }
            else {
                displayText("Already disconnected from pitch axis COM port");
            }
            comboPitchCom.Enabled = true;
            buttonPitchConnect.Enabled = true;
            button1.Enabled = false;
            buttonPitchDisconnect.Enabled = false;
        }

        private void comboPitchCom_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboPitchCom.Text != "") {
                buttonPitchConnect.Enabled = true;
            }
            else {
                buttonPitchConnect.Enabled = false;
            }
        }

        private void buttonBankConnect_Click(object sender, EventArgs e) {
            if (comboBankCom.Text == "") {
                displayText("Select bank axis COM port first");
                return;
            }
            try {
                comboBankCom.Enabled = false;
                portBank = new SerialPort(comboBankCom.Text, 115200, Parity.None, 8, StopBits.One);
                portBank.Open();
                buttonBankConnect.Enabled = false;
                buttonBankDisconnect.Enabled = true;
                buttonBankTest.Enabled = true;
                displayText("Connected to " + comboBankCom.Text);
            }
            catch (Exception) {
                displayText("Error connecting to " + comboBankCom.Text);
                comboBankCom.Enabled = true;
                buttonBankConnect.Enabled = true;
                buttonBankDisconnect.Enabled = true;
            }
        }

        private void buttonBankTest_Click(object sender, EventArgs e) {
            if (!portBank.IsOpen) {
                displayText("Bank axis COM port is not connected");
                return;
            }
            if (buttonBankTest.Text == "Test ON") {
                portBank.Write("on,");
                displayText("Verify LED is now ON on bank axis Arduino controller");
                buttonBankTest.Text = "Test OFF";
            }
            else {
                portBank.Write("off,");
                displayText("Verify LED is now OFF on bank axis Arduino controller");
                buttonBankTest.Text = "Test ON";
            }
        }

        private void comboBankCom_SelectedIndexChanged(object sender, EventArgs e) {
            buttonBankConnect.Enabled = (comboBankCom.Text != "");
        }

        private void buttonBankDisconnect_Click(object sender, EventArgs e) {
            if (portBank.IsOpen) {
                portBank.Close();
                displayText("Disconnected from bank axis COM port");
            }
            else {
                displayText("Already disconnected from bank axis COM port");
            }
            comboBankCom.Enabled = true;
            buttonBankConnect.Enabled = true;
            buttonBankTest.Enabled = false;
            buttonBankDisconnect.Enabled = false;
        }
    }
}
