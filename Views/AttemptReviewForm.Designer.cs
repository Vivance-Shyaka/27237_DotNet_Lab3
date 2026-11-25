namespace WLAMS.Views
{
    partial class AttemptReviewForm
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
            lblSummary = new Label();
            dgvReview = new DataGridView();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReview).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(31, 58, 95);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(76, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Result";
            // 
            // lblSummary
            // 
            lblSummary.AutoSize = true;
            lblSummary.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSummary.Location = new Point(20, 55);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(0, 25);
            lblSummary.TabIndex = 1;
            // 
            // dgvReview
            // 
            dgvReview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReview.Location = new Point(20, 95);
            dgvReview.Name = "dgvReview";
            dgvReview.RowHeadersWidth = 51;
            dgvReview.Size = new Size(1040, 450);
            dgvReview.TabIndex = 2;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(20, 570);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 45);
            btnBack.TabIndex = 3;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // AttemptReviewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 633);
            Controls.Add(btnBack);
            Controls.Add(dgvReview);
            Controls.Add(lblSummary);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AttemptReviewForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quiz result";
            ((System.ComponentModel.ISupportInitialize)dgvReview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblSummary;
        private DataGridView dgvReview;
        private Button btnBack;
    }
}