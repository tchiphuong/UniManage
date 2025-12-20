using System.Text.RegularExpressions;
using FluentValidation.Results;
using UniManage.Model.Common;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Common validation utility functions
    /// </summary>
    public static class ValidationHelper
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex PhoneRegex = new Regex(
            @"^(\+84|84|0)[3|5|7|8|9][0-9]{8}$",
            RegexOptions.Compiled);

        /// <summary>
        /// Validate email format
        /// </summary>
        /// <param name="email">Email to validate</param>
        /// <returns>True if valid email format</returns>
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (email.Length > 254) // RFC 5321 limit
                return false;

            return EmailRegex.IsMatch(email);
        }

        /// <summary>
        /// Validate Vietnamese phone number format
        /// </summary>
        /// <param name="phone">Phone number to validate</param>
        /// <returns>True if valid phone format</returns>
        public static bool IsValidPhoneNumber(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Remove spaces and dashes
            var cleanPhone = phone.Replace(" ", "").Replace("-", "");

            return PhoneRegex.IsMatch(cleanPhone);
        }

        /// <summary>
        /// Check if string contains only alphanumeric characters and allowed special chars
        /// </summary>
        /// <param name="input">String to validate</param>
        /// <param name="allowedSpecialChars">Allowed special characters (default: underscore, dash)</param>
        /// <returns>True if valid</returns>
        public static bool IsAlphanumeric(string? input, string allowedSpecialChars = "_-")
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" + allowedSpecialChars;

            return input.All(c => allowedChars.Contains(c));
        }

        /// <summary>
        /// Validate user code format (alphanumeric, 3-50 chars)
        /// </summary>
        /// <param name="userCode">User code to validate</param>
        /// <returns>True if valid user code</returns>
        public static bool IsValidUserCode(string? userCode)
        {
            if (string.IsNullOrWhiteSpace(userCode))
                return false;

            if (userCode.Length < 3 || userCode.Length > 50)
                return false;

            return IsAlphanumeric(userCode);
        }

        /// <summary>
        /// Validate employee code format
        /// </summary>
        /// <param name="employeeCode">Employee code to validate</param>
        /// <returns>True if valid employee code</returns>
        public static bool IsValidEmployeeCode(string? employeeCode)
        {
            if (string.IsNullOrWhiteSpace(employeeCode))
                return false;

            if (employeeCode.Length < 3 || employeeCode.Length > 20)
                return false;

            return IsAlphanumeric(employeeCode);
        }

        /// <summary>
        /// Check if string length is within range
        /// </summary>
        /// <param name="input">String to check</param>
        /// <param name="minLength">Minimum length</param>
        /// <param name="maxLength">Maximum length</param>
        /// <returns>True if length is valid</returns>
        public static bool IsValidLength(string? input, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return minLength == 0;

            return input.Length >= minLength && input.Length <= maxLength;
        }

        /// <summary>
        /// Normalize string (trim, remove extra spaces)
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Normalized string</returns>
        public static string? NormalizeString(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            return Regex.Replace(input.Trim(), @"\s+", " ");
        }

        /// <summary>
        /// Chuyển danh sách ValidationFailure thành List<FieldErrorModel> gom nhiều lỗi theo field.
        /// </summary>
        /// <param name="failures">Danh sách ValidationFailure</param>
        /// <returns>Danh sách FieldErrorModel với nhiều message trên 1 field</returns>
        public static List<FieldErrorModel> ToFieldErrorModels(this IList<ValidationFailure> failures)
        {
            if (failures == null || failures.Count == 0)
                return new List<FieldErrorModel>();

            return failures
                .GroupBy(f => f.PropertyName)
                .Select(g => new FieldErrorModel(g.Key, g.Select(f => f.ErrorMessage).ToList()))
                .ToList();
        }
    }
}
