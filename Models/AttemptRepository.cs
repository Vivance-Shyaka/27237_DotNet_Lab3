using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class AttemptRepository
{
    private readonly string _connectionString;

    public AttemptRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // ---------- the published quizzes of a course, with this learner's attempt counts ----------
    public DataTable GetCourseQuizzes(int userId, int courseId)
    {
        const string sql = @"SELECT q.QuizID, q.Title, q.Instructions, q.TimeLimitMinutes,
                                    q.PassMark, q.MaxAttempts,
                                    (SELECT COUNT(*) FROM Questions x
                                      WHERE x.QuizID = q.QuizID) AS Questions,
                                    (SELECT COALESCE(SUM(x.Marks), 0) FROM Questions x
                                      WHERE x.QuizID = q.QuizID) AS TotalMarks,
                                    (SELECT COUNT(*) FROM QuizAttempts a
                                      WHERE a.QuizID = q.QuizID AND a.UserID = @u) AS AttemptsUsed,
                                    (SELECT COUNT(*) FROM QuizAttempts a
                                      WHERE a.QuizID = q.QuizID AND a.UserID = @u
                                        AND a.IsPassed = 1) AS PassedCount,
                                    (SELECT COUNT(*) FROM QuizAttempts a
                                      WHERE a.QuizID = q.QuizID AND a.UserID = @u
                                        AND a.Status = 'PendingGrading') AS PendingCount
                             FROM Quizzes q
                             WHERE q.CourseID = @c AND q.IsPublished = 1
                             ORDER BY q.QuizID";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@c", MySqlDbType.Int32).Value = courseId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    // ---------- questions for the learner (NO IsCorrect) ----------
    public List<QuizQuestion> GetQuestionsForTaking(int quizId)
    {
        var questions = new List<QuizQuestion>();

        using var con = new MySqlConnection(_connectionString);
        con.Open();

        using (var cmd = new MySqlCommand(
            @"SELECT QuestionID, QuestionText, QuestionType, Marks
              FROM Questions WHERE QuizID = @id ORDER BY QuestionID", con))
        {
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                questions.Add(new QuizQuestion
                {
                    QuestionID = reader.GetInt32("QuestionID"),
                    QuestionText = reader.GetString("QuestionText"),
                    QuestionType = reader.GetString("QuestionType"),
                    Marks = reader.GetInt32("Marks")
                });
            }
        }

        using (var cmd = new MySqlCommand(
            @"SELECT o.QuestionID, o.OptionID, o.OptionText
              FROM AnswerOptions o
              JOIN Questions q ON q.QuestionID = o.QuestionID
              WHERE q.QuizID = @id
              ORDER BY o.OptionID", con))
        {
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int questionId = reader.GetInt32("QuestionID");
                QuizQuestion? question = questions.FirstOrDefault(q => q.QuestionID == questionId);
                question?.Options.Add(new QuizOption
                {
                    OptionID = reader.GetInt32("OptionID"),
                    OptionText = reader.GetString("OptionText")
                });
            }
        }

        return questions;
    }

    // ---------- the answer key (used only when grading) ----------
    public List<GradingKey> GetGradingKey(int quizId)
    {
        var keys = new List<GradingKey>();

        using var con = new MySqlConnection(_connectionString);
        con.Open();

        using (var cmd = new MySqlCommand(
            @"SELECT QuestionID, QuestionType, Marks
              FROM Questions WHERE QuizID = @id ORDER BY QuestionID", con))
        {
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                keys.Add(new GradingKey
                {
                    QuestionID = reader.GetInt32("QuestionID"),
                    QuestionType = reader.GetString("QuestionType"),
                    Marks = reader.GetInt32("Marks")
                });
            }
        }

        using (var cmd = new MySqlCommand(
            @"SELECT o.QuestionID, o.OptionID, o.IsCorrect
              FROM AnswerOptions o
              JOIN Questions q ON q.QuestionID = o.QuestionID
              WHERE q.QuizID = @id", con))
        {
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int questionId = reader.GetInt32("QuestionID");
                GradingKey? key = keys.FirstOrDefault(k => k.QuestionID == questionId);
                if (key == null) continue;

                int optionId = reader.GetInt32("OptionID");
                key.OptionIds.Add(optionId);
                if (reader.GetBoolean("IsCorrect"))
                    key.CorrectOptionId = optionId;
            }
        }

        return keys;
    }

    // ---------- attempts ----------

    public int CountAttempts(int userId, int quizId)
    {
        const string sql = "SELECT COUNT(*) FROM QuizAttempts WHERE UserID = @u AND QuizID = @q";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@q", MySqlDbType.Int32).Value = quizId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    // Attempts left "in progress" (the app was closed): close them with a score of 0
    public void CloseStaleAttempts(int userId, int quizId)
    {
        const string sql = @"UPDATE QuizAttempts
                             SET Status = 'Graded', SubmittedAt = NOW(), Score = 0, IsPassed = 0,
                                 TotalMarks = (SELECT COALESCE(SUM(Marks), 0)
                                               FROM Questions WHERE QuizID = @q)
                             WHERE UserID = @u AND QuizID = @q AND Status = 'InProgress'";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@q", MySqlDbType.Int32).Value = quizId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public int StartAttempt(int userId, int quizId, int totalMarks)
    {
        const string sql = "INSERT INTO QuizAttempts (QuizID, UserID, TotalMarks) VALUES (@q, @u, @t)";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@q", MySqlDbType.Int32).Value = quizId;
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@t", MySqlDbType.Int32).Value = totalMarks;
        con.Open();
        cmd.ExecuteNonQuery();
        return (int)cmd.LastInsertedId;
    }

    // The learner left the quiz without submitting: it counts and scores 0
    public void AbandonAttempt(int attemptId, int userId)
    {
        const string sql = @"UPDATE QuizAttempts
                             SET Status = 'Graded', SubmittedAt = NOW()
                             WHERE AttemptID = @a AND UserID = @u AND Status = 'InProgress'";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public QuizAttempt? GetAttempt(int attemptId)
    {
        const string sql = @"SELECT AttemptID, QuizID, UserID, StartedAt, SubmittedAt,
                                    Score, TotalMarks, IsPassed, Status
                             FROM QuizAttempts WHERE AttemptID = @a";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        int submittedOrdinal = reader.GetOrdinal("SubmittedAt");
        return new QuizAttempt
        {
            AttemptID = reader.GetInt32("AttemptID"),
            QuizID = reader.GetInt32("QuizID"),
            UserID = reader.GetInt32("UserID"),
            StartedAt = reader.GetDateTime("StartedAt"),
            SubmittedAt = reader.IsDBNull(submittedOrdinal) ? null : reader.GetDateTime(submittedOrdinal),
            Score = reader.GetInt32("Score"),
            TotalMarks = reader.GetInt32("TotalMarks"),
            IsPassed = reader.GetBoolean("IsPassed"),
            Status = reader.GetString("Status")
        };
    }

    // Saves the result and all the answers together. Returns false when the attempt
    // was not "InProgress" any more (already submitted): nothing is saved then.
    public bool SubmitAttempt(int attemptId, int userId, int score, int totalMarks,
                              bool passed, string status, List<AttemptAnswerRow> answers)
    {
        const string updateSql = @"UPDATE QuizAttempts
                                   SET SubmittedAt = NOW(), Score = @score, TotalMarks = @total,
                                       IsPassed = @passed, Status = @status
                                   WHERE AttemptID = @a AND UserID = @u AND Status = 'InProgress'";

        const string insertSql = @"INSERT INTO AttemptAnswers
                                      (AttemptID, QuestionID, SelectedOptionID, AnswerText, MarksAwarded)
                                   VALUES (@a, @q, @opt, @text, @marks)";

        using var con = new MySqlConnection(_connectionString);
        con.Open();
        using var tx = con.BeginTransaction();      // not committed = rolled back

        using (var cmd = new MySqlCommand(updateSql, con, tx))
        {
            cmd.Parameters.Add("@score", MySqlDbType.Int32).Value = score;
            cmd.Parameters.Add("@total", MySqlDbType.Int32).Value = totalMarks;
            cmd.Parameters.Add("@passed", MySqlDbType.Int32).Value = passed ? 1 : 0;
            cmd.Parameters.Add("@status", MySqlDbType.VarChar, 20).Value = status;
            cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
            cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;

            if (cmd.ExecuteNonQuery() == 0)
                return false;
        }

        foreach (AttemptAnswerRow answer in answers)
        {
            using var cmd = new MySqlCommand(insertSql, con, tx);
            cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
            cmd.Parameters.Add("@q", MySqlDbType.Int32).Value = answer.QuestionID;
            cmd.Parameters.Add("@opt", MySqlDbType.Int32).Value = (object?)answer.SelectedOptionID ?? DBNull.Value;
            cmd.Parameters.Add("@text", MySqlDbType.Text).Value = (object?)answer.AnswerText ?? DBNull.Value;
            cmd.Parameters.Add("@marks", MySqlDbType.Int32).Value = (object?)answer.MarksAwarded ?? DBNull.Value;
            cmd.ExecuteNonQuery();
        }

        tx.Commit();
        return true;
    }

    // ---------- history and review ----------

    public DataTable GetResults(int userId)
    {
        const string sql = @"SELECT a.AttemptID, c.Title AS Course, q.Title AS Quiz,
                                    a.StartedAt, a.SubmittedAt, a.Score, a.TotalMarks,
                                    a.IsPassed, a.Status
                             FROM QuizAttempts a
                             JOIN Quizzes q ON q.QuizID = a.QuizID
                             JOIN Courses c ON c.CourseID = q.CourseID
                             WHERE a.UserID = @u AND a.Status <> 'InProgress'
                             ORDER BY a.AttemptID DESC";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    // One row per question of the quiz: what the learner answered and the correct answer
    public DataTable GetReview(int attemptId, int userId)
    {
        const string sql = @"SELECT q.QuestionID, q.QuestionText, q.QuestionType, q.Marks,
                                    aa.SelectedOptionID, aa.AnswerText, aa.MarksAwarded,
                                    so.OptionText AS YourOption,
                                    (SELECT GROUP_CONCAT(o.OptionText SEPARATOR ', ')
                                       FROM AnswerOptions o
                                      WHERE o.QuestionID = q.QuestionID AND o.IsCorrect = 1) AS CorrectOption
                             FROM QuizAttempts a
                             JOIN Questions q ON q.QuizID = a.QuizID
                             LEFT JOIN AttemptAnswers aa
                                    ON aa.AttemptID = a.AttemptID AND aa.QuestionID = q.QuestionID
                             LEFT JOIN AnswerOptions so ON so.OptionID = aa.SelectedOptionID
                             WHERE a.AttemptID = @a AND a.UserID = @u
                             ORDER BY q.QuestionID";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@a", MySqlDbType.Int32).Value = attemptId;
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }
}