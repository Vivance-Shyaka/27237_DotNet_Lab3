using System.Text.RegularExpressions;

namespace WLAMS.Helpers;

public static class Validator
{
    public static void RequireFullName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Enter the full name.");
    }

    public static void RequireEmail(string value)
    {
        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Enter a valid email address.");
    }

    public static void RequireUsername(string value)
    {
        if (!Regex.IsMatch(value, @"^[A-Za-z0-9_]{3,30}$"))
            throw new ArgumentException(
                "Username must be 3 to 30 characters: letters, numbers or underscore only.");
    }

    public static void RequirePassword(string value)
    {
        if (!Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
            throw new ArgumentException(
                "Password must be at least 8 characters with an upper-case letter, " +
                "a lower-case letter and a number.");
    }
}