using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class CoursesForm : Form
{
    private readonly CourseController _controller;
    private int _selectedId = 0;
    private string _selectedTitle = "";

    public CoursesForm()
    {
        InitializeComponent();
        _controller = new CourseController(
            new CourseRepository(DbConfig.ConnectionString),
            new CategoryRepository(DbConfig.ConnectionString));

        // grid settings
        dgvCourses.ReadOnly = true;
        dgvCourses.AllowUserToAddRows = false;
        dgvCourses.AllowUserToDeleteRows = false;
        dgvCourses.MultiSelect = false;
        dgvCourses.RowHeadersVisible = false;
        dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => { LoadCategories(); LoadCourses(); };
        dgvCourses.CellClick += dgvCourses_CellClick;
        btnSave.Click += btnSave_Click;
        btnUpdate.Click += btnUpdate_Click;
        btnDelete.Click += btnDelete_Click;
        btnPublish.Click += (s, e) => ChangePublished(true);
        btnUnpublish.Click += (s, e) => ChangePublished(false);
        btnLessons.Click += btnLessons_Click;
        btnClear.Click += (s, e) => ClearForm();
        btnSearch.Click += (s, e) => LoadCourses(txtSearch.Text);
        btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadCourses(); ClearForm(); };
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

    private void LoadCategories()
    {
        TryRun(() =>
        {
            cboCategory.DisplayMember = "CategoryName";     // set these BEFORE the DataSource
            cboCategory.ValueMember = "CategoryID";
            cboCategory.DataSource = _controller.GetCategories();
        }, "Categories");
    }

    private void LoadCourses(string keyword = "")
    {
        TryRun(() =>
        {
            dgvCourses.DataSource = _controller.GetMyCourses(keyword);
            dgvCourses.ClearSelection();

            // these two columns are only used to fill the form on the left
            foreach (string name in new[] { "Description", "CategoryID" })
                if (dgvCourses.Columns.Contains(name))
                    dgvCourses.Columns[name].Visible = false;
        }, "Courses");
    }

    private int GetSelectedCategoryId() =>
        cboCategory.SelectedValue == null ? 0 : Convert.ToInt32(cboCategory.SelectedValue);

    private string GetSelectedLevel() =>
        rbAdvanced.Checked ? CourseLevels.Advanced :
        rbIntermediate.Checked ? CourseLevels.Intermediate : CourseLevels.Beginner;

    private void SetLevel(string level)
    {
        rbAdvanced.Checked = level == CourseLevels.Advanced;
        rbIntermediate.Checked = level == CourseLevels.Intermediate;
        rbBeginner.Checked = level != CourseLevels.Advanced && level != CourseLevels.Intermediate;
    }

    private void dgvCourses_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvCourses.Rows[e.RowIndex];

        _selectedId = Convert.ToInt32(row.Cells["CourseID"].Value);
        _selectedTitle = Convert.ToString(row.Cells["Title"].Value) ?? "";

        txtTitle.Text = _selectedTitle;
        txtDescription.Text = Convert.ToString(row.Cells["Description"].Value);
        cboCategory.SelectedValue = Convert.ToInt32(row.Cells["CategoryID"].Value);
        SetLevel(Convert.ToString(row.Cells["CourseLevel"].Value) ?? "");

        bool published = Convert.ToBoolean(row.Cells["IsPublished"].Value);
        lblStatus.Text = "Status: " + (published ? "Published" : "Draft");
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.AddCourse(txtTitle.Text, txtDescription.Text,
                                               GetSelectedCategoryId(), GetSelectedLevel()),
                   "Add course"))
        {
            MessageBox.Show("Course added as a draft. Add lessons, then publish it.", "Courses");
            LoadCourses();
            ClearForm();
        }
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.EditCourse(_selectedId, txtTitle.Text, txtDescription.Text,
                                                GetSelectedCategoryId(), GetSelectedLevel()),
                   "Update course"))
        {
            MessageBox.Show("Course updated.", "Courses");
            LoadCourses();
            ClearForm();
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a course first.", "Delete course");
            return;
        }

        if (MessageBox.Show("Delete this course with all its lessons and quizzes?",
                "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        if (TryRun(() => _controller.RemoveCourse(_selectedId), "Delete course"))
        {
            MessageBox.Show("Course deleted.", "Courses");
            LoadCourses();
            ClearForm();
        }
    }

    private void ChangePublished(bool publish)
    {
        string title = publish ? "Publish course" : "Unpublish course";
        if (TryRun(() => _controller.SetPublished(_selectedId, publish), title))
        {
            MessageBox.Show(publish ? "Course published. Learners can now see it."
                                    : "Course unpublished.", "Courses");
            LoadCourses();
            ClearForm();
        }
    }

    private void btnLessons_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a course first.", "Lessons");
            return;
        }

        using (var form = new LessonsForm(_selectedId, _selectedTitle))
            form.ShowDialog();

        LoadCourses(txtSearch.Text);      // refresh the lesson counts
    }

    private void ClearForm()
    {
        _selectedId = 0;
        _selectedTitle = "";
        txtTitle.Clear();
        txtDescription.Clear();
        SetLevel(CourseLevels.Beginner);
        if (cboCategory.Items.Count > 0)
            cboCategory.SelectedIndex = 0;
        lblStatus.Text = "";
        txtTitle.Focus();
        dgvCourses.ClearSelection();
    }
}