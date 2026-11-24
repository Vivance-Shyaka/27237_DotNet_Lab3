namespace WLAMS.Views
{
    partial class QuizzesForm
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
            cboCourse = new ComboBox();
            label2 = new Label();
            txtTitle = new TextBox();
            label3 = new Label();
            txtInstructions = new TextBox();
            label4 = new Label();
            nudTime = new NumericUpDown();
            label5 = new Label();
            nudPass = new NumericUpDown();
            label6 = new Label();
            nudAttempts = new NumericUpDown();
            lblStatus = new Label();
            btnSave = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnPublish = new Button();
            btnUnpublish = new Button();
            btnQuestions = new Button();
            btnBack = new Button();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnRefresh = new Button();
            dgvQuizzes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)nudTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPass).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAttempts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvQuizzes).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 0;
            label1.Text = "Course";
            // 
            // cboCourse
            // 
            cboCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCourse.FormattingEnabled = true;
            cboCourse.Location = new Point(20, 45);
            cboCourse.Name = "cboCourse";
            cboCourse.Size = new Size(330, 28);
            cboCourse.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 85);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 2;
            label2.Text = "Quiz title";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(20, 110);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(330, 27);
            txtTitle.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 150);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 4;
            label3.Text = "Instructions";
            // 
            // txtInstructions
            // 
            txtInstructions.Location = new Point(20, 175);
            txtInstructions.Multiline = true;
            txtInstructions.Name = "txtInstructions";
            txtInstructions.ScrollBars = ScrollBars.Vertical;
            txtInstructions.Size = new Size(330, 70);
            txtInstructions.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 255);
            label4.Name = "label4";
            label4.Size = new Size(149, 20);
            label4.TabIndex = 6;
            label4.Text = "Time limit (0 = none)";
            // 
            // nudTime
            // 
            nudTime.Location = new Point(20, 280);
            nudTime.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTime.Name = "nudTime";
            nudTime.Size = new Size(140, 27);
            nudTime.TabIndex = 7;
            nudTime.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(190, 255);
            label5.Name = "label5";
            label5.Size = new Size(99, 20);
            label5.TabIndex = 8;
            label5.Text = "Pass mark (%)";
            // 
            // nudPass
            // 
            nudPass.Location = new Point(190, 280);
            nudPass.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudPass.Name = "nudPass";
            nudPass.Size = new Size(140, 27);
            nudPass.TabIndex = 9;
            nudPass.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 320);
            label6.Name = "label6";
            label6.Size = new Size(203, 20);
            label6.TabIndex = 10;
            label6.Text = "Max attempts (0 = unlimited)";
            // 
            // nudAttempts
            // 
            nudAttempts.Location = new Point(20, 345);
            nudAttempts.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudAttempts.Name = "nudAttempts";
            nudAttempts.Size = new Size(140, 27);
            nudAttempts.TabIndex = 11;
            nudAttempts.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(20, 385);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 23);
            lblStatus.TabIndex = 12;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(20, 415);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 40);
            btnSave.TabIndex = 13;
            btnSave.Text = "Add quiz";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(190, 415);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(160, 40);
            btnUpdate.TabIndex = 14;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 465);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(160, 40);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(190, 465);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(160, 40);
            btnClear.TabIndex = 16;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnPublish
            // 
            btnPublish.Location = new Point(20, 520);
            btnPublish.Name = "btnPublish";
            btnPublish.Size = new Size(160, 40);
            btnPublish.TabIndex = 17;
            btnPublish.Text = "Publish";
            btnPublish.UseVisualStyleBackColor = true;
            // 
            // btnUnpublish
            // 
            btnUnpublish.Location = new Point(190, 520);
            btnUnpublish.Name = "btnUnpublish";
            btnUnpublish.Size = new Size(160, 40);
            btnUnpublish.TabIndex = 18;
            btnUnpublish.Text = "Unpublish";
            btnUnpublish.UseVisualStyleBackColor = true;
            // 
            // btnQuestions
            // 
            btnQuestions.Location = new Point(20, 575);
            btnQuestions.Name = "btnQuestions";
            btnQuestions.Size = new Size(330, 42);
            btnQuestions.TabIndex = 19;
            btnQuestions.Text = "Manage questions";
            btnQuestions.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 630);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(330, 42);
            btnBack.TabIndex = 20;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(390, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(370, 27);
            txtSearch.TabIndex = 21;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(770, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(120, 34);
            btnSearch.TabIndex = 22;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(900, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 34);
            btnRefresh.TabIndex = 23;
            btnRefresh.Text = "Show all";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // dgvQuizzes
            // 
            dgvQuizzes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvQuizzes.Location = new Point(390, 65);
            dgvQuizzes.Name = "dgvQuizzes";
            dgvQuizzes.RowHeadersWidth = 51;
            dgvQuizzes.Size = new Size(640, 560);
            dgvQuizzes.TabIndex = 24;
            // 
            // QuizzesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 693);
            Controls.Add(dgvQuizzes);
            Controls.Add(btnRefresh);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnBack);
            Controls.Add(btnQuestions);
            Controls.Add(btnUnpublish);
            Controls.Add(btnPublish);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnSave);
            Controls.Add(lblStatus);
            Controls.Add(nudAttempts);
            Controls.Add(label6);
            Controls.Add(nudPass);
            Controls.Add(label5);
            Controls.Add(nudTime);
            Controls.Add(label4);
            Controls.Add(txtInstructions);
            Controls.Add(label3);
            Controls.Add(txtTitle);
            Controls.Add(label2);
            Controls.Add(cboCourse);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "QuizzesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My quizzes";
      //      Load += QuizzesForm_Load;
            ((System.ComponentModel.ISupportInitialize)nudTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPass).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAttempts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvQuizzes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cboCourse;
        private Label label2;
        private TextBox txtTitle;
        private Label label3;
        private TextBox txtInstructions;
        private Label label4;
        private NumericUpDown nudTime;
        private Label label5;
        private NumericUpDown nudPass;
        private Label label6;
        private NumericUpDown nudAttempts;
        private Label lblStatus;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnPublish;
        private Button btnUnpublish;
        private Button btnQuestions;
        private Button btnBack;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;
        private DataGridView dgvQuizzes;
    }
}