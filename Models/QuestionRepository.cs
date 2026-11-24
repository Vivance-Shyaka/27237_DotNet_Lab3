using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class QuestionRepository
{
    private readonly string _connectionString;

    public QuestionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Read: all questions of a quiz
    public DataTable GetByQuiz(int quizId)
    {
        const string sql = @"SELECT q.QuestionID, q.QuestionText, q.QuestionType, q.Marks,
                                    (SELECT COUNT(*) FROM AnswerOptions o
                                      WHERE o.QuestionID = q.QuestionID) AS Options
                             FROM Questions q
                             WHERE q.QuizID = @id
                             ORDER BY q.QuestionID";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = quizId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    public Question? GetById(int questionId)
    {
        const string sql = @"SELECT QuestionID, QuizID, QuestionText, QuestionType, Marks
                             FROM Questions WHERE QuestionID = @id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = questionId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return new Question
        {
            QuestionID = reader.GetInt32("QuestionID"),
            QuizID = reader.GetInt32("QuizID"),
            QuestionText = reader.GetString("QuestionText"),
            QuestionType = reader.GetString("QuestionType"),
            Marks = reader.GetInt32("Marks")
        };
    }

    public List<AnswerOption> GetOptions(int questionId)
    {
        const string sql = @"SELECT OptionID, QuestionID, OptionText, IsCorrect
                             FROM AnswerOptions
                             WHERE QuestionID = @id
                             ORDER BY OptionID";

        var list = new List<AnswerOption>();
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = questionId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new AnswerOption
            {
                OptionID = reader.GetInt32("OptionID"),
                QuestionID = reader.GetInt32("QuestionID"),
                OptionText = reader.GetString("OptionText"),
                IsCorrect = reader.GetBoolean("IsCorrect")
            });
        }
        return list;
    }

    // Create the question and its options together
    public int Insert(Question question, List<AnswerOption> options)
    {
        const string sql = @"INSERT INTO Questions (QuizID, QuestionText, QuestionType, Marks)
                             VALUES (@quiz, @text, @type, @marks)";

        using var con = new MySqlConnection(_connectionString);
        con.Open();
        using var tx = con.BeginTransaction();      // not committed = rolled back

        long newId;
        using (var cmd = new MySqlCommand(sql, con, tx))
        {
            cmd.Parameters.Add("@quiz", MySqlDbType.Int32).Value = question.QuizID;
            cmd.Parameters.Add("@text", MySqlDbType.Text).Value = question.QuestionText;
            cmd.Parameters.Add("@type", MySqlDbType.VarChar, 20).Value = question.QuestionType;
            cmd.Parameters.Add("@marks", MySqlDbType.Int32).Value = question.Marks;
            cmd.ExecuteNonQuery();
            newId = cmd.LastInsertedId;
        }

        InsertOptions(con, tx, (int)newId, options);
        tx.Commit();
        return (int)newId;
    }

    // Update the question and replace all of its options
    public void Update(Question question, List<AnswerOption> options)
    {
        const string sql = @"UPDATE Questions
                             SET QuestionText=@text, QuestionType=@type, Marks=@marks
                             WHERE QuestionID=@id";

        using var con = new MySqlConnection(_connectionString);
        con.Open();
        using var tx = con.BeginTransaction();

        using (var cmd = new MySqlCommand(sql, con, tx))
        {
            cmd.Parameters.Add("@text", MySqlDbType.Text).Value = question.QuestionText;
            cmd.Parameters.Add("@type", MySqlDbType.VarChar, 20).Value = question.QuestionType;
            cmd.Parameters.Add("@marks", MySqlDbType.Int32).Value = question.Marks;
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = question.QuestionID;
            cmd.ExecuteNonQuery();
        }

        using (var cmd = new MySqlCommand("DELETE FROM AnswerOptions WHERE QuestionID = @id", con, tx))
        {
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = question.QuestionID;
            cmd.ExecuteNonQuery();
        }

        InsertOptions(con, tx, question.QuestionID, options);
        tx.Commit();
    }

    // Delete (its options are deleted automatically by the database)
    public void Delete(int questionId)
    {
        const string sql = "DELETE FROM Questions WHERE QuestionID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = questionId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    private static void InsertOptions(MySqlConnection con, MySqlTransaction tx,
                                      int questionId, List<AnswerOption> options)
    {
        const string sql = @"INSERT INTO AnswerOptions (QuestionID, OptionText, IsCorrect)
                             VALUES (@q, @text, @correct)";

        foreach (AnswerOption option in options)
        {
            using var cmd = new MySqlCommand(sql, con, tx);
            cmd.Parameters.Add("@q", MySqlDbType.Int32).Value = questionId;
            cmd.Parameters.Add("@text", MySqlDbType.VarChar, 255).Value = option.OptionText;
            cmd.Parameters.Add("@correct", MySqlDbType.Int32).Value = option.IsCorrect ? 1 : 0;
            cmd.ExecuteNonQuery();
        }
    }
}