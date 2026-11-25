using System.Data;
using MySqlConnector;
using WLAMS.Controllers;

namespace WLAMS.Views;

public partial class CourseStudyForm : Form
{
    private readonly LearnerController _controller;
    private readonly int _courseId;
    private int _selectedLessonId = 0;
    private bool _selectedCompleted = false;

    public CourseStudyForm(int courseId, string courseTitle)
    {
        InitializeComponent();
        _courseId = courseId;
        _controller = LearnerController.Create();

        lblCourse.Text = courseTitle;
        Text = "Study: " + courseTitle;

        // grid settings
        dgvLessons.ReadOnly = true;
        dgvLessons.AllowUserToAddRows = false;
        dgvLessons.AllowUserToDeleteRows = false;
        dgvLessons.MultiSelect = false;
        dgvLessons.RowHeadersVisible = false;
        dgvLessons.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLessons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => LoadLessons(0, true);
        dgvLessons.CellClick += (s, e) => { if (e.RowIndex >= 0) ShowLesson(e.RowIndex); };
        btnToggle.Click += btnToggle_Click;
        btnNext.Click += btnNext_Click;

        // ----- Back button: closes the window, also with the Esc key -----
        btnBack.DialogResult = DialogResult.Cancel;
        CancelButton = btnBack;
        btnBack.BringToFront();

        // ----- Quizzes button (remove this block if you have no btnQuizzes) -----
        btnQuizzes.Click += (s, e) =>
        {
            using (var form = new CourseQuizzesForm(_courseId, lblCourse.Text))
                form.ShowDialog();
        };
        // ------------------------------------------------------------------------

        // make sure the window fits on the screen
        Rectangle area = Screen.FromControl(this).WorkingArea;
        if (Height > area.Height)
            Height = area.Height - 20;
        AutoScroll = true;
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

    // keepLessonId > 0: stay on that lesson. Otherwise, optionally jump to the first unfinished lesson.
    private void LoadLessons(int keepLessonId, bool pickFirstIncomplete)
    {
        DataTable? table = null;
        if (!TryRun(() => table = _controller.GetStudyLessons(_courseId), "Lessons") || table == null)
        {
            ShowNoLesson("The lessons could not be loaded.");
            return;
        }

        dgvLessons.DataSource = table;

        // only used internally
        foreach (string name in new[] { "LessonID", "Content" })
            if (dgvLessons.Columns.Contains(name))
                dgvLessons.Columns[name].Visible = false;

        UpdateProgress();

        if (table.Rows.Count == 0)
        {
            ShowNoLesson("This course has no lessons yet.");
            return;
        }

        int index = 0;
        if (keepLessonId > 0)
        {
            for (int i = 0; i < table.Rows.Count; i++)
                if (Convert.ToInt32(table.Rows[i]["LessonID"]) == keepLessonId)
                    index = i;
        }
        else if (pickFirstIncomplete)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Convert.ToString(table.Rows[i]["Completed"]) == "No")
                {
                    index = i;
                    break;
                }
            }
        }

        SelectRow(index);
    }

    private void ShowNoLesson(string message)
    {
        _selectedLessonId = 0;
        lblLessonTitle.Text = message;
        txtContent.Clear();
        btnToggle.Enabled = false;
        btnNext.Enabled = false;
    }

    private void SelectRow(int index)
    {
        dgvLessons.ClearSelection();
        dgvLessons.Rows[index].Selected = true;
        dgvLessons.CurrentCell = dgvLessons.Rows[index].Cells["Title"];
        ShowLesson(index);
    }

    private void ShowLesson(int index)
    {
        DataGridViewRow row = dgvLessons.Rows[index];

        _selectedLessonId = Convert.ToInt32(row.Cells["LessonID"].Value);
        _selectedCompleted = Convert.ToString(row.Cells["Completed"].Value) == "Yes";

        lblLessonTitle.Text = "Lesson " + row.Cells["LessonOrder"].Value + ": " + row.Cells["Title"].Value;
        txtContent.Text = Convert.ToString(row.Cells["Content"].Value);

        btnToggle.Enabled = true;
        btnToggle.Text = _selectedCompleted ? "Mark as not complete" : "Mark as complete";
        btnNext.Enabled = index < dgvLessons.Rows.Count - 1;
    }

    private void UpdateProgress()
    {
        TryRun(() =>
        {
            var progress = _controller.GetProgress(_courseId);
            lblProgress.Text = "Progress: " + progress.Completed + " of " + progress.Total +
                               " lessons (" + progress.Percent + "%)";
            pbProgress.Value = Math.Clamp(progress.Percent, 0, 100);
        }, "Progress");
    }

    private void btnToggle_Click(object? sender, EventArgs e)
    {
        bool markComplete = !_selectedCompleted;
        int lessonId = _selectedLessonId;

        if (!TryRun(() => _controller.SetLessonCompleted(lessonId, markComplete), "Lesson"))
            return;

        LoadLessons(lessonId, false);       // refresh the list and the progress, stay on this lesson

        if (markComplete)
        {
            int percent = 0;
            TryRun(() => percent = _controller.GetProgress(_courseId).Percent, "Progress");
            if (percent == 100)
                MessageBox.Show("Congratulations! You have completed all the lessons of this course.",
                    "Course completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnNext_Click(object? sender, EventArgs e)
    {
        int index = dgvLessons.CurrentRow?.Index ?? 0;
        if (index + 1 < dgvLessons.Rows.Count)
            SelectRow(index + 1);
    }
}