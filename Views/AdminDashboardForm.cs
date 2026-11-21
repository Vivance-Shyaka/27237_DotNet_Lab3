using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class AdminDashboardForm : Form
{
    private readonly UserController _users;

    public AdminDashboardForm()
    {
        InitializeComponent();
        _users = new UserController(new UserRepository(DbConfig.ConnectionString));

        lblWelcome.Text = "Welcome, " + Session.CurrentUser?.FullName;
        RefreshPending();

        btnCategories.Click += (s, e) => OpenChild(new CategoriesForm());
        btnUsers.Click += (s, e) => OpenChild(new UsersForm());
        btnLogout.Click += (s, e) => Close();
    }

    private void OpenChild(Form form)
    {
        Hide();
        form.ShowDialog();
        form.Dispose();
        RefreshPending();
        Show();
    }

    private void RefreshPending()
    {
        try
        {
            lblPending.Text = "Pending instructor requests: " + _users.CountPending();
        }
        catch (MySqlException ex)
        {
            lblPending.Text = "Database error: " + ex.Message;
        }
    }
}