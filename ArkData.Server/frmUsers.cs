using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ArkData.Server.Data;

namespace ArkData.Server
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            using(var ctx = new DataContext())
            {
                lstUsers.Items.Clear();
                lstUsers.Items.AddRange(
                    ctx.XUsers.Select(u => u.Username).ToArray());
                if (lstUsers.Items.Count > 0)
                    lstUsers.SelectedIndex = 0;
                else
                {
                    txtDescription.Text = txtUsername.Text = txtLastLogin.Text = string.Empty;
                    btnDelete.Enabled = btnReset.Enabled = false;
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            new frmAddUser().ShowDialog(this);
            frmUsers_Load(null, null);
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = new DataContext().XUsers.Single(u => u.Username == (string)lstUsers.SelectedItem);
            txtDescription.Text = user.Description;
            txtUsername.Text = user.Username;
            txtLastLogin.Text = user.LastLogin.ToString("dd-MM-yyyy HH:mm:ss");
            btnDelete.Enabled = btnReset.Enabled = true;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(
                "Are you sure you want to delete this user?",
                "Delete user",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using(var ctx = new DataContext())
                {
                    ctx.XUsers.Remove(ctx.XUsers.Single(u => u.Username == (string)lstUsers.SelectedItem));
                    await ctx.SaveChangesAsync();
                }
                frmUsers_Load(null, null);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            new frmReset().ShowDialog(this, (string)lstUsers.SelectedItem);
            frmUsers_Load(null, null);
        }
    }
}
