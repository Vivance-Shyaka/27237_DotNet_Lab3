using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class RegisterForm : Form
{
    private readonly AuthController _auth;

    // LoginForm reads this after a successful registration
    public string RegisteredUsername { get; private set; } = "";

    public RegisterForm()
    {
        InitializeComponent();
        _auth = new AuthController(new UserRepository(DbConfig.ConnectionString));
       
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            string role = rbInstructor.Checked ? Roles.Instructor : Roles.Learner;

            bool needsApproval = _auth.Register(txtFullName.Text, txtEmail.Text, txtUsername.Text,
                                                txtPassword.Text, txtConfirmPassword.Text,
                                                role);

            if (needsApproval)
                MessageBox.Show(
                    "Your instructor account was created and is waiting for administrator approval.\n" +
                    "You will be able to log in once it is approved.",
                    "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Account created! You can now log in.", "Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            RegisteredUsername = txtUsername.Text.Trim();
            DialogResult = DialogResult.OK;        // closes this form
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Register", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Database error: " + ex.Message, "Register",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}