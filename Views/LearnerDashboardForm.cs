using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;

namespace WLAMS.Views;

public partial class LearnerDashboardForm : Form
{
    private readonly LearnerController _controller;

    public LearnerDashboardForm()
    {
        InitializeComponent();
        _controller = LearnerController.Create();

        lblWelcome.Text = "Welcome, " + Session.CurrentUser?.FullName;
        RefreshSummary();

        btnBrowse.Click += (s, e) => OpenChild(new CatalogueForm());
        btnMyCourses.Click += (s, e) => OpenChild(new MyCoursesForm());
        btnResults.Click += (s, e) => OpenChild(new MyResultsForm());
        btnLogout.Click += (s, e) => Close();
    }

    private void OpenChild(Form form)
    {
        Hide();
        form.ShowDialog();
        form.Dispose();
        RefreshSummary();
        Show();
    }

    private void RefreshSummary()
    {
        try
        {
            lblSummary.Text = "You are enrolled in " + _controller.GetMyCourses().Rows.Count +
                              " course(s).";
        }
        catch (MySqlException ex)
        {
            lblSummary.Text = "Database error: " + ex.Message;
        }
    }
}