using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using UniManage.Shared.Application.Models;

namespace UniManage.Shared.Infrastructure.Utilities
{
    /// <summary>
    /// Shared validation utility functions.
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
        /// Validates if a string matches a standard email format.
        /// </summary>
        /// <param name="email">Email string to validate.</param>
        /// <returns>True if the email format is valid.</returns>
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (email.Length > 254) // RFC 5321 limit
                return false;

            return EmailRegex.IsMatch(email);
        }

        /// <summary>
        /// Validates if a string matches the Vietnamese phone number format.
        /// </summary>
        /// <param name="phone">Phone number string to validate.</param>
        /// <returns>True if the phone format is valid.</returns>
        public static bool IsValidPhoneNumber(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Remove spaces and dashes
            var cleanPhone = phone.Replace(" ", "").Replace("-", "");

            return PhoneRegex.IsMatch(cleanPhone);
        }

        /// <summary>
        /// Checks if a string contains only alphanumeric characters and allowed special characters.
        /// </summary>
        /// <param name="input">Input string to validate.</param>
        /// <param name="allowedSpecialChars">Allowed special characters (default: underscore, dash).</param>
        /// <returns>True if the string is valid alphanumeric.</returns>
        public static bool IsAlphanumeric(string? input, string allowedSpecialChars = "_-")
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" + allowedSpecialChars;

            return input.All(c => allowedChars.Contains(c));
        }

        /// <summary>
        /// Validates the format for a user code (3-50 alphanumeric characters).
        /// </summary>
        /// <param name="userCode">User code string to validate.</param>
        /// <returns>True if the user code is valid.</returns>
        public static bool IsValidUserCode(string? userCode)
        {
            if (string.IsNullOrWhiteSpace(userCode))
                return false;

            if (userCode.Length < 3 || userCode.Length > 50)
                return false;

            return IsAlphanumeric(userCode);
        }

        /// <summary>
        /// Validates the format for an employee code.
        /// </summary>
        /// <param name="employeeCode">Employee code string to validate.</param>
        /// <returns>True if the employee code is valid.</returns>
        public static bool IsValidEmployeeCode(string? employeeCode)
        {
            if (string.IsNullOrWhiteSpace(employeeCode))
                return false;

            if (employeeCode.Length < 3 || employeeCode.Length > 20)
                return false;

            return IsAlphanumeric(employeeCode);
        }

        /// <summary>
        /// Checks if the length of a string is within the specified range.
        /// </summary>
        /// <param name="input">Input string to check.</param>
        /// <param name="minLength">Minimum required length.</param>
        /// <param name="maxLength">Maximum allowed length.</param>
        /// <returns>True if the length is within range.</returns>
        public static bool IsValidLength(string? input, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return minLength == 0;

            return input.Length >= minLength && input.Length <= maxLength;
        }

        /// <summary>
        /// Normalizes a string by trimming whitespace and removing extra inner spaces.
        /// </summary>
        /// <param name="input">Input string to normalize.</param>
        /// <returns>The normalized string instance.</returns>
        public static string? NormalizeString(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            return Regex.Replace(input.Trim(), @"\s+", " ");
        }

        /// <summary>
        /// Converts a list of ValidationFailures into a list of FieldErrors.
        /// </summary>
        /// <param name="failures">Collection of failures from FluentValidation.</param>
        /// <returns>A list of models containing mapped field errors.</returns>
        public static List<FieldError> ToFieldErrors(this IList<ValidationFailure> failures)
        {
            if (failures == null || failures.Count == 0)
                return new List<FieldError>();

            return failures
                .GroupBy(f => f.PropertyName)
                .Select(g => new FieldError(g.Key, g.Select(f => f.ErrorMessage).ToList()))
                .ToList();
        }
        /// <summary>
        /// Gets the maximum length of a property from StringLength or MaxLength attributes using Lambda / 
        /// Lấy độ dài tối đa của thuộc tính từ attribute StringLength hoặc MaxLength dùng Lambda
        /// </summary>
        /// <typeparam name="T">Entity class type / Kiểu lớp thực thể</typeparam>
        /// <param name="propertyLambda">Lambda expression (e.g., x => x.Username)</param>
        /// <returns>Maximum length or int.MaxValue if not defined / Độ dài tối đa hoặc int.MaxValue nếu không có định nghĩa</returns>
        public static int GetMaxLength<T>(Expression<Func<T, object?>> propertyLambda)
        {
            MemberExpression? member = propertyLambda.Body as MemberExpression;
            if (member == null && propertyLambda.Body is UnaryExpression unary)
                member = unary.Operand as MemberExpression;

            string? propertyName = member?.Member.Name;
            if (string.IsNullOrEmpty(propertyName)) return int.MaxValue;

            return GetMaxLength<T>(propertyName);
        }

        /// <summary>
        /// Gets the maximum length of a property from StringLength or MaxLength attributes / 
        /// Lấy độ dài tối đa của thuộc tính từ attribute StringLength hoặc MaxLength
        /// </summary>
        /// <typeparam name="T">Entity class type / Kiểu lớp thực thể</typeparam>
        /// <param name="propertyName">Property name / Tên thuộc tính</param>
        /// <returns>Maximum length or int.MaxValue if not defined / Độ dài tối đa hoặc int.MaxValue nếu không có định nghĩa</returns>
        public static int GetMaxLength<T>(string propertyName)
        {
            PropertyInfo? property = typeof(T).GetProperty(propertyName);
            if (property == null) return int.MaxValue;

            // Try StringLengthAttribute
            var stringLength = property.GetCustomAttribute<StringLengthAttribute>();
            if (stringLength is not null) return stringLength.MaximumLength;

            // Try MaxLengthAttribute
            var maxLength = property.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLength is not null) return maxLength.Length;

            return int.MaxValue;
        }
    }
}


