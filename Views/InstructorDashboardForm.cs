using WLAMS.Helpers;

namespace WLAMS.Views;

public partial class InstructorDashboardForm : Form
{
    public InstructorDashboardForm()
    {
        InitializeComponent();
        lblWelcome.Text = "Welcome, " + Session.CurrentUser?.FullName;

        btnCourses.Click += (s, e) => OpenChild(new CoursesForm());
        btnLogout.Click += (s, e) => Close();
    }

    private void OpenChild(Form form)
    {
        Hide();
        form.ShowDialog();
        form.Dispose();
        Show();
    }
}