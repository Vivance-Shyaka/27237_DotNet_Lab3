namespace WLAMS.Models;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Instructor = "Instructor";
    public const string Learner = "Learner";
}

public static class Approval
{
    public const string Approved = "Approved";
    public const string Pending = "Pending";
    public const string Rejected = "Rejected";
}

public class User
{
    public int UserID { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Role { get; set; } = Roles.Learner;
    public bool IsActive { get; set; } = true;
    public string ApprovalStatus { get; set; } = Approval.Approved;
    public DateTime CreatedAt { get; set; }
}