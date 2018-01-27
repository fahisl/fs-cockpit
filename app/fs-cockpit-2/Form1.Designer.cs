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
            this.portScanner = new System.ComponentModel.BackgroundWorker();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.richResponse = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDisconnectAllPorts = new System.Windows.Forms.Button();
            this.buttonConnectAllPorts = new System.Windows.Forms.Button();
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
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBankConnStatus = new System.Windows.Forms.TextBox();
            this.textPitchConnStatus = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(6, 19);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(116, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click_1);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(128, 19);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(115, 23);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(507, 212);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonDisconnectAllPorts);
            this.groupBox3.Controls.Add(this.buttonConnectAllPorts);
            this.groupBox3.Location = new System.Drawing.Point(12, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 52);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Controllers";
            // 
            // buttonDisconnectAllPorts
            // 
            this.buttonDisconnectAllPorts.Enabled = false;
            this.buttonDisconnectAllPorts.Location = new System.Drawing.Point(128, 20);
            this.buttonDisconnectAllPorts.Name = "buttonDisconnectAllPorts";
            this.buttonDisconnectAllPorts.Size = new System.Drawing.Size(115, 23);
            this.buttonDisconnectAllPorts.TabIndex = 3;
            this.buttonDisconnectAllPorts.Text = "Disconnect";
            this.buttonDisconnectAllPorts.UseVisualStyleBackColor = true;
            this.buttonDisconnectAllPorts.Click += new System.EventHandler(this.buttonDisconnectAllPorts_Click);
            // 
            // buttonConnectAllPorts
            // 
            this.buttonConnectAllPorts.Location = new System.Drawing.Point(6, 20);
            this.buttonConnectAllPorts.Name = "buttonConnectAllPorts";
            this.buttonConnectAllPorts.Size = new System.Drawing.Size(116, 23);
            this.buttonConnectAllPorts.TabIndex = 11;
            this.buttonConnectAllPorts.Text = "Connect";
            this.buttonConnectAllPorts.UseVisualStyleBackColor = true;
            this.buttonConnectAllPorts.Click += new System.EventHandler(this.buttonConnectAllPorts_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textAircraft);
            this.groupBox5.Location = new System.Drawing.Point(270, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(250, 49);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Prepar3D Status";
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
            this.groupBox6.Location = new System.Drawing.Point(12, 126);
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
            this.groupBox7.Location = new System.Drawing.Point(270, 126);
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
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.textBankConnStatus);
            this.groupBox9.Controls.Add(this.textPitchConnStatus);
            this.groupBox9.Location = new System.Drawing.Point(269, 68);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(250, 49);
            this.groupBox9.TabIndex = 9;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Controller Status";
            // 
            // textBankConnStatus
            // 
            this.textBankConnStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBankConnStatus.Location = new System.Drawing.Point(128, 19);
            this.textBankConnStatus.Name = "textBankConnStatus";
            this.textBankConnStatus.ReadOnly = true;
            this.textBankConnStatus.Size = new System.Drawing.Size(116, 20);
            this.textBankConnStatus.TabIndex = 5;
            this.textBankConnStatus.Text = "Bank: Disconnected";
            // 
            // textPitchConnStatus
            // 
            this.textPitchConnStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPitchConnStatus.Location = new System.Drawing.Point(6, 19);
            this.textPitchConnStatus.Name = "textPitchConnStatus";
            this.textPitchConnStatus.ReadOnly = true;
            this.textPitchConnStatus.Size = new System.Drawing.Size(116, 20);
            this.textPitchConnStatus.TabIndex = 4;
            this.textPitchConnStatus.Text = "Pitch: Disconnected";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 431);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "JetSim 1.0.3";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.RichTextBox richResponse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
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
        private System.Windows.Forms.Button buttonDisconnectAllPorts;
        private System.Windows.Forms.Button buttonConnectAllPorts;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox textBankConnStatus;
        private System.Windows.Forms.TextBox textPitchConnStatus;
    }
}

