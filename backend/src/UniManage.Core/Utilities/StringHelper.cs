using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Helper functions for string manipulation and formatting
    /// </summary>
    public static class StringHelper
    {
        private static readonly Regex WhitespaceRegex = new(@"\s+", RegexOptions.Compiled);
        private static readonly Regex SpecialCharsRegex = new(@"[^\w\s-]", RegexOptions.Compiled);
        private static readonly Regex MultipleHyphensRegex = new(@"-+", RegexOptions.Compiled);

        /// <summary>
        /// Converts string to slug format (URL-friendly)
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Slug string</returns>
        public static string ToSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Convert to lowercase and remove diacritics
            var slug = RemoveDiacritics(input.ToLowerInvariant());

            // Replace whitespace with hyphens
            slug = WhitespaceRegex.Replace(slug, "-");

            // Remove special characters
            slug = SpecialCharsRegex.Replace(slug, "");

            // Replace multiple hyphens with single hyphen
            slug = MultipleHyphensRegex.Replace(slug, "-");

            // Trim hyphens from start and end
            slug = slug.Trim('-');

            return slug;
        }

        /// <summary>
        /// Removes diacritics (accents) from string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>String without diacritics</returns>
        public static string RemoveDiacritics(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Truncates string to specified length with ellipsis
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="ellipsis">Ellipsis string (default: "...")</param>
        /// <returns>Truncated string</returns>
        public static string Truncate(string input, int maxLength, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input ?? string.Empty;

            if (maxLength <= ellipsis.Length)
                return ellipsis.Substring(0, maxLength);

            return input.Substring(0, maxLength - ellipsis.Length) + ellipsis;
        }

        /// <summary>
        /// Capitalizes first letter of each word
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Title case string</returns>
        public static string ToTitleCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLowerInvariant());
        }

        /// <summary>
        /// Capitalizes first letter of string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Capitalized string</returns>
        public static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (input.Length == 1)
                return input.ToUpperInvariant();

            return char.ToUpperInvariant(input[0]) + input.Substring(1).ToLowerInvariant();
        }

        /// <summary>
        /// Converts string to camelCase
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>CamelCase string</returns>
        public static string ToCamelCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var words = input.Split(new[] { ' ', '_', '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
                return string.Empty;

            var result = words[0].ToLowerInvariant();
            for (int i = 1; i < words.Length; i++)
            {
                result += Capitalize(words[i]);
            }

            return result;
        }

        /// <summary>
        /// Converts string to PascalCase
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>PascalCase string</returns>
        public static string ToPascalCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var words = input.Split(new[] { ' ', '_', '-' }, StringSplitOptions.RemoveEmptyEntries);
            var result = string.Empty;

            foreach (var word in words)
            {
                result += Capitalize(word);
            }

            return result;
        }

        /// <summary>
        /// Masks sensitive data in string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="visibleStart">Number of characters to show at start</param>
        /// <param name="visibleEnd">Number of characters to show at end</param>
        /// <param name="maskChar">Character to use for masking</param>
        /// <returns>Masked string</returns>
        public static string MaskSensitiveData(string input, int visibleStart = 2, int visibleEnd = 2, char maskChar = '*')
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (input.Length <= visibleStart + visibleEnd)
                return new string(maskChar, input.Length);

            var start = input.Substring(0, visibleStart);
            var end = input.Substring(input.Length - visibleEnd);
            var maskLength = input.Length - visibleStart - visibleEnd;
            var mask = new string(maskChar, maskLength);

            return start + mask + end;
        }

        /// <summary>
        /// Generates random string with specified length and character set
        /// </summary>
        /// <param name="length">String length</param>
        /// <param name="useUppercase">Include uppercase letters</param>
        /// <param name="useLowercase">Include lowercase letters</param>
        /// <param name="useNumbers">Include numbers</param>
        /// <param name="useSpecialChars">Include special characters</param>
        /// <returns>Random string</returns>
        public static string GenerateRandomString(
            int length,
            bool useUppercase = true,
            bool useLowercase = true,
            bool useNumbers = true,
            bool useSpecialChars = false)
        {
            if (length <= 0)
                return string.Empty;

            var chars = new StringBuilder();

            if (useUppercase) chars.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            if (useLowercase) chars.Append("abcdefghijklmnopqrstuvwxyz");
            if (useNumbers) chars.Append("0123456789");
            if (useSpecialChars) chars.Append("!@#$%^&*()_+-=[]{}|;:,.<>?");

            if (chars.Length == 0)
                throw new ArgumentException("At least one character set must be enabled");

            var charArray = chars.ToString().ToCharArray();
            var random = new Random();
            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(charArray[random.Next(charArray.Length)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Checks if string is null, empty, or whitespace
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>True if null, empty, or whitespace</returns>
        public static bool IsNullOrWhiteSpace(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Safely trims string, returns empty string if null
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Trimmed string or empty string</returns>
        public static string SafeTrim(string input)
        {
            return input?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Joins strings with separator, ignoring null or empty values
        /// </summary>
        /// <param name="separator">Separator string</param>
        /// <param name="values">Values to join</param>
        /// <returns>Joined string</returns>
        public static string JoinNonEmpty(string separator, params string[] values)
        {
            if (values == null || values.Length == 0)
                return string.Empty;

            var nonEmptyValues = values.Where(v => !string.IsNullOrWhiteSpace(v));
            return string.Join(separator, nonEmptyValues);
        }

        /// <summary>
        /// Compares strings ignoring case and culture
        /// </summary>
        /// <param name="str1">First string</param>
        /// <param name="str2">Second string</param>
        /// <returns>True if strings are equal ignoring case</returns>
        public static bool EqualsIgnoreCase(string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Checks if string contains value ignoring case
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="value">Value to search for</param>
        /// <returns>True if contains value ignoring case</returns>
        public static bool ContainsIgnoreCase(string source, string value)
        {
            if (source == null || value == null)
                return false;

            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        /// <summary>
        /// Generates a random code with alphanumeric characters
        /// </summary>
        /// <param name="length">Length of the code</param>
        /// <returns>Random code string</returns>
        public static string GenerateCode(int length = 6)
        {
            return GenerateRandomString(length, useUppercase: true, useLowercase: false, useNumbers: true, useSpecialChars: false);
        }
    }
}