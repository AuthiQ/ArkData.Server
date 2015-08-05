namespace ArkData.Server
{
    partial class frmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenu = new System.Windows.Forms.ContextMenu();
            this.ConfigItem = new System.Windows.Forms.MenuItem();
            this.ExitItem = new System.Windows.Forms.MenuItem();
            this.gbxLogging = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.gbxConfig = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSteam = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.DomainUpDown();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.ArkDataBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.gbxAuth = new System.Windows.Forms.GroupBox();
            this.btnTokensX = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.rbXAuth = new System.Windows.Forms.RadioButton();
            this.rbAnon = new System.Windows.Forms.RadioButton();
            this.gbxHttp = new System.Windows.Forms.GroupBox();
            this.txtDNS = new System.Windows.Forms.TextBox();
            this.cbIPAddress = new System.Windows.Forms.ComboBox();
            this.txtHttpPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.rbDNS = new System.Windows.Forms.RadioButton();
            this.rbIPAddress = new System.Windows.Forms.RadioButton();
            this.rbLocalhost = new System.Windows.Forms.RadioButton();
            this.gbxLogging.SuspendLayout();
            this.gbxConfig.SuspendLayout();
            this.gbxAuth.SuspendLayout();
            this.gbxHttp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHttpPort)).BeginInit();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenu = this.TrayMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "ArkData Server";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
            // 
            // TrayMenu
            // 
            this.TrayMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ConfigItem,
            this.ExitItem});
            // 
            // ConfigItem
            // 
            this.ConfigItem.Index = 0;
            this.ConfigItem.Text = "Configuration";
            this.ConfigItem.Click += new System.EventHandler(this.ConfigItem_Click);
            // 
            // ExitItem
            // 
            this.ExitItem.Index = 1;
            this.ExitItem.Text = "Exit";
            this.ExitItem.Click += new System.EventHandler(this.ExitItem_Click);
            // 
            // gbxLogging
            // 
            this.gbxLogging.Controls.Add(this.txtLog);
            this.gbxLogging.Location = new System.Drawing.Point(290, 12);
            this.gbxLogging.Name = "gbxLogging";
            this.gbxLogging.Size = new System.Drawing.Size(401, 522);
            this.gbxLogging.TabIndex = 0;
            this.gbxLogging.TabStop = false;
            this.gbxLogging.Text = "Server Log";
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(6, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(389, 497);
            this.txtLog.TabIndex = 0;
            // 
            // gbxConfig
            // 
            this.gbxConfig.Controls.Add(this.label2);
            this.gbxConfig.Controls.Add(this.txtSteam);
            this.gbxConfig.Controls.Add(this.label1);
            this.gbxConfig.Controls.Add(this.txtPort);
            this.gbxConfig.Controls.Add(this.txtIP);
            this.gbxConfig.Controls.Add(this.lblIPAddress);
            this.gbxConfig.Controls.Add(this.btnBrowse);
            this.gbxConfig.Controls.Add(this.txtFolder);
            this.gbxConfig.Controls.Add(this.lblFolder);
            this.gbxConfig.Location = new System.Drawing.Point(12, 12);
            this.gbxConfig.Name = "gbxConfig";
            this.gbxConfig.Size = new System.Drawing.Size(272, 171);
            this.gbxConfig.TabIndex = 1;
            this.gbxConfig.TabStop = false;
            this.gbxConfig.Text = "Server Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Steam API key:";
            this.label2.Leave += new System.EventHandler(this.txtSteam_Leave);
            // 
            // txtSteam
            // 
            this.txtSteam.Location = new System.Drawing.Point(6, 143);
            this.txtSteam.Name = "txtSteam";
            this.txtSteam.Size = new System.Drawing.Size(257, 20);
            this.txtSteam.TabIndex = 10;
            this.txtSteam.Leave += new System.EventHandler(this.txtSteam_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(130, 94);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(57, 20);
            this.txtPort.TabIndex = 8;
            this.txtPort.Text = "27015";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(6, 94);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(111, 20);
            this.txtIP.TabIndex = 7;
            this.txtIP.Leave += new System.EventHandler(this.txtIP_Leave);
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(6, 78);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(61, 13);
            this.lblIPAddress.TabIndex = 6;
            this.lblIPAddress.Text = "IP Address:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(241, 38);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 21);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Tag = "Select ARK server data folder";
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(5, 38);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(226, 20);
            this.txtFolder.TabIndex = 3;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 22);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(98, 13);
            this.lblFolder.TabIndex = 2;
            this.lblFolder.Text = "ARK Server Folder:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(209, 511);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Server";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(124, 511);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop Server";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // ArkDataBrowser
            // 
            this.ArkDataBrowser.Description = "Select the folder containing the ARK server data.";
            this.ArkDataBrowser.ShowNewFolderButton = false;
            // 
            // gbxAuth
            // 
            this.gbxAuth.Controls.Add(this.btnTokensX);
            this.gbxAuth.Controls.Add(this.btnUsers);
            this.gbxAuth.Controls.Add(this.rbXAuth);
            this.gbxAuth.Controls.Add(this.rbAnon);
            this.gbxAuth.Location = new System.Drawing.Point(12, 394);
            this.gbxAuth.Name = "gbxAuth";
            this.gbxAuth.Size = new System.Drawing.Size(272, 111);
            this.gbxAuth.TabIndex = 2;
            this.gbxAuth.TabStop = false;
            this.gbxAuth.Text = "Server Authentication";
            // 
            // btnTokensX
            // 
            this.btnTokensX.Enabled = false;
            this.btnTokensX.Image = ((System.Drawing.Image)(resources.GetObject("btnTokensX.Image")));
            this.btnTokensX.Location = new System.Drawing.Point(45, 73);
            this.btnTokensX.Name = "btnTokensX";
            this.btnTokensX.Size = new System.Drawing.Size(25, 25);
            this.btnTokensX.TabIndex = 3;
            this.btnTokensX.UseVisualStyleBackColor = true;
            this.btnTokensX.Click += new System.EventHandler(this.btnTokensX_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.Enabled = false;
            this.btnUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnUsers.Image")));
            this.btnUsers.Location = new System.Drawing.Point(12, 73);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(25, 25);
            this.btnUsers.TabIndex = 2;
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // rbXAuth
            // 
            this.rbXAuth.AutoSize = true;
            this.rbXAuth.Location = new System.Drawing.Point(5, 50);
            this.rbXAuth.Name = "rbXAuth";
            this.rbXAuth.Size = new System.Drawing.Size(128, 17);
            this.rbXAuth.TabIndex = 1;
            this.rbXAuth.TabStop = true;
            this.rbXAuth.Text = "X-Auth Authentication";
            this.rbXAuth.UseVisualStyleBackColor = true;
            this.rbXAuth.CheckedChanged += new System.EventHandler(this.rbXAuth_CheckedChanged);
            // 
            // rbAnon
            // 
            this.rbAnon.AutoSize = true;
            this.rbAnon.Checked = true;
            this.rbAnon.Location = new System.Drawing.Point(5, 24);
            this.rbAnon.Name = "rbAnon";
            this.rbAnon.Size = new System.Drawing.Size(118, 17);
            this.rbAnon.TabIndex = 0;
            this.rbAnon.TabStop = true;
            this.rbAnon.Text = "Anonymous Access";
            this.rbAnon.UseVisualStyleBackColor = true;
            // 
            // gbxHttp
            // 
            this.gbxHttp.Controls.Add(this.txtDNS);
            this.gbxHttp.Controls.Add(this.cbIPAddress);
            this.gbxHttp.Controls.Add(this.txtHttpPort);
            this.gbxHttp.Controls.Add(this.label3);
            this.gbxHttp.Controls.Add(this.rbDNS);
            this.gbxHttp.Controls.Add(this.rbIPAddress);
            this.gbxHttp.Controls.Add(this.rbLocalhost);
            this.gbxHttp.Location = new System.Drawing.Point(12, 190);
            this.gbxHttp.Name = "gbxHttp";
            this.gbxHttp.Size = new System.Drawing.Size(272, 198);
            this.gbxHttp.TabIndex = 3;
            this.gbxHttp.TabStop = false;
            this.gbxHttp.Text = "HTTP Settings";
            // 
            // txtDNS
            // 
            this.txtDNS.Enabled = false;
            this.txtDNS.Location = new System.Drawing.Point(16, 134);
            this.txtDNS.Name = "txtDNS";
            this.txtDNS.Size = new System.Drawing.Size(233, 20);
            this.txtDNS.TabIndex = 6;
            // 
            // cbIPAddress
            // 
            this.cbIPAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIPAddress.Enabled = false;
            this.cbIPAddress.FormattingEnabled = true;
            this.cbIPAddress.Location = new System.Drawing.Point(16, 72);
            this.cbIPAddress.Name = "cbIPAddress";
            this.cbIPAddress.Size = new System.Drawing.Size(159, 21);
            this.cbIPAddress.TabIndex = 5;
            // 
            // txtHttpPort
            // 
            this.txtHttpPort.Location = new System.Drawing.Point(41, 170);
            this.txtHttpPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtHttpPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHttpPort.Name = "txtHttpPort";
            this.txtHttpPort.Size = new System.Drawing.Size(66, 20);
            this.txtHttpPort.TabIndex = 4;
            this.txtHttpPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port:";
            // 
            // rbDNS
            // 
            this.rbDNS.AutoSize = true;
            this.rbDNS.Location = new System.Drawing.Point(5, 104);
            this.rbDNS.Name = "rbDNS";
            this.rbDNS.Size = new System.Drawing.Size(99, 17);
            this.rbDNS.TabIndex = 2;
            this.rbDNS.TabStop = true;
            this.rbDNS.Text = "Use DNS name";
            this.rbDNS.UseVisualStyleBackColor = true;
            this.rbDNS.CheckedChanged += new System.EventHandler(this.rbDNS_CheckedChanged);
            // 
            // rbIPAddress
            // 
            this.rbIPAddress.AutoSize = true;
            this.rbIPAddress.Location = new System.Drawing.Point(5, 42);
            this.rbIPAddress.Name = "rbIPAddress";
            this.rbIPAddress.Size = new System.Drawing.Size(109, 17);
            this.rbIPAddress.TabIndex = 1;
            this.rbIPAddress.TabStop = true;
            this.rbIPAddress.Text = "Select IP Address";
            this.rbIPAddress.UseVisualStyleBackColor = true;
            this.rbIPAddress.CheckedChanged += new System.EventHandler(this.rbIPAddress_CheckedChanged);
            // 
            // rbLocalhost
            // 
            this.rbLocalhost.AutoSize = true;
            this.rbLocalhost.Checked = true;
            this.rbLocalhost.Location = new System.Drawing.Point(5, 19);
            this.rbLocalhost.Name = "rbLocalhost";
            this.rbLocalhost.Size = new System.Drawing.Size(89, 17);
            this.rbLocalhost.TabIndex = 0;
            this.rbLocalhost.TabStop = true;
            this.rbLocalhost.Text = "Use localhost";
            this.rbLocalhost.UseVisualStyleBackColor = true;
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 541);
            this.Controls.Add(this.gbxHttp);
            this.Controls.Add(this.gbxAuth);
            this.Controls.Add(this.gbxConfig);
            this.Controls.Add(this.gbxLogging);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ArkData Server Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfig_FormClosing);
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.Shown += new System.EventHandler(this.frmConfig_Shown);
            this.gbxLogging.ResumeLayout(false);
            this.gbxLogging.PerformLayout();
            this.gbxConfig.ResumeLayout(false);
            this.gbxConfig.PerformLayout();
            this.gbxAuth.ResumeLayout(false);
            this.gbxAuth.PerformLayout();
            this.gbxHttp.ResumeLayout(false);
            this.gbxHttp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHttpPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenu TrayMenu;
        private System.Windows.Forms.MenuItem ConfigItem;
        private System.Windows.Forms.MenuItem ExitItem;
        private System.Windows.Forms.GroupBox gbxLogging;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox gbxConfig;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.FolderBrowserDialog ArkDataBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DomainUpDown txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.GroupBox gbxAuth;
        private System.Windows.Forms.RadioButton rbAnon;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.RadioButton rbXAuth;
        private System.Windows.Forms.Button btnTokensX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSteam;
        private System.Windows.Forms.GroupBox gbxHttp;
        private System.Windows.Forms.TextBox txtDNS;
        private System.Windows.Forms.ComboBox cbIPAddress;
        private System.Windows.Forms.NumericUpDown txtHttpPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbDNS;
        private System.Windows.Forms.RadioButton rbIPAddress;
        private System.Windows.Forms.RadioButton rbLocalhost;
    }
}

