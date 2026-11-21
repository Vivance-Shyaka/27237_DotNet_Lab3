using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class CategoriesForm : Form
{
    private readonly CategoryController _controller;
    private int _selectedId = 0;

    public CategoriesForm()
    {
        InitializeComponent();
        _controller = new CategoryController(new CategoryRepository(DbConfig.ConnectionString));

        // grid settings
        dgvCategories.ReadOnly = true;
        dgvCategories.AllowUserToAddRows = false;
        dgvCategories.AllowUserToDeleteRows = false;
        dgvCategories.MultiSelect = false;
        dgvCategories.RowHeadersVisible = false;
        dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, so nothing is double-clicked in the Designer)
        Load += (s, e) => LoadCategories();
        dgvCategories.CellClick += dgvCategories_CellClick;
        btnSave.Click += btnSave_Click;
        btnUpdate.Click += btnUpdate_Click;
        btnDelete.Click += btnDelete_Click;
        btnClear.Click += (s, e) => ClearForm();
        btnSearch.Click += (s, e) => LoadCategories(txtSearch.Text);
        btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadCategories(); ClearForm(); };
        btnBack.Click += (s, e) => Close();
    }

    // Runs an action and shows a friendly message if the controller or database complains
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

    private void LoadCategories(string keyword = "")
    {
        TryRun(() =>
        {
            dgvCategories.DataSource = _controller.GetCategories(keyword);
            dgvCategories.ClearSelection();
        }, "Categories");
    }

    private void dgvCategories_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvCategories.Rows[e.RowIndex];
        _selectedId = Convert.ToInt32(row.Cells["CategoryID"].Value);
        txtCategoryName.Text = Convert.ToString(row.Cells["CategoryName"].Value);
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.Add(txtCategoryName.Text), "Add category"))
        {
            MessageBox.Show("Category added.", "Categories");
            LoadCategories();
            ClearForm();
        }
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.Edit(_selectedId, txtCategoryName.Text), "Update category"))
        {
            MessageBox.Show("Category updated.", "Categories");
            LoadCategories();
            ClearForm();
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a category first.", "Delete category");
            return;
        }

        if (MessageBox.Show("Delete this category?", "Confirm deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        if (TryRun(() => _controller.Remove(_selectedId), "Delete category"))
        {
            MessageBox.Show("Category deleted.", "Categories");
            LoadCategories();
            ClearForm();
        }
    }

    private void ClearForm()
    {
        _selectedId = 0;
        txtCategoryName.Clear();
        txtCategoryName.Focus();
        dgvCategories.ClearSelection();
    }
}