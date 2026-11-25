namespace WLAMS.Views
{
    partial class InstructorDashboardForm
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
            btnCourses = new Button();
            lblWelcome = new Label();
            btnLogout = new Button();
            btnQuizzes = new Button();
            lblSummary = new Label();
            btnGrading = new Button();
            btnGradebook = new Button();
            btnStats = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(31, 58, 95);
            lblTitle.Location = new Point(40, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(318, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Instructor Dashboard";
            // 
            // btnCourses
            // 
            btnCourses.Location = new Point(40, 150);
            btnCourses.Name = "btnCourses";
            btnCourses.Size = new Size(260, 70);
            btnCourses.TabIndex = 1;
            btnCourses.Text = "My courses";
            btnCourses.UseVisualStyleBackColor = true;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(42, 80);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(86, 25);
            lblWelcome.TabIndex = 2;
            lblWelcome.Text = "Welcome";
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(330, 320);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(260, 70);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnQuizzes
            // 
            btnQuizzes.Location = new Point(330, 150);
            btnQuizzes.Name = "btnQuizzes";
            btnQuizzes.Size = new Size(260, 70);
            btnQuizzes.TabIndex = 4;
            btnQuizzes.Text = "My quizzes";
            btnQuizzes.UseVisualStyleBackColor = true;
            // 
            // lblSummary
            // 
            lblSummary.AutoSize = true;
            lblSummary.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSummary.ForeColor = Color.Firebrick;
            lblSummary.Location = new Point(42, 115);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(267, 25);
            lblSummary.TabIndex = 5;
            lblSummary.Text = " Attempts waiting for grading: 0";
            // 
            // btnGrading
            // 
            btnGrading.Location = new Point(40, 235);
            btnGrading.Name = "btnGrading";
            btnGrading.Size = new Size(260, 70);
            btnGrading.TabIndex = 6;
            btnGrading.Text = "Grade answers";
            btnGrading.UseVisualStyleBackColor = true;
            // 
            // btnGradebook
            // 
            btnGradebook.Location = new Point(330, 235);
            btnGradebook.Name = "btnGradebook";
            btnGradebook.Size = new Size(260, 70);
            btnGradebook.TabIndex = 7;
            btnGradebook.Text = "Gradebook";
            btnGradebook.UseVisualStyleBackColor = true;
            // 
            // btnStats
            // 
            btnStats.Location = new Point(40, 320);
            btnStats.Name = "btnStats";
            btnStats.Size = new Size(260, 70);
            btnStats.TabIndex = 8;
            btnStats.Text = "Quiz statistics";
            btnStats.UseVisualStyleBackColor = true;
            // 
            // InstructorDashboardForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(852, 581);
            Controls.Add(btnStats);
            Controls.Add(btnGradebook);
            Controls.Add(btnGrading);
            Controls.Add(lblSummary);
            Controls.Add(btnQuizzes);
            Controls.Add(btnLogout);
            Controls.Add(lblWelcome);
            Controls.Add(btnCourses);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            Name = "InstructorDashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WLAMS - Instructor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnCourses;
        private Label lblWelcome;
        private Button btnLogout;
        private Button btnQuizzes;
        private Label lblSummary;
        private Button btnGrading;
        private Button btnGradebook;
        private Button btnStats;
    }
}