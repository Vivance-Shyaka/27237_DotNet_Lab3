using MySqlConnector;
using WLAMS.Controllers;

namespace WLAMS.Views;

public partial class MyResultsForm : Form
{
    private readonly AttemptController _controller;
    private int _selectedAttemptId = 0;

    public MyResultsForm()
    {
        InitializeComponent();
        _controller = AttemptController.Create();

        // grid settings
        dgvResults.ReadOnly = true;
        dgvResults.AllowUserToAddRows = false;
        dgvResults.AllowUserToDeleteRows = false;
        dgvResults.MultiSelect = false;
        dgvResults.RowHeadersVisible = false;
        dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => LoadResults();
        dgvResults.CellClick += dgvResults_CellClick;
        dgvResults.CellDoubleClick += (s, e) =>
        {
            if (e.RowIndex >= 0)
            {
                dgvResults_CellClick(s, e);
                OpenReview();
            }
        };
        btnReview.Click += (s, e) => OpenReview();
        btnBack.Click += (s, e) => Close();
    }

    private void LoadResults()
    {
        try
        {
            dgvResults.DataSource = _controller.GetMyResults();
            dgvResults.ClearSelection();

            if (dgvResults.Columns.Contains("AttemptID"))
                dgvResults.Columns["AttemptID"].Visible = false;
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Database error: " + ex.Message, "Results",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        _selectedAttemptId = 0;
    }

    private void dgvResults_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        _selectedAttemptId = Convert.ToInt32(dgvResults.Rows[e.RowIndex].Cells["AttemptID"].Value);
    }

    private void OpenReview()
    {
        if (_selectedAttemptId <= 0)
        {
            MessageBox.Show("Select an attempt first.", "Review");
            return;
        }

        using (var form = new AttemptReviewForm(_selectedAttemptId))
            form.ShowDialog();
    }

    private void MyResultsForm_Load(object sender, EventArgs e)
    {

    }
}