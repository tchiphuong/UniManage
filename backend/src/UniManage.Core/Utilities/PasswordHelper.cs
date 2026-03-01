using System.Security.Cryptography;
using BCrypt.Net;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Password utility functions using BCrypt.
    /// 
    /// [SECURITY] Key design decisions:
    /// - BCrypt with work factor 12 (adaptive, resistant to GPU attacks)
    /// - CSPRNG for password generation (not System.Random)
    /// - Consistent password policy enforced across all endpoints
    /// </summary>
    public static class PasswordHelper
    {
        // ===========================================
        // [SECURITY] Password policy constants
        // Used by IsValidPassword() and referenced by all validators.
        // Change these values to adjust policy globally.
        // ===========================================
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 128;
        public const int DefaultGeneratedPasswordLength = 16;

        /// <summary>
        /// Hash password using BCrypt with work factor 12.
        /// Work factor 12 = ~250ms per hash on modern hardware.
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
        /// Verify password against BCrypt hash.
        /// Uses constant-time comparison internally (built into BCrypt).
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
        /// Check if password meets security requirements (H4).
        /// 
        /// [SECURITY] H4 — Unified password policy:
        /// - Minimum 8 characters
        /// - Maximum 128 characters (prevents DoS via bcrypt)
        /// - At least one uppercase letter
        /// - At least one lowercase letter
        /// - At least one digit
        /// - At least one special character
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < MinPasswordLength)
                return false;

            if (password.Length > MaxPasswordLength)
                return false;

            // Must contain at least one uppercase letter
            if (!password.Any(char.IsUpper))
                return false;

            // Must contain at least one lowercase letter
            if (!password.Any(char.IsLower))
                return false;

            // Must contain at least one digit
            if (!password.Any(char.IsDigit))
                return false;

            // Must contain at least one special character
            if (!password.Any(c => !char.IsLetterOrDigit(c)))
                return false;

            return true;
        }

        /// <summary>
        /// Generate cryptographically secure random password.
        /// 
        /// [SECURITY] H3 — Uses RandomNumberGenerator (CSPRNG)
        /// instead of System.Random which is predictable.
        /// Generated passwords always meet IsValidPassword() requirements.
        /// </summary>
        /// <param name="length">Password length (default 16, minimum 8)</param>
        /// <returns>Random password meeting all complexity requirements</returns>
        public static string GenerateRandomPassword(int length = DefaultGeneratedPasswordLength)
        {
            if (length < MinPasswordLength)
                length = MinPasswordLength;

            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*";
            const string allChars = upperChars + lowerChars + digitChars + specialChars;

            var chars = new char[length];

            // [SECURITY] H3 — Use CSPRNG for unpredictable random generation
            var randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);

            // Guarantee at least one char from each required category
            chars[0] = upperChars[randomBytes[0] % upperChars.Length];
            chars[1] = lowerChars[randomBytes[1] % lowerChars.Length];
            chars[2] = digitChars[randomBytes[2] % digitChars.Length];
            chars[3] = specialChars[randomBytes[3] % specialChars.Length];

            // Fill remaining positions with random chars from all categories
            for (int i = 4; i < length; i++)
            {
                chars[i] = allChars[randomBytes[i] % allChars.Length];
            }

            // Shuffle to avoid predictable positions for guaranteed chars
            var shuffleBytes = new byte[length];
            RandomNumberGenerator.Fill(shuffleBytes);
            chars = chars.Zip(shuffleBytes, (c, b) => (c, b))
                .OrderBy(x => x.b)
                .Select(x => x.c)
                .ToArray();

            return new string(chars);
        }
    }
}