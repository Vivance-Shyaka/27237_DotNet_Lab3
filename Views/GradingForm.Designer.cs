namespace WLAMS.Views
{
    partial class GradingForm
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
            lblAttemptsTitle = new Label();
            dgvAttempts = new DataGridView();
            label1 = new Label();
            dgvAnswers = new DataGridView();
            label2 = new Label();
            txtQuestion = new TextBox();
            label3 = new Label();
            txtLearnerAnswer = new TextBox();
            label4 = new Label();
            nudMarks = new NumericUpDown();
            lblMax = new Label();
            btnSave = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAttempts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAnswers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMarks).BeginInit();
            SuspendLayout();
            // 
            // lblAttemptsTitle
            // 
            lblAttemptsTitle.AutoSize = true;
            lblAttemptsTitle.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAttemptsTitle.ForeColor = Color.FromArgb(31, 58, 95);
            lblAttemptsTitle.Location = new Point(20, 15);
            lblAttemptsTitle.Name = "lblAttemptsTitle";
            lblAttemptsTitle.Size = new Size(260, 25);
            lblAttemptsTitle.TabIndex = 0;
            lblAttemptsTitle.Text = "Attempts waiting for grading";
            // 
            // dgvAttempts
            // 
            dgvAttempts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttempts.Location = new Point(20, 45);
            dgvAttempts.Name = "dgvAttempts";
            dgvAttempts.RowHeadersWidth = 51;
            dgvAttempts.Size = new Size(1060, 170);
            dgvAttempts.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(31, 58, 95);
            label1.Location = new Point(20, 230);
            label1.Name = "label1";
            label1.Size = new Size(303, 25);
            label1.TabIndex = 2;
            label1.Text = "Short answers to grade (click one)";
            // 
            // dgvAnswers
            // 
            dgvAnswers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnswers.Location = new Point(20, 260);
            dgvAnswers.Name = "dgvAnswers";
            dgvAnswers.RowHeadersWidth = 51;
            dgvAnswers.Size = new Size(1060, 190);
            dgvAnswers.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 465);
            label2.Name = "label2";
            label2.Size = new Size(79, 23);
            label2.TabIndex = 4;
            label2.Text = "Question";
            // 
            // txtQuestion
            // 
            txtQuestion.Location = new Point(20, 490);
            txtQuestion.Multiline = true;
            txtQuestion.Name = "txtQuestion";
            txtQuestion.ReadOnly = true;
            txtQuestion.Size = new Size(520, 80);
            txtQuestion.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(560, 465);
            label3.Name = "label3";
            label3.Size = new Size(136, 23);
            label3.TabIndex = 6;
            label3.Text = "Learner's answer";
            // 
            // txtLearnerAnswer
            // 
            txtLearnerAnswer.Location = new Point(560, 490);
            txtLearnerAnswer.Multiline = true;
            txtLearnerAnswer.Name = "txtLearnerAnswer";
            txtLearnerAnswer.ReadOnly = true;
            txtLearnerAnswer.Size = new Size(520, 80);
            txtLearnerAnswer.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 585);
            label4.Name = "label4";
            label4.Size = new Size(60, 23);
            label4.TabIndex = 8;
            label4.Text = " Marks";
            // 
            // nudMarks
            // 
            nudMarks.Location = new Point(20, 610);
            nudMarks.Name = "nudMarks";
            nudMarks.Size = new Size(100, 30);
            nudMarks.TabIndex = 9;
            // 
            // lblMax
            // 
            lblMax.AutoSize = true;
            lblMax.BackColor = Color.White;
            lblMax.ForeColor = Color.Black;
            lblMax.Location = new Point(130, 615);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(31, 23);
            lblMax.TabIndex = 10;
            lblMax.Text = "/ 0";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(42, 157, 143);
            btnSave.Location = new Point(300, 603);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(200, 45);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save mark";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(520, 603);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 45);
            btnBack.TabIndex = 12;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // GradingForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 693);
            Controls.Add(btnBack);
            Controls.Add(btnSave);
            Controls.Add(lblMax);
            Controls.Add(nudMarks);
            Controls.Add(label4);
            Controls.Add(txtLearnerAnswer);
            Controls.Add(label3);
            Controls.Add(txtQuestion);
            Controls.Add(label2);
            Controls.Add(dgvAnswers);
            Controls.Add(label1);
            Controls.Add(dgvAttempts);
            Controls.Add(lblAttemptsTitle);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "GradingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Grade answers";
         //   Load += GradingForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAttempts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAnswers).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMarks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAttemptsTitle;
        private DataGridView dgvAttempts;
        private Label label1;
        private DataGridView dgvAnswers;
        private Label label2;
        private TextBox txtQuestion;
        private Label label3;
        private TextBox txtLearnerAnswer;
        private Label label4;
        private NumericUpDown nudMarks;
        private Label lblMax;
        private Button btnSave;
        private Button btnBack;
    }
}