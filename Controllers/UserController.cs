using System.Data;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class UserController
{
    private readonly UserRepository _users;

    public UserController(UserRepository users)
    {
        _users = users;
    }

    public DataTable GetUsers(string keyword = "") => _users.GetAll(keyword.Trim());

    public int CountPending() => _users.CountPending();

    // ---------- Create ----------
    public void AddUser(string fullName, string email, string username,
                        string password, string role)
    {
        fullName = fullName.Trim();
        email = email.Trim();
        username = username.Trim();

        Validator.RequireFullName(fullName);
        Validator.RequireEmail(email);
        Validator.RequireUsername(username);
        Validator.RequirePassword(password);
        CheckRole(role);

        if (_users.UsernameExists(username))
            throw new ArgumentException("This username is already taken.");
        if (_users.EmailExists(email))
            throw new ArgumentException("This email is already registered.");

        _users.Insert(new User
        {
            FullName = fullName,
            Email = email,
            Username = username,
            PasswordHash = PasswordHelper.Hash(password),
            Role = role,
            ApprovalStatus = Approval.Approved      // created by the admin = approved
        });
    }

    // ---------- Update (blank newPassword keeps the current password) ----------
    public void EditUser(int userId, string fullName, string email, string username,
                         string newPassword, string role, bool isActive)
    {
        if (userId <= 0)
            throw new ArgumentException("Select a user first.");

        User existing = _users.GetById(userId)
                        ?? throw new ArgumentException("User not found.");

        fullName = fullName.Trim();
        email = email.Trim();
        username = username.Trim();

        Validator.RequireFullName(fullName);
        Validator.RequireEmail(email);
        Validator.RequireUsername(username);
        CheckRole(role);

        string? newHash = null;
        if (newPassword.Length > 0)
        {
            Validator.RequirePassword(newPassword);
            newHash = PasswordHelper.Hash(newPassword);
        }

        // The admin must not lock himself out
        bool isSelf = Session.CurrentUser?.UserID == userId;
        if (isSelf && (role != existing.Role || !isActive))
            throw new ArgumentException("You cannot change your own role or deactivate your own account.");

        // An instructor who owns courses cannot become a Learner/Admin
        if (existing.Role == Roles.Instructor && role != Roles.Instructor &&
            _users.CountCoursesByInstructor(userId) > 0)
            throw new ArgumentException("This instructor owns courses, so the role cannot be changed.");

        if (_users.UsernameExists(username, userId))
            throw new ArgumentException("This username is already taken.");
        if (_users.EmailExists(email, userId))
            throw new ArgumentException("This email is already registered.");

        // Only instructors need approval: any other role is automatically approved
        string approval = role == Roles.Instructor ? existing.ApprovalStatus : Approval.Approved;

        _users.Update(new User
        {
            UserID = userId,
            FullName = fullName,
            Email = email,
            Username = username,
            Role = role,
            IsActive = isActive,
            ApprovalStatus = approval
        }, newHash);
    }

    // ---------- Delete ----------
    public void RemoveUser(int userId)
    {
        if (userId <= 0)
            throw new ArgumentException("Select a user first.");

        if (Session.CurrentUser?.UserID == userId)
            throw new ArgumentException("You cannot delete your own account.");

        int courses = _users.CountCoursesByInstructor(userId);
        if (courses > 0)
            throw new ArgumentException(
                "This instructor owns " + courses + " course(s). Deactivate the account instead.");

        _users.Delete(userId);
    }

    // ---------- Approve / Reject instructors ----------
    public void Approve(int userId)
    {
        User user = GetForApproval(userId);
        if (user.ApprovalStatus == Approval.Approved)
            throw new ArgumentException("This account is already approved.");
        _users.SetApproval(userId, Approval.Approved);
    }

    public void Reject(int userId)
    {
        User user = GetForApproval(userId);
        if (user.ApprovalStatus != Approval.Pending)
            throw new ArgumentException("Only pending requests can be rejected.");
        _users.SetApproval(userId, Approval.Rejected);
    }

    private User GetForApproval(int userId)
    {
        if (userId <= 0)
            throw new ArgumentException("Select a user first.");

        User user = _users.GetById(userId) ?? throw new ArgumentException("User not found.");
        if (user.Role != Roles.Instructor)
            throw new ArgumentException("Only instructor accounts need approval.");
        return user;
    }

    private static void CheckRole(string role)
    {
        if (role != Roles.Admin && role != Roles.Instructor && role != Roles.Learner)
            throw new ArgumentException("Choose a valid role.");
    }
}