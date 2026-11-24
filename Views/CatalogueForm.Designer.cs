namespace WLAMS.Views
{
    partial class CatalogueForm
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
            txtSearch = new TextBox();
            btnSearch = new Button();
            dgvCourses = new DataGridView();
            label1 = new Label();
            txtDescription = new TextBox();
            btnEnroll = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(20, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by title, category or instructor";
            txtSearch.Size = new Size(420, 30);
            txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(570, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 34);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // dgvCourses
            // 
            dgvCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCourses.Location = new Point(20, 65);
            dgvCourses.Name = "dgvCourses";
            dgvCourses.RowHeadersWidth = 51;
            dgvCourses.Size = new Size(1060, 350);
            dgvCourses.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 430);
            label1.Name = "label1";
            label1.Size = new Size(152, 23);
            label1.TabIndex = 3;
            label1.Text = "Course description";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(20, 455);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(1060, 100);
            txtDescription.TabIndex = 4;
            // 
            // btnEnroll
            // 
            btnEnroll.Location = new Point(12, 575);
            btnEnroll.Name = "btnEnroll";
            btnEnroll.Size = new Size(260, 45);
            btnEnroll.TabIndex = 5;
            btnEnroll.Text = "Enroll in this course";
            btnEnroll.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(300, 575);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 45);
            btnBack.TabIndex = 6;
            btnBack.Text = " Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // CatalogueForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 653);
            Controls.Add(btnBack);
            Controls.Add(btnEnroll);
            Controls.Add(txtDescription);
            Controls.Add(label1);
            Controls.Add(dgvCourses);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CatalogueForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Browse courses";
            //Load += CatalogueForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dgvCourses;
        private Label label1;
        private TextBox txtDescription;
        private Button btnEnroll;
        private Button btnBack;
    }
}