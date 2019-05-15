namespace VoiceCallApplication
{
    partial class VoiceCallForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoiceCallForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnDrop = new System.Windows.Forms.Button();
            this.btnPlaceCall = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAux = new System.Windows.Forms.Button();
            this.btnAvailable = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnConference = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnKeyBoard = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtIncoming = new System.Windows.Forms.TextBox();
            this.txtbDialNumber = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lbAgtName = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblStation = new System.Windows.Forms.ToolStripLabel();
            this.lblCallTimer = new System.Windows.Forms.Label();
            this.lblHoldTimer = new System.Windows.Forms.Label();
            this.lbAgentID = new System.Windows.Forms.ToolStripLabel();
            this.callTimer = new System.Windows.Forms.Timer(this.components);
            this.holdTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLogout);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(139, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login/Logout";
            // 
            // btnLogout
            // 
            this.btnLogout.Enabled = false;
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.Location = new System.Drawing.Point(72, 21);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(56, 47);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
            this.btnLogin.Location = new System.Drawing.Point(10, 21);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(56, 47);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnHold);
            this.groupBox2.Controls.Add(this.btnDrop);
            this.groupBox2.Controls.Add(this.btnPlaceCall);
            this.groupBox2.Location = new System.Drawing.Point(147, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 75);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phone";
            // 
            // btnHold
            // 
            this.btnHold.Enabled = false;
            this.btnHold.Image = ((System.Drawing.Image)(resources.GetObject("btnHold.Image")));
            this.btnHold.Location = new System.Drawing.Point(130, 22);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(56, 47);
            this.btnHold.TabIndex = 4;
            this.btnHold.UseVisualStyleBackColor = true;
            this.btnHold.Click += new System.EventHandler(this.btnHold_Click);
            // 
            // btnDrop
            // 
            this.btnDrop.Enabled = false;
            this.btnDrop.Image = ((System.Drawing.Image)(resources.GetObject("btnDrop.Image")));
            this.btnDrop.Location = new System.Drawing.Point(68, 22);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(56, 47);
            this.btnDrop.TabIndex = 3;
            this.btnDrop.UseVisualStyleBackColor = true;
            this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
            // 
            // btnPlaceCall
            // 
            this.btnPlaceCall.Enabled = false;
            this.btnPlaceCall.Image = ((System.Drawing.Image)(resources.GetObject("btnPlaceCall.Image")));
            this.btnPlaceCall.Location = new System.Drawing.Point(6, 21);
            this.btnPlaceCall.Name = "btnPlaceCall";
            this.btnPlaceCall.Size = new System.Drawing.Size(56, 47);
            this.btnPlaceCall.TabIndex = 2;
            this.btnPlaceCall.UseVisualStyleBackColor = true;
            this.btnPlaceCall.Click += new System.EventHandler(this.btnPlaceCall_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAux);
            this.groupBox3.Controls.Add(this.btnAvailable);
            this.groupBox3.Location = new System.Drawing.Point(2, 92);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 75);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // btnAux
            // 
            this.btnAux.Enabled = false;
            this.btnAux.Image = ((System.Drawing.Image)(resources.GetObject("btnAux.Image")));
            this.btnAux.Location = new System.Drawing.Point(72, 20);
            this.btnAux.Name = "btnAux";
            this.btnAux.Size = new System.Drawing.Size(56, 47);
            this.btnAux.TabIndex = 5;
            this.btnAux.UseVisualStyleBackColor = true;
            this.btnAux.Click += new System.EventHandler(this.btnAux_Click);
            // 
            // btnAvailable
            // 
            this.btnAvailable.Enabled = false;
            this.btnAvailable.Image = ((System.Drawing.Image)(resources.GetObject("btnAvailable.Image")));
            this.btnAvailable.Location = new System.Drawing.Point(10, 22);
            this.btnAvailable.Name = "btnAvailable";
            this.btnAvailable.Size = new System.Drawing.Size(56, 47);
            this.btnAvailable.TabIndex = 4;
            this.btnAvailable.UseVisualStyleBackColor = true;
            this.btnAvailable.Click += new System.EventHandler(this.btnAvailable_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnConference);
            this.groupBox4.Controls.Add(this.btnTransfer);
            this.groupBox4.Controls.Add(this.btnKeyBoard);
            this.groupBox4.Location = new System.Drawing.Point(147, 92);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(194, 75);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Consult";
            // 
            // btnConference
            // 
            this.btnConference.Enabled = false;
            this.btnConference.Image = ((System.Drawing.Image)(resources.GetObject("btnConference.Image")));
            this.btnConference.Location = new System.Drawing.Point(130, 21);
            this.btnConference.Name = "btnConference";
            this.btnConference.Size = new System.Drawing.Size(56, 47);
            this.btnConference.TabIndex = 7;
            this.btnConference.UseVisualStyleBackColor = true;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Enabled = false;
            this.btnTransfer.Image = ((System.Drawing.Image)(resources.GetObject("btnTransfer.Image")));
            this.btnTransfer.Location = new System.Drawing.Point(68, 21);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(56, 47);
            this.btnTransfer.TabIndex = 6;
            this.btnTransfer.UseVisualStyleBackColor = true;
            // 
            // btnKeyBoard
            // 
            this.btnKeyBoard.Enabled = false;
            this.btnKeyBoard.Image = ((System.Drawing.Image)(resources.GetObject("btnKeyBoard.Image")));
            this.btnKeyBoard.Location = new System.Drawing.Point(6, 20);
            this.btnKeyBoard.Name = "btnKeyBoard";
            this.btnKeyBoard.Size = new System.Drawing.Size(56, 47);
            this.btnKeyBoard.TabIndex = 5;
            this.btnKeyBoard.UseVisualStyleBackColor = true;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(360, 23);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(139, 22);
            this.txtStatus.TabIndex = 5;
            this.txtStatus.Text = " Logged Off";
            // 
            // txtIncoming
            // 
            this.txtIncoming.Location = new System.Drawing.Point(360, 51);
            this.txtIncoming.Name = "txtIncoming";
            this.txtIncoming.ReadOnly = true;
            this.txtIncoming.Size = new System.Drawing.Size(139, 22);
            this.txtIncoming.TabIndex = 6;
            // 
            // txtbDialNumber
            // 
            this.txtbDialNumber.Location = new System.Drawing.Point(360, 79);
            this.txtbDialNumber.Name = "txtbDialNumber";
            this.txtbDialNumber.ReadOnly = true;
            this.txtbDialNumber.Size = new System.Drawing.Size(139, 22);
            this.txtbDialNumber.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 190);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 26);
            this.panel1.TabIndex = 10;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbAgtName,
            this.toolStripSeparator1,
            this.lblStation,
            this.lbAgentID});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(569, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lbAgtName
            // 
            this.lbAgtName.Name = "lbAgtName";
            this.lbAgtName.Size = new System.Drawing.Size(90, 22);
            this.lbAgtName.Text = "lblAgtName";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblStation
            // 
            this.lblStation.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(73, 25);
            this.lblStation.Text = "lblStation";
            // 
            // lblCallTimer
            // 
            this.lblCallTimer.AutoSize = true;
            this.lblCallTimer.Location = new System.Drawing.Point(357, 127);
            this.lblCallTimer.Name = "lblCallTimer";
            this.lblCallTimer.Size = new System.Drawing.Size(81, 17);
            this.lblCallTimer.TabIndex = 11;
            this.lblCallTimer.Text = "lblCallTimer";
            // 
            // lblHoldTimer
            // 
            this.lblHoldTimer.AutoSize = true;
            this.lblHoldTimer.Location = new System.Drawing.Point(453, 127);
            this.lblHoldTimer.Name = "lblHoldTimer";
            this.lblHoldTimer.Size = new System.Drawing.Size(87, 17);
            this.lblHoldTimer.TabIndex = 12;
            this.lblHoldTimer.Text = "lblHoldTimer";
            // 
            // lbAgentID
            // 
            this.lbAgentID.Name = "lbAgentID";
            this.lbAgentID.Size = new System.Drawing.Size(77, 22);
            this.lbAgentID.Text = "lbAgentID";
            // 
            // callTimer
            // 
            this.callTimer.Interval = 1000;
            this.callTimer.Tick += new System.EventHandler(this.callTimer_Tick);
            // 
            // holdTimer
            // 
            this.holdTimer.Interval = 1000;
            this.holdTimer.Tick += new System.EventHandler(this.holdTimer_Tick);
            // 
            // VoiceCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 216);
            this.Controls.Add(this.lblHoldTimer);
            this.Controls.Add(this.lblCallTimer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtbDialNumber);
            this.Controls.Add(this.txtIncoming);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "VoiceCallForm";
            this.Text = "VoiceCallForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtIncoming;
        private System.Windows.Forms.TextBox txtbDialNumber;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnDrop;
        private System.Windows.Forms.Button btnPlaceCall;
        private System.Windows.Forms.Button btnConference;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnKeyBoard;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnAux;
        private System.Windows.Forms.Button btnAvailable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lbAgtName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblStation;
        private System.Windows.Forms.ToolStripLabel lbAgentID;
        private System.Windows.Forms.Label lblCallTimer;
        private System.Windows.Forms.Label lblHoldTimer;
        private System.Windows.Forms.Timer callTimer;
        private System.Windows.Forms.Timer holdTimer;
    }
}