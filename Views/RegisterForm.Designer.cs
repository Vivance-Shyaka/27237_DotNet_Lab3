namespace WLAMS.Views
{
    partial class RegisterForm
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
            lblTitle = new Label();
            txtFullName = new TextBox();
            btnRegister = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            txtUsername = new TextBox();
            txtEmail = new TextBox();
            btnCancel = new Button();
            label1 = new Label();
            rbLearner = new RadioButton();
            rbInstructor = new RadioButton();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(31, 58, 95);
            lblTitle.Location = new Point(164, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(227, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Create account";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(236, 77);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(320, 30);
            txtFullName.TabIndex = 1;
            // txtFullName.TextChanged += txtFullName_TextChanged; // Event handler in code-behind: private void txtFullName_TextChanged(object sender, EventArgs e)

            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(56, 501);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(150, 40);
            btnRegister.TabIndex = 2;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 80);
            label2.Name = "label2";
            label2.Size = new Size(88, 23);
            label2.TabIndex = 3;
            label2.Text = "Full name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(50, 144);
            label3.Name = "label3";
            label3.Size = new Size(54, 23);
            label3.TabIndex = 4;
            label3.Text = "Email";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 213);
            label4.Name = "label4";
            label4.Size = new Size(89, 23);
            label4.TabIndex = 5;
            label4.Text = "Username";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(45, 270);
            label5.Name = "label5";
            label5.Size = new Size(85, 23);
            label5.TabIndex = 6;
            label5.Text = "Password";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(45, 325);
            label6.Name = "label6";
            label6.Size = new Size(157, 23);
            label6.TabIndex = 7;
            label6.Text = "Confirm password";
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Gray;
            label7.Location = new Point(45, 399);
            label7.Name = "label7";
            label7.Size = new Size(521, 62);
            label7.TabIndex = 8;
            label7.Text = "Password: at least 8 characters with an upper-case letter, a lower-case letter and a number.";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(236, 263);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(320, 30);
            txtPassword.TabIndex = 9;
            txtPassword.UseSystemPasswordChar = true;
           // txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(236, 318);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(320, 30);
            txtConfirmPassword.TabIndex = 10;
            txtConfirmPassword.UseSystemPasswordChar = true;
           // txtConfirmPassword.TextChanged += txtConfirmPassword_TextChanged;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(236, 213);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(320, 30);
            txtUsername.TabIndex = 11;
      // txtUsername.TextChanged += txtUsername_TextChanged; // Event handler in code-behind: private void txtUsername_TextChanged(object sender, EventArgs e)

            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(236, 141);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(320, 30);
            txtEmail.TabIndex = 12;
   // txtEmail.TextChanged += txtEmail_TextChanged; // Event handler in code-behind: private void txtEmail_TextChanged(object sender, EventArgs e)

            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(374, 501);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(150, 40);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 452);
            label1.Name = "label1";
            label1.Size = new Size(97, 23);
            label1.TabIndex = 14;
            label1.Text = "Register as";
            // 
            // rbLearner
            // 
            rbLearner.AutoSize = true;
            rbLearner.Checked = true;
            rbLearner.Location = new Point(236, 452);
            rbLearner.Name = "rbLearner";
            rbLearner.Size = new Size(91, 27);
            rbLearner.TabIndex = 15;
            rbLearner.TabStop = true;
            rbLearner.Text = "Learner";
            rbLearner.UseVisualStyleBackColor = true;
            // 
            // rbInstructor
            // 
            rbInstructor.AutoSize = true;
            rbInstructor.Location = new Point(415, 450);
            rbInstructor.Name = "rbInstructor";
            rbInstructor.Size = new Size(109, 27);
            rbInstructor.TabIndex = 16;
            rbInstructor.Text = "Instructor";
            rbInstructor.UseVisualStyleBackColor = true;
            // 
            // RegisterForm
            // 
            AcceptButton = btnRegister;
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(589, 553);
            Controls.Add(rbInstructor);
            Controls.Add(rbLearner);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(txtEmail);
            Controls.Add(txtUsername);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnRegister);
            Controls.Add(txtFullName);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(110, 20);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WLAMS - Create account";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtFullName;
        private Button btnRegister;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private TextBox txtUsername;
        private TextBox txtEmail;
        private Button btnCancel;
        private Label label1;
        private RadioButton rbLearner;
        private RadioButton rbInstructor;

        public EventHandler RegisterForm_Load { get; private set; }
    }
}