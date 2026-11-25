using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;

namespace WLAMS.Views;

public partial class InstructorDashboardForm : Form
{
    private readonly GradingController _grading;

    public InstructorDashboardForm()
    {
        InitializeComponent();
        _grading = GradingController.Create();

        lblWelcome.Text = "Welcome, " + Session.CurrentUser?.FullName;
        RefreshSummary();

        btnCourses.Click += (s, e) => OpenChild(new CoursesForm());
        btnQuizzes.Click += (s, e) => OpenChild(new QuizzesForm());
        btnGrading.Click += (s, e) => OpenChild(new GradingForm());
        btnGradebook.Click += (s, e) => OpenChild(new GradebookForm());
        btnStats.Click += (s, e) => OpenChild(new QuizStatsForm());
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
            lblSummary.Text = "Attempts waiting for grading: " + _grading.CountPendingAttempts();
        }
        catch (MySqlException ex)
        {
            lblSummary.Text = "Database error: " + ex.Message;
        }
    }
}