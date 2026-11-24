namespace WLAMS.Views
{
    partial class MyCoursesForm
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
            btnRefresh = new Button();
            dgvCourses = new DataGridView();
            btnOpen = new Button();
            btnUnenroll = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(20, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by title or category";
            txtSearch.Size = new Size(420, 30);
            txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(450, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 34);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(570, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 34);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Show all";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // dgvCourses
            // 
            dgvCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCourses.Location = new Point(20, 65);
            dgvCourses.Name = "dgvCourses";
            dgvCourses.RowHeadersWidth = 51;
            dgvCourses.Size = new Size(1060, 460);
            dgvCourses.TabIndex = 3;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(20, 550);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(220, 45);
            btnOpen.TabIndex = 4;
            btnOpen.Text = "Open course";
            btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnUnenroll
            // 
            btnUnenroll.Location = new Point(260, 550);
            btnUnenroll.Name = "btnUnenroll";
            btnUnenroll.Size = new Size(200, 45);
            btnUnenroll.TabIndex = 5;
            btnUnenroll.Text = "Unenroll";
            btnUnenroll.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(480, 550);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 45);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // MyCoursesForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 653);
            Controls.Add(btnBack);
            Controls.Add(btnUnenroll);
            Controls.Add(btnOpen);
            Controls.Add(dgvCourses);
            Controls.Add(btnRefresh);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MyCoursesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My courses";
         //   Load += MyCoursesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;
        private DataGridView dgvCourses;
        private Button btnOpen;
        private Button btnUnenroll;
        private Button btnBack;
    }
}