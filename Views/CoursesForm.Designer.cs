namespace WLAMS.Views
{
    partial class CoursesForm
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
            txtTitle = new TextBox();
            cboCategory = new ComboBox();
            rbBeginner = new RadioButton();
            btnSave = new Button();
            dgvCourses = new DataGridView();
            label2 = new Label();
            txtDescription = new TextBox();
            label3 = new Label();
            label4 = new Label();
            rbAdvanced = new RadioButton();
            rbIntermediate = new RadioButton();
            lblStatus = new Label();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnBack = new Button();
            btnLessons = new Button();
            btnUnpublish = new Button();
            btnPublish = new Button();
            btnClear = new Button();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(38, 20);
            label1.TabIndex = 0;
            label1.Text = "Title";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(20, 45);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(330, 27);
            txtTitle.TabIndex = 1;
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(20, 240);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(330, 28);
            cboCategory.TabIndex = 2;
            // 
            // rbBeginner
            // 
            rbBeginner.AutoSize = true;
            rbBeginner.Checked = true;
            rbBeginner.Location = new Point(20, 310);
            rbBeginner.Name = "rbBeginner";
            rbBeginner.Size = new Size(89, 24);
            rbBeginner.TabIndex = 3;
            rbBeginner.TabStop = true;
            rbBeginner.Text = "Beginner";
            rbBeginner.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(20, 385);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 40);
            btnSave.TabIndex = 4;
            btnSave.Text = "Add course";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // dgvCourses
            // 
            dgvCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCourses.Location = new Point(390, 65);
            dgvCourses.Name = "dgvCourses";
            dgvCourses.RowHeadersWidth = 51;
            dgvCourses.Size = new Size(640, 560);
            dgvCourses.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 85);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 6;
            label2.Text = "Description";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(20, 110);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(330, 90);
            txtDescription.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 215);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 8;
            label3.Text = "Category";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 285);
            label4.Name = "label4";
            label4.Size = new Size(43, 20);
            label4.TabIndex = 9;
            label4.Text = "Level";
            // 
            // rbAdvanced
            // 
            rbAdvanced.AutoSize = true;
            rbAdvanced.Location = new Point(250, 310);
            rbAdvanced.Name = "rbAdvanced";
            rbAdvanced.Size = new Size(96, 24);
            rbAdvanced.TabIndex = 10;
            rbAdvanced.Text = "Advanced";
            rbAdvanced.UseVisualStyleBackColor = true;
            // 
            // rbIntermediate
            // 
            rbIntermediate.AutoSize = true;
            rbIntermediate.Location = new Point(125, 310);
            rbIntermediate.Name = "rbIntermediate";
            rbIntermediate.Size = new Size(115, 24);
            rbIntermediate.TabIndex = 11;
            rbIntermediate.Text = "Intermediate";
            rbIntermediate.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(20, 350);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(62, 23);
            lblStatus.TabIndex = 12;
            lblStatus.Text = "empty";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 435);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(160, 40);
            btnDelete.TabIndex = 13;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(190, 385);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(160, 40);
            btnUpdate.TabIndex = 14;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 610);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(330, 42);
            btnBack.TabIndex = 15;
            btnBack.Text = " Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnLessons
            // 
            btnLessons.Location = new Point(20, 545);
            btnLessons.Name = "btnLessons";
            btnLessons.Size = new Size(330, 42);
            btnLessons.TabIndex = 16;
            btnLessons.Text = "Manage lessons";
            btnLessons.UseVisualStyleBackColor = true;
            // 
            // btnUnpublish
            // 
            btnUnpublish.Location = new Point(190, 490);
            btnUnpublish.Name = "btnUnpublish";
            btnUnpublish.Size = new Size(160, 40);
            btnUnpublish.TabIndex = 17;
            btnUnpublish.Text = "Unpublish";
            btnUnpublish.UseVisualStyleBackColor = true;
            // 
            // btnPublish
            // 
            btnPublish.Location = new Point(20, 490);
            btnPublish.Name = "btnPublish";
            btnPublish.Size = new Size(160, 40);
            btnPublish.TabIndex = 18;
            btnPublish.Text = "Publish";
            btnPublish.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(190, 435);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(160, 40);
            btnClear.TabIndex = 19;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(390, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(370, 27);
            txtSearch.TabIndex = 20;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(770, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(120, 34);
            btnSearch.TabIndex = 21;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(900, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 34);
            btnRefresh.TabIndex = 22;
            btnRefresh.Text = "Show all";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // CoursesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 693);
            Controls.Add(btnRefresh);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnClear);
            Controls.Add(btnPublish);
            Controls.Add(btnUnpublish);
            Controls.Add(btnLessons);
            Controls.Add(btnBack);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(lblStatus);
            Controls.Add(rbIntermediate);
            Controls.Add(rbAdvanced);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtDescription);
            Controls.Add(label2);
            Controls.Add(dgvCourses);
            Controls.Add(btnSave);
            Controls.Add(rbBeginner);
            Controls.Add(cboCategory);
            Controls.Add(txtTitle);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CoursesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My courses";
          //  Load += CoursesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtTitle;
        private ComboBox cboCategory;
        private RadioButton rbBeginner;
        private Button btnSave;
        private DataGridView dgvCourses;
        private Label label2;
        private TextBox txtDescription;
        private Label label3;
        private Label label4;
        private RadioButton rbAdvanced;
        private RadioButton rbIntermediate;
        private Label lblStatus;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnBack;
        private Button btnLessons;
        private Button btnUnpublish;
        private Button btnPublish;
        private Button btnClear;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;
    }
}