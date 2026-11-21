using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class CategoryRepository
{
    private readonly string _connectionString;

    public CategoryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Read: all categories (with the number of courses in each)
    public DataTable GetAll(string keyword = "")
    {
        const string sql = @"SELECT c.CategoryID, c.CategoryName, COUNT(co.CourseID) AS Courses
                             FROM Categories c
                             LEFT JOIN Courses co ON co.CategoryID = c.CategoryID
                             WHERE c.CategoryName LIKE @key
                             GROUP BY c.CategoryID, c.CategoryName
                             ORDER BY c.CategoryName";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@key", MySqlDbType.VarChar, 120).Value = "%" + keyword + "%";
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    // Create
    public void Insert(Category category)
    {
        const string sql = "INSERT INTO Categories (CategoryName) VALUES (@name)";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = category.CategoryName;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // Update
    public void Update(Category category)
    {
        const string sql = "UPDATE Categories SET CategoryName = @name WHERE CategoryID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = category.CategoryName;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = category.CategoryID;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // Delete
    public void Delete(int categoryId)
    {
        const string sql = "DELETE FROM Categories WHERE CategoryID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = categoryId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // Is the name already used by ANOTHER category?
    public bool NameExists(string name, int exceptCategoryId)
    {
        const string sql = @"SELECT COUNT(*) FROM Categories
                             WHERE CategoryName = @name AND CategoryID <> @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = name;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = exceptCategoryId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public int CountCourses(int categoryId)
    {
        const string sql = "SELECT COUNT(*) FROM Courses WHERE CategoryID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = categoryId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}