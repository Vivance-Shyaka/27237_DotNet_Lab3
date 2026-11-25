using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class AttemptController
{
    private readonly AttemptRepository _attempts;
    private readonly QuizRepository _quizzes;
    private readonly CourseRepository _courses;
    private readonly EnrollmentRepository _enrollments;

    public AttemptController(AttemptRepository attempts, QuizRepository quizzes,
                             CourseRepository courses, EnrollmentRepository enrollments)
    {
        _attempts = attempts;
        _quizzes = quizzes;
        _courses = courses;
        _enrollments = enrollments;
    }

    public static AttemptController Create() => new AttemptController(
        new AttemptRepository(DbConfig.ConnectionString),
        new QuizRepository(DbConfig.ConnectionString),
        new CourseRepository(DbConfig.ConnectionString),
        new EnrollmentRepository(DbConfig.ConnectionString));

    private static int CurrentUserId =>
        Session.CurrentUser?.UserID ?? throw new InvalidOperationException("No user is logged in.");

    private static int ToInt(object value) => Convert.ToInt32(value);

    // ---------- the quizzes of a course (with Attempts and Status columns) ----------

    public DataTable GetCourseQuizzes(int courseId)
    {
        EnsureCanStudy(courseId);

        DataTable table = _attempts.GetCourseQuizzes(CurrentUserId, courseId);
        table.Columns.Add("Attempts", typeof(string));
        table.Columns.Add("Status", typeof(string));

        foreach (DataRow row in table.Rows)
        {
            int used = ToInt(row["AttemptsUsed"]);
            int max = ToInt(row["MaxAttempts"]);

            row["Attempts"] = used + " / " + (max == 0 ? "unlimited" : max.ToString());

            if (ToInt(row["PassedCount"]) > 0) row["Status"] = "Passed";
            else if (ToInt(row["PendingCount"]) > 0) row["Status"] = "Pending grading";
            else if (used > 0) row["Status"] = "Not passed";
            else row["Status"] = "Not started";
        }
        return table;
    }

    // ---------- starting a quiz ----------

    public QuizSession StartQuiz(int quizId)
    {
        if (Session.CurrentUser?.Role != Roles.Learner)
            throw new ArgumentException("Only learners can take quizzes.");
        if (quizId <= 0)
            throw new ArgumentException("Select a quiz first.");

        Quiz quiz = _quizzes.GetById(quizId) ?? throw new ArgumentException("Quiz not found.");
        if (!quiz.IsPublished)
            throw new ArgumentException("This quiz is not available.");
        EnsureCanStudy(quiz.CourseID);

        List<QuizQuestion> questions = _attempts.GetQuestionsForTaking(quizId);
        if (questions.Count == 0)
            throw new ArgumentException("This quiz has no questions yet.");

        // an attempt left "in progress" (app closed) is closed with a score of 0
        _attempts.CloseStaleAttempts(CurrentUserId, quizId);

        int used = _attempts.CountAttempts(CurrentUserId, quizId);
        if (quiz.MaxAttempts > 0 && used >= quiz.MaxAttempts)
            throw new ArgumentException(
                "You have used all " + quiz.MaxAttempts + " attempt(s) of this quiz.");

        int totalMarks = questions.Sum(q => q.Marks);
        int attemptId = _attempts.StartAttempt(CurrentUserId, quizId, totalMarks);

        return new QuizSession
        {
            AttemptID = attemptId,
            QuizID = quizId,
            Title = quiz.Title,
            Instructions = quiz.Instructions,
            TimeLimitMinutes = quiz.TimeLimitMinutes,
            PassMark = quiz.PassMark,
            Questions = questions
        };
    }

    // ---------- submitting: this is where the grading happens ----------

    public AttemptResult SubmitQuiz(int attemptId, Dictionary<int, AnswerInput> answers)
    {
        QuizAttempt attempt = GetOwnedAttempt(attemptId);
        if (attempt.Status != "InProgress")
            throw new ArgumentException("This attempt has already been submitted.");

        Quiz quiz = _quizzes.GetById(attempt.QuizID) ?? throw new ArgumentException("Quiz not found.");
        List<GradingKey> keys = _attempts.GetGradingKey(attempt.QuizID);

        var rows = new List<AttemptAnswerRow>();
        int score = 0;
        int total = 0;
        bool pending = false;

        foreach (GradingKey key in keys)
        {
            total += key.Marks;
            answers.TryGetValue(key.QuestionID, out AnswerInput? given);

            var row = new AttemptAnswerRow { QuestionID = key.QuestionID };

            if (key.QuestionType == QuestionTypes.ShortAnswer)
            {
                string text = (given?.Text ?? "").Trim();
                if (text.Length > 2000)
                    text = text.Substring(0, 2000);

                if (text.Length == 0)
                {
                    row.MarksAwarded = 0;               // left blank: nothing to grade
                }
                else
                {
                    row.AnswerText = text;
                    row.MarksAwarded = null;            // the instructor will grade it
                    pending = true;
                }
            }
            else
            {
                int? chosen = given?.OptionId;
                if (chosen != null && key.OptionIds.Contains(chosen.Value))
                {
                    row.SelectedOptionID = chosen;
                    row.MarksAwarded = chosen == key.CorrectOptionId ? key.Marks : 0;
                    score += row.MarksAwarded.Value;
                }
                else
                {
                    row.MarksAwarded = 0;               // not answered
                }
            }

            rows.Add(row);
        }

        string status = pending ? "PendingGrading" : "Graded";
        bool passed = !pending && total > 0 && score * 100 >= quiz.PassMark * total;

        if (!_attempts.SubmitAttempt(attemptId, CurrentUserId, score, total, passed, status, rows))
            throw new ArgumentException("This attempt has already been submitted.");

        return GetAttemptResult(attemptId);
    }

    // The learner left without submitting
    public void AbandonAttempt(int attemptId)
    {
        GetOwnedAttempt(attemptId);
        _attempts.AbandonAttempt(attemptId, CurrentUserId);
    }

    // ---------- results ----------

    public AttemptResult GetAttemptResult(int attemptId)
    {
        QuizAttempt attempt = GetOwnedAttempt(attemptId);
        Quiz? quiz = _quizzes.GetById(attempt.QuizID);

        return new AttemptResult
        {
            AttemptID = attempt.AttemptID,
            QuizTitle = quiz?.Title ?? "",
            Score = attempt.Score,
            TotalMarks = attempt.TotalMarks,
            IsPassed = attempt.IsPassed,
            Status = attempt.Status
        };
    }

    // The attempt history of the logged-in learner
    public DataTable GetMyResults()
    {
        DataTable raw = _attempts.GetResults(CurrentUserId);

        var table = new DataTable();
        table.Columns.Add("AttemptID", typeof(int));
        table.Columns.Add("Course", typeof(string));
        table.Columns.Add("Quiz", typeof(string));
        table.Columns.Add("Started", typeof(string));
        table.Columns.Add("Score", typeof(string));
        table.Columns.Add("Percent", typeof(string));
        table.Columns.Add("Result", typeof(string));

        foreach (DataRow r in raw.Rows)
        {
            int score = ToInt(r["Score"]);
            int total = ToInt(r["TotalMarks"]);
            string status = Convert.ToString(r["Status"]) ?? "";
            bool passed = Convert.ToBoolean(r["IsPassed"]);

            string percent = total == 0 ? "0%" : Math.Round(100.0 * score / total) + "%";
            string result = status == "PendingGrading" ? "Pending grading" : passed ? "Passed" : "Failed";

            table.Rows.Add(
                ToInt(r["AttemptID"]),
                Convert.ToString(r["Course"]),
                Convert.ToString(r["Quiz"]),
                Convert.ToDateTime(r["StartedAt"]).ToString("yyyy-MM-dd HH:mm"),
                score + " / " + total,
                percent,
                result);
        }
        return table;
    }

    // The review table: your answer, the correct answer and the marks, per question
    public DataTable GetReview(int attemptId)
    {
        QuizAttempt attempt = GetOwnedAttempt(attemptId);
        if (attempt.Status == "InProgress")
            throw new ArgumentException("You can review an attempt only after submitting it.");

        DataTable raw = _attempts.GetReview(attemptId, CurrentUserId);

        var table = new DataTable();
        table.Columns.Add("No", typeof(int));
        table.Columns.Add("Question", typeof(string));
        table.Columns.Add("YourAnswer", typeof(string));
        table.Columns.Add("CorrectAnswer", typeof(string));
        table.Columns.Add("Marks", typeof(string));

        int number = 0;
        foreach (DataRow r in raw.Rows)
        {
            number++;
            string type = Convert.ToString(r["QuestionType"]) ?? "";
            int available = ToInt(r["Marks"]);
            bool isShort = type == QuestionTypes.ShortAnswer;

            string yourAnswer;
            if (isShort)
            {
                string text = r["AnswerText"] == DBNull.Value ? "" : Convert.ToString(r["AnswerText"]) ?? "";
                yourAnswer = text.Length == 0 ? "(no answer)" : text;
            }
            else
            {
                yourAnswer = r["YourOption"] == DBNull.Value ? "(no answer)" : Convert.ToString(r["YourOption"]) ?? "";
            }

            string correct = isShort
                ? "Graded by the instructor"
                : (r["CorrectOption"] == DBNull.Value ? "" : Convert.ToString(r["CorrectOption"]) ?? "");

            string marks;
            if (r["MarksAwarded"] != DBNull.Value)
                marks = ToInt(r["MarksAwarded"]) + " / " + available;
            else if (isShort && yourAnswer != "(no answer)")
                marks = "Pending (max " + available + ")";
            else
                marks = "0 / " + available;

            table.Rows.Add(number, Convert.ToString(r["QuestionText"]), yourAnswer, correct, marks);
        }
        return table;
    }

    // ---------- helpers ----------

    private QuizAttempt GetOwnedAttempt(int attemptId)
    {
        QuizAttempt attempt = _attempts.GetAttempt(attemptId)
                              ?? throw new ArgumentException("Attempt not found.");
        if (attempt.UserID != CurrentUserId)
            throw new ArgumentException("This attempt does not belong to you.");
        return attempt;
    }

    // The course must be published and the learner enrolled
    private void EnsureCanStudy(int courseId)
    {
        Course course = _courses.GetById(courseId) ?? throw new ArgumentException("Course not found.");
        if (!course.IsPublished)
            throw new ArgumentException("This course is not available.");
        if (!_enrollments.IsEnrolled(CurrentUserId, courseId))
            throw new ArgumentException("You are not enrolled in this course.");
    }
}