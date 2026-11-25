using MySqlConnector;
using WLAMS.Controllers;

namespace WLAMS.Views;

public partial class GradingForm : Form
{
    private readonly GradingController _controller;
    private int _attemptId = 0;
    private int _answerId = 0;

    public GradingForm()
    {
        InitializeComponent();
        _controller = GradingController.Create();

        SetupGrid(dgvAttempts);
        SetupGrid(dgvAnswers);

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => LoadAttempts(0);
        dgvAttempts.CellClick += dgvAttempts_CellClick;
        dgvAnswers.CellClick += dgvAnswers_CellClick;
        btnSave.Click += btnSave_Click;

        btnBack.DialogResult = DialogResult.Cancel;
        CancelButton = btnBack;
        btnBack.BringToFront();
    }

    private static void SetupGrid(DataGridView grid)
    {
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.MultiSelect = false;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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

    // keepAttemptId > 0: select that attempt again after reloading (if it is still waiting)
    private void LoadAttempts(int keepAttemptId)
    {
        TryRun(() =>
        {
            dgvAttempts.DataSource = _controller.GetPendingAttempts();
            dgvAttempts.ClearSelection();

            foreach (string name in new[] { "AttemptID", "Score", "TotalMarks" })
                if (dgvAttempts.Columns.Contains(name))
                    dgvAttempts.Columns[name].Visible = false;

            if (dgvAttempts.Columns.Contains("ScoreSoFar")) dgvAttempts.Columns["ScoreSoFar"].HeaderText = "Score so far";
            if (dgvAttempts.Columns.Contains("PendingAnswers")) dgvAttempts.Columns["PendingAnswers"].HeaderText = "To grade";
            if (dgvAttempts.Columns.Contains("SubmittedAt")) dgvAttempts.Columns["SubmittedAt"].HeaderText = "Submitted";

            lblAttemptsTitle.Text = "Attempts waiting for grading: " + dgvAttempts.Rows.Count;
        }, "Grading");

        _attemptId = 0;
        dgvAnswers.DataSource = null;
        ClearAnswerDetails();

        if (keepAttemptId > 0)
        {
            foreach (DataGridViewRow row in dgvAttempts.Rows)
            {
                if (Convert.ToInt32(row.Cells["AttemptID"].Value) == keepAttemptId)
                {
                    row.Selected = true;
                    _attemptId = keepAttemptId;
                    LoadAnswers();
                    break;
                }
            }
        }
    }

    private void LoadAnswers()
    {
        TryRun(() =>
        {
            dgvAnswers.DataSource = _controller.GetPendingAnswers(_attemptId);
            dgvAnswers.ClearSelection();

            if (dgvAnswers.Columns.Contains("AnswerID"))
                dgvAnswers.Columns["AnswerID"].Visible = false;
            if (dgvAnswers.Columns.Contains("MaxMarks"))
                dgvAnswers.Columns["MaxMarks"].HeaderText = "Max marks";
        }, "Grading");

        ClearAnswerDetails();
    }

    private void ClearAnswerDetails()
    {
        _answerId = 0;
        txtQuestion.Clear();
        txtLearnerAnswer.Clear();
        nudMarks.Value = 0;
        lblMax.Text = "/ 0";
    }

    private void dgvAttempts_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        _attemptId = Convert.ToInt32(dgvAttempts.Rows[e.RowIndex].Cells["AttemptID"].Value);
        LoadAnswers();
    }

    private void dgvAnswers_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvAnswers.Rows[e.RowIndex];

        _answerId = Convert.ToInt32(row.Cells["AnswerID"].Value);
        int max = Convert.ToInt32(row.Cells["MaxMarks"].Value);

        txtQuestion.Text = Convert.ToString(row.Cells["Question"].Value);
        txtLearnerAnswer.Text = Convert.ToString(row.Cells["Answer"].Value);

        nudMarks.Value = 0;
        nudMarks.Maximum = max;
        lblMax.Text = "/ " + max;
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (_answerId <= 0)
        {
            MessageBox.Show("Select a short answer to grade first.", "Grading");
            return;
        }

        bool completed = false;
        if (TryRun(() => completed = _controller.GradeAnswer(_answerId, (int)nudMarks.Value), "Save mark"))
        {
            MessageBox.Show(completed
                ? "Mark saved. All answers of this attempt are graded: the learner can now see the final result."
                : "Mark saved.", "Grading", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadAttempts(_attemptId);
        }
    }
}