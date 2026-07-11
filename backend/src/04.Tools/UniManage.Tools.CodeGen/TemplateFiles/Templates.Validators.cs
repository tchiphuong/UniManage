using System.Collections.Generic;
using System.Linq;

namespace UniManage.Tools.CodeGen
{
    public static partial class Templates
    {
        private static string GenerateSmartRules(string screenCode, List<ColumnInfo> columns)
        {
            var rules = new List<string>();

            foreach (var c in columns)
            {
                if (c == null) continue;

                var ruleBuilder = $"            RuleFor(x => x.{c.Name})";
                bool hasDependentRules = false;
                var dependentRules = new List<string>();

                // 1. Basic Required check
                if (!c.IsNullable && c.CSharpType == "string")
                {
                    ruleBuilder += $"\n                .NotEmpty()\n                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.{ToResourceProp(screenCode, "lbl", c.Name)}))";
                    hasDependentRules = true;
                }
                else if (!c.IsNullable)
                {
                    ruleBuilder += $"\n                .NotNull()\n                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.{ToResourceProp(screenCode, "lbl", c.Name)}))";
                }

                // 2. Length check (only for strings)
                if (c.MaxLength > 0 && c.CSharpType == "string")
                {
                    string lengthRule = $@"                    RuleFor(x => x.{c.Name})
                        .MaximumLength({c.MaxLength})
                        .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.{ToResourceProp(screenCode, "lbl", c.Name)}, {c.MaxLength}))";
                    dependentRules.Add(lengthRule);
                }

                // 3. Smart detection based on Column Name
                var lowerName = c.Name.ToLower();

                if (lowerName.Contains("email"))
                {
                    dependentRules.Add($@"                    RuleFor(x => x.{c.Name})
                        .Must(ValidationHelper.IsValidEmail)
                        .WithMessage(CoreResource.validation_invalidEmail)");
                    hasDependentRules = true;
                }
                else if (lowerName.Contains("phone"))
                {
                    dependentRules.Add($@"                    RuleFor(x => x.{c.Name})
                        .Must(ValidationHelper.IsValidPhoneNumber)
                        .WithMessage(CoreResource.validation_invalidPhone)");
                    hasDependentRules = true;
                }
                else if (lowerName.EndsWith("status") || lowerName.EndsWith("type"))
                {
                    if (c.CSharpType == "byte" || c.CSharpType == "int")
                    {
                        dependentRules.Add($@"                    RuleFor(x => x.{c.Name})
                        .Must(val => val == 0 || val == 1)
                        .WithMessage(CoreResource.validation_invalidStatus)");
                        hasDependentRules = true;
                    }
                    else if (c.CSharpType == "string")
                    {
                        dependentRules.Add($@"                    RuleFor(x => x.{c.Name})
                        .Must(val => CoreCommon.Value.{c.Name}.All.Contains(val))
                        .WithMessage(CoreResource.validation_invalidStatus)");
                        hasDependentRules = true;
                    }
                }
                else if (lowerName.Contains("code") || lowerName.Contains("username"))
                {
                    // For codes/usernames, often alphanumeric
                    dependentRules.Add($@"                    RuleFor(x => x.{c.Name})
                        .Must(ValidationHelper.IsValidUserCode)
                        .WithMessage(CoreResource.validation_alphanumericOnly)");
                    hasDependentRules = true;
                }
                else if (lowerName.Contains("password"))
                {
                    dependentRules.Add($@"                    RuleFor(x => x.{c.Name})
                        .MinimumLength(PasswordHelper.MinPasswordLength).WithMessage(string.Format(CoreResource.validation_minLength, CoreResource.lbl_password, PasswordHelper.MinPasswordLength))
                        .Must(PasswordHelper.IsValidPassword).WithMessage(CoreResource.validation_passwordComplexity)");
                    hasDependentRules = true;
                }

                // Assemble the rule
                if (dependentRules.Any() && hasDependentRules)
                {
                    ruleBuilder += "\n                .DependentRules(() =>\n                {\n";
                    ruleBuilder += string.Join(";\n\n", dependentRules) + ";\n                })";
                }
                else if (dependentRules.Any())
                {
                    // Just append directly if no initial NotEmpty/NotNull is present but length check exists
                    ruleBuilder += "\n" + string.Join("\n", dependentRules).Replace("                    RuleFor", "                .MaximumLength").Replace("x => x." + c.Name + ")", "").TrimEnd();
                }

                ruleBuilder += ";";
                rules.Add(ruleBuilder);
            }

            return string.Join("\n\n", rules);
        }

        public static string GetValidatorsTemplate(string module, string entity, string screenCode, List<ColumnInfo> allColumns, List<string> editCols)
        {
            var editColumns = editCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .OfType<ColumnInfo>()
                .ToList();

            var smartRules = GenerateSmartRules(screenCode, editColumns);

            return $@"using FluentValidation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Resource;
using UniManage.Shared.Application.Models;
using UniManage.Modules.{module}.Application.Commands.{entity};
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;
using UniManage.Modules.{module}.Domain;

namespace UniManage.Modules.{module}.Application.Validators.{entity}
{{
    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh tạo mới {entity}
    /// </summary>
    public sealed class Create{entity}Validator : AbstractValidator<Create{entity}Command>
    {{
        public Create{entity}Validator()
        {{
{smartRules}
        }}
    }}

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh cập nhật {entity}
    /// </summary>
    public sealed class Update{entity}Validator : AbstractValidator<Update{entity}Command>
    {{
        public Update{entity}Validator()
        {{
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(string.Format(CoreResource.validation_required, ""Id""));

{smartRules}
        }}
    }}
}}";
        }
    }
}
