using FluentValidation.Results;
using UniManage.Core.Models;

namespace UniManage.Core.Extensions
{
    public static class ValidationResultExtensions
    {
        public static List<FieldErrorModel> ToFieldErrorModels(this ValidationResult validationResult)
        {
            return validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .Select(g => new FieldErrorModel(
                    field: g.Key,
                    messages: g.Select(e => e.ErrorMessage).ToList()
                ))
                .ToList();
        }
    }
}