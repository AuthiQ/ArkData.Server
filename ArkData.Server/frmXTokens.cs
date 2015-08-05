using System;
using System.Linq;
using System.Windows.Forms;
using ArkData.Server.Data;

namespace ArkData.Server
{
    public partial class frmXTokens : Form
    {
        public frmXTokens()
        {
            InitializeComponent();
        }

        private void frmXTokens_Load(object sender, EventArgs e)
        {
            lstTokens.Items.Clear();
            lstTokens.Items.AddRange(new DataContext().XTokens.Select(t => t.Token).ToArray());
            if (lstTokens.Items.Count > 0)
                lstTokens.SelectedIndex = 0;
            else
            {
                txtOwner.Text = txtToken.Text = txtCreated.Text = string.Empty;
                btnDelete.Enabled = false;
            }
        }

        private void lstTokens_SelectedIndexChanged(object sender, EventArgs e)
        {
            var token = new DataContext().XTokens.Single(t => t.Token == (string)lstTokens.SelectedItem);
            txtOwner.Text = token.Username;
            txtToken.Text = token.Token;
            txtCreated.Text = token.Created.ToString("dd-MM-yyyy HH:mm:ss");
            btnDelete.Enabled = true;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Are you sure you want to delete this token?",
                "Delete user",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var ctx = new DataContext())
                {
                    ctx.XTokens.Remove(ctx.XTokens.Single(t => t.Token == (string)lstTokens.SelectedItem));
                    await ctx.SaveChangesAsync();
                }
                frmXTokens_Load(null, null);
            }
        }
    }
}
