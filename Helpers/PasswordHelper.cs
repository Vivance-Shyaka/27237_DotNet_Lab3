using System.Security.Cryptography;
using System.Text;

namespace WLAMS.Helpers;

public static class PasswordHelper
{
    // SHA-256, written as 64 lowercase hex characters
    public static string Hash(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}