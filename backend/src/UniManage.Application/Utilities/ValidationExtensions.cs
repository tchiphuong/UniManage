using FluentValidation;
using UniManage.Core.Utilities;
using UniManage.Resource;

namespace UniManage.Application.Utilities
{
    /// <summary>
    /// Custom FluentValidation extensions for UniManage
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Unified password security policy rule.
        /// Enforces complexity (H4) and length requirements using PasswordHelper.
        /// </summary>
        /// <typeparam name="T">Command/Query type</typeparam>
        /// <param name="ruleBuilder">The rule builder</param>
        /// <param name="fieldName">Resource string for the field name (e.g. "Password")</param>
        /// <returns>Modified rule builder</returns>
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, fieldName))
                .MinimumLength(PasswordHelper.MinPasswordLength).WithMessage(string.Format(CoreResource.validation_minLength, fieldName, PasswordHelper.MinPasswordLength))
                .MaximumLength(PasswordHelper.MaxPasswordLength).WithMessage(string.Format(CoreResource.validation_maxLength, fieldName, PasswordHelper.MaxPasswordLength))
                .Must(PasswordHelper.IsValidPassword).WithMessage(CoreResource.validation_passwordComplexity);
        }
    }
}
