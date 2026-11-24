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
            btnLogout.Location = new Point(40, 300);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(260, 45);
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
            // InstructorDashboardForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(642, 393);
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
       //     Load += InstructorDashboardForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnCourses;
        private Label lblWelcome;
        private Button btnLogout;
        private Button btnQuizzes;
    }
}