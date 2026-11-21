using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class LoginForm : Form
{
    private readonly AuthController _auth;

    public LoginForm()
    {
        InitializeComponent();
        _auth = new AuthController(new UserRepository(DbConfig.ConnectionString));
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        User user;
        try
        {
            user = _auth.Login(txtUsername.Text, txtPassword.Text);
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Database error: " + ex.Message, "Login",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Session.Login(user);
        OpenDashboard(user);          // returns when the user logs out
        Session.Logout();
        txtPassword.Clear();
    }

    private void OpenDashboard(User user)
    {
        Form? dashboard = null;

        if (user.Role == Roles.Admin)
            dashboard = new AdminDashboardForm();
        else if (user.Role == Roles.Instructor)
            dashboard = new InstructorDashboardForm();

        if (dashboard == null)
        {
            // The Learner dashboard comes in Step 9
            MessageBox.Show("Welcome, " + user.FullName + " (" + user.Role + ")\n" +
                            "The screens for your role are coming in the next steps.",
                "Login successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        Hide();
        using (dashboard)
            dashboard.ShowDialog();
        Show();
    }

    private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
    {
        txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        using var form = new RegisterForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            txtUsername.Text = form.RegisteredUsername;
            txtPassword.Clear();
            txtPassword.Focus();
        }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void LoginForm_Load_1(object sender, EventArgs e)
    {

    }
}