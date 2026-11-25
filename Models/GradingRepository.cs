using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class GradingRepository
{
    private readonly string _connectionString;

    public GradingRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Attempts of this instructor's quizzes that wait for grading
    public DataTable GetPendingAttempts(int instructorId)
    {
        const string sql = @"SELECT a.AttemptID, u.FullName AS Learner, c.Title AS Course,
                                    qz.Title AS Quiz, a.SubmittedAt, a.Score, a.TotalMarks,
                                    (SELECT COUNT(*) FROM AttemptAnswers aa
                                      WHERE aa.AttemptID = a.AttemptID
                                        AND aa.MarksAwarded IS NULL) AS PendingAnswers
                             FROM QuizAttempts a
                             JOIN Quizzes qz ON qz.QuizID = a.QuizID
                             JOIN Courses c ON c.CourseID = qz.CourseID
                             JOIN Users u ON u.UserID = a.UserID
                             WHERE c.InstructorID = @inst AND a.Status = 'PendingGrading'
                             ORDER BY a.SubmittedAt";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@inst", MySqlDbType.Int32).Value = instructorId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    public int CountPendingAttempts(int instructorId)
    {
        const string sql = @"SELECT COUNT(*)
                             FROM QuizAttempts a
                             JOIN Quizzes qz ON qz.QuizID = a.QuizID
                             JOIN Courses c ON c.CourseID = qz.CourseID
                             WHERE c.InstructorID = @inst AND a.Status = 'PendingGrading'";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@inst", MySqlDbType.Int32).Value = instructorId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    // The instructor who owns the quiz of an attempt (null if the attempt does not exist)
    public int? GetAttemptOwnerId(int attemptId)
    {
        const string sql = @"SELECT c.InstructorID
                             FROM QuizAttempts a
                             JOIN Quizzes q ON q.QuizID = a.QuizID
                             JOIN Courses c ON c.CourseID = q.CourseID
                             WHERE a.AttemptID = @a";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
        con.Open();
        object? result = cmd.ExecuteScalar();
        if (result == null || result == DBNull.Value)
            return null;
        return Convert.ToInt32(result);
    }

    // The short answers of an attempt that still have to be graded
    public DataTable GetPendingAnswers(int attemptId)
    {
        const string sql = @"SELECT aa.AnswerID, q.QuestionText AS Question,
                                    aa.AnswerText AS Answer, q.Marks AS MaxMarks
                             FROM AttemptAnswers aa
                             JOIN Questions q ON q.QuestionID = aa.QuestionID
                             WHERE aa.AttemptID = @a AND aa.MarksAwarded IS NULL
                             ORDER BY q.QuestionID";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    public AnswerInfo? GetAnswerInfo(int answerId)
    {
        const string sql = @"SELECT aa.AnswerID, aa.AttemptID, q.Marks, aa.MarksAwarded,
                                    a.Status, c.InstructorID, qz.PassMark
                             FROM AttemptAnswers aa
                             JOIN QuizAttempts a ON a.AttemptID = aa.AttemptID
                             JOIN Questions q ON q.QuestionID = aa.QuestionID
                             JOIN Quizzes qz ON qz.QuizID = a.QuizID
                             JOIN Courses c ON c.CourseID = qz.CourseID
                             WHERE aa.AnswerID = @id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = answerId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        int marksOrdinal = reader.GetOrdinal("MarksAwarded");
        return new AnswerInfo
        {
            AnswerID = reader.GetInt32("AnswerID"),
            AttemptID = reader.GetInt32("AttemptID"),
            QuestionMarks = reader.GetInt32("Marks"),
            MarksAwarded = reader.IsDBNull(marksOrdinal) ? null : reader.GetInt32(marksOrdinal),
            AttemptStatus = reader.GetString("Status"),
            InstructorID = reader.GetInt32("InstructorID"),
            PassMark = reader.GetInt32("PassMark")
        };
    }

    // Saves the mark, recalculates the attempt and, when it was the LAST pending answer,
    // finishes the attempt. Everything happens in one transaction.
    // isPassed(score, total) is the pass rule, supplied by the controller.
    public bool SaveGrade(int answerId, int marks, Func<int, int, bool> isPassed,
                          out bool attemptCompleted)
    {
        attemptCompleted = false;

        using var con = new MySqlConnection(_connectionString);
        con.Open();
        using var tx = con.BeginTransaction();      // not committed = rolled back

        // 1. which attempt does the answer belong to?
        int attemptId;
        using (var cmd = new MySqlCommand(
            "SELECT AttemptID FROM AttemptAnswers WHERE AnswerID = @id", con, tx))
        {
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = answerId;
            object? result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value)
                return false;
            attemptId = Convert.ToInt32(result);
        }

        // 2. save the mark (only if the answer is still waiting)
        using (var cmd = new MySqlCommand(
            "UPDATE AttemptAnswers SET MarksAwarded = @m WHERE AnswerID = @id AND MarksAwarded IS NULL",
            con, tx))
        {
            cmd.Parameters.Add("@m", MySqlDbType.Int32).Value = marks;
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = answerId;
            if (cmd.ExecuteNonQuery() == 0)
                return false;
        }

        // 3. totals of the attempt
        int score;
        int pending;
        using (var cmd = new MySqlCommand(
            @"SELECT COALESCE(SUM(MarksAwarded), 0), COALESCE(SUM(MarksAwarded IS NULL), 0)
              FROM AttemptAnswers WHERE AttemptID = @a", con, tx))
        {
            cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
            using var reader = cmd.ExecuteReader();
            reader.Read();
            score = Convert.ToInt32(reader.GetValue(0));
            pending = Convert.ToInt32(reader.GetValue(1));
        }

        int total;
        using (var cmd = new MySqlCommand(
            "SELECT TotalMarks FROM QuizAttempts WHERE AttemptID = @a", con, tx))
        {
            cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
            total = Convert.ToInt32(cmd.ExecuteScalar());
        }

        // 4. update the attempt: finished only when nothing is pending any more
        string sql = pending == 0
            ? "UPDATE QuizAttempts SET Score = @s, Status = 'Graded', IsPassed = @p WHERE AttemptID = @a"
            : "UPDATE QuizAttempts SET Score = @s WHERE AttemptID = @a";

        using (var cmd = new MySqlCommand(sql, con, tx))
        {
            cmd.Parameters.Add("@s", MySqlDbType.Int32).Value = score;
            cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
            if (pending == 0)
                cmd.Parameters.Add("@p", MySqlDbType.Int32).Value = isPassed(score, total) ? 1 : 0;
            cmd.ExecuteNonQuery();
        }

        tx.Commit();
        attemptCompleted = pending == 0;
        return true;
    }
}