namespace WLAMS.Views
{
    partial class MyResultsForm
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
            dgvResults = new DataGridView();
            btnReview = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            // 
            // dgvResults
            // 
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(20, 20);
            dgvResults.Name = "dgvResults";
            dgvResults.RowHeadersWidth = 51;
            dgvResults.Size = new Size(1060, 470);
            dgvResults.TabIndex = 0;
            // 
            // btnReview
            // 
            btnReview.Location = new Point(20, 510);
            btnReview.Name = "btnReview";
            btnReview.Size = new Size(220, 45);
            btnReview.TabIndex = 1;
            btnReview.Text = "Review answers";
            btnReview.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(260, 510);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(220, 45);
            btnBack.TabIndex = 2;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // MyResultsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 613);
            Controls.Add(btnBack);
            Controls.Add(btnReview);
            Controls.Add(dgvResults);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            Name = "MyResultsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My quiz results";
            Load += MyResultsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvResults;
        private Button btnReview;
        private Button btnBack;
    }
}