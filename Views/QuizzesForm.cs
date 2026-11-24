using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class QuizzesForm : Form
{
    private readonly QuizController _controller;
    private int _selectedId = 0;
    private string _selectedTitle = "";

    public QuizzesForm()
    {
        InitializeComponent();
        _controller = new QuizController(
            new QuizRepository(DbConfig.ConnectionString),
            new CourseRepository(DbConfig.ConnectionString));

        // grid settings
        dgvQuizzes.ReadOnly = true;
        dgvQuizzes.AllowUserToAddRows = false;
        dgvQuizzes.AllowUserToDeleteRows = false;
        dgvQuizzes.MultiSelect = false;
        dgvQuizzes.RowHeadersVisible = false;
        dgvQuizzes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvQuizzes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => { LoadCourses(); LoadQuizzes(); };
        dgvQuizzes.CellClick += dgvQuizzes_CellClick;
        btnSave.Click += btnSave_Click;
        btnUpdate.Click += btnUpdate_Click;
        btnDelete.Click += btnDelete_Click;
        btnPublish.Click += (s, e) => ChangePublished(true);
        btnUnpublish.Click += (s, e) => ChangePublished(false);
        btnQuestions.Click += btnQuestions_Click;
        btnClear.Click += (s, e) => ClearForm();
        btnSearch.Click += (s, e) => LoadQuizzes(txtSearch.Text);
        btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadQuizzes(); ClearForm(); };
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
        return false;
    }

    private void LoadCourses()
    {
        TryRun(() =>
        {
            cboCourse.DisplayMember = "Title";          // set these BEFORE the DataSource
            cboCourse.ValueMember = "CourseID";
            cboCourse.DataSource = _controller.GetMyCourses();
        }, "Courses");
    }

    private void LoadQuizzes(string keyword = "")
    {
        TryRun(() =>
        {
            dgvQuizzes.DataSource = _controller.GetMyQuizzes(keyword);
            dgvQuizzes.ClearSelection();

            // only used to fill the form on the left
            foreach (string name in new[] { "Instructions", "CourseID" })
                if (dgvQuizzes.Columns.Contains(name))
                    dgvQuizzes.Columns[name].Visible = false;
        }, "Quizzes");
    }

    private int GetSelectedCourseId() =>
        cboCourse.SelectedValue == null ? 0 : Convert.ToInt32(cboCourse.SelectedValue);

    private static decimal Clamp(NumericUpDown box, int value) =>
        Math.Clamp(value, (int)box.Minimum, (int)box.Maximum);

    private void dgvQuizzes_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvQuizzes.Rows[e.RowIndex];

        _selectedId = Convert.ToInt32(row.Cells["QuizID"].Value);
        _selectedTitle = Convert.ToString(row.Cells["Title"].Value) ?? "";

        txtTitle.Text = _selectedTitle;
        txtInstructions.Text = Convert.ToString(row.Cells["Instructions"].Value);
        cboCourse.SelectedValue = Convert.ToInt32(row.Cells["CourseID"].Value);
        cboCourse.Enabled = false;                     // the course of a quiz cannot be changed

        nudTime.Value = Clamp(nudTime, Convert.ToInt32(row.Cells["TimeLimitMinutes"].Value));
        nudPass.Value = Clamp(nudPass, Convert.ToInt32(row.Cells["PassMark"].Value));
        nudAttempts.Value = Clamp(nudAttempts, Convert.ToInt32(row.Cells["MaxAttempts"].Value));

        bool published = Convert.ToBoolean(row.Cells["IsPublished"].Value);
        lblStatus.Text = "Status: " + (published ? "Published" : "Draft");
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.AddQuiz(GetSelectedCourseId(), txtTitle.Text,
                                             txtInstructions.Text, (int)nudTime.Value,
                                             (int)nudPass.Value, (int)nudAttempts.Value),
                   "Add quiz"))
        {
            MessageBox.Show("Quiz added as a draft. Add questions, then publish it.", "Quizzes");
            LoadQuizzes();
            ClearForm();
        }
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.EditQuiz(_selectedId, txtTitle.Text, txtInstructions.Text,
                                              (int)nudTime.Value, (int)nudPass.Value,
                                              (int)nudAttempts.Value),
                   "Update quiz"))
        {
            MessageBox.Show("Quiz updated.", "Quizzes");
            LoadQuizzes();
            ClearForm();
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a quiz first.", "Delete quiz");
            return;
        }

        if (MessageBox.Show("Delete this quiz with all its questions?", "Confirm deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        if (TryRun(() => _controller.RemoveQuiz(_selectedId), "Delete quiz"))
        {
            MessageBox.Show("Quiz deleted.", "Quizzes");
            LoadQuizzes();
            ClearForm();
        }
    }

    private void ChangePublished(bool publish)
    {
        string title = publish ? "Publish quiz" : "Unpublish quiz";
        if (TryRun(() => _controller.SetPublished(_selectedId, publish), title))
        {
            MessageBox.Show(publish ? "Quiz published. Enrolled learners can now take it."
                                    : "Quiz unpublished.", "Quizzes");
            LoadQuizzes();
            ClearForm();
        }
    }

    private void btnQuestions_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a quiz first.", "Questions");
            return;
        }

        using (var form = new QuestionsForm(_selectedId, _selectedTitle))
            form.ShowDialog();

        LoadQuizzes(txtSearch.Text);        // refresh the question counts and total marks
    }

    private void ClearForm()
    {
        _selectedId = 0;
        _selectedTitle = "";
        txtTitle.Clear();
        txtInstructions.Clear();
        nudTime.Value = 1;
        nudPass.Value = 50;
        nudAttempts.Value = 3;
        cboCourse.Enabled = true;
        if (cboCourse.Items.Count > 0)
            cboCourse.SelectedIndex = 0;
        lblStatus.Text = "";
        txtTitle.Focus();
        dgvQuizzes.ClearSelection();
    }
}