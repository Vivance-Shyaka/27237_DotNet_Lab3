namespace WLAMS.Views
{
    partial class QuizTakeForm
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
            lblQuizTitle = new Label();
            lblTimer = new Label();
            lblQuestionNo = new Label();
            txtQuestion = new TextBox();
            pnlChoices = new Panel();
            txtAnswer = new TextBox();
            btnPrev = new Button();
            btnNext = new Button();
            lblAnswered = new Label();
            btnSubmit = new Button();
            rbOpt1 = new RadioButton();
            rbOpt2 = new RadioButton();
            rbOpt3 = new RadioButton();
            rbOpt4 = new RadioButton();
            pnlChoices.SuspendLayout();
            SuspendLayout();
            // 
            // lblQuizTitle
            // 
            lblQuizTitle.AutoSize = true;
            lblQuizTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuizTitle.ForeColor = Color.FromArgb(31, 58, 95);
            lblQuizTitle.Location = new Point(20, 15);
            lblQuizTitle.Name = "lblQuizTitle";
            lblQuizTitle.Size = new Size(63, 31);
            lblQuizTitle.TabIndex = 0;
            lblQuizTitle.Text = "Quiz";
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTimer.Location = new Point(760, 18);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(98, 28);
            lblTimer.TabIndex = 1;
            lblTimer.Text = "Time left";
            // 
            // lblQuestionNo
            // 
            lblQuestionNo.AutoSize = true;
            lblQuestionNo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuestionNo.Location = new Point(20, 62);
            lblQuestionNo.Name = "lblQuestionNo";
            lblQuestionNo.Size = new Size(81, 23);
            lblQuestionNo.TabIndex = 2;
            lblQuestionNo.Text = "Question";
            // 
            // txtQuestion
            // 
            txtQuestion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtQuestion.Location = new Point(20, 95);
            txtQuestion.Multiline = true;
            txtQuestion.Name = "txtQuestion";
            txtQuestion.ReadOnly = true;
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.Size = new Size(940, 110);
            txtQuestion.TabIndex = 3;
            // 
            // pnlChoices
            // 
            pnlChoices.Controls.Add(rbOpt4);
            pnlChoices.Controls.Add(rbOpt3);
            pnlChoices.Controls.Add(rbOpt2);
            pnlChoices.Controls.Add(rbOpt1);
            pnlChoices.Location = new Point(20, 215);
            pnlChoices.Name = "pnlChoices";
            pnlChoices.Size = new Size(940, 180);
            pnlChoices.TabIndex = 4;
            // 
            // txtAnswer
            // 
            txtAnswer.Location = new Point(20, 410);
            txtAnswer.Multiline = true;
            txtAnswer.Name = "txtAnswer";
            txtAnswer.ScrollBars = ScrollBars.Vertical;
            txtAnswer.Size = new Size(940, 100);
            txtAnswer.TabIndex = 5;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(20, 600);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(180, 45);
            btnPrev.TabIndex = 6;
            btnPrev.Text = "Previous";
            btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(210, 600);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(180, 45);
            btnNext.TabIndex = 7;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // lblAnswered
            // 
            lblAnswered.AutoSize = true;
            lblAnswered.Location = new Point(420, 612);
            lblAnswered.Name = "lblAnswered";
            lblAnswered.Size = new Size(136, 23);
            lblAnswered.TabIndex = 8;
            lblAnswered.Text = "Answered: 0 of 0";
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.FromArgb(42, 157, 143);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Location = new Point(780, 600);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(180, 45);
            btnSubmit.TabIndex = 9;
            btnSubmit.Text = "Submit quiz";
            btnSubmit.UseVisualStyleBackColor = false;
            // 
            // rbOpt1
            // 
            rbOpt1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbOpt1.Location = new Point(15, 8);
            rbOpt1.Name = "rbOpt1";
            rbOpt1.Size = new Size(900, 40);
            rbOpt1.TabIndex = 0;
            rbOpt1.TabStop = true;
            rbOpt1.Text = "radioButton1";
            rbOpt1.UseVisualStyleBackColor = true;
            // 
            // rbOpt2
            // 
            rbOpt2.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbOpt2.Location = new Point(15, 50);
            rbOpt2.Name = "rbOpt2";
            rbOpt2.Size = new Size(900, 40);
            rbOpt2.TabIndex = 1;
            rbOpt2.TabStop = true;
            rbOpt2.Text = "radioButton1";
            rbOpt2.UseVisualStyleBackColor = true;
            // 
            // rbOpt3
            // 
            rbOpt3.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbOpt3.Location = new Point(15, 92);
            rbOpt3.Name = "rbOpt3";
            rbOpt3.Size = new Size(900, 40);
            rbOpt3.TabIndex = 2;
            rbOpt3.TabStop = true;
            rbOpt3.Text = "radioButton1";
            rbOpt3.UseVisualStyleBackColor = true;
            // 
            // rbOpt4
            // 
            rbOpt4.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbOpt4.Location = new Point(15, 134);
            rbOpt4.Name = "rbOpt4";
            rbOpt4.Size = new Size(900, 40);
            rbOpt4.TabIndex = 3;
            rbOpt4.TabStop = true;
            rbOpt4.Text = "radioButton1";
            rbOpt4.UseVisualStyleBackColor = true;
            // 
            // QuizTakeForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 673);
            Controls.Add(btnSubmit);
            Controls.Add(lblAnswered);
            Controls.Add(btnNext);
            Controls.Add(btnPrev);
            Controls.Add(txtAnswer);
            Controls.Add(pnlChoices);
            Controls.Add(txtQuestion);
            Controls.Add(lblQuestionNo);
            Controls.Add(lblTimer);
            Controls.Add(lblQuizTitle);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "QuizTakeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quiz";
        //    Load += QuizTakeForm_Load;
            pnlChoices.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblQuizTitle;
        private Label lblTimer;
        private Label lblQuestionNo;
        private TextBox txtQuestion;
        private Panel pnlChoices;
        private TextBox txtAnswer;
        private Button btnPrev;
        private Button btnNext;
        private Label lblAnswered;
        private RadioButton rbOpt1;
        private Button btnSubmit;
        private RadioButton rbOpt4;
        private RadioButton rbOpt3;
        private RadioButton rbOpt2;
    }
}