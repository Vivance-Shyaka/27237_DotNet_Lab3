using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class CourseRepository
{
    private readonly string _connectionString;

    public CourseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Read: the courses of ONE instructor (with category name, lesson and learner counts)
    public DataTable GetByInstructor(int instructorId, string keyword = "")
    {
        const string sql = @"SELECT c.CourseID, c.Title, c.Description, c.CategoryID,
                                    cat.CategoryName AS Category, c.CourseLevel, c.IsPublished,
                                    (SELECT COUNT(*) FROM Lessons l
                                      WHERE l.CourseID = c.CourseID) AS Lessons,
                                    (SELECT COUNT(*) FROM Enrollments e
                                      WHERE e.CourseID = c.CourseID) AS Learners
                             FROM Courses c
                             JOIN Categories cat ON cat.CategoryID = c.CategoryID
                             WHERE c.InstructorID = @inst
                               AND (c.Title LIKE @k OR cat.CategoryName LIKE @k)
                             ORDER BY c.CourseID DESC";

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

    public Course? GetById(int courseId)
    {
        const string sql = @"SELECT CourseID, Title, Description, CategoryID, InstructorID,
                                    CourseLevel, IsPublished, CreatedAt
                             FROM Courses WHERE CourseID = @id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = courseId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        int descOrdinal = reader.GetOrdinal("Description");
        return new Course
        {
            CourseID = reader.GetInt32("CourseID"),
            Title = reader.GetString("Title"),
            Description = reader.IsDBNull(descOrdinal) ? "" : reader.GetString(descOrdinal),
            CategoryID = reader.GetInt32("CategoryID"),
            InstructorID = reader.GetInt32("InstructorID"),
            CourseLevel = reader.GetString("CourseLevel"),
            IsPublished = reader.GetBoolean("IsPublished"),
            CreatedAt = reader.GetDateTime("CreatedAt")
        };
    }

    // Create (a new course is always a draft)
    public void Insert(Course course)
    {
        const string sql = @"INSERT INTO Courses
                                (Title, Description, CategoryID, InstructorID, CourseLevel)
                             VALUES (@title, @desc, @cat, @inst, @level)";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 150).Value = course.Title;
        cmd.Parameters.Add("@desc", MySqlDbType.Text).Value = course.Description;
        cmd.Parameters.Add("@cat", MySqlDbType.Int32).Value = course.CategoryID;
        cmd.Parameters.Add("@inst", MySqlDbType.Int32).Value = course.InstructorID;
        cmd.Parameters.Add("@level", MySqlDbType.VarChar, 20).Value = course.CourseLevel;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // Update
    public void Update(Course course)
    {
        const string sql = @"UPDATE Courses
                             SET Title=@title, Description=@desc,
                                 CategoryID=@cat, CourseLevel=@level
                             WHERE CourseID=@id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 150).Value = course.Title;
        cmd.Parameters.Add("@desc", MySqlDbType.Text).Value = course.Description;
        cmd.Parameters.Add("@cat", MySqlDbType.Int32).Value = course.CategoryID;
        cmd.Parameters.Add("@level", MySqlDbType.VarChar, 20).Value = course.CourseLevel;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = course.CourseID;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void SetPublished(int courseId, bool published)
    {
        const string sql = "UPDATE Courses SET IsPublished = @p WHERE CourseID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@p", MySqlDbType.Int32).Value = published ? 1 : 0;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = courseId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // Delete (its lessons and quizzes are deleted automatically by the database)
    public void Delete(int courseId)
    {
        const string sql = "DELETE FROM Courses WHERE CourseID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = courseId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public int CountLessons(int courseId) =>
        Count("SELECT COUNT(*) FROM Lessons WHERE CourseID = @id", courseId);

    public int CountEnrollments(int courseId) =>
        Count("SELECT COUNT(*) FROM Enrollments WHERE CourseID = @id", courseId);

    private int Count(string sql, int id)
    {
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}