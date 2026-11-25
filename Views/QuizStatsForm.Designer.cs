namespace WLAMS.Views
{
    partial class QuizStatsForm
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
            label1 = new Label();
            cboQuiz = new ComboBox();
            btnShow = new Button();
            btnExport = new Button();
            lblStats = new Label();
            label2 = new Label();
            dgvQuestions = new DataGridView();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvQuestions).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 15);
            label1.Name = "label1";
            label1.Size = new Size(45, 23);
            label1.TabIndex = 0;
            label1.Text = "Quiz";
            // 
            // cboQuiz
            // 
            cboQuiz.DropDownStyle = ComboBoxStyle.DropDownList;
            cboQuiz.FormattingEnabled = true;
            cboQuiz.Location = new Point(20, 40);
            cboQuiz.Name = "cboQuiz";
            cboQuiz.Size = new Size(520, 31);
            cboQuiz.TabIndex = 1;
            // 
            // btnShow
            // 
            btnShow.Location = new Point(560, 38);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(160, 34);
            btnShow.TabIndex = 2;
            btnShow.Text = "Show statistics";
            btnShow.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(730, 38);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(140, 34);
            btnExport.TabIndex = 3;
            btnExport.Text = "Export CSV";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // lblStats
            // 
            lblStats.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStats.ForeColor = Color.FromArgb(31, 58, 95);
            lblStats.Location = new Point(20, 90);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(1060, 60);
            lblStats.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(20, 160);
            label2.Name = "label2";
            label2.Size = new Size(293, 25);
            label2.TabIndex = 5;
            label2.Text = "Questions (the most missed first)";
            // 
            // dgvQuestions
            // 
            dgvQuestions.AllowUserToAddRows = false;
            dgvQuestions.AllowUserToDeleteRows = false;
            dgvQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvQuestions.Location = new Point(20, 190);
            dgvQuestions.Name = "dgvQuestions";
            dgvQuestions.ReadOnly = true;
            dgvQuestions.RowHeadersWidth = 51;
            dgvQuestions.Size = new Size(1060, 400);
            dgvQuestions.TabIndex = 6;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 605);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 45);
            btnBack.TabIndex = 7;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // QuizStatsForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1240, 751);
            Controls.Add(btnBack);
            Controls.Add(dgvQuestions);
            Controls.Add(label2);
            Controls.Add(lblStats);
            Controls.Add(btnExport);
            Controls.Add(btnShow);
            Controls.Add(cboQuiz);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "QuizStatsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quiz statistics";
         //   Load += QuizStatsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvQuestions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cboQuiz;
        private Button btnShow;
        private Button btnExport;
        private Label lblStats;
        private Label label2;
        private DataGridView dgvQuestions;
        private Button btnBack;
    }
}