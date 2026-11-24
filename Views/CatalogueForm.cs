using MySqlConnector;
using WLAMS.Controllers;

namespace WLAMS.Views;

public partial class CatalogueForm : Form
{
    private readonly LearnerController _controller;
    private int _selectedId = 0;

    public CatalogueForm()
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
        Load += (s, e) => LoadCatalogue();
        dgvCourses.CellClick += dgvCourses_CellClick;
        btnEnroll.Click += btnEnroll_Click;
        btnSearch.Click += (s, e) => LoadCatalogue(txtSearch.Text);
        //btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadCatalogue(); };
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

    private void LoadCatalogue(string keyword = "")
    {
        TryRun(() =>
        {
            dgvCourses.DataSource = _controller.GetCatalogue(keyword);
            dgvCourses.ClearSelection();

            // only used internally
            foreach (string name in new[] { "CourseID", "Description" })
                if (dgvCourses.Columns.Contains(name))
                    dgvCourses.Columns[name].Visible = false;
        }, "Courses");

        _selectedId = 0;
        txtDescription.Clear();
    }

    private void dgvCourses_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvCourses.Rows[e.RowIndex];

        _selectedId = Convert.ToInt32(row.Cells["CourseID"].Value);
        txtDescription.Text = Convert.ToString(row.Cells["Description"].Value);
    }

    private void btnEnroll_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.Enroll(_selectedId), "Enroll"))
        {
            MessageBox.Show("You are now enrolled. Find the course in \"My courses\".", "Enroll",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCatalogue(txtSearch.Text);      // the Enrolled column now says Yes
        }
    }
}