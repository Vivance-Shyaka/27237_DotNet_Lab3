namespace WLAMS.Models;

public class QuizAttempt
{
    public int AttemptID { get; set; }
    public int QuizID { get; set; }
    public int UserID { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public int Score { get; set; }
    public int TotalMarks { get; set; }
    public bool IsPassed { get; set; }
    public string Status { get; set; } = "InProgress";     // InProgress / PendingGrading / Graded
}

// What the learner sees while taking a quiz: NO correct answers here
public class QuizOption
{
    public int OptionID { get; set; }
    public string OptionText { get; set; } = "";
}

public class QuizQuestion
{
    public int QuestionID { get; set; }
    public string QuestionText { get; set; } = "";
    public string QuestionType { get; set; } = QuestionTypes.SingleChoice;
    public int Marks { get; set; } = 1;
    public List<QuizOption> Options { get; set; } = new();
}

public class QuizSession
{
    public int AttemptID { get; set; }
    public int QuizID { get; set; }
    public string Title { get; set; } = "";
    public string Instructions { get; set; } = "";
    public int TimeLimitMinutes { get; set; }
    public int PassMark { get; set; }
    public List<QuizQuestion> Questions { get; set; } = new();
}

// One answer given by the learner (kept in memory until the quiz is submitted)
public class AnswerInput
{
    public int? OptionId { get; set; }
    public string Text { get; set; } = "";
}

// Used only by the grading code (contains the correct answer)
public class GradingKey
{
    public int QuestionID { get; set; }
    public string QuestionType { get; set; } = "";
    public int Marks { get; set; }
    public List<int> OptionIds { get; set; } = new();
    public int? CorrectOptionId { get; set; }
}

// One answer row to save in the database
public class AttemptAnswerRow
{
    public int QuestionID { get; set; }
    public int? SelectedOptionID { get; set; }
    public string? AnswerText { get; set; }
    public int? MarksAwarded { get; set; }          // null = waiting for the instructor
}

public class AttemptResult
{
    public int AttemptID { get; set; }
    public string QuizTitle { get; set; } = "";
    public int Score { get; set; }
    public int TotalMarks { get; set; }
    public bool IsPassed { get; set; }
    public string Status { get; set; } = "";

    public int Percent => TotalMarks == 0 ? 0 : (int)Math.Round(100.0 * Score / TotalMarks);

    public string ResultText =>
        Status == "PendingGrading" ? "Pending grading" :
        IsPassed ? "Passed" : "Failed";
}