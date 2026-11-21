using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class LessonsForm : Form
{
    private readonly LessonController _controller;
    private readonly int _courseId;
    private int _selectedId = 0;

    public LessonsForm(int courseId, string courseTitle)
    {
        InitializeComponent();
        _courseId = courseId;
        _controller = new LessonController(
            new LessonRepository(DbConfig.ConnectionString),
            new CourseRepository(DbConfig.ConnectionString));

        lblCourse.Text = "Course: " + courseTitle;

        // grid settings
        dgvLessons.ReadOnly = true;
        dgvLessons.AllowUserToAddRows = false;
        dgvLessons.AllowUserToDeleteRows = false;
        dgvLessons.MultiSelect = false;
        dgvLessons.RowHeadersVisible = false;
        dgvLessons.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLessons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => { LoadLessons(); ClearForm(); };
        dgvLessons.CellClick += dgvLessons_CellClick;
        btnSave.Click += btnSave_Click;
        btnUpdate.Click += btnUpdate_Click;
        btnDelete.Click += btnDelete_Click;
        btnClear.Click += (s, e) => ClearForm();
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

    private void LoadLessons()
    {
        TryRun(() =>
        {
            dgvLessons.DataSource = _controller.GetLessons(_courseId);
            dgvLessons.ClearSelection();

            if (dgvLessons.Columns.Contains("Content"))    // shown in the box on the left instead
                dgvLessons.Columns["Content"].Visible = false;
        }, "Lessons");
    }

    private void dgvLessons_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvLessons.Rows[e.RowIndex];

        _selectedId = Convert.ToInt32(row.Cells["LessonID"].Value);
        txtLessonTitle.Text = Convert.ToString(row.Cells["Title"].Value);
        txtContent.Text = Convert.ToString(row.Cells["Content"].Value);

        int order = Convert.ToInt32(row.Cells["LessonOrder"].Value);
        nudOrder.Value = Math.Clamp(order, (int)nudOrder.Minimum, (int)nudOrder.Maximum);
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.AddLesson(_courseId, txtLessonTitle.Text,
                                               txtContent.Text, (int)nudOrder.Value),
                   "Add lesson"))
        {
            MessageBox.Show("Lesson added.", "Lessons");
            LoadLessons();
            ClearForm();
        }
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.EditLesson(_selectedId, txtLessonTitle.Text,
                                                txtContent.Text, (int)nudOrder.Value),
                   "Update lesson"))
        {
            MessageBox.Show("Lesson updated.", "Lessons");
            LoadLessons();
            ClearForm();
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a lesson first.", "Delete lesson");
            return;
        }

        if (MessageBox.Show("Delete this lesson?", "Confirm deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        if (TryRun(() => _controller.RemoveLesson(_selectedId), "Delete lesson"))
        {
            MessageBox.Show("Lesson deleted.", "Lessons");
            LoadLessons();
            ClearForm();
        }
    }

    private void ClearForm()
    {
        _selectedId = 0;
        txtLessonTitle.Clear();
        txtContent.Clear();

        // suggest the next free lesson number
        TryRun(() =>
        {
            int next = _controller.NextOrder(_courseId);
            nudOrder.Value = Math.Clamp(next, (int)nudOrder.Minimum, (int)nudOrder.Maximum);
        }, "Lessons");

        txtLessonTitle.Focus();
        dgvLessons.ClearSelection();
    }
}