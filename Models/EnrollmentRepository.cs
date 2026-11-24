using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class EnrollmentRepository
{
    private readonly string _connectionString;

    public EnrollmentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // The catalogue: every PUBLISHED course, with a Yes/No "Enrolled" column for this learner
    public DataTable GetCatalogue(int userId, string keyword = "")
    {
        const string sql = @"SELECT c.CourseID, c.Title, c.Description,
                                    cat.CategoryName AS Category, c.CourseLevel,
                                    u.FullName AS Instructor,
                                    (SELECT COUNT(*) FROM Lessons l
                                      WHERE l.CourseID = c.CourseID) AS Lessons,
                                    (SELECT COUNT(*) FROM Quizzes q
                                      WHERE q.CourseID = c.CourseID AND q.IsPublished = 1) AS Quizzes,
                                    IF(EXISTS(SELECT 1 FROM Enrollments e
                                               WHERE e.CourseID = c.CourseID AND e.UserID = @uid),
                                       'Yes', 'No') AS Enrolled
                             FROM Courses c
                             JOIN Categories cat ON cat.CategoryID = c.CategoryID
                             JOIN Users u ON u.UserID = c.InstructorID
                             WHERE c.IsPublished = 1
                               AND (c.Title LIKE @k OR cat.CategoryName LIKE @k OR u.FullName LIKE @k)
                             ORDER BY c.Title";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@uid", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@k", MySqlDbType.VarChar, 160).Value = "%" + keyword + "%";
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    // "My courses": the published courses this learner is enrolled in
    public DataTable GetMyCourses(int userId, string keyword = "")
    {
        const string sql = @"SELECT c.CourseID, c.Title, cat.CategoryName AS Category,
                                    c.CourseLevel, u.FullName AS Instructor,
                                    (SELECT COUNT(*) FROM Lessons l
                                      WHERE l.CourseID = c.CourseID) AS Lessons,
                                    (SELECT COUNT(*) FROM LessonProgress p
                                       JOIN Lessons l ON l.LessonID = p.LessonID
                                      WHERE l.CourseID = c.CourseID AND p.UserID = @uid) AS Completed,
                                    e.EnrolledAt
                             FROM Enrollments e
                             JOIN Courses c ON c.CourseID = e.CourseID
                             JOIN Categories cat ON cat.CategoryID = c.CategoryID
                             JOIN Users u ON u.UserID = c.InstructorID
                             WHERE e.UserID = @uid AND c.IsPublished = 1
                               AND (c.Title LIKE @k OR cat.CategoryName LIKE @k)
                             ORDER BY e.EnrolledAt DESC";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@uid", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@k", MySqlDbType.VarChar, 160).Value = "%" + keyword + "%";
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    public bool IsEnrolled(int userId, int courseId)
    {
        const string sql = "SELECT COUNT(*) FROM Enrollments WHERE UserID = @u AND CourseID = @c";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@c", MySqlDbType.Int32).Value = courseId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public void Enroll(int userId, int courseId)
    {
        const string sql = "INSERT INTO Enrollments (UserID, CourseID) VALUES (@u, @c)";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@c", MySqlDbType.Int32).Value = courseId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void Unenroll(int userId, int courseId)
    {
        const string sql = "DELETE FROM Enrollments WHERE UserID = @u AND CourseID = @c";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@u", MySqlDbType.Int32).Value = userId;
        cmd.Parameters.Add("@c", MySqlDbType.Int32).Value = courseId;
        con.Open();
        cmd.ExecuteNonQuery();
    }
}