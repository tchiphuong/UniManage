using log4net.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApp.Core.Helpers
{
    public static class ValidatorHelpers
    {
        /// <summary>
        /// Converts a FluentValidation ValidationResult to a list of custom Validation objects.
        /// </summary>
        /// <param name="validationResult">The FluentValidation ValidationResult to convert.</param>
        /// <returns>A list of Validation objects containing field and message information.</returns>
        public static List<Validation> ToErrors(this FluentValidation.Results.ValidationResult validationResult)
        {
            if (validationResult?.Errors?.Any() != true)
            {
                return null;
            }

            return validationResult.Errors.Select(error => new Validation { Field = error.PropertyName, Message = error.ErrorMessage })
            .ToList();
        }

        /// <summary>
        /// Adds errors from the specified collection to the provided ModelStateDictionary.
        /// </summary>
        /// <param name="errors">The collection of errors to add.</param>
        /// <param name="modelState">The ModelStateDictionary to which errors will be added.</param>
        public static void AddErrorsToModelState(this IEnumerable<Validation> errors, ControllerBase controller)
        {
            if (errors != null && errors.Any())
            {
                foreach (var item in errors)
                {
                    controller.ModelState.AddModelError(item.Field, item.Message);
                }
            }
        }

        /// <summary>
        /// Checks whether a string matches a specific code pattern.
        /// </summary>
        /// <param name="code">The string to be checked.</param>
        /// <returns>True if the string matches the specified code pattern; otherwise, false.</returns>
        public static bool IsCode(string code)
        {
            // Code pattern allowing letters, numbers, underscores, and dashes.
            string pattern = "^[a-zA-Z0-9_-]+$";
            return Regex.IsMatch(code, pattern);
        }
    }
}
