namespace ArkData.Server
{
    partial class frmUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsers));
            this.gbxDetails = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtLastLogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.gbxDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxDetails
            // 
            this.gbxDetails.Controls.Add(this.btnReset);
            this.gbxDetails.Controls.Add(this.label3);
            this.gbxDetails.Controls.Add(this.btnDelete);
            this.gbxDetails.Controls.Add(this.txtLastLogin);
            this.gbxDetails.Controls.Add(this.label2);
            this.gbxDetails.Controls.Add(this.txtUsername);
            this.gbxDetails.Controls.Add(this.label1);
            this.gbxDetails.Controls.Add(this.txtDescription);
            this.gbxDetails.Location = new System.Drawing.Point(273, 12);
            this.gbxDetails.Name = "gbxDetails";
            this.gbxDetails.Size = new System.Drawing.Size(228, 235);
            this.gbxDetails.TabIndex = 0;
            this.gbxDetails.TabStop = false;
            this.gbxDetails.Text = "User Details";
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(45, 202);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(96, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset Password";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Last login:";
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(147, 202);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtLastLogin
            // 
            this.txtLastLogin.Location = new System.Drawing.Point(6, 158);
            this.txtLastLogin.Name = "txtLastLogin";
            this.txtLastLogin.ReadOnly = true;
            this.txtLastLogin.Size = new System.Drawing.Size(216, 20);
            this.txtLastLogin.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(6, 98);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.Size = new System.Drawing.Size(216, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(6, 39);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(216, 20);
            this.txtDescription.TabIndex = 0;
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Location = new System.Drawing.Point(13, 12);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.ScrollAlwaysVisible = true;
            this.lstUsers.Size = new System.Drawing.Size(254, 277);
            this.lstUsers.TabIndex = 1;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(420, 266);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 300);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.gbxDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Users";
            this.Load += new System.EventHandler(this.frmUsers_Load);
            this.gbxDetails.ResumeLayout(false);
            this.gbxDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxDetails;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtLastLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Button btnCreate;
    }
}