using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class GradingController
{
    private readonly GradingRepository _grading;

    public GradingController(GradingRepository grading)
    {
        _grading = grading;
    }

    public static GradingController Create() =>
        new GradingController(new GradingRepository(DbConfig.ConnectionString));

    private static int CurrentUserId =>
        Session.CurrentUser?.UserID ?? throw new InvalidOperationException("No user is logged in.");

    public int CountPendingAttempts() => _grading.CountPendingAttempts(CurrentUserId);

    public DataTable GetPendingAttempts()
    {
        DataTable table = _grading.GetPendingAttempts(CurrentUserId);

        table.Columns.Add("ScoreSoFar", typeof(string));
        foreach (DataRow row in table.Rows)
            row["ScoreSoFar"] = Convert.ToInt32(row["Score"]) + " / " + Convert.ToInt32(row["TotalMarks"]);

        return table;
    }

    public DataTable GetPendingAnswers(int attemptId)
    {
        if (attemptId <= 0)
            throw new ArgumentException("Select an attempt first.");

        int? owner = _grading.GetAttemptOwnerId(attemptId);
        if (owner == null)
            throw new ArgumentException("Attempt not found.");
        if (owner != CurrentUserId)
            throw new ArgumentException("You can only grade the answers of your own quizzes.");

        return _grading.GetPendingAnswers(attemptId);
    }

    // Returns true when this was the last pending answer and the attempt is now final
    public bool GradeAnswer(int answerId, int marks)
    {
        if (answerId <= 0)
            throw new ArgumentException("Select an answer first.");

        AnswerInfo info = _grading.GetAnswerInfo(answerId)
                          ?? throw new ArgumentException("Answer not found.");

        if (info.InstructorID != CurrentUserId)
            throw new ArgumentException("You can only grade the answers of your own quizzes.");
        if (info.AttemptStatus != "PendingGrading" || info.MarksAwarded != null)
            throw new ArgumentException("This answer has already been graded.");
        if (marks < 0 || marks > info.QuestionMarks)
            throw new ArgumentException("The mark must be between 0 and " + info.QuestionMarks + ".");

        // the pass rule: score as a percentage of the total reaches the pass mark
        int passMark = info.PassMark;
        bool saved = _grading.SaveGrade(answerId, marks,
            (score, total) => total > 0 && score * 100 >= passMark * total,
            out bool attemptCompleted);

        if (!saved)
            throw new ArgumentException("This answer has already been graded.");

        return attemptCompleted;
    }
}