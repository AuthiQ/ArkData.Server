using System.Net;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ArkData.Server.Data;

namespace ArkData.Server
{
    public partial class frmConfig : Form
    {
        private const string log_message = "[{0}]: {1} \r\n";
        private const string ip_pattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
            @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
            @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
            @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
        private const string url_format = "http://{0}:{1}/";

        public bool useAuthentication
        {
            get
            {
                return rbXAuth.Checked;
            }
        }

        public frmConfig()
        {
            InitializeComponent();
        }

        #region Tray events
        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }

            if (this.WindowState == FormWindowState.Normal)
                this.Activate();
        }

        private void ExitItem_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void ConfigItem_Click(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }

            if (this.WindowState == FormWindowState.Normal)
                this.Activate();
        }

        private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Textbox input validation
        private void txtIP_Leave(object sender, System.EventArgs e)
        {
            if (txtIP.Text != string.Empty)
                if (!Regex.IsMatch(txtIP.Text, ip_pattern))
                {
                    MessageBox.Show("The format of the IP address isn't valid. Please use the IP address format: 255.255.255.255.",
                        "IP Format wrong",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    txtIP.Clear();
                    txtIP.Focus();
                }
        }

        private void txtSteam_Leave(object sender, System.EventArgs e)
        {
            if (txtSteam.Text.Length != 0 && txtSteam.Text.Length != 32)
            {
                MessageBox.Show(
                    "Your Steam web API key seems to be invalid. A Steam API key is precisely 32 characters long.",
                    "Invalid Steam API key",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );
                txtSteam.Clear();
                txtSteam.Focus();
            }
        }
        #endregion

        #region Radio Button Events
        private void rbXAuth_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rbXAuth.Checked)
            {
                btnTokensX.Enabled = true;
                btnUsers.Enabled = true;
            }
            else
            {
                btnTokensX.Enabled = false;
                btnUsers.Enabled = false;
            }
        }

        private void rbIPAddress_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbIPAddress.Checked)
                cbIPAddress.Enabled = true;
            else
                cbIPAddress.Enabled = false;
        }

        private void rbDNS_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbDNS.Checked)
                txtDNS.Enabled = true;
            else
                txtDNS.Enabled = false;
        }

        #endregion

        #region Button events
        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            if (this.ArkDataBrowser.ShowDialog() == DialogResult.OK)
                if (this.ArkDataBrowser.SelectedPath != this.txtFolder.Text)
                {
                    this.txtFolder.Text = this.ArkDataBrowser.SelectedPath;
                }
        }


        private void btnStop_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(
                "The server is running, this will stop the server from running, are you sure?",
                "Server is running",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;
            else
            {
                Server.Stop();
                Log("Server has been stopped");
                OpenUI();
            }
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (txtFolder.Text == string.Empty)
            {
                MessageBox.Show(
                    "Please specify the folder containing the ARK server data.",
                    "No folder specified",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if (txtIP.Text == string.Empty)
            {
                MessageBox.Show(
                    "The server query IP address is required. Please specify it before starting the server.",
                    "No IP address specified",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtIP.Focus();
                return;
            }
            if (txtSteam.Text == string.Empty)
            {
                MessageBox.Show(
                    "The use of a Steam web API key is required. Please specify it before starting the server. " +
                    "\r\n\r\nIf you don't have a key yet, you can simply request one from Steam for free.",
                    "No Steam key specified",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtSteam.Focus();
                return;
            }
            if (rbDNS.Checked && txtDNS.Text == string.Empty)
            {
                MessageBox.Show(
                    "You've specified to publish the endpoint over a DNS name. Please specify a valid DNS name before starting the server.",
                    "No DNS name specified",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtDNS.Focus();
                return;
            }

            CloseUI();

            var url = string.Format(url_format,
                rbLocalhost.Checked ? "localhost" : rbIPAddress.Checked ? cbIPAddress.SelectedItem : txtDNS.Text,
                txtHttpPort.Value);
            SaveSettings();

            Server.Start(txtFolder.Text, txtSteam.Text,
                txtIP.Text, System.Convert.ToInt32(txtPort.Text), url);
        }

        public void CloseUI()
        {
            TrayIcon.Text = "ArkData Server - Running";
            btnStop.Enabled = true;
            btnStart.Enabled = btnBrowse.Enabled =
            txtIP.Enabled = txtPort.Enabled =
            txtSteam.Enabled = rbAnon.Enabled =
            rbXAuth.Enabled = txtHttpPort.Enabled =
            rbDNS.Enabled = rbIPAddress.Enabled =
            rbLocalhost.Enabled = txtDNS.Enabled =
            cbIPAddress.Enabled = txtHttpPort.Enabled = false;
        }

        public void OpenUI()
        {
            TrayIcon.Text = "ArkData Server";
            btnStop.Enabled = false;
            btnStart.Enabled = btnBrowse.Enabled =
            txtIP.Enabled = txtPort.Enabled =
            txtSteam.Enabled = rbAnon.Enabled =
            rbIPAddress.Enabled = rbDNS.Enabled =
            rbXAuth.Enabled = txtHttpPort.Enabled =
            rbLocalhost.Enabled = true;

            if (rbDNS.Checked)
                txtDNS.Enabled = true;
            if (rbIPAddress.Checked)
                cbIPAddress.Enabled = true;
        }

        private void btnUsers_Click(object sender, System.EventArgs e)
        {
            new frmUsers().ShowDialog(this);
        }

        private void btnTokensX_Click(object sender, System.EventArgs e)
        {
            new frmXTokens().ShowDialog(this);
        }
        #endregion

        private void frmConfig_Load(object sender, System.EventArgs e)
        {
            cbIPAddress.Items.Clear();

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
                foreach (IPAddressInformation uniCast in adapter.GetIPProperties().UnicastAddresses)
                    if (!IPAddress.IsLoopback(uniCast.Address) && uniCast.Address.AddressFamily != AddressFamily.InterNetworkV6)
                        cbIPAddress.Items.Add(uniCast.Address.ToString());

            cbIPAddress.SelectedIndex = 0;

            using (var ctx = new DataContext())
            {
                var settings = ctx.Settings.ToList();

                var setting = settings.SingleOrDefault(s => s.Key == "txtFolder");
                if (setting != null)
                    txtFolder.Text = setting.Value;

                setting = settings.SingleOrDefault(s => s.Key == "txtIP");
                if (setting != null)
                    txtIP.Text = setting.Value;

                setting = settings.SingleOrDefault(s => s.Key == "txtPort");
                if (setting != null)
                    txtPort.Text = setting.Value;

                setting = settings.SingleOrDefault(s => s.Key == "txtSteam");
                if (setting != null)
                    txtSteam.Text = setting.Value;

                setting = settings.SingleOrDefault(s => s.Key == "txtHttpPort");
                if (setting != null)
                    txtHttpPort.Value = System.Convert.ToInt32(setting.Value);

                setting = settings.SingleOrDefault(s => s.Key == "SelectedHttp");
                if (setting != null)
                {
                    if (setting.Value == "local")
                    {
                        rbLocalhost.Checked = true;
                        rbIPAddress.Checked = rbDNS.Checked = false;
                    }
                    if (setting.Value == "IP")
                    {
                        rbIPAddress.Checked = true;
                        rbLocalhost.Checked = rbDNS.Checked = false;

                        setting = settings.SingleOrDefault(s => s.Key == "cbIP");
                        if (setting != null)
                            for (var i = 0; i < cbIPAddress.Items.Count; i++)
                                if ((string)cbIPAddress.Items[i] == setting.Value)
                                {
                                    cbIPAddress.SelectedIndex = i;
                                    break;
                                }
                    }
                    if (setting.Value == "DNS")
                    {
                        rbDNS.Checked = true;
                        rbLocalhost.Checked = rbIPAddress.Checked = false;

                        setting = settings.SingleOrDefault(s => s.Key == "txtDNS");
                        if (setting != null)
                            txtDNS.Text = setting.Value;
                    }
                }

                setting = settings.SingleOrDefault(s => s.Key == "SelectedAuth");
                if (setting != null)
                {
                    if (setting.Value == "anonymous")
                    {
                        rbAnon.Checked = true;
                        rbXAuth.Checked = false;
                    }
                    if (setting.Value == "XAuth")
                    {
                        rbXAuth.Checked = true;
                        rbAnon.Checked = false;
                    }
                }
            }
        }

        private async void SaveSettings()
        {
            using (var ctx = new DataContext())
            {
                CommitSetting(ctx, "txtFolder", txtFolder.Text);
                CommitSetting(ctx, "txtIP", txtIP.Text);
                CommitSetting(ctx, "txtPort", txtPort.Text);
                CommitSetting(ctx, "txtSteam", txtSteam.Text);
                CommitSetting(ctx, "txtHttpPort", txtHttpPort.Value.ToString());
                CommitSetting(ctx, "SelectedHttp",
                    rbLocalhost.Checked ? "local" : rbIPAddress.Checked ? "IP" : "DNS");
                if (rbIPAddress.Checked)
                    CommitSetting(ctx, "cbIP", (string)cbIPAddress.SelectedItem);
                if (rbDNS.Checked)
                    CommitSetting(ctx, "txtDNS", txtDNS.Text);
                CommitSetting(ctx, "SelectedAuth",
                    rbAnon.Checked ? "anonymous" : "XAuth");

                await ctx.SaveChangesAsync();
            }
        }

        private void CommitSetting(DataContext ctx, string key, string value)
        {
            var setting = ctx.Settings.SingleOrDefault(s => s.Key == key);
            if (setting != null)
            {
                setting.Value = value;

                ctx.Settings.Attach(setting);
                ctx.Entry<Setting>(setting).Property(p => p.Value).IsModified = true;
            }
            else
                ctx.Settings.Add(new Setting()
                {
                    Key = key,
                    Value = value
                });
        }

        public void Log(string message)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.txtLog.AppendText(string.Format(log_message,
                System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), message));
                this.txtLog.ScrollToCaret();
            }));
        }

        private void frmConfig_Shown(object sender, System.EventArgs e)
        {
            Log("Client started");
        }
    }
}
