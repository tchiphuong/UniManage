using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManage.Core.Models;

namespace UniManage.Core.Helpers
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Validates a phone number format
        /// </summary>
        /// <param name="phoneNumber">Phone number to validate</param>
        /// <returns>True if phone number is valid or empty, false otherwise</returns>
        public static bool IsValidPhoneNumber(string? phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return true;

            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+?[0-9]{10,15}$");
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
