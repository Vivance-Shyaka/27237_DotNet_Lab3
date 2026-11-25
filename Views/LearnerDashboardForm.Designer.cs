namespace WLAMS.Views
{
    partial class LearnerDashboardForm
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
            lblWelcome = new Label();
            lblSummary = new Label();
            btnBrowse = new Button();
            btnMyCourses = new Button();
            btnLogout = new Button();
            btnResults = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(31, 58, 95);
            lblTitle.Location = new Point(40, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(284, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Learner Dashboard";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(42, 80);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(85, 25);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Welcome";
            // 
            // lblSummary
            // 
            lblSummary.AutoSize = true;
            lblSummary.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSummary.ForeColor = Color.Firebrick;
            lblSummary.Location = new Point(42, 115);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(252, 25);
            lblSummary.TabIndex = 2;
            lblSummary.Text = "You are enrolled in 0 course(s).";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(40, 170);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(260, 70);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse courses";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnMyCourses
            // 
            btnMyCourses.Location = new Point(330, 170);
            btnMyCourses.Name = "btnMyCourses";
            btnMyCourses.Size = new Size(260, 70);
            btnMyCourses.TabIndex = 4;
            btnMyCourses.Text = "My courses";
            btnMyCourses.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(330, 255);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(260, 70);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnResults
            // 
            btnResults.Location = new Point(40, 255);
            btnResults.Name = "btnResults";
            btnResults.Size = new Size(260, 70);
            btnResults.TabIndex = 6;
            btnResults.Text = "My quiz results";
            btnResults.UseVisualStyleBackColor = true;
            // 
            // LearnerDashboardForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(642, 393);
            Controls.Add(btnResults);
            Controls.Add(btnLogout);
            Controls.Add(btnMyCourses);
            Controls.Add(btnBrowse);
            Controls.Add(lblSummary);
            Controls.Add(lblWelcome);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LearnerDashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WLAMS - Learner";
         //   Load += LearnerDashboardForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblWelcome;
        private Label lblSummary;
        private Button btnBrowse;
        private Button btnMyCourses;
        private Button btnLogout;
        private Button btnResults;
    }
}