using BCrypt.Net;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Password utility functions using BCrypt
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Hash password using BCrypt with work factor 12
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <returns>BCrypt hashed password</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        /// <summary>
        /// Verify password against BCrypt hash
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <param name="hashedPassword">BCrypt hashed password</param>
        /// <returns>True if password matches, false otherwise</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch
            {
                // Invalid hash format or other BCrypt errors
                return false;
            }
        }

        /// <summary>
        /// Check if password meets minimum requirements
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Minimum 6 characters
            if (password.Length < 6)
                return false;

            // Add more rules as needed:
            // - At least one uppercase letter
            // - At least one lowercase letter  
            // - At least one digit
            // - At least one special character

            return true;
        }

        /// <summary>
        /// Generate random password
        /// </summary>
        /// <param name="length">Password length (default 12)</param>
        /// <returns>Random password</returns>
        public static string GenerateRandomPassword(int length = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            var random = new Random();
            var chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(chars);
        }
    }
}