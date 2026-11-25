using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class ReportRepository
{
    private readonly string _connectionString;

    public ReportRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Runs a SELECT with integer parameters and returns the result as a table
    private DataTable Query(string sql, params (string Name, int Value)[] parameters)
    {
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        foreach (var p in parameters)
            cmd.Parameters.Add(p.Name, MySqlDbType.Int32).Value = p.Value;
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    // ---------- gradebook ----------

    public DataTable GetEnrolledLearners(int courseId) =>
        Query(@"SELECT u.UserID, u.FullName
                FROM Enrollments e
                JOIN Users u ON u.UserID = e.UserID
                WHERE e.CourseID = @c
                ORDER BY u.FullName", ("@c", courseId));

    public DataTable GetCourseQuizzes(int courseId) =>
        Query("SELECT QuizID, Title FROM Quizzes WHERE CourseID = @c ORDER BY QuizID",
              ("@c", courseId));

    // The best GRADED percentage of each learner on each quiz (and how many attempts are pending)
    public DataTable GetBestResults(int courseId) =>
        Query(@"SELECT a.UserID, a.QuizID,
                       MAX(CASE WHEN a.Status = 'Graded'
                                THEN a.Score * 100 / NULLIF(a.TotalMarks, 0) END) AS BestPercent,
                       COALESCE(SUM(a.Status = 'PendingGrading'), 0) AS Pending,
                       COUNT(*) AS Attempts
                FROM QuizAttempts a
                JOIN Quizzes q ON q.QuizID = a.QuizID
                WHERE q.CourseID = @c AND a.Status <> 'InProgress'
                GROUP BY a.UserID, a.QuizID", ("@c", courseId));

    public DataTable GetCompletedLessons(int courseId) =>
        Query(@"SELECT p.UserID, COUNT(*) AS Completed
                FROM LessonProgress p
                JOIN Lessons l ON l.LessonID = p.LessonID
                WHERE l.CourseID = @c
                GROUP BY p.UserID", ("@c", courseId));

    // ---------- quiz statistics ----------

    public DataTable GetQuizSummary(int quizId) =>
        Query(@"SELECT COUNT(*) AS Attempts,
                       COUNT(DISTINCT a.UserID) AS Learners,
                       COALESCE(SUM(a.Status = 'PendingGrading'), 0) AS Pending,
                       COALESCE(SUM(a.Status = 'Graded'), 0) AS Graded,
                       COALESCE(SUM(CASE WHEN a.Status = 'Graded' AND a.IsPassed = 1
                                         THEN 1 ELSE 0 END), 0) AS Passed,
                       AVG(CASE WHEN a.Status = 'Graded'
                                THEN a.Score * 100 / NULLIF(a.TotalMarks, 0) END) AS AvgPercent,
                       MAX(CASE WHEN a.Status = 'Graded'
                                THEN a.Score * 100 / NULLIF(a.TotalMarks, 0) END) AS Highest,
                       MIN(CASE WHEN a.Status = 'Graded'
                                THEN a.Score * 100 / NULLIF(a.TotalMarks, 0) END) AS Lowest
                FROM QuizAttempts a
                WHERE a.QuizID = @q AND a.Status <> 'InProgress'", ("@q", quizId));

    // Per question: how many graded answers and how many marks were earned
    public DataTable GetQuestionStats(int quizId) =>
        Query(@"SELECT q.QuestionID, q.QuestionText, q.QuestionType, q.Marks,
                       COUNT(aa.AnswerID) AS Answers,
                       COALESCE(SUM(aa.MarksAwarded), 0) AS Earned
                FROM Questions q
                LEFT JOIN AttemptAnswers aa
                       ON aa.QuestionID = q.QuestionID AND aa.MarksAwarded IS NOT NULL
                WHERE q.QuizID = @q
                GROUP BY q.QuestionID, q.QuestionText, q.QuestionType, q.Marks
                ORDER BY q.QuestionID", ("@q", quizId));
}