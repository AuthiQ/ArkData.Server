using System;
using System.Text;
using System.Windows.Forms;
using ArkData.Server.Data;
using System.Security.Cryptography;

namespace ArkData.Server
{
    public partial class frmAddUser : Form
    {
        private bool hasSaved = false;

        public frmAddUser()
        {
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text == string.Empty && txtDescription.Text.Length < 50)
            {
                MessageBox.Show(
                    "The description is a required property, it has to be between 1 and 50 characters long.",
                    "Description invalid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtDescription.Focus();
                return;
            }
            if (txtUsername.Text == string.Empty &&
                txtUsername.Text.Length < 50 &&
                txtUsername.Text.Length > 4)
            {
                MessageBox.Show(
                    "The username is a required property, it has to be between 4 and 50 characters long.",
                    "Username invalid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtUsername.Focus();
                return;
            }
            if (txtPassword.Text == string.Empty &&
                txtPassword.Text.Length < 50 &&
                txtPassword.Text.Length > 5)
            {
                MessageBox.Show(
                    "The password is a required property, it has to be between 5 and 50 characters long.",
                    "Password invalid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return;
            }

            using (var ctx = new DataContext())
            {
                var hash = Convert.ToBase64String(new HMACSHA256(Encoding.Unicode.GetBytes(txtPassword.Text))
                    .ComputeHash(Encoding.Unicode.GetBytes(txtUsername.Text)));
                ctx.XUsers.Add(new XUser()
                {
                    Description = txtDescription.Text,
                    Username = txtUsername.Text.ToLower(),
                    Password = hash,
                    LastLogin = DateTime.Now
                });
                await ctx.SaveChangesAsync();
            }
            hasSaved = true;
            this.Close();
        }

        private void frmAddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!hasSaved)
                if (txtDescription.Text != String.Empty ||
                    txtPassword.Text != String.Empty ||
                    txtUsername.Text != String.Empty)
                    if (MessageBox.Show(
                        "This will discard all user details, are you sure?",
                        "Discard details",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                        e.Cancel = true;
        }
    }
}
