namespace WLAMS.Views
{
    partial class LessonsForm
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
            label1 = new Label();
            txtLessonTitle = new TextBox();
            label2 = new Label();
            nudOrder = new NumericUpDown();
            label3 = new Label();
            txtContent = new TextBox();
            btnSave = new Button();
            btnUpdate = new Button();
            btnBack = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            dgvLessons = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)nudOrder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLessons).BeginInit();
            SuspendLayout();
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCourse.ForeColor = Color.FromArgb(31, 58, 95);
            lblCourse.Location = new Point(20, 15);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(81, 28);
            lblCourse.TabIndex = 0;
            lblCourse.Text = "Course:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 65);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 1;
            label1.Text = "Lesson title";
            // 
            // txtLessonTitle
            // 
            txtLessonTitle.Location = new Point(20, 90);
            txtLessonTitle.Name = "txtLessonTitle";
            txtLessonTitle.Size = new Size(410, 27);
            txtLessonTitle.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 130);
            label2.Name = "label2";
            label2.Size = new Size(158, 20);
            label2.TabIndex = 3;
            label2.Text = "Lesson number (order)";
            // 
            // nudOrder
            // 
            nudOrder.Location = new Point(20, 155);
            nudOrder.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nudOrder.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudOrder.Name = "nudOrder";
            nudOrder.Size = new Size(100, 27);
            nudOrder.TabIndex = 4;
            nudOrder.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 195);
            label3.Name = "label3";
            label3.Size = new Size(61, 20);
            label3.TabIndex = 5;
            label3.Text = "Content";
            // 
            // txtContent
            // 
            txtContent.Location = new Point(20, 220);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.ScrollBars = ScrollBars.Vertical;
            txtContent.Size = new Size(410, 250);
            txtContent.TabIndex = 6;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(20, 490);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(195, 40);
            btnSave.TabIndex = 7;
            btnSave.Text = "Add lesson";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(235, 490);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(195, 40);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 605);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(410, 42);
            btnBack.TabIndex = 9;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(235, 540);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(195, 40);
            btnClear.TabIndex = 10;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 540);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(195, 40);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // dgvLessons
            // 
            dgvLessons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLessons.Location = new Point(460, 65);
            dgvLessons.Name = "dgvLessons";
            dgvLessons.RowHeadersWidth = 51;
            dgvLessons.Size = new Size(600, 560);
            dgvLessons.TabIndex = 12;
            // 
            // LessonsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 683);
            Controls.Add(dgvLessons);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(btnBack);
            Controls.Add(btnUpdate);
            Controls.Add(btnSave);
            Controls.Add(txtContent);
            Controls.Add(label3);
            Controls.Add(nudOrder);
            Controls.Add(label2);
            Controls.Add(txtLessonTitle);
            Controls.Add(label1);
            Controls.Add(lblCourse);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            Name = "LessonsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage lessons";
          //  Load += LessonsForm_Load;
            ((System.ComponentModel.ISupportInitialize)nudOrder).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLessons).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCourse;
        private Label label1;
        private TextBox txtLessonTitle;
        private Label label2;
        private NumericUpDown nudOrder;
        private Label label3;
        private TextBox txtContent;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnBack;
        private Button btnClear;
        private Button btnDelete;
        private DataGridView dgvLessons;
    }
}