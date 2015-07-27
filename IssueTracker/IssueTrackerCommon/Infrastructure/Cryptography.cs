using System;
using System.Security.Cryptography;
using System.Text;

namespace IssueTrackerCommon.Infrastructure
{
    public static class Cryptography
    {
        public static string CreateSalt()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string HashPassword(string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(bytes));
        }
    }
}
