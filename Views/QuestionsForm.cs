using System.Data;
using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class QuestionsForm : Form
{
    private readonly QuestionController _controller;
    private readonly int _quizId;
    private int _selectedId = 0;
    private string _currentType = QuestionTypes.SingleChoice;

    private readonly TextBox[] _optionBoxes;
    private readonly RadioButton[] _correctRadios;

    public QuestionsForm(int quizId, string quizTitle)
    {
        InitializeComponent();
        _quizId = quizId;
        _controller = new QuestionController(
            new QuestionRepository(DbConfig.ConnectionString),
            new QuizRepository(DbConfig.ConnectionString));

        _optionBoxes = new[] { txtOption1, txtOption2, txtOption3, txtOption4 };
        _correctRadios = new[] { rbCorrect1, rbCorrect2, rbCorrect3, rbCorrect4 };

        lblQuiz.Text = "Quiz: " + quizTitle;

        // grid settings
        dgvQuestions.ReadOnly = true;
        dgvQuestions.AllowUserToAddRows = false;
        dgvQuestions.AllowUserToDeleteRows = false;
        dgvQuestions.MultiSelect = false;
        dgvQuestions.RowHeadersVisible = false;
        dgvQuestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvQuestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // events (wired here, nothing is double-clicked in the Designer)
        Load += (s, e) => { LoadQuestions(); ClearForm(); };
        dgvQuestions.CellClick += dgvQuestions_CellClick;
        rbSingle.CheckedChanged += TypeChanged;
        rbTrueFalse.CheckedChanged += TypeChanged;
        rbShort.CheckedChanged += TypeChanged;
        btnSave.Click += btnSave_Click;
        btnUpdate.Click += btnUpdate_Click;
        btnDelete.Click += btnDelete_Click;
        btnClear.Click += (s, e) => ClearForm();
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

    // ---------- loading ----------

    private void LoadQuestions()
    {
        TryRun(() =>
        {
            DataTable table = _controller.GetQuestions(_quizId);
            dgvQuestions.DataSource = table;
            dgvQuestions.ClearSelection();

            int total = 0;
            foreach (DataRow row in table.Rows)
                total += Convert.ToInt32(row["Marks"]);

            lblTotal.Text = "Questions: " + table.Rows.Count + "      Total marks: " + total;
        }, "Questions");
    }

    // ---------- question type handling ----------

    private string GetSelectedType() =>
        rbTrueFalse.Checked ? QuestionTypes.TrueFalse :
        rbShort.Checked ? QuestionTypes.ShortAnswer : QuestionTypes.SingleChoice;

    private void SetSelectedType(string type)
    {
        if (type == QuestionTypes.TrueFalse) rbTrueFalse.Checked = true;
        else if (type == QuestionTypes.ShortAnswer) rbShort.Checked = true;
        else rbSingle.Checked = true;
        ApplyType();
    }

    private void TypeChanged(object? sender, EventArgs e)
    {
        // react only to the radio that has just been ticked
        if (sender is RadioButton radio && radio.Checked)
            ApplyType();
    }

    // Shows or locks the option boxes depending on the question type
    private void ApplyType()
    {
        string type = GetSelectedType();
        bool isTrueFalse = type == QuestionTypes.TrueFalse;

        grpOptions.Enabled = type != QuestionTypes.ShortAnswer;

        txtOption1.ReadOnly = isTrueFalse;
        txtOption2.ReadOnly = isTrueFalse;
        txtOption3.Enabled = !isTrueFalse;
        txtOption4.Enabled = !isTrueFalse;
        rbCorrect3.Enabled = !isTrueFalse;
        rbCorrect4.Enabled = !isTrueFalse;

        if (isTrueFalse)
        {
            txtOption1.Text = "True";
            txtOption2.Text = "False";
            txtOption3.Clear();
            txtOption4.Clear();
            if (rbCorrect3.Checked || rbCorrect4.Checked)
                rbCorrect1.Checked = true;
        }
        else if (_currentType == QuestionTypes.TrueFalse)
        {
            txtOption1.Clear();          // leaving True/False: start with empty options
            txtOption2.Clear();
        }

        _currentType = type;
    }

    // ---------- options <-> form ----------

    private List<AnswerOption> BuildOptions()
    {
        var list = new List<AnswerOption>();
        if (rbShort.Checked)
            return list;

        for (int i = 0; i < 4; i++)
        {
            list.Add(new AnswerOption
            {
                OptionText = _optionBoxes[i].Text,
                IsCorrect = _correctRadios[i].Checked
            });
        }
        return list;
    }

    private void FillOptions(List<AnswerOption> options)
    {
        for (int i = 0; i < 4; i++)
        {
            _optionBoxes[i].Text = i < options.Count ? options[i].OptionText : "";
            _correctRadios[i].Checked = i < options.Count && options[i].IsCorrect;
        }

        if (!_correctRadios.Any(r => r.Checked))
            rbCorrect1.Checked = true;
    }

    // ---------- events ----------

    private void dgvQuestions_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvQuestions.Rows[e.RowIndex];

        _selectedId = Convert.ToInt32(row.Cells["QuestionID"].Value);
        txtQuestion.Text = Convert.ToString(row.Cells["QuestionText"].Value);
        nudMarks.Value = Math.Clamp(Convert.ToInt32(row.Cells["Marks"].Value),
                                    (int)nudMarks.Minimum, (int)nudMarks.Maximum);

        SetSelectedType(Convert.ToString(row.Cells["QuestionType"].Value) ?? "");

        var options = new List<AnswerOption>();
        TryRun(() => options = _controller.GetOptions(_selectedId), "Question");
        FillOptions(options);
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.AddQuestion(_quizId, txtQuestion.Text, GetSelectedType(),
                                                 (int)nudMarks.Value, BuildOptions()),
                   "Add question"))
        {
            MessageBox.Show("Question added.", "Questions");
            LoadQuestions();
            ClearForm();
        }
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (TryRun(() => _controller.EditQuestion(_selectedId, txtQuestion.Text, GetSelectedType(),
                                                  (int)nudMarks.Value, BuildOptions()),
                   "Update question"))
        {
            MessageBox.Show("Question updated.", "Questions");
            LoadQuestions();
            ClearForm();
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedId <= 0)
        {
            MessageBox.Show("Select a question first.", "Delete question");
            return;
        }

        if (MessageBox.Show("Delete this question?", "Confirm deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        if (TryRun(() => _controller.RemoveQuestion(_selectedId), "Delete question"))
        {
            MessageBox.Show("Question deleted.", "Questions");
            LoadQuestions();
            ClearForm();
        }
    }

    private void ClearForm()
    {
        _selectedId = 0;
        txtQuestion.Clear();
        nudMarks.Value = 1;

        rbSingle.Checked = true;
        ApplyType();

        foreach (TextBox box in _optionBoxes)
            box.Clear();
        rbCorrect1.Checked = true;

        txtQuestion.Focus();
        dgvQuestions.ClearSelection();
    }
}