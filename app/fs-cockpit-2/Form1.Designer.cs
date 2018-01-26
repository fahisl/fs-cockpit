namespace fs_cockpit_2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonRequestData = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.richResponse = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonPitchDisconnect = new System.Windows.Forms.Button();
            this.buttonPitchConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboPitchCom = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonBankDisconnect = new System.Windows.Forms.Button();
            this.buttonBankConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBankTest = new System.Windows.Forms.Button();
            this.comboBankCom = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textAircraft = new System.Windows.Forms.TextBox();
            this.textSimPitch = new System.Windows.Forms.TextBox();
            this.textControllerPitch = new System.Windows.Forms.TextBox();
            this.textControllerPitchStatus2 = new System.Windows.Forms.TextBox();
            this.textPitchControllerStatus = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textControllerBankStatus = new System.Windows.Forms.TextBox();
            this.textControllerBankStatus2 = new System.Windows.Forms.TextBox();
            this.textControllerBank = new System.Windows.Forms.TextBox();
            this.textSimBank = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(6, 19);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click_1);
            // 
            // buttonRequestData
            // 
            this.buttonRequestData.Location = new System.Drawing.Point(87, 19);
            this.buttonRequestData.Name = "buttonRequestData";
            this.buttonRequestData.Size = new System.Drawing.Size(75, 23);
            this.buttonRequestData.TabIndex = 1;
            this.buttonRequestData.Text = "Start";
            this.buttonRequestData.UseVisualStyleBackColor = true;
            this.buttonRequestData.Click += new System.EventHandler(this.buttonRequestData_Click_1);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(168, 19);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click_1);
            // 
            // richResponse
            // 
            this.richResponse.BackColor = System.Drawing.SystemColors.Window;
            this.richResponse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richResponse.Cursor = System.Windows.Forms.Cursors.Default;
            this.richResponse.Location = new System.Drawing.Point(6, 19);
            this.richResponse.Name = "richResponse";
            this.richResponse.ReadOnly = true;
            this.richResponse.Size = new System.Drawing.Size(493, 187);
            this.richResponse.TabIndex = 3;
            this.richResponse.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRequestData);
            this.groupBox1.Controls.Add(this.buttonConnect);
            this.groupBox1.Controls.Add(this.buttonDisconnect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 49);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richResponse);
            this.groupBox2.Location = new System.Drawing.Point(12, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(507, 212);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonPitchDisconnect);
            this.groupBox3.Controls.Add(this.buttonPitchConnect);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.comboPitchCom);
            this.groupBox3.Location = new System.Drawing.Point(12, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 75);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pitch Axis";
            // 
            // buttonPitchDisconnect
            // 
            this.buttonPitchDisconnect.Enabled = false;
            this.buttonPitchDisconnect.Location = new System.Drawing.Point(168, 46);
            this.buttonPitchDisconnect.Name = "buttonPitchDisconnect";
            this.buttonPitchDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonPitchDisconnect.TabIndex = 7;
            this.buttonPitchDisconnect.Text = "Disconnect";
            this.buttonPitchDisconnect.UseVisualStyleBackColor = true;
            this.buttonPitchDisconnect.Click += new System.EventHandler(this.buttonPitchDisconnectClick);
            // 
            // buttonPitchConnect
            // 
            this.buttonPitchConnect.Enabled = false;
            this.buttonPitchConnect.Location = new System.Drawing.Point(6, 46);
            this.buttonPitchConnect.Name = "buttonPitchConnect";
            this.buttonPitchConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonPitchConnect.TabIndex = 9;
            this.buttonPitchConnect.Text = "Connect";
            this.buttonPitchConnect.UseVisualStyleBackColor = true;
            this.buttonPitchConnect.Click += new System.EventHandler(this.buttonPitchConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "COM Port";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(87, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Test ON";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboPitchCom
            // 
            this.comboPitchCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPitchCom.FormattingEnabled = true;
            this.comboPitchCom.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.comboPitchCom.Location = new System.Drawing.Point(87, 19);
            this.comboPitchCom.Name = "comboPitchCom";
            this.comboPitchCom.Size = new System.Drawing.Size(156, 21);
            this.comboPitchCom.TabIndex = 0;
            this.comboPitchCom.SelectedIndexChanged += new System.EventHandler(this.comboPitchCom_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonBankDisconnect);
            this.groupBox4.Controls.Add(this.buttonBankConnect);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.buttonBankTest);
            this.groupBox4.Controls.Add(this.comboBankCom);
            this.groupBox4.Location = new System.Drawing.Point(12, 149);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 75);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bank Axis";
            // 
            // buttonBankDisconnect
            // 
            this.buttonBankDisconnect.Enabled = false;
            this.buttonBankDisconnect.Location = new System.Drawing.Point(168, 46);
            this.buttonBankDisconnect.Name = "buttonBankDisconnect";
            this.buttonBankDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonBankDisconnect.TabIndex = 7;
            this.buttonBankDisconnect.Text = "Disconnect";
            this.buttonBankDisconnect.UseVisualStyleBackColor = true;
            this.buttonBankDisconnect.Click += new System.EventHandler(this.buttonBankDisconnect_Click);
            // 
            // buttonBankConnect
            // 
            this.buttonBankConnect.Enabled = false;
            this.buttonBankConnect.Location = new System.Drawing.Point(6, 46);
            this.buttonBankConnect.Name = "buttonBankConnect";
            this.buttonBankConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonBankConnect.TabIndex = 9;
            this.buttonBankConnect.Text = "Connect";
            this.buttonBankConnect.UseVisualStyleBackColor = true;
            this.buttonBankConnect.Click += new System.EventHandler(this.buttonBankConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "COM Port";
            // 
            // buttonBankTest
            // 
            this.buttonBankTest.Enabled = false;
            this.buttonBankTest.Location = new System.Drawing.Point(87, 46);
            this.buttonBankTest.Name = "buttonBankTest";
            this.buttonBankTest.Size = new System.Drawing.Size(75, 23);
            this.buttonBankTest.TabIndex = 7;
            this.buttonBankTest.Text = "Test ON";
            this.buttonBankTest.UseVisualStyleBackColor = true;
            this.buttonBankTest.Click += new System.EventHandler(this.buttonBankTest_Click);
            // 
            // comboBankCom
            // 
            this.comboBankCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBankCom.FormattingEnabled = true;
            this.comboBankCom.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.comboBankCom.Location = new System.Drawing.Point(87, 19);
            this.comboBankCom.Name = "comboBankCom";
            this.comboBankCom.Size = new System.Drawing.Size(156, 21);
            this.comboBankCom.TabIndex = 0;
            this.comboBankCom.SelectedIndexChanged += new System.EventHandler(this.comboBankCom_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textAircraft);
            this.groupBox5.Location = new System.Drawing.Point(269, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(250, 48);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Prepar3D";
            // 
            // textAircraft
            // 
            this.textAircraft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAircraft.Location = new System.Drawing.Point(6, 19);
            this.textAircraft.Name = "textAircraft";
            this.textAircraft.ReadOnly = true;
            this.textAircraft.Size = new System.Drawing.Size(236, 20);
            this.textAircraft.TabIndex = 4;
            // 
            // textSimPitch
            // 
            this.textSimPitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSimPitch.Location = new System.Drawing.Point(7, 22);
            this.textSimPitch.Name = "textSimPitch";
            this.textSimPitch.ReadOnly = true;
            this.textSimPitch.Size = new System.Drawing.Size(115, 20);
            this.textSimPitch.TabIndex = 0;
            // 
            // textControllerPitch
            // 
            this.textControllerPitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textControllerPitch.Location = new System.Drawing.Point(7, 48);
            this.textControllerPitch.Name = "textControllerPitch";
            this.textControllerPitch.ReadOnly = true;
            this.textControllerPitch.Size = new System.Drawing.Size(115, 20);
            this.textControllerPitch.TabIndex = 1;
            // 
            // textControllerPitchStatus2
            // 
            this.textControllerPitchStatus2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textControllerPitchStatus2.Location = new System.Drawing.Point(128, 22);
            this.textControllerPitchStatus2.Name = "textControllerPitchStatus2";
            this.textControllerPitchStatus2.ReadOnly = true;
            this.textControllerPitchStatus2.Size = new System.Drawing.Size(115, 20);
            this.textControllerPitchStatus2.TabIndex = 2;
            // 
            // textPitchControllerStatus
            // 
            this.textPitchControllerStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPitchControllerStatus.Location = new System.Drawing.Point(128, 48);
            this.textPitchControllerStatus.Name = "textPitchControllerStatus";
            this.textPitchControllerStatus.ReadOnly = true;
            this.textPitchControllerStatus.Size = new System.Drawing.Size(115, 20);
            this.textPitchControllerStatus.TabIndex = 3;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textPitchControllerStatus);
            this.groupBox6.Controls.Add(this.textControllerPitchStatus2);
            this.groupBox6.Controls.Add(this.textControllerPitch);
            this.groupBox6.Controls.Add(this.textSimPitch);
            this.groupBox6.Location = new System.Drawing.Point(268, 68);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(250, 75);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Pitch Axis Status";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textControllerBankStatus);
            this.groupBox7.Controls.Add(this.textControllerBankStatus2);
            this.groupBox7.Controls.Add(this.textControllerBank);
            this.groupBox7.Controls.Add(this.textSimBank);
            this.groupBox7.Location = new System.Drawing.Point(269, 149);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(250, 75);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Bank Axis Status";
            // 
            // textControllerBankStatus
            // 
            this.textControllerBankStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textControllerBankStatus.Location = new System.Drawing.Point(128, 48);
            this.textControllerBankStatus.Name = "textControllerBankStatus";
            this.textControllerBankStatus.ReadOnly = true;
            this.textControllerBankStatus.Size = new System.Drawing.Size(115, 20);
            this.textControllerBankStatus.TabIndex = 3;
            // 
            // textControllerBankStatus2
            // 
            this.textControllerBankStatus2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textControllerBankStatus2.Location = new System.Drawing.Point(128, 22);
            this.textControllerBankStatus2.Name = "textControllerBankStatus2";
            this.textControllerBankStatus2.ReadOnly = true;
            this.textControllerBankStatus2.Size = new System.Drawing.Size(115, 20);
            this.textControllerBankStatus2.TabIndex = 2;
            // 
            // textControllerBank
            // 
            this.textControllerBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textControllerBank.Location = new System.Drawing.Point(7, 48);
            this.textControllerBank.Name = "textControllerBank";
            this.textControllerBank.ReadOnly = true;
            this.textControllerBank.Size = new System.Drawing.Size(115, 20);
            this.textControllerBank.TabIndex = 1;
            // 
            // textSimBank
            // 
            this.textSimBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSimBank.Location = new System.Drawing.Point(7, 22);
            this.textSimBank.Name = "textSimBank";
            this.textSimBank.ReadOnly = true;
            this.textSimBank.Size = new System.Drawing.Size(115, 20);
            this.textSimBank.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 450);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "JetSim 1.0.2-dev-smooth-pwm-bank";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonRequestData;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.RichTextBox richResponse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboPitchCom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonPitchConnect;
        private System.Windows.Forms.Button buttonPitchDisconnect;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonBankDisconnect;
        private System.Windows.Forms.Button buttonBankConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBankTest;
        private System.Windows.Forms.ComboBox comboBankCom;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textSimPitch;
        private System.Windows.Forms.TextBox textControllerPitch;
        private System.Windows.Forms.TextBox textControllerPitchStatus2;
        private System.Windows.Forms.TextBox textPitchControllerStatus;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox textControllerBankStatus;
        private System.Windows.Forms.TextBox textControllerBankStatus2;
        private System.Windows.Forms.TextBox textControllerBank;
        private System.Windows.Forms.TextBox textSimBank;
        private System.Windows.Forms.TextBox textAircraft;
    }
}

