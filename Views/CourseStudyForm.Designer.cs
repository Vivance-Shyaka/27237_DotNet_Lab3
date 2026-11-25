namespace WLAMS.Views
{
    partial class CourseStudyForm
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
            lblCourse = new Label();
            lblProgress = new Label();
            pbProgress = new ProgressBar();
            dgvLessons = new DataGridView();
            btnBack = new Button();
            lblLessonTitle = new Label();
            txtContent = new TextBox();
            btnToggle = new Button();
            btnNext = new Button();
            btnQuizzes = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLessons).BeginInit();
            SuspendLayout();
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCourse.ForeColor = Color.FromArgb(31, 58, 95);
            lblCourse.Location = new Point(20, 12);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(87, 31);
            lblCourse.TabIndex = 0;
            lblCourse.Text = "Course";
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProgress.ForeColor = Color.Black;
            lblProgress.Location = new Point(20, 55);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(78, 23);
            lblProgress.TabIndex = 1;
            lblProgress.Text = "Progress";
            // 
            // pbProgress
            // 
            pbProgress.Location = new Point(20, 85);
            pbProgress.Name = "pbProgress";
            pbProgress.Size = new Size(380, 22);
            pbProgress.TabIndex = 2;
            // 
            // dgvLessons
            // 
            dgvLessons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLessons.Location = new Point(20, 120);
            dgvLessons.Name = "dgvLessons";
            dgvLessons.RowHeadersWidth = 51;
            dgvLessons.Size = new Size(380, 500);
            dgvLessons.TabIndex = 3;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 640);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(380, 45);
            btnBack.TabIndex = 4;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // lblLessonTitle
            // 
            lblLessonTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLessonTitle.Location = new Point(430, 55);
            lblLessonTitle.Name = "lblLessonTitle";
            lblLessonTitle.Size = new Size(760, 35);
            lblLessonTitle.TabIndex = 5;
            // 
            // txtContent
            // 
            txtContent.Location = new Point(430, 100);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.ReadOnly = true;
            txtContent.ScrollBars = ScrollBars.Vertical;
            txtContent.Size = new Size(760, 470);
            txtContent.TabIndex = 6;
            // 
            // btnToggle
            // 
            btnToggle.Location = new Point(430, 590);
            btnToggle.Name = "btnToggle";
            btnToggle.Size = new Size(260, 45);
            btnToggle.TabIndex = 7;
            btnToggle.Text = "Mark as complete";
            btnToggle.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(710, 590);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(200, 45);
            btnNext.TabIndex = 8;
            btnNext.Text = "Next lesson";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // btnQuizzes
            // 
            btnQuizzes.Location = new Point(916, 12);
            btnQuizzes.Name = "btnQuizzes";
            btnQuizzes.Size = new Size(260, 45);
            btnQuizzes.TabIndex = 9;
            btnQuizzes.Text = "Quizzes of this course";
            btnQuizzes.UseVisualStyleBackColor = true;
            // 
            // CourseStudyForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1202, 733);
            Controls.Add(btnQuizzes);
            Controls.Add(btnNext);
            Controls.Add(btnToggle);
            Controls.Add(txtContent);
            Controls.Add(lblLessonTitle);
            Controls.Add(btnBack);
            Controls.Add(dgvLessons);
            Controls.Add(pbProgress);
            Controls.Add(lblProgress);
            Controls.Add(lblCourse);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CourseStudyForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Study";
          //  Load += CourseStudyForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLessons).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCourse;
        private Label lblProgress;
        private ProgressBar pbProgress;
        private DataGridView dgvLessons;
        private Button btnBack;
        private Label lblLessonTitle;
        private TextBox txtContent;
        private Button btnToggle;
        private Button btnNext;
        private Button btnQuizzes;
    }
}