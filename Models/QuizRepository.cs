using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class QuizRepository
{
    private readonly string _connectionString;

    public QuizRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Read: the quizzes of ONE instructor (through the courses they own)
    public DataTable GetByInstructor(int instructorId, string keyword = "")
    {
        const string sql = @"SELECT q.QuizID, q.Title, q.Instructions, q.CourseID,
                                    c.Title AS Course, q.TimeLimitMinutes, q.PassMark,
                                    q.MaxAttempts, q.IsPublished,
                                    (SELECT COUNT(*) FROM Questions x
                                      WHERE x.QuizID = q.QuizID) AS Questions,
                                    (SELECT COALESCE(SUM(x.Marks), 0) FROM Questions x
                                      WHERE x.QuizID = q.QuizID) AS TotalMarks,
                                    (SELECT COUNT(*) FROM QuizAttempts a
                                      WHERE a.QuizID = q.QuizID) AS Attempts
                             FROM Quizzes q
                             JOIN Courses c ON c.CourseID = q.CourseID
                             WHERE c.InstructorID = @inst
                               AND (q.Title LIKE @k OR c.Title LIKE @k)
                             ORDER BY q.QuizID DESC";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@inst", MySqlDbType.Int32).Value = instructorId;
        cmd.Parameters.Add("@k", MySqlDbType.VarChar, 160).Value = "%" + keyword + "%";
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    public Quiz? GetById(int quizId)
    {
        const string sql = @"SELECT QuizID, CourseID, Title, Instructions, TimeLimitMinutes,
                                    PassMark, MaxAttempts, IsPublished, CreatedAt
                             FROM Quizzes WHERE QuizID = @id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        int insOrdinal = reader.GetOrdinal("Instructions");
        return new Quiz
        {
            QuizID = reader.GetInt32("QuizID"),
            CourseID = reader.GetInt32("CourseID"),
            Title = reader.GetString("Title"),
            Instructions = reader.IsDBNull(insOrdinal) ? "" : reader.GetString(insOrdinal),
            TimeLimitMinutes = reader.GetInt32("TimeLimitMinutes"),
            PassMark = reader.GetInt32("PassMark"),
            MaxAttempts = reader.GetInt32("MaxAttempts"),
            IsPublished = reader.GetBoolean("IsPublished"),
            CreatedAt = reader.GetDateTime("CreatedAt")
        };
    }

    // The UserID of the instructor who owns the quiz (null if the quiz does not exist)
    public int? GetOwnerId(int quizId)
    {
        const string sql = @"SELECT c.InstructorID
                             FROM Quizzes q
                             JOIN Courses c ON c.CourseID = q.CourseID
                             WHERE q.QuizID = @id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
        con.Open();
        object? result = cmd.ExecuteScalar();
        if (result == null || result == DBNull.Value)
            return null;
        return Convert.ToInt32(result);
    }

    public void Insert(Quiz quiz)
    {
        const string sql = @"INSERT INTO Quizzes
                                (CourseID, Title, Instructions, TimeLimitMinutes, PassMark, MaxAttempts)
                             VALUES (@course, @title, @ins, @time, @pass, @max)";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@course", MySqlDbType.Int32).Value = quiz.CourseID;
        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 150).Value = quiz.Title;
        cmd.Parameters.Add("@ins", MySqlDbType.Text).Value = quiz.Instructions;
        cmd.Parameters.Add("@time", MySqlDbType.Int32).Value = quiz.TimeLimitMinutes;
        cmd.Parameters.Add("@pass", MySqlDbType.Int32).Value = quiz.PassMark;
        cmd.Parameters.Add("@max", MySqlDbType.Int32).Value = quiz.MaxAttempts;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // The course of a quiz is never changed
    public void Update(Quiz quiz)
    {
        const string sql = @"UPDATE Quizzes
                             SET Title=@title, Instructions=@ins, TimeLimitMinutes=@time,
                                 PassMark=@pass, MaxAttempts=@max
                             WHERE QuizID=@id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 150).Value = quiz.Title;
        cmd.Parameters.Add("@ins", MySqlDbType.Text).Value = quiz.Instructions;
        cmd.Parameters.Add("@time", MySqlDbType.Int32).Value = quiz.TimeLimitMinutes;
        cmd.Parameters.Add("@pass", MySqlDbType.Int32).Value = quiz.PassMark;
        cmd.Parameters.Add("@max", MySqlDbType.Int32).Value = quiz.MaxAttempts;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quiz.QuizID;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void SetPublished(int quizId, bool published)
    {
        const string sql = "UPDATE Quizzes SET IsPublished = @p WHERE QuizID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@p", MySqlDbType.Int32).Value = published ? 1 : 0;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void Delete(int quizId)
    {
        const string sql = "DELETE FROM Quizzes WHERE QuizID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public int CountQuestions(int quizId) =>
        Count("SELECT COUNT(*) FROM Questions WHERE QuizID = @id", quizId);

    public int CountAttempts(int quizId) =>
        Count("SELECT COUNT(*) FROM QuizAttempts WHERE QuizID = @id", quizId);

    private int Count(string sql, int id)
    {
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}