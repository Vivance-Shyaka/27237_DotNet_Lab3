using MySqlConnector;
using WLAMS.Controllers;
using WLAMS.Models;

namespace WLAMS.Views;

public partial class QuizTakeForm : Form
{
    private readonly AttemptController _controller;
    private readonly QuizSession _session;
    private readonly Dictionary<int, AnswerInput> _answers = new();
    private readonly RadioButton[] _radios;
    private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

    private int _index = 0;
    private int _secondsLeft = 0;
    private bool _finished = false;
    private bool _loading = false;          // true while the code fills the controls

    // > 0 when the quiz was submitted: the caller then opens the review
    public int SubmittedAttemptId { get; private set; } = 0;

    public QuizTakeForm(QuizSession session)
    {
        InitializeComponent();
        _session = session;
        _controller = AttemptController.Create();
        _radios = new[] { rbOpt1, rbOpt2, rbOpt3, rbOpt4 };

        lblQuizTitle.Text = session.Title;
        Text = "Quiz: " + session.Title;

        // the short-answer box takes the place of the option panel
        txtAnswer.Location = pnlChoices.Location;
        txtAnswer.Size = pnlChoices.Size;
        txtAnswer.Font = new Font(Font.FontFamily, 12F);

        foreach (RadioButton radio in _radios)
        {
            radio.UseMnemonic = false;                   // show '&' as a normal character
            radio.CheckedChanged += OptionChanged;
        }
        txtAnswer.TextChanged += AnswerTextChanged;

        btnPrev.Click += (s, e) => ShowQuestion(_index - 1);
        btnNext.Click += (s, e) => ShowQuestion(_index + 1);
        btnSubmit.Click += btnSubmit_Click;
        FormClosing += QuizTakeForm_FormClosing;
        FormClosed += (s, e) => _timer.Dispose();

        Load += (s, e) =>
        {
            ShowQuestion(0);
            StartTimer();
        };
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

    // ---------- timer ----------

    private void StartTimer()
    {
        if (_session.TimeLimitMinutes <= 0)
        {
            lblTimer.Text = "No time limit";
            return;
        }

        _secondsLeft = _session.TimeLimitMinutes * 60;
        UpdateTimerLabel();
        _timer.Interval = 1000;
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _secondsLeft--;
        UpdateTimerLabel();

        if (_secondsLeft <= 0)
        {
            _timer.Stop();
            MessageBox.Show("Time is up! Your answers will now be submitted.", "Time is up",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            SubmitNow();
        }
    }

    private void UpdateTimerLabel()
    {
        int seconds = Math.Max(_secondsLeft, 0);
        lblTimer.Text = "Time left: " + (seconds / 60).ToString("00") + ":" + (seconds % 60).ToString("00");
        lblTimer.ForeColor = seconds <= 60 ? Color.Firebrick : Color.Black;
    }

    // ---------- showing a question ----------

    private void ShowQuestion(int index)
    {
        if (index < 0 || index >= _session.Questions.Count)
            return;

        _loading = true;
        _index = index;

        QuizQuestion question = _session.Questions[index];
        bool isShort = question.QuestionType == QuestionTypes.ShortAnswer;

        lblQuestionNo.Text = "Question " + (index + 1) + " of " + _session.Questions.Count +
                             "   (" + question.Marks + (question.Marks == 1 ? " mark" : " marks") + ")";
        txtQuestion.Text = question.QuestionText;

        pnlChoices.Visible = !isShort;
        txtAnswer.Visible = isShort;

        _answers.TryGetValue(question.QuestionID, out AnswerInput? saved);

        if (isShort)
        {
            txtAnswer.Text = saved?.Text ?? "";
        }
        else
        {
            for (int i = 0; i < _radios.Length; i++)
            {
                if (i < question.Options.Count)
                {
                    QuizOption option = question.Options[i];
                    _radios[i].Visible = true;
                    _radios[i].Text = option.OptionText;
                    _radios[i].Tag = option.OptionID;
                    _radios[i].Checked = saved?.OptionId == option.OptionID;
                }
                else
                {
                    _radios[i].Visible = false;
                    _radios[i].Checked = false;
                    _radios[i].Tag = null;
                }
            }
        }

        btnPrev.Enabled = index > 0;
        btnNext.Enabled = index < _session.Questions.Count - 1;
        UpdateAnsweredLabel();
        _loading = false;
    }

    // ---------- remembering the answers (in memory until the quiz is submitted) ----------

    private void OptionChanged(object? sender, EventArgs e)
    {
        if (_loading) return;

        if (sender is RadioButton radio && radio.Checked && radio.Tag is int optionId)
        {
            int questionId = _session.Questions[_index].QuestionID;
            _answers[questionId] = new AnswerInput { OptionId = optionId };
            UpdateAnsweredLabel();
        }
    }

    private void AnswerTextChanged(object? sender, EventArgs e)
    {
        if (_loading) return;

        int questionId = _session.Questions[_index].QuestionID;
        _answers[questionId] = new AnswerInput { Text = txtAnswer.Text };
        UpdateAnsweredLabel();
    }

    private int CountAnswered() =>
        _answers.Values.Count(a => a.OptionId != null || !string.IsNullOrWhiteSpace(a.Text));

    private void UpdateAnsweredLabel()
    {
        lblAnswered.Text = "Answered: " + CountAnswered() + " of " + _session.Questions.Count;
    }

    // ---------- submitting ----------

    private void btnSubmit_Click(object? sender, EventArgs e)
    {
        int unanswered = _session.Questions.Count - CountAnswered();
        string message = unanswered > 0
            ? "You have " + unanswered + " unanswered question(s).\nSubmit anyway?"
            : "Submit your answers now?";

        if (MessageBox.Show(message, "Submit quiz", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        SubmitNow();
    }

    private void SubmitNow()
    {
        _timer.Stop();

        AttemptResult? result = null;
        if (!TryRun(() => result = _controller.SubmitQuiz(_session.AttemptID, _answers), "Submit quiz")
            || result == null)
        {
            // the submission failed: let the learner try again while there is time left
            if (_session.TimeLimitMinutes > 0 && _secondsLeft > 0)
                _timer.Start();
            return;
        }

        _finished = true;
        SubmittedAttemptId = result.AttemptID;

        string message = result.Status == "PendingGrading"
            ? "Your quiz has been submitted.\nScore so far: " + result.Score + " / " + result.TotalMarks +
              "\nSome answers must be graded by your instructor."
            : "Score: " + result.Score + " / " + result.TotalMarks + " (" + result.Percent + "%)\n" +
              "Result: " + result.ResultText.ToUpper() + "  (pass mark " + _session.PassMark + "%)";

        MessageBox.Show(message, "Quiz submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Close();
    }

    // Closing the window without submitting: the attempt counts and scores 0
    private void QuizTakeForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_finished) return;

        if (MessageBox.Show("Leave the quiz now?\nThis attempt will still count and will score 0.",
                "Leave quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
        {
            e.Cancel = true;
            return;
        }

        _timer.Stop();
        TryRun(() => _controller.AbandonAttempt(_session.AttemptID), "Leave quiz");
        _finished = true;
    }
}