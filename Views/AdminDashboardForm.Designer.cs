namespace WLAMS.Views
{
    partial class AdminDashboardForm
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
            lblTitle = new Label();
            btnCategories = new Button();
            lblWelcome = new Label();
            lblPending = new Label();
            btnUsers = new Button();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(31, 58, 95);
            lblTitle.Location = new Point(156, 20);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(377, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Administrator Dashboard";
            // 
            // btnCategories
            // 
            btnCategories.Location = new Point(65, 198);
            btnCategories.Margin = new Padding(4, 3, 4, 3);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(260, 70);
            btnCategories.TabIndex = 1;
            btnCategories.Text = "Manage categories";
            btnCategories.UseVisualStyleBackColor = true;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(26, 74);
            lblWelcome.Margin = new Padding(4, 0, 4, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(92, 25);
            lblWelcome.TabIndex = 2;
            lblWelcome.Text = "Welcome!";
            // 
            // lblPending
            // 
            lblPending.AutoSize = true;
            lblPending.ForeColor = Color.Firebrick;
            lblPending.Location = new Point(26, 145);
            lblPending.Margin = new Padding(4, 0, 4, 0);
            lblPending.Name = "lblPending";
            lblPending.Size = new Size(237, 23);
            lblPending.TabIndex = 3;
            lblPending.Text = "Pending instructor requests: 0";
            // 
            // btnUsers
            // 
            btnUsers.Location = new Point(477, 198);
            btnUsers.Margin = new Padding(4, 3, 4, 3);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(260, 70);
            btnUsers.TabIndex = 4;
            btnUsers.Text = "Manage users";
            btnUsers.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(477, 319);
            btnLogout.Margin = new Padding(4, 3, 4, 3);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(260, 56);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // AdminDashboardForm
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 452);
            Controls.Add(btnLogout);
            Controls.Add(btnUsers);
            Controls.Add(lblPending);
            Controls.Add(lblWelcome);
            Controls.Add(btnCategories);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(42, 115);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "AdminDashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WLAMS - Administrator";
        //   Load += AdminDashboardForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnCategories;
        private Label lblWelcome;
        private Label lblPending;
        private Button btnUsers;
        private Button btnLogout;
    }
}