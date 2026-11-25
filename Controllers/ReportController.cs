using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class ReportController
{
    private readonly ReportRepository _reports;
    private readonly CourseRepository _courses;
    private readonly QuizRepository _quizzes;

    public ReportController(ReportRepository reports, CourseRepository courses, QuizRepository quizzes)
    {
        _reports = reports;
        _courses = courses;
        _quizzes = quizzes;
    }

    public static ReportController Create() => new ReportController(
        new ReportRepository(DbConfig.ConnectionString),
        new CourseRepository(DbConfig.ConnectionString),
        new QuizRepository(DbConfig.ConnectionString));

    private static int CurrentUserId =>
        Session.CurrentUser?.UserID ?? throw new InvalidOperationException("No user is logged in.");

    private static int ToInt(object value) => Convert.ToInt32(value);

    private static double? ToNullableDouble(object value) =>
        value == DBNull.Value ? null : Convert.ToDouble(value);

    // ---------- lists for the drop-downs ----------

    public DataTable GetMyCourses() => _courses.GetByInstructor(CurrentUserId);

    public DataTable GetMyQuizzes()
    {
        DataTable table = _quizzes.GetByInstructor(CurrentUserId);
        table.Columns.Add("Display", typeof(string));
        foreach (DataRow row in table.Rows)
            row["Display"] = Convert.ToString(row["Course"]) + "  -  " + Convert.ToString(row["Title"]);
        return table;
    }

    // ---------- gradebook: one row per learner, one column per quiz ----------

    public DataTable GetGradebook(int courseId)
    {
        EnsureOwnsCourse(courseId);

        DataTable learners = _reports.GetEnrolledLearners(courseId);
        DataTable quizzes = _reports.GetCourseQuizzes(courseId);
        DataTable results = _reports.GetBestResults(courseId);
        DataTable completed = _reports.GetCompletedLessons(courseId);
        int totalLessons = _courses.CountLessons(courseId);

        var table = new DataTable();
        table.Columns.Add("Learner", typeof(string));
        table.Columns.Add("Progress", typeof(string));

        // one column per quiz (a unique column name is needed)
        var quizColumns = new Dictionary<int, string>();
        foreach (DataRow quiz in quizzes.Rows)
        {
            string baseName = Convert.ToString(quiz["Title"]) ?? "Quiz";
            string name = baseName;
            int n = 2;
            while (table.Columns.Contains(name) || name == "Average")
                name = baseName + " (" + n++ + ")";

            table.Columns.Add(name, typeof(string));
            quizColumns[ToInt(quiz["QuizID"])] = name;
        }
        table.Columns.Add("Average", typeof(string));

        foreach (DataRow learner in learners.Rows)
        {
            int userId = ToInt(learner["UserID"]);
            DataRow row = table.NewRow();
            row["Learner"] = Convert.ToString(learner["FullName"]);

            // lesson progress (calculated, never stored)
            DataRow[] done = completed.Select("UserID = " + userId);
            int doneCount = done.Length == 0 ? 0 : ToInt(done[0]["Completed"]);
            int percent = totalLessons == 0 ? 0 : (int)Math.Round(100.0 * doneCount / totalLessons);
            row["Progress"] = doneCount + " / " + totalLessons + " (" + percent + "%)";

            // best result per quiz
            double sum = 0;
            int count = 0;
            foreach (var pair in quizColumns)
            {
                DataRow[] found = results.Select("UserID = " + userId + " AND QuizID = " + pair.Key);

                string text = "-";
                if (found.Length > 0)
                {
                    if (found[0]["BestPercent"] != DBNull.Value)
                    {
                        double best = Convert.ToDouble(found[0]["BestPercent"]);
                        text = Math.Round(best) + "%";
                        sum += best;
                        count++;
                    }
                    else if (ToInt(found[0]["Pending"]) > 0)
                    {
                        text = "Pending";
                    }
                }
                row[pair.Value] = text;
            }

            row["Average"] = count == 0 ? "-" : Math.Round(sum / count) + "%";
            table.Rows.Add(row);
        }

        return table;
    }

    // ---------- statistics of one quiz ----------

    public QuizStatistics GetQuizStatistics(int quizId)
    {
        EnsureOwnsQuiz(quizId);

        DataRow s = _reports.GetQuizSummary(quizId).Rows[0];
        var stats = new QuizStatistics
        {
            Attempts = ToInt(s["Attempts"]),
            Learners = ToInt(s["Learners"]),
            Pending = ToInt(s["Pending"]),
            Graded = ToInt(s["Graded"]),
            Passed = ToInt(s["Passed"]),
            AveragePercent = ToNullableDouble(s["AvgPercent"]),
            HighestPercent = ToNullableDouble(s["Highest"]),
            LowestPercent = ToNullableDouble(s["Lowest"])
        };

        // the success rate of every question
        var items = new List<(string Question, string Type, int Marks, int Answers, double? Rate)>();
        foreach (DataRow r in _reports.GetQuestionStats(quizId).Rows)
        {
            int marks = ToInt(r["Marks"]);
            int answers = ToInt(r["Answers"]);
            int earned = ToInt(r["Earned"]);
            double? rate = (answers == 0 || marks == 0) ? null : 100.0 * earned / (answers * marks);

            items.Add((Convert.ToString(r["QuestionText"]) ?? "",
                       Convert.ToString(r["QuestionType"]) ?? "",
                       marks, answers, rate));
        }

        var table = new DataTable();
        table.Columns.Add("Question", typeof(string));
        table.Columns.Add("Type", typeof(string));
        table.Columns.Add("Marks", typeof(int));
        table.Columns.Add("Answers", typeof(int));
        table.Columns.Add("Correct", typeof(string));

        // hardest first; questions nobody answered yet go last
        foreach (var item in items.OrderBy(x => x.Rate ?? 101.0))
        {
            table.Rows.Add(item.Question, item.Type, item.Marks, item.Answers,
                           item.Rate == null ? "-" : Math.Round(item.Rate.Value) + "%");
        }

        stats.Questions = table;
        return stats;
    }

    // ---------- ownership checks ----------

    private void EnsureOwnsCourse(int courseId)
    {
        if (courseId <= 0)
            throw new ArgumentException("Select a course first.");

        Course course = _courses.GetById(courseId) ?? throw new ArgumentException("Course not found.");
        if (course.InstructorID != CurrentUserId)
            throw new ArgumentException("You can only view reports of your own courses.");
    }

    private void EnsureOwnsQuiz(int quizId)
    {
        if (quizId <= 0)
            throw new ArgumentException("Select a quiz first.");

        int? owner = _quizzes.GetOwnerId(quizId);
        if (owner == null)
            throw new ArgumentException("Quiz not found.");
        if (owner != CurrentUserId)
            throw new ArgumentException("You can only view reports of your own quizzes.");
    }
}