namespace WLAMS.Views
{
    partial class CourseQuizzesForm
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
            dgvQuizzes = new DataGridView();
            label1 = new Label();
            txtInstructions = new TextBox();
            btnStart = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvQuizzes).BeginInit();
            SuspendLayout();
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCourse.ForeColor = Color.FromArgb(31, 58, 95);
            lblCourse.Location = new Point(20, 15);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(91, 28);
            lblCourse.TabIndex = 0;
            lblCourse.Text = " Quizzes";
            // 
            // dgvQuizzes
            // 
            dgvQuizzes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvQuizzes.Location = new Point(20, 55);
            dgvQuizzes.Name = "dgvQuizzes";
            dgvQuizzes.RowHeadersWidth = 51;
            dgvQuizzes.Size = new Size(1060, 300);
            dgvQuizzes.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 370);
            label1.Name = "label1";
            label1.Size = new Size(99, 23);
            label1.TabIndex = 2;
            label1.Text = "Instructions";
            // 
            // txtInstructions
            // 
            txtInstructions.Location = new Point(20, 395);
            txtInstructions.Multiline = true;
            txtInstructions.Name = "txtInstructions";
            txtInstructions.ReadOnly = true;
            txtInstructions.Size = new Size(1060, 90);
            txtInstructions.TabIndex = 3;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(20, 505);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(220, 45);
            btnStart.TabIndex = 4;
            btnStart.Text = "Start quiz";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(260, 505);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 45);
            btnBack.TabIndex = 5;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // CourseQuizzesForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 593);
            Controls.Add(btnBack);
            Controls.Add(btnStart);
            Controls.Add(txtInstructions);
            Controls.Add(label1);
            Controls.Add(dgvQuizzes);
            Controls.Add(lblCourse);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CourseQuizzesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Course quizzes";
           // Load += CourseQuizzesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvQuizzes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCourse;
        private DataGridView dgvQuizzes;
        private Label label1;
        private TextBox txtInstructions;
        private Button btnStart;
        private Button btnBack;
    }
}