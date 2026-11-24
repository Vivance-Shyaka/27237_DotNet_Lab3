using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class ProgressRepository
{
    private readonly string _connectionString;

    public ProgressRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // The lessons of a course, each with Completed = Yes / No for this learner
    public DataTable GetLessonsWithProgress(int userId, int courseId)
    {
        const string sql = @"SELECT l.LessonID, l.LessonOrder, l.Title, l.Content,
                                    IF(p.ProgressID IS NULL, 'No', 'Yes') AS Completed
                             FROM Lessons l
                             LEFT JOIN LessonProgress p
                                    ON p.LessonID = l.LessonID AND p.UserID = @u
                             WHERE l.CourseID = @c
                             ORDER BY l.LessonOrder, l.LessonID";

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

    // INSERT IGNORE: marking the same lesson twice does nothing (the table has a UNIQUE key)
    public void MarkComplete(int userId, int lessonId)
    {
        const string sql = "INSERT IGNORE INTO LessonProgress (UserID, LessonID) VALUES (@u, @l)";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@l", MySqlDbType.Int32).Value = lessonId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void MarkIncomplete(int userId, int lessonId)
    {
        const string sql = "DELETE FROM LessonProgress WHERE UserID = @u AND LessonID = @l";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@l", MySqlDbType.Int32).Value = lessonId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public int CountCompleted(int userId, int courseId)
    {
        const string sql = @"SELECT COUNT(*)
                             FROM LessonProgress p
                             JOIN Lessons l ON l.LessonID = p.LessonID
                             WHERE p.UserID = @u AND l.CourseID = @c";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@c", MySqlDbType.Int32).Value = courseId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}