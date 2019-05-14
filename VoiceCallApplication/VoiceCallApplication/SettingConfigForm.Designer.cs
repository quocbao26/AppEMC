namespace VoiceCallApplication
{
    partial class SettingConfigForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cmbServerLink = new System.Windows.Forms.ComboBox();
            this.txtbServerPort = new System.Windows.Forms.TextBox();
            this.txtbServerIP = new System.Windows.Forms.TextBox();
            this.txtbAgentPassword = new System.Windows.Forms.TextBox();
            this.txtbAgentID = new System.Windows.Forms.TextBox();
            this.txtbStationID = new System.Windows.Forms.TextBox();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.lblServerLink = new System.Windows.Forms.Label();
            this.lblAgentPassword = new System.Windows.Forms.Label();
            this.lblAgentID = new System.Windows.Forms.Label();
            this.lblStationID = new System.Windows.Forms.Label();
            this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.cmbServerLink);
            this.groupBox1.Controls.Add(this.txtbServerPort);
            this.groupBox1.Controls.Add(this.txtbServerIP);
            this.groupBox1.Controls.Add(this.txtbAgentPassword);
            this.groupBox1.Controls.Add(this.txtbAgentID);
            this.groupBox1.Controls.Add(this.txtbStationID);
            this.groupBox1.Controls.Add(this.lblServerIP);
            this.groupBox1.Controls.Add(this.lblServerPort);
            this.groupBox1.Controls.Add(this.lblServerLink);
            this.groupBox1.Controls.Add(this.lblAgentPassword);
            this.groupBox1.Controls.Add(this.lblAgentID);
            this.groupBox1.Controls.Add(this.lblStationID);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config EMC";
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(238, 315);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 28);
            this.btnConnect.TabIndex = 17;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(434, 122);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(47, 24);
            this.btnBrowse.TabIndex = 16;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cmbServerLink
            // 
            this.cmbServerLink.FormattingEnabled = true;
            this.cmbServerLink.Location = new System.Drawing.Point(186, 126);
            this.cmbServerLink.Margin = new System.Windows.Forms.Padding(4);
            this.cmbServerLink.Name = "cmbServerLink";
            this.cmbServerLink.Size = new System.Drawing.Size(229, 24);
            this.cmbServerLink.TabIndex = 15;
            // 
            // txtbServerPort
            // 
            this.txtbServerPort.Location = new System.Drawing.Point(186, 87);
            this.txtbServerPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtbServerPort.Name = "txtbServerPort";
            this.txtbServerPort.Size = new System.Drawing.Size(229, 22);
            this.txtbServerPort.TabIndex = 13;
            this.txtbServerPort.Text = "29096";
            // 
            // txtbServerIP
            // 
            this.txtbServerIP.Location = new System.Drawing.Point(186, 43);
            this.txtbServerIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtbServerIP.Name = "txtbServerIP";
            this.txtbServerIP.Size = new System.Drawing.Size(229, 22);
            this.txtbServerIP.TabIndex = 11;
            this.txtbServerIP.Text = "172.23.24.223";
            // 
            // txtbAgentPassword
            // 
            this.txtbAgentPassword.Location = new System.Drawing.Point(186, 268);
            this.txtbAgentPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtbAgentPassword.Name = "txtbAgentPassword";
            this.txtbAgentPassword.PasswordChar = '*';
            this.txtbAgentPassword.Size = new System.Drawing.Size(229, 22);
            this.txtbAgentPassword.TabIndex = 14;
            this.txtbAgentPassword.Text = "1234";
            // 
            // txtbAgentID
            // 
            this.txtbAgentID.Location = new System.Drawing.Point(186, 218);
            this.txtbAgentID.Margin = new System.Windows.Forms.Padding(4);
            this.txtbAgentID.Name = "txtbAgentID";
            this.txtbAgentID.Size = new System.Drawing.Size(229, 22);
            this.txtbAgentID.TabIndex = 12;
            this.txtbAgentID.Text = "6001";
            // 
            // txtbStationID
            // 
            this.txtbStationID.Location = new System.Drawing.Point(186, 172);
            this.txtbStationID.Margin = new System.Windows.Forms.Padding(4);
            this.txtbStationID.Name = "txtbStationID";
            this.txtbStationID.Size = new System.Drawing.Size(229, 22);
            this.txtbStationID.TabIndex = 10;
            this.txtbStationID.Text = "1011";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Location = new System.Drawing.Point(21, 43);
            this.lblServerIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(98, 17);
            this.lblServerIP.TabIndex = 5;
            this.lblServerIP.Text = "XML Server IP";
            // 
            // lblServerPort
            // 
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.Location = new System.Drawing.Point(21, 90);
            this.lblServerPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(112, 17);
            this.lblServerPort.TabIndex = 7;
            this.lblServerPort.Text = "XML Server Port";
            // 
            // lblServerLink
            // 
            this.lblServerLink.AutoSize = true;
            this.lblServerLink.Location = new System.Drawing.Point(21, 129);
            this.lblServerLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerLink.Name = "lblServerLink";
            this.lblServerLink.Size = new System.Drawing.Size(41, 17);
            this.lblServerLink.TabIndex = 9;
            this.lblServerLink.Text = "Links";
            // 
            // lblAgentPassword
            // 
            this.lblAgentPassword.AutoSize = true;
            this.lblAgentPassword.Location = new System.Drawing.Point(21, 273);
            this.lblAgentPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAgentPassword.Name = "lblAgentPassword";
            this.lblAgentPassword.Size = new System.Drawing.Size(110, 17);
            this.lblAgentPassword.TabIndex = 8;
            this.lblAgentPassword.Text = "Agent Password";
            // 
            // lblAgentID
            // 
            this.lblAgentID.AutoSize = true;
            this.lblAgentID.Location = new System.Drawing.Point(21, 218);
            this.lblAgentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAgentID.Name = "lblAgentID";
            this.lblAgentID.Size = new System.Drawing.Size(62, 17);
            this.lblAgentID.TabIndex = 6;
            this.lblAgentID.Text = "Agent ID";
            // 
            // lblStationID
            // 
            this.lblStationID.AutoSize = true;
            this.lblStationID.Location = new System.Drawing.Point(21, 172);
            this.lblStationID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStationID.Name = "lblStationID";
            this.lblStationID.Size = new System.Drawing.Size(69, 17);
            this.lblStationID.TabIndex = 4;
            this.lblStationID.Text = "Station ID";
            // 
            // SettingConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 374);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingConfigForm";
            this.Text = "SettingConfigForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.Label lblServerLink;
        private System.Windows.Forms.Label lblAgentPassword;
        private System.Windows.Forms.Label lblAgentID;
        private System.Windows.Forms.Label lblStationID;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ComboBox cmbServerLink;
        private System.Windows.Forms.TextBox txtbServerPort;
        private System.Windows.Forms.TextBox txtbServerIP;
        private System.Windows.Forms.TextBox txtbAgentPassword;
        private System.Windows.Forms.TextBox txtbAgentID;
        private System.Windows.Forms.TextBox txtbStationID;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolTip myToolTip;
    }
}