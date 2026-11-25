using System.Data;
using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class QuizStatsForm : Form
{
    private readonly ReportController _controller;
    private DataTable? _current;

    public QuizStatsForm()
    {
        InitializeComponent();
        _controller = ReportController.Create();

        dgvQuestions.ReadOnly = true;
        dgvQuestions.AllowUserToAddRows = false;
        dgvQuestions.AllowUserToDeleteRows = false;
        dgvQuestions.MultiSelect = false;
        dgvQuestions.RowHeadersVisible = false;
        dgvQuestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvQuestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvQuestions.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        dgvQuestions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        Load += (s, e) => LoadQuizzes();
        btnShow.Click += (s, e) => ShowStatistics();
        btnExport.Click += btnExport_Click;

        btnBack.DialogResult = DialogResult.Cancel;
        CancelButton = btnBack;
        btnBack.BringToFront();
    }

    private bool TryRun(Action action, string title)
    {
        try
        {
            action();
            return true;
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Database error: " + ex.Message, title,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Unexpected error: " + ex.Message, title,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return false;
    }

    private static string Pct(double? value) =>
        value == null ? "-" : Math.Round(value.Value) + "%";

    private void LoadQuizzes()
    {
        TryRun(() =>
        {
            cboQuiz.DisplayMember = "Display";          // set these BEFORE the DataSource
            cboQuiz.ValueMember = "QuizID";
            cboQuiz.DataSource = _controller.GetMyQuizzes();
        }, "Quizzes");
    }

    private void ShowStatistics()
    {
        int quizId = cboQuiz.SelectedValue == null ? 0 : Convert.ToInt32(cboQuiz.SelectedValue);

        TryRun(() =>
        {
            QuizStatistics stats = _controller.GetQuizStatistics(quizId);

            lblStats.Text =
                "Attempts: " + stats.Attempts +
                (stats.Pending > 0 ? " (" + stats.Pending + " waiting for grading)" : "") +
                "      Learners: " + stats.Learners +
                "      Average: " + Pct(stats.AveragePercent) +
                "      Pass rate: " + (stats.PassRatePercent == null ? "-" : stats.PassRatePercent + "%") +
                "      Highest: " + Pct(stats.HighestPercent) +
                "      Lowest: " + Pct(stats.LowestPercent);

            _current = stats.Questions;
            dgvQuestions.DataSource = _current;
            dgvQuestions.ClearSelection();

            if (dgvQuestions.Columns.Contains("Question")) dgvQuestions.Columns["Question"].FillWeight = 250;
            if (dgvQuestions.Columns.Contains("Correct")) dgvQuestions.Columns["Correct"].HeaderText = "Correct answers";
        }, "Statistics");
    }

    private void btnExport_Click(object? sender, EventArgs e)
    {
        if (_current == null || _current.Rows.Count == 0)
        {
            MessageBox.Show("Show the statistics of a quiz first.", "Export");
            return;
        }

        using (var dialog = new SaveFileDialog { Filter = "CSV file (*.csv)|*.csv", FileName = "quiz-statistics.csv" })
        {
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                CsvExporter.Save(_current, dialog.FileName);
                MessageBox.Show("Saved to " + dialog.FileName, "Export");
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("The file could not be saved: " + ex.Message, "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}