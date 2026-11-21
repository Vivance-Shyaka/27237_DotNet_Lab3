namespace WLAMS.Views
{
    partial class UsersForm
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
            txtFullName = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            label5 = new Label();
            lblStatus = new Label();
            rbLearner = new RadioButton();
            rbInstructor = new RadioButton();
            rbAdmin = new RadioButton();
            chkActive = new CheckBox();
            btnSave = new Button();
            btnBack = new Button();
            btnReject = new Button();
            btnApprove = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            txtSearch = new TextBox();
            dgvUsers = new DataGridView();
            btnSearch = new Button();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 0;
            label1.Text = "Full name";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(20, 45);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(330, 27);
            txtFullName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 85);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 2;
            label2.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 150);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 3;
            label3.Text = "Username";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 215);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 4;
            label4.Text = "Password";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(20, 110);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(330, 27);
            txtEmail.TabIndex = 5;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(20, 240);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Required for new users, blank = keep\t";
            txtPassword.Size = new Size(330, 27);
            txtPassword.TabIndex = 7;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(20, 175);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(330, 27);
            txtUsername.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 285);
            label5.Name = "label5";
            label5.Size = new Size(39, 20);
            label5.TabIndex = 9;
            label5.Text = "Role";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(20, 500);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(62, 23);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "empty";
            // 
            // rbLearner
            // 
            rbLearner.AutoSize = true;
            rbLearner.Checked = true;
            rbLearner.Location = new Point(20, 310);
            rbLearner.Name = "rbLearner";
            rbLearner.Size = new Size(79, 24);
            rbLearner.TabIndex = 11;
            rbLearner.TabStop = true;
            rbLearner.Text = "Learner";
            rbLearner.UseVisualStyleBackColor = true;
            // 
            // rbInstructor
            // 
            rbInstructor.AutoSize = true;
            rbInstructor.Location = new Point(120, 310);
            rbInstructor.Name = "rbInstructor";
            rbInstructor.Size = new Size(92, 24);
            rbInstructor.TabIndex = 12;
            rbInstructor.Text = "Instructor";
            rbInstructor.UseVisualStyleBackColor = true;
            // 
            // rbAdmin
            // 
            rbAdmin.AutoSize = true;
            rbAdmin.Location = new Point(240, 310);
            rbAdmin.Name = "rbAdmin";
            rbAdmin.Size = new Size(74, 24);
            rbAdmin.TabIndex = 13;
            rbAdmin.Text = "Admin";
            rbAdmin.UseVisualStyleBackColor = true;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Checked = true;
            chkActive.CheckState = CheckState.Checked;
            chkActive.Location = new Point(20, 345);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(142, 24);
            chkActive.TabIndex = 14;
            chkActive.Text = "Account is active";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(20, 390);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 40);
            btnSave.TabIndex = 15;
            btnSave.Text = "Add user";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 600);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(330, 42);
            btnBack.TabIndex = 16;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnReject
            // 
            btnReject.Location = new Point(190, 530);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(160, 40);
            btnReject.TabIndex = 17;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = true;
            // 
            // btnApprove
            // 
            btnApprove.Location = new Point(20, 530);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(160, 40);
            btnApprove.TabIndex = 18;
            btnApprove.Text = "Approve";
            btnApprove.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(190, 440);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(160, 40);
            btnClear.TabIndex = 19;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 440);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(160, 40);
            btnDelete.TabIndex = 20;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(190, 390);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(160, 40);
            btnUpdate.TabIndex = 21;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(390, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(370, 27);
            txtSearch.TabIndex = 22;
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(390, 65);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.Size = new Size(640, 560);
            dgvUsers.TabIndex = 23;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(770, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(120, 34);
            btnSearch.TabIndex = 24;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(900, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 34);
            btnRefresh.TabIndex = 25;
            btnRefresh.Text = "Show all";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // UsersForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 693);
            Controls.Add(btnRefresh);
            Controls.Add(btnSearch);
            Controls.Add(dgvUsers);
            Controls.Add(txtSearch);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(btnApprove);
            Controls.Add(btnReject);
            Controls.Add(btnBack);
            Controls.Add(btnSave);
            Controls.Add(chkActive);
            Controls.Add(rbAdmin);
            Controls.Add(rbInstructor);
            Controls.Add(rbLearner);
            Controls.Add(lblStatus);
            Controls.Add(label5);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtFullName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "UsersForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage users";
          //  Load += UsersForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFullName;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Label label5;
        private Label lblStatus;
        private RadioButton rbLearner;
        private RadioButton rbInstructor;
        private RadioButton rbAdmin;
        private CheckBox chkActive;
        private Button btnSave;
        private Button btnBack;
        private Button btnReject;
        private Button btnApprove;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private TextBox txtSearch;
        private DataGridView dgvUsers;
        private Button btnSearch;
        private Button btnRefresh;
    }
}