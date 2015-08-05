using ArkData.Server.Data;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ArkData.Server
{
    public partial class frmReset : Form
    {
        private string username;

        public frmReset()
        {
            InitializeComponent();
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text != txtPasswordRepeat.Text)
            {
                MessageBox.Show(
                    "The passwords specified don't match. Please try again.",
                    "Passwords don't match",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtPassword.Clear();
                txtPasswordRepeat.Clear();
                txtPassword.Focus();
                return;
            }
            if(txtPassword.Text.Length < 5 &&
                txtPassword.Text.Length > 50)
            {
                MessageBox.Show(
                    "The password size is invalid, please use a password between 5 and 50 characters long.",
                    "Password size invalid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtPassword.Clear();
                txtPasswordRepeat.Clear();
                txtPassword.Focus();
                return;
            }

            using(var ctx = new DataContext())
            {
                var user = ctx.XUsers.Single(u => u.Username == this.username);

                user.Password = Convert.ToBase64String(new HMACSHA256(Encoding.Unicode.GetBytes(txtPassword.Text))
                    .ComputeHash(Encoding.Unicode.GetBytes(this.username)));

                ctx.XUsers.Attach(user);
                ctx.Entry<XUser>(user).Property(p => p.Password).IsModified = true;
                await ctx.SaveChangesAsync();
            }
            this.Close();
        }

        public DialogResult ShowDialog(IWin32Window owner, string username)
        {
            this.username = username;
            return this.ShowDialog(owner);
        }
    }
}
