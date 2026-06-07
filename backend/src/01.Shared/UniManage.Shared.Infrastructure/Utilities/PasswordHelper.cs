using System.Security.Cryptography;

namespace UniManage.Shared.Infrastructure.Utilities
{
    /// <summary>
    /// Password utility functions using BCrypt for secure hashing and CSPRNG for random generation.
    /// 
    /// [SECURITY] Key design decisions:
    /// - Uses BCrypt with a default work factor of 12 (adaptive, resistant to GPU attacks).
    /// - Uses CSPRNG for random password generation (not System.Random).
    /// - Consistent password policy applied across all endpoints.
    /// </summary>
    public static class PasswordHelper
    {
        // ===========================================
        // [SECURITY] Password Policy Constants
        // Used by IsValidPassword() and referenced by all validators.
        // Modify these values to adjust the global policy.
        // ===========================================
        
        /// <summary>Minimum password length.</summary>
        public const int MinPasswordLength = 8;
        
        /// <summary>Maximum password length.</summary>
        public const int MaxPasswordLength = 128;
        
        /// <summary>Default length for randomly generated passwords.</summary>
        public const int DefaultGeneratedPasswordLength = 16;

        /// <summary>
        /// Hashes a password using BCrypt with a work factor of 12.
        /// Work factor 12 equates to ~250ms per hash on modern hardware.
        /// </summary>
        /// <param name="password">Plain text password.</param>
        /// <returns>BCrypt hashed password.</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        /// <summary>
        /// Verifies a password against a BCrypt hash.
        /// Uses constant-time comparison internally.
        /// </summary>
        /// <param name="password">Plain text password.</param>
        /// <param name="hashedPassword">BCrypt hashed password.</param>
        /// <returns>True if password matches the hash.</returns>
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
                // Invalid hash format or BCrypt error
                return false;
            }
        }

        /// <summary>
        /// Validates if a password meets complexity requirements (H4).
        /// 
        /// [SECURITY] H4 — Unified Password Policy:
        /// - Minimum 8 characters.
        /// - Maximum 128 characters (prevent DoS via bcrypt).
        /// - At least one uppercase letter.
        /// - At least one lowercase letter.
        /// - At least one digit.
        /// - At least one special character.
        /// </summary>
        /// <param name="password">Password string to validate.</param>
        /// <returns>True if the password meets all requirements.</returns>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < MinPasswordLength)
                return false;

            if (password.Length > MaxPasswordLength)
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if (!password.Any(char.IsLower))
                return false;

            if (!password.Any(char.IsDigit))
                return false;

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
                return false;

            return true;
        }

        /// <summary>
        /// Generates a cryptographically secure random password.
        /// 
        /// [SECURITY] H3 — Uses RandomNumberGenerator (CSPRNG)
        /// instead of predictable System.Random.
        /// Generated password always meets IsValidPassword() requirements.
        /// </summary>
        /// <param name="length">Length of password (default 16, min 8).</param>
        /// <returns>Random password meeting all complexity requirements.</returns>
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

            // [SECURITY] H3 — Use CSPRNG for unpredictable randomness
            var randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);

            // Ensure at least one character from each required category
            chars[0] = upperChars[randomBytes[0] % upperChars.Length];
            chars[1] = lowerChars[randomBytes[1] % lowerChars.Length];
            chars[2] = digitChars[randomBytes[2] % digitChars.Length];
            chars[3] = specialChars[randomBytes[3] % specialChars.Length];

            // Fill the rest with random characters from all sets
            for (int i = 4; i < length; i++)
            {
                chars[i] = allChars[randomBytes[i] % allChars.Length];
            }

            // Shuffle to avoid predictable positions for mandatory characters
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
