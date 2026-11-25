using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class CourseQuizzesForm : Form
{
    private readonly AttemptController _controller;
    private readonly int _courseId;
    private int _selectedId = 0;
    private string _selectedTitle = "";
    private int _selectedTime = 0;

    public CourseQuizzesForm(int courseId, string courseTitle)
    {
        InitializeComponent();
        _courseId = courseId;
        _controller = AttemptController.Create();

        lblCourse.Text = "Quizzes: " + courseTitle;

        // grid settings
        dgvQuizzes.ReadOnly = true;
        dgvQuizzes.AllowUserToAddRows = false;
        dgvQuizzes.AllowUserToDeleteRows = false;
        dgvQuizzes.MultiSelect = false;
        dgvQuizzes.RowHeadersVisible = false;
        dgvQuizzes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvQuizzes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => LoadQuizzes();
        dgvQuizzes.CellClick += dgvQuizzes_CellClick;
        btnStart.Click += btnStart_Click;
        btnBack.Click += (s, e) => Close();
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

    private void LoadQuizzes()
    {
        TryRun(() =>
        {
            dgvQuizzes.DataSource = _controller.GetCourseQuizzes(_courseId);
            dgvQuizzes.ClearSelection();

            // only used internally
            foreach (string name in new[] { "QuizID", "Instructions", "MaxAttempts",
                                            "AttemptsUsed", "PassedCount", "PendingCount" })
                if (dgvQuizzes.Columns.Contains(name))
                    dgvQuizzes.Columns[name].Visible = false;
        }, "Quizzes");

        _selectedId = 0;
        txtInstructions.Clear();
    }

    private void dgvQuizzes_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvQuizzes.Rows[e.RowIndex];

        _selectedId = Convert.ToInt32(row.Cells["QuizID"].Value);
        _selectedTitle = Convert.ToString(row.Cells["Title"].Value) ?? "";
        _selectedTime = Convert.ToInt32(row.Cells["TimeLimitMinutes"].Value);
        txtInstructions.Text = Convert.ToString(row.Cells["Instructions"].Value);
    }

    private void btnStart_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a quiz first.", "Start quiz");
            return;
        }

        string timeText = _selectedTime == 0 ? "no time limit" : _selectedTime + " minute(s)";
        if (MessageBox.Show("Start \"" + _selectedTitle + "\"?\n\nTime limit: " + timeText +
                            "\nThis counts as one of your attempts.",
                "Start quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        QuizSession? session = null;
        if (!TryRun(() => session = _controller.StartQuiz(_selectedId), "Start quiz") || session == null)
        {
            LoadQuizzes();
            return;
        }

        int attemptId;
        using (var form = new QuizTakeForm(session))
        {
            form.ShowDialog();
            attemptId = form.SubmittedAttemptId;
        }

        LoadQuizzes();

        if (attemptId > 0)                   // submitted: show the review
        {
            using (var review = new AttemptReviewForm(attemptId))
                review.ShowDialog();
        }
    }
}