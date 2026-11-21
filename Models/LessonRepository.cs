using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class LessonRepository
{
    private readonly string _connectionString;

    public LessonRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Read: all lessons of a course, in order
    public DataTable GetByCourse(int courseId)
    {
        const string sql = @"SELECT LessonID, LessonOrder, Title, Content
                             FROM Lessons
                             WHERE CourseID = @id
                             ORDER BY LessonOrder, LessonID";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = courseId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    public Lesson? GetById(int lessonId)
    {
        const string sql = @"SELECT LessonID, CourseID, Title, Content, LessonOrder
                             FROM Lessons WHERE LessonID = @id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = lessonId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        int contentOrdinal = reader.GetOrdinal("Content");
        return new Lesson
        {
            LessonID = reader.GetInt32("LessonID"),
            CourseID = reader.GetInt32("CourseID"),
            Title = reader.GetString("Title"),
            Content = reader.IsDBNull(contentOrdinal) ? "" : reader.GetString(contentOrdinal),
            LessonOrder = reader.GetInt32("LessonOrder")
        };
    }

    public void Insert(Lesson lesson)
    {
        const string sql = @"INSERT INTO Lessons (CourseID, Title, Content, LessonOrder)
                             VALUES (@course, @title, @content, @order)";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@course", MySqlDbType.Int32).Value = lesson.CourseID;
        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 150).Value = lesson.Title;
        cmd.Parameters.Add("@content", MySqlDbType.Text).Value = lesson.Content;
        cmd.Parameters.Add("@order", MySqlDbType.Int32).Value = lesson.LessonOrder;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void Update(Lesson lesson)
    {
        const string sql = @"UPDATE Lessons
                             SET Title=@title, Content=@content, LessonOrder=@order
                             WHERE LessonID=@id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 150).Value = lesson.Title;
        cmd.Parameters.Add("@content", MySqlDbType.Text).Value = lesson.Content;
        cmd.Parameters.Add("@order", MySqlDbType.Int32).Value = lesson.LessonOrder;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = lesson.LessonID;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void Delete(int lessonId)
    {
        const string sql = "DELETE FROM Lessons WHERE LessonID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = lessonId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // The next free lesson number of a course (1 for an empty course)
    public int NextOrder(int courseId)
    {
        const string sql = "SELECT COALESCE(MAX(LessonOrder), 0) + 1 FROM Lessons WHERE CourseID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = courseId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}