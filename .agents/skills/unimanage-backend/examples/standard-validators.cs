// =============================================================================
// STANDARD VALIDATORS TEMPLATE — UniManage Consolidated Pattern
// =============================================================================
// Rules:
//   - Group all validators for a module into ONE file per type:
//     * {Module}QueriesValidators.cs  (for all Query validators)
//     * {Module}CommandValidators.cs  (for all Command validators)
//   - Use CoreResource for ALL labels and error messages
//   - Use DependentRules() for complex validation chains
//   - Existence checks: EF Core AnyAsync() (NOT raw SQL)
// =============================================================================

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.{Module}.Queries;

// =============================================================================
// FILE: ItemQueriesValidators.cs
// =============================================================================

/// <summary>
/// Validator for GetItemListQuery
/// </summary>
public sealed class GetItemListQueryValidator : AbstractValidator<GetItemListQuery>
{
    public GetItemListQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .WithMessage(string.Format(CoreResource.validation_invalidFormat, "PageIndex"));

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage(string.Format(CoreResource.validation_maxLength, "PageSize", 100));
    }
}

/// <summary>
/// Validator for GetItemByIdQuery
/// </summary>
public sealed class GetItemByIdQueryValidator : AbstractValidator<GetItemByIdQuery>
{
    public GetItemByIdQueryValidator()
    {
        RuleFor(x => x.Uuid)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, "Uuid"));
    }
}


// =============================================================================
// FILE: ItemCommandValidators.cs
// =============================================================================

namespace UniManage.Shared.Application.Modules.{Module}.Commands;

/// <summary>
/// Validator for CreateItemCommand
/// </summary>
public sealed class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        // Name: required + length + uniqueness check
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name))
            .DependentRules(() =>
            {
                RuleFor(x => x.Name)
                    .Length(3, 100)
                    .WithMessage(string.Format(CoreResource.validation_length, CoreResource.lbl_name, 3, 100))
                    .MustAsync(async (name, cancel) => !await IsNameExistsAsync(name))
                    .WithMessage(string.Format(CoreResource.validation_alreadyExists, CoreResource.lbl_name));
            });

        // Status: required + must be valid value from CoreCommon
        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_status))
            .DependentRules(() =>
            {
                RuleFor(x => x.Status)
                    .Must(status => CoreCommon.Value.Commonstatus.All.Contains(status))
                    .WithMessage(CoreResource.validation_invalidStatus);
            });
    }

    /// <summary>
    /// Check if item name already exists in database
    /// </summary>
    private static async Task<bool> IsNameExistsAsync(string name)
    {
        // Use EF Core AnyAsync for existence checks (NOT raw SQL)
        using (var dbContext = new DbContext())
        {
            return await dbContext.Set<ItemEntity>()
                .AnyAsync(x => x.Name == name);
        }
    }
}

/// <summary>
/// Validator for UpdateItemCommand
/// </summary>
public sealed class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(x => x.Uuid)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, "Uuid"));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name))
            .Length(3, 100)
            .WithMessage(string.Format(CoreResource.validation_length, CoreResource.lbl_name, 3, 100));

        RuleFor(x => x.RowVersion)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, "RowVersion"));
    }
}

/// <summary>
/// Validator for DeleteItemCommand
/// </summary>
public sealed class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandValidator()
    {
        RuleFor(x => x.Uuids)
            .NotEmpty()
            .WithMessage("At least one UUID is required for deletion.");
    }
}
