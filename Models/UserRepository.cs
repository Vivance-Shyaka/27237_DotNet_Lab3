using System.Data;
using MySqlConnector;

namespace WLAMS.Models;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private const string SelectColumns =
        @"SELECT UserID, FullName, Email, Username, PasswordHash,
                 Role, IsActive, ApprovalStatus, CreatedAt
          FROM Users ";

    private static User Map(MySqlDataReader reader) => new User
    {
        UserID = reader.GetInt32("UserID"),
        FullName = reader.GetString("FullName"),
        Email = reader.GetString("Email"),
        Username = reader.GetString("Username"),
        PasswordHash = reader.GetString("PasswordHash"),
        Role = reader.GetString("Role"),
        IsActive = reader.GetBoolean("IsActive"),
        ApprovalStatus = reader.GetString("ApprovalStatus"),
        CreatedAt = reader.GetDateTime("CreatedAt")
    };

    // ---------- single user ----------

    public User? GetByUsernameOrEmail(string identifier)
    {
        string sql = SelectColumns + "WHERE Username = @id OR Email = @id LIMIT 1";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.VarChar, 100).Value = identifier;
        con.Open();
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    public User? GetById(int userId)
    {
        string sql = SelectColumns + "WHERE UserID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = userId;
        con.Open();
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    // ---------- duplicate checks (exceptUserId lets an edit keep its own values) ----------

    public bool UsernameExists(string username, int exceptUserId = 0) =>
        Exists("SELECT COUNT(*) FROM Users WHERE Username = @v AND UserID <> @id",
               username, exceptUserId);

    public bool EmailExists(string email, int exceptUserId = 0) =>
        Exists("SELECT COUNT(*) FROM Users WHERE Email = @v AND UserID <> @id",
               email, exceptUserId);

    private bool Exists(string sql, string value, int exceptUserId)
    {
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@v", MySqlDbType.VarChar, 100).Value = value;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = exceptUserId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    // ---------- Create ----------

    public int Insert(User user)
    {
        const string sql = @"INSERT INTO Users
                                (FullName, Email, Username, PasswordHash, Role, ApprovalStatus)
                             VALUES (@name, @email, @username, @hash, @role, @approval)";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = user.FullName;
        cmd.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value = user.Email;
        cmd.Parameters.Add("@username", MySqlDbType.VarChar, 50).Value = user.Username;
        cmd.Parameters.Add("@hash", MySqlDbType.VarChar, 64).Value = user.PasswordHash;
        cmd.Parameters.Add("@role", MySqlDbType.VarChar, 20).Value = user.Role;
        cmd.Parameters.Add("@approval", MySqlDbType.VarChar, 20).Value = user.ApprovalStatus;
        con.Open();
        cmd.ExecuteNonQuery();
        return (int)cmd.LastInsertedId;
    }

    // ---------- Read (for the admin grid; the password hash is NOT returned) ----------

    public DataTable GetAll(string keyword = "")
    {
        const string sql = @"SELECT UserID, FullName, Email, Username, Role,
                                    IsActive, ApprovalStatus, CreatedAt
                             FROM Users
                             WHERE FullName LIKE @k OR Email LIKE @k OR Username LIKE @k
                                   OR Role LIKE @k OR ApprovalStatus LIKE @k
                             ORDER BY UserID";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@k", MySqlDbType.VarChar, 120).Value = "%" + keyword + "%";
        con.Open();
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    // ---------- Update (newPasswordHash == null keeps the current password) ----------

    public void Update(User user, string? newPasswordHash)
    {
        string sql = @"UPDATE Users
                       SET FullName=@name, Email=@email, Username=@username,
                           Role=@role, IsActive=@active, ApprovalStatus=@approval";
        if (newPasswordHash != null)
            sql += ", PasswordHash=@hash";
        sql += " WHERE UserID=@id";

        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = user.FullName;
        cmd.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value = user.Email;
        cmd.Parameters.Add("@username", MySqlDbType.VarChar, 50).Value = user.Username;
        cmd.Parameters.Add("@role", MySqlDbType.VarChar, 20).Value = user.Role;
        cmd.Parameters.Add("@active", MySqlDbType.Int32).Value = user.IsActive ? 1 : 0;
        cmd.Parameters.Add("@approval", MySqlDbType.VarChar, 20).Value = user.ApprovalStatus;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = user.UserID;
        if (newPasswordHash != null)
            cmd.Parameters.Add("@hash", MySqlDbType.VarChar, 64).Value = newPasswordHash;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    public void SetApproval(int userId, string status)
    {
        const string sql = "UPDATE Users SET ApprovalStatus = @status WHERE UserID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@status", MySqlDbType.VarChar, 20).Value = status;
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = userId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // ---------- Delete ----------

    public void Delete(int userId)
    {
        const string sql = "DELETE FROM Users WHERE UserID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = userId;
        con.Open();
        cmd.ExecuteNonQuery();
    }

    // ---------- counts ----------

    public int CountCoursesByInstructor(int userId)
    {
        const string sql = "SELECT COUNT(*) FROM Courses WHERE InstructorID = @id";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = userId;
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int CountPending()
    {
        const string sql = @"SELECT COUNT(*) FROM Users
                             WHERE Role = 'Instructor' AND ApprovalStatus = 'Pending'";
        using var con = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand(sql, con);
        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}