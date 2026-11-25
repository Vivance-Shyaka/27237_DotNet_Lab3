using System.Data;
using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;

namespace WLAMS.Views;

public partial class GradebookForm : Form
{
    private readonly ReportController _controller;
    private DataTable? _current;

    public GradebookForm()
    {
        InitializeComponent();
        _controller = ReportController.Create();

        dgvGradebook.ReadOnly = true;
        dgvGradebook.AllowUserToAddRows = false;
        dgvGradebook.AllowUserToDeleteRows = false;
        dgvGradebook.MultiSelect = false;
        dgvGradebook.RowHeadersVisible = false;
        dgvGradebook.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGradebook.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        Load += (s, e) => LoadCourses();
        btnShow.Click += (s, e) => ShowGradebook();
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

    private void LoadCourses()
    {
        TryRun(() =>
        {
            cboCourse.DisplayMember = "Title";          // set these BEFORE the DataSource
            cboCourse.ValueMember = "CourseID";
            cboCourse.DataSource = _controller.GetMyCourses();
        }, "Courses");
    }

    private void ShowGradebook()
    {
        int courseId = cboCourse.SelectedValue == null ? 0 : Convert.ToInt32(cboCourse.SelectedValue);

        TryRun(() =>
        {
            _current = _controller.GetGradebook(courseId);
            dgvGradebook.DataSource = _current;
            dgvGradebook.ClearSelection();

            if (_current.Rows.Count == 0)
                MessageBox.Show("No learner is enrolled in this course yet.", "Gradebook");
        }, "Gradebook");
    }

    private void btnExport_Click(object? sender, EventArgs e)
    {
        if (_current == null || _current.Rows.Count == 0)
        {
            MessageBox.Show("Show a gradebook with learners first.", "Export");
            return;
        }

        using (var dialog = new SaveFileDialog { Filter = "CSV file (*.csv)|*.csv", FileName = "gradebook.csv" })
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