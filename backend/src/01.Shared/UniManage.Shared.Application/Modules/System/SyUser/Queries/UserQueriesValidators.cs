using FluentValidation;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyUser.Queries
{
    /// <summary>
    /// Data validator for user list query
    /// </summary>
    public sealed class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
    {
        public GetUserListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_invalidFormat, "PageIndex"));

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage(string.Format(CoreResource.validation_maxLength, "PageSize", 100));
        }
    }

    /// <summary>
    /// Validator for GetUserByIdQuery
    /// </summary>
    public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Uuid)
                .NotEmpty().WithMessage("Uuid must not be empty");
        }
    }

    /// <summary>
    /// Validator for GetUserRolesQuery
    /// </summary>
    public sealed class GetUserRolesQueryValidator : AbstractValidator<GetUserRolesQuery>
    {
        public GetUserRolesQueryValidator()
        {
            RuleFor(x => x.Uuid)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "Uuid"));
        }
    }
}
