using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class UsersForm : Form
{
    private readonly UserController _controller;
    private int _selectedId = 0;

    public UsersForm()
    {
        InitializeComponent();
        _controller = new UserController(new UserRepository(DbConfig.ConnectionString));

        // grid settings
        dgvUsers.ReadOnly = true;
        dgvUsers.AllowUserToAddRows = false;
        dgvUsers.AllowUserToDeleteRows = false;
        dgvUsers.MultiSelect = false;
        dgvUsers.RowHeadersVisible = false;
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, so nothing is double-clicked in the Designer)
        Load += (s, e) => LoadUsers();
        dgvUsers.CellClick += dgvUsers_CellClick;
        btnSave.Click += btnSave_Click;
        btnUpdate.Click += btnUpdate_Click;
        btnDelete.Click += btnDelete_Click;
        btnApprove.Click += btnApprove_Click;
        btnReject.Click += btnReject_Click;
        btnClear.Click += (s, e) => ClearForm();
        btnSearch.Click += (s, e) => LoadUsers(txtSearch.Text);
        btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadUsers(); ClearForm(); };
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

    private void LoadUsers(string keyword = "")
    {
        TryRun(() =>
        {
            dgvUsers.DataSource = _controller.GetUsers(keyword);
            dgvUsers.ClearSelection();
        }, "Users");
    }

    private string GetSelectedRole() =>
        rbAdmin.Checked ? Roles.Admin :
        rbInstructor.Checked ? Roles.Instructor : Roles.Learner;

    private void SetRole(string role)
    {
        rbAdmin.Checked = role == Roles.Admin;
        rbInstructor.Checked = role == Roles.Instructor;
        rbLearner.Checked = role == Roles.Learner;
    }

    private void dgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

        _selectedId = Convert.ToInt32(row.Cells["UserID"].Value);
        txtFullName.Text = Convert.ToString(row.Cells["FullName"].Value);
        txtEmail.Text = Convert.ToString(row.Cells["Email"].Value);
        txtUsername.Text = Convert.ToString(row.Cells["Username"].Value);
        txtPassword.Clear();
        SetRole(Convert.ToString(row.Cells["Role"].Value) ?? Roles.Learner);
        chkActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
        lblStatus.Text = "Approval status: " + Convert.ToString(row.Cells["ApprovalStatus"].Value);
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.AddUser(txtFullName.Text, txtEmail.Text, txtUsername.Text,
                                             txtPassword.Text, GetSelectedRole()), "Add user"))
        {
            MessageBox.Show("User added.", "Users");
            LoadUsers();
            ClearForm();
        }
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.EditUser(_selectedId, txtFullName.Text, txtEmail.Text,
                                              txtUsername.Text, txtPassword.Text,
                                              GetSelectedRole(), chkActive.Checked), "Update user"))
        {
            MessageBox.Show("User updated.", "Users");
            LoadUsers();
            ClearForm();
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a user first.", "Delete user");
            return;
        }

        if (MessageBox.Show("Delete this user and all of their enrolments and quiz results?",
                "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        if (TryRun(() => _controller.RemoveUser(_selectedId), "Delete user"))
        {
            MessageBox.Show("User deleted.", "Users");
            LoadUsers();
            ClearForm();
        }
    }

    private void btnApprove_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.Approve(_selectedId), "Approve instructor"))
        {
            MessageBox.Show("Instructor approved. They can now log in.", "Users");
            LoadUsers();
            ClearForm();
        }
    }

    private void btnReject_Click(object? sender, EventArgs e)
    {
        if (MessageBox.Show("Reject this instructor request?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        if (TryRun(() => _controller.Reject(_selectedId), "Reject instructor"))
        {
            MessageBox.Show("Request rejected.", "Users");
            LoadUsers();
            ClearForm();
        }
    }

    private void ClearForm()
    {
        _selectedId = 0;
        txtFullName.Clear();
        txtEmail.Clear();
        txtUsername.Clear();
        txtPassword.Clear();
        SetRole(Roles.Learner);
        chkActive.Checked = true;
        lblStatus.Text = "";
        txtFullName.Focus();
        dgvUsers.ClearSelection();
    }
}