namespace WLAMS.Views
{
    partial class CategoriesForm
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
            txtCategoryName = new TextBox();
            btnSave = new Button();
            dgvCategories = new DataGridView();
            btnUpdate = new Button();
            btnBack = new Button();
            btnRefresh = new Button();
            btnSearch = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            label2 = new Label();
            txtSearch = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(110, 20);
            label1.TabIndex = 0;
            label1.Text = "Category name";
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(20, 45);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(300, 27);
            txtCategoryName.TabIndex = 1;
//            txtCategoryName.TextChanged += txtCategoryName_TextChanged;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(20, 95);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 38);
            btnSave.TabIndex = 2;
            btnSave.Text = "Add";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Location = new Point(350, 20);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.RowHeadersWidth = 51;
            dgvCategories.Size = new Size(390, 450);
            dgvCategories.TabIndex = 3;
         //   dgvCategories.CellContentClick += dgvCategories_CellContentClick;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(180, 95);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(140, 38);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 440);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(300, 42);
            btnBack.TabIndex = 5;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(180, 290);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(140, 38);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "Show all";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(20, 290);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(140, 38);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(180, 143);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(140, 38);
            btnClear.TabIndex = 8;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 143);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(140, 38);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 215);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 11;
            label2.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(20, 240);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 27);
            txtSearch.TabIndex = 12;
            // 
            // CategoriesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 513);
            Controls.Add(txtSearch);
            Controls.Add(label2);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(btnSearch);
            Controls.Add(btnRefresh);
            Controls.Add(btnBack);
            Controls.Add(btnUpdate);
            Controls.Add(dgvCategories);
            Controls.Add(btnSave);
            Controls.Add(txtCategoryName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CategoriesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage categories";
        // Load += CategoriesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtCategoryName;
        private Button btnSave;
        private DataGridView dgvCategories;
        private Button btnUpdate;
        private Button btnBack;
        private Button btnRefresh;
        private Button btnSearch;
        private Button btnClear;
        private Button btnDelete;
        private Label label2;
        private TextBox txtSearch;
    }
}