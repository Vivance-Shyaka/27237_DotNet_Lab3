namespace WLAMS.Views
{
    partial class GradebookForm
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
            btnShow = new Button();
            btnExport = new Button();
            dgvGradebook = new DataGridView();
            label2 = new Label();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvGradebook).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 13);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 0;
            label1.Text = "Course";
            // 
            // cboCourse
            // 
            cboCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCourse.FormattingEnabled = true;
            cboCourse.Location = new Point(18, 35);
            cboCourse.Name = "cboCourse";
            cboCourse.Size = new Size(463, 28);
            cboCourse.TabIndex = 1;
            // 
            // btnShow
            // 
            btnShow.Location = new Point(498, 33);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(142, 30);
            btnShow.TabIndex = 2;
            btnShow.Text = "Show gradebook";
            btnShow.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(649, 33);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(124, 30);
            btnExport.TabIndex = 3;
            btnExport.Text = "Export CSV";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // dgvGradebook
            // 
            dgvGradebook.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGradebook.Location = new Point(18, 78);
            dgvGradebook.Name = "dgvGradebook";
            dgvGradebook.RowHeadersWidth = 51;
            dgvGradebook.Size = new Size(1031, 417);
            dgvGradebook.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(20, 580);
            label2.Name = "label2";
            label2.Size = new Size(696, 20);
            label2.TabIndex = 5;
            label2.Text = "Percentages show each learner's best graded attempt. \"Pending\" means an answer is waiting for grading";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 610);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 45);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // GradebookForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1068, 697);
            Controls.Add(btnBack);
            Controls.Add(label2);
            Controls.Add(dgvGradebook);
            Controls.Add(btnExport);
            Controls.Add(btnShow);
            Controls.Add(cboCourse);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "GradebookForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gradebook";
          //  Load += GradebookForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGradebook).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cboCourse;
        private Button btnShow;
        private Button btnExport;
        private DataGridView dgvGradebook;
        private Label label2;
        private Button btnBack;
    }
}