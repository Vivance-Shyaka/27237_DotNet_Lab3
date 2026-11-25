using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class AttemptReviewForm : Form
{
    private readonly AttemptController _controller;
    private readonly int _attemptId;

    public AttemptReviewForm(int attemptId)
    {
        InitializeComponent();
        _attemptId = attemptId;
        _controller = AttemptController.Create();

        // grid settings (long texts wrap onto several lines)
        dgvReview.ReadOnly = true;
        dgvReview.AllowUserToAddRows = false;
        dgvReview.AllowUserToDeleteRows = false;
        dgvReview.MultiSelect = false;
        dgvReview.RowHeadersVisible = false;
        dgvReview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvReview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvReview.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        dgvReview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        Load += (s, e) => LoadReview();

        // ----- Back button: closes the window, also with the Esc key -----
        btnBack.DialogResult = DialogResult.Cancel;
        CancelButton = btnBack;
        btnBack.BringToFront();

        // make sure the window fits on the screen
        Rectangle area = Screen.FromControl(this).WorkingArea;
        if (Height > area.Height)
            Height = area.Height - 20;
        AutoScroll = true;
    }

    private void LoadReview()
    {
        try
        {
            AttemptResult result = _controller.GetAttemptResult(_attemptId);
            lblTitle.Text = "Result: " + result.QuizTitle;
            lblSummary.Text = "Score: " + result.Score + " / " + result.TotalMarks +
                              " (" + result.Percent + "%)        Result: " + result.ResultText;

            dgvReview.DataSource = _controller.GetReview(_attemptId);
            dgvReview.ClearSelection();

            // column widths: a small "No", wide text columns
            if (dgvReview.Columns.Contains("No")) dgvReview.Columns["No"].FillWeight = 25;
            if (dgvReview.Columns.Contains("Question")) dgvReview.Columns["Question"].FillWeight = 200;
            if (dgvReview.Columns.Contains("YourAnswer")) dgvReview.Columns["YourAnswer"].FillWeight = 120;
            if (dgvReview.Columns.Contains("CorrectAnswer")) dgvReview.Columns["CorrectAnswer"].FillWeight = 120;
            if (dgvReview.Columns.Contains("Marks")) dgvReview.Columns["Marks"].FillWeight = 50;
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Review", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Database error: " + ex.Message, "Review",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Unexpected error: " + ex.Message, "Review",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}