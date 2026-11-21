using System.Text.RegularExpressions;
using WLAMS.Helpers;
using WLAMS.Models;

namespace WLAMS.Controllers;

public class AuthController
{
    private readonly UserRepository _users;

    public AuthController(UserRepository users)
    {
        _users = users;
    }

    public User Login(string identifier, string password)
    {
        if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrEmpty(password))
            throw new ArgumentException("Enter your username (or email) and your password.");

        User? user = _users.GetByUsernameOrEmail(identifier.Trim());

        // Same message for both cases, so nobody can guess which usernames exist
        if (user == null || user.PasswordHash != PasswordHelper.Hash(password))
            throw new ArgumentException("Invalid username or password.");

        if (!user.IsActive)
            throw new ArgumentException("This account is deactivated. Contact the administrator.");

        if (user.ApprovalStatus == Approval.Pending)
            throw new ArgumentException(
                "Your instructor account is waiting for approval by the administrator.");

        if (user.ApprovalStatus == Approval.Rejected)
            throw new ArgumentException(
                "Your instructor request was rejected. Contact the administrator.");

        return user;
    }

    // role must be "Learner" or "Instructor"
    // returns true when the account must wait for admin approval
    public bool Register(string fullName, string email, string username,
                         string password, string confirmPassword, string role)
    {
        fullName = fullName.Trim();
        email = email.Trim();
        username = username.Trim();

        if (fullName.Length == 0)
            throw new ArgumentException("Enter your full name.");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Enter a valid email address.");

        if (!Regex.IsMatch(username, @"^[A-Za-z0-9_]{3,30}$"))
            throw new ArgumentException(
                "Username must be 3 to 30 characters: letters, numbers or underscore only.");

        if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
            throw new ArgumentException(
                "Password must be at least 8 characters with an upper-case letter, " +
                "a lower-case letter and a number.");

        if (password != confirmPassword)
            throw new ArgumentException("The two passwords do not match.");

        // Nobody can register as Admin, only Learner or Instructor
        if (role != Roles.Learner && role != Roles.Instructor)
            throw new ArgumentException("Choose whether you register as a Learner or an Instructor.");

        if (_users.UsernameExists(username))
            throw new ArgumentException("This username is already taken.");

        if (_users.EmailExists(email))
            throw new ArgumentException("This email is already registered.");

        bool needsApproval = role == Roles.Instructor;

        var user = new User
        {
            FullName = fullName,
            Email = email,
            Username = username,
            PasswordHash = PasswordHelper.Hash(password),
            Role = role,
            ApprovalStatus = needsApproval ? Approval.Pending : Approval.Approved
        };

        _users.Insert(user);
        return needsApproval;
    }
}