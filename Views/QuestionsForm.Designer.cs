namespace WLAMS.Views
{
    partial class QuestionsForm
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
            lblQuiz = new Label();
            label1 = new Label();
            txtQuestion = new TextBox();
            label2 = new Label();
            rbSingle = new RadioButton();
            rbTrueFalse = new RadioButton();
            rbShort = new RadioButton();
            label3 = new Label();
            nudMarks = new NumericUpDown();
            grpOptions = new GroupBox();
            btnSave = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnBack = new Button();
            lblTotal = new Label();
            dgvQuestions = new DataGridView();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            rbCorrect1 = new RadioButton();
            txtOption1 = new TextBox();
            rbCorrect2 = new RadioButton();
            txtOption2 = new TextBox();
            rbCorrect3 = new RadioButton();
            txtOption3 = new TextBox();
            rbCorrect4 = new RadioButton();
            txtOption4 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nudMarks).BeginInit();
            grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvQuestions).BeginInit();
            SuspendLayout();
            // 
            // lblQuiz
            // 
            lblQuiz.AutoSize = true;
            lblQuiz.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuiz.ForeColor = Color.FromArgb(31, 58, 95);
            lblQuiz.Location = new Point(20, 15);
            lblQuiz.Name = "lblQuiz";
            lblQuiz.Size = new Size(60, 28);
            lblQuiz.TabIndex = 0;
            lblQuiz.Text = "Quiz:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 55);
            label1.Name = "label1";
            label1.Size = new Size(68, 20);
            label1.TabIndex = 1;
            label1.Text = "Question";
            // 
            // txtQuestion
            // 
            txtQuestion.Location = new Point(20, 80);
            txtQuestion.Multiline = true;
            txtQuestion.Name = "txtQuestion";
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.Size = new Size(480, 80);
            txtQuestion.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 170);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 3;
            label2.Text = "Type";
            // 
            // rbSingle
            // 
            rbSingle.AutoSize = true;
            rbSingle.Checked = true;
            rbSingle.Location = new Point(20, 195);
            rbSingle.Name = "rbSingle";
            rbSingle.Size = new Size(118, 24);
            rbSingle.TabIndex = 4;
            rbSingle.TabStop = true;
            rbSingle.Text = "Single choice";
            rbSingle.UseVisualStyleBackColor = true;
            // 
            // rbTrueFalse
            // 
            rbTrueFalse.AutoSize = true;
            rbTrueFalse.Location = new Point(170, 195);
            rbTrueFalse.Name = "rbTrueFalse";
            rbTrueFalse.Size = new Size(104, 24);
            rbTrueFalse.TabIndex = 5;
            rbTrueFalse.Text = "True / False";
            rbTrueFalse.UseVisualStyleBackColor = true;
            // 
            // rbShort
            // 
            rbShort.AutoSize = true;
            rbShort.Location = new Point(300, 195);
            rbShort.Name = "rbShort";
            rbShort.Size = new Size(115, 24);
            rbShort.TabIndex = 6;
            rbShort.Text = "Short answer";
            rbShort.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 235);
            label3.Name = "label3";
            label3.Size = new Size(48, 20);
            label3.TabIndex = 7;
            label3.Text = "Marks";
            // 
            // nudMarks
            // 
            nudMarks.Location = new Point(20, 260);
            nudMarks.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudMarks.Name = "nudMarks";
            nudMarks.Size = new Size(100, 27);
            nudMarks.TabIndex = 8;
            nudMarks.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // grpOptions
            // 
            grpOptions.Controls.Add(txtOption4);
            grpOptions.Controls.Add(rbCorrect4);
            grpOptions.Controls.Add(txtOption3);
            grpOptions.Controls.Add(rbCorrect3);
            grpOptions.Controls.Add(txtOption2);
            grpOptions.Controls.Add(rbCorrect2);
            grpOptions.Controls.Add(txtOption1);
            grpOptions.Controls.Add(rbCorrect1);
            grpOptions.Location = new Point(20, 305);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new Size(480, 200);
            grpOptions.TabIndex = 9;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options (select the correct answer)";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(20, 525);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(235, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "Add question";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(265, 525);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(235, 40);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = " Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 575);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(235, 40);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(265, 575);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(235, 40);
            btnClear.TabIndex = 12;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 635);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(480, 42);
            btnBack.TabIndex = 13;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(520, 22);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 23);
            lblTotal.TabIndex = 14;
            // 
            // dgvQuestions
            // 
            dgvQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvQuestions.Location = new Point(520, 55);
            dgvQuestions.Name = "dgvQuestions";
            dgvQuestions.RowHeadersWidth = 51;
            dgvQuestions.Size = new Size(720, 580);
            dgvQuestions.TabIndex = 15;
            // 
            // rbCorrect1
            // 
            rbCorrect1.AutoSize = true;
            rbCorrect1.Checked = true;
            rbCorrect1.Location = new Point(15, 38);
            rbCorrect1.Name = "rbCorrect1";
            rbCorrect1.Size = new Size(17, 16);
            rbCorrect1.TabIndex = 0;
            rbCorrect1.TabStop = true;
            rbCorrect1.UseVisualStyleBackColor = true;
            // 
            // txtOption1
            // 
            txtOption1.Location = new Point(45, 32);
            txtOption1.Name = "txtOption1";
            txtOption1.Size = new Size(410, 27);
            txtOption1.TabIndex = 1;
            // 
            // rbCorrect2
            // 
            rbCorrect2.AutoSize = true;
            rbCorrect2.Location = new Point(15, 78);
            rbCorrect2.Name = "rbCorrect2";
            rbCorrect2.Size = new Size(17, 16);
            rbCorrect2.TabIndex = 2;
            rbCorrect2.TabStop = true;
            rbCorrect2.UseVisualStyleBackColor = true;
            // 
            // txtOption2
            // 
            txtOption2.Location = new Point(45, 72);
            txtOption2.Name = "txtOption2";
            txtOption2.Size = new Size(410, 27);
            txtOption2.TabIndex = 3;
            // 
            // rbCorrect3
            // 
            rbCorrect3.AutoSize = true;
            rbCorrect3.Location = new Point(15, 118);
            rbCorrect3.Name = "rbCorrect3";
            rbCorrect3.Size = new Size(17, 16);
            rbCorrect3.TabIndex = 4;
            rbCorrect3.TabStop = true;
            rbCorrect3.UseVisualStyleBackColor = true;
            // 
            // txtOption3
            // 
            txtOption3.Location = new Point(45, 112);
            txtOption3.Name = "txtOption3";
            txtOption3.Size = new Size(410, 27);
            txtOption3.TabIndex = 5;
            // 
            // rbCorrect4
            // 
            rbCorrect4.AutoSize = true;
            rbCorrect4.Location = new Point(15, 158);
            rbCorrect4.Name = "rbCorrect4";
            rbCorrect4.Size = new Size(17, 16);
            rbCorrect4.TabIndex = 6;
            rbCorrect4.TabStop = true;
            rbCorrect4.UseVisualStyleBackColor = true;
            // 
            // txtOption4
            // 
            txtOption4.Location = new Point(45, 152);
            txtOption4.Name = "txtOption4";
            txtOption4.Size = new Size(410, 27);
            txtOption4.TabIndex = 7;
            // 
            // QuestionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 713);
            Controls.Add(dgvQuestions);
            Controls.Add(lblTotal);
            Controls.Add(btnBack);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnSave);
            Controls.Add(grpOptions);
            Controls.Add(nudMarks);
            Controls.Add(label3);
            Controls.Add(rbShort);
            Controls.Add(rbTrueFalse);
            Controls.Add(rbSingle);
            Controls.Add(label2);
            Controls.Add(txtQuestion);
            Controls.Add(label1);
            Controls.Add(lblQuiz);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "QuestionsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage questions";
       //     Load += QuestionsForm_Load;
            ((System.ComponentModel.ISupportInitialize)nudMarks).EndInit();
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvQuestions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblQuiz;
        private Label label1;
        private TextBox txtQuestion;
        private Label label2;
        private RadioButton rbSingle;
        private RadioButton rbTrueFalse;
        private RadioButton rbShort;
        private Label label3;
        private NumericUpDown nudMarks;
        private GroupBox grpOptions;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnBack;
        private Label lblTotal;
        private DataGridView dgvQuestions;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox txtOption3;
        private RadioButton rbCorrect3;
        private TextBox txtOption2;
        private RadioButton rbCorrect2;
        private TextBox txtOption1;
        private RadioButton rbCorrect1;
        private TextBox txtOption4;
        private RadioButton rbCorrect4;
    }
}