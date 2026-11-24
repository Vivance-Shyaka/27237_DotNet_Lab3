using MySqlConnector;
using WLAMS.Controllers;

namespace WLAMS.Views;

public partial class MyCoursesForm : Form
{
    private readonly LearnerController _controller;
    private int _selectedId = 0;
    private string _selectedTitle = "";

    public MyCoursesForm()
    {
        InitializeComponent();
        _controller = LearnerController.Create();

        // grid settings
        dgvCourses.ReadOnly = true;
        dgvCourses.AllowUserToAddRows = false;
        dgvCourses.AllowUserToDeleteRows = false;
        dgvCourses.MultiSelect = false;
        dgvCourses.RowHeadersVisible = false;
        dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => LoadCourses();
        dgvCourses.CellClick += dgvCourses_CellClick;
        dgvCourses.CellDoubleClick += (s, e) =>
        {
            if (e.RowIndex >= 0)
            {
                dgvCourses_CellClick(s, e);
                OpenCourse();
            }
        };
        btnOpen.Click += (s, e) => OpenCourse();
        btnUnenroll.Click += btnUnenroll_Click;
        btnSearch.Click += (s, e) => LoadCourses(txtSearch.Text);
        btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadCourses(); };
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

    private void LoadCourses(string keyword = "")
    {
        TryRun(() =>
        {
            dgvCourses.DataSource = _controller.GetMyCourses(keyword);
            dgvCourses.ClearSelection();

            if (dgvCourses.Columns.Contains("CourseID"))
                dgvCourses.Columns["CourseID"].Visible = false;
        }, "My courses");

        _selectedId = 0;
        _selectedTitle = "";
    }

    private void dgvCourses_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvCourses.Rows[e.RowIndex];

        _selectedId = Convert.ToInt32(row.Cells["CourseID"].Value);
        _selectedTitle = Convert.ToString(row.Cells["Title"].Value) ?? "";
    }

    private void OpenCourse()
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a course first.", "Open course");
            return;
        }

        using (var form = new CourseStudyForm(_selectedId, _selectedTitle))
            form.ShowDialog();

        LoadCourses(txtSearch.Text);          // refresh the progress column
    }

    private void btnUnenroll_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a course first.", "Unenroll");
            return;
        }

        if (MessageBox.Show("Unenroll from this course?\nYour progress is kept if you enroll again.",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        if (TryRun(() => _controller.Unenroll(_selectedId), "Unenroll"))
        {
            MessageBox.Show("You have been unenrolled.", "My courses");
            LoadCourses(txtSearch.Text);
        }
    }
}