using System.Security.Cryptography;
using System.Text;

namespace Synt_W1_P1.Services.Users
{
    public static class PasswordHasher
    {
        public static void createHashedPassword(string password, out byte[] passwordHash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            
            passwordHash = hmac.ComputeHash(passwordBytes);
        }

        public static bool VerifyHashedPassword(string password, byte[] storedHash, byte[] salt)
        {
            using var hmac = new HMACSHA512();
            
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var computeHash = hmac.ComputeHash(passwordBytes);
            var compareHash = computeHash.SequenceEqual(storedHash);
            return compareHash;
        }
    }
}
