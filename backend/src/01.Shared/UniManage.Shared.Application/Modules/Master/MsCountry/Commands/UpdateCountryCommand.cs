using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.MsCountry.Commands
{
    #region Command

    /// <summary>
    /// Command to update an existing country
    /// </summary>
    public sealed class UpdateCountryCommand : BaseCommand, IRequest<ApiResponse<UpdateCountryCommand.Response>>
    {
        public string Code { get; set; } = default!;
        public string NameVi { get; init; } = default!;
        public string NameEn { get; init; } = default!;
        public string? FullNameVi { get; init; }
        public string? FullNameEn { get; init; }
        public string? PhoneCode { get; init; }
        public int SortOrder { get; init; } = 0;
        public bool IsActive { get; init; } = true;

        public sealed class Response
        {
            public bool Success { get; init; }
            public string Code { get; init; } = default!;
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for UpdateCountryCommand
    /// </summary>
    public sealed class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MaximumLength(20).WithMessage(string.Format(CoreResource.validation_maxLength, 20));

            RuleFor(x => x.NameVi)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MaximumLength(100).WithMessage(string.Format(CoreResource.validation_maxLength, 100));

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MaximumLength(100).WithMessage(string.Format(CoreResource.validation_maxLength, 100));

            RuleFor(x => x.FullNameVi)
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, 200));

            RuleFor(x => x.FullNameEn)
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, 200));

            RuleFor(x => x.PhoneCode)
                .MaximumLength(20).WithMessage(string.Format(CoreResource.validation_maxLength, 20));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for UpdateCountryCommand
    /// </summary>
    public sealed class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, ApiResponse<UpdateCountryCommand.Response>>
    {
        public async Task<ApiResponse<UpdateCountryCommand.Response>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            ApiLogModel logData = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Code), request.Code),
                    new LogParamModel(nameof(request.NameVi), request.NameVi),
                    new LogParamModel(nameof(request.NameEn), request.NameEn)
                }
            };

            using var dbContext = new DbContext(openTransaction: true);
            var country = await dbContext.Set<AdCountries>()
                .FirstOrDefaultAsync(x => x.Code == request.Code, cancellationToken);

            if (country == null)
            {
                return ResponseHelper.NotFound<UpdateCountryCommand.Response>(string.Format(CoreResource.common_notFound, CoreResource.entity_country));
            }

            country.NameVi = request.NameVi;
            country.NameEn = request.NameEn;
            country.FullNameVi = request.FullNameVi;
            country.FullNameEn = request.FullNameEn;
            country.CodeName = StringHelper.ToSlug(request.NameEn);
            country.PhoneCode = request.PhoneCode;
            country.SortOrder = request.SortOrder;
            country.IsActive = request.IsActive;
            country.UpdatedAt = DateTimeHelper.Now;

            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.CommitAsync();

            var responseData = new UpdateCountryCommand.Response { Success = true, Code = request.Code };
            var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_updateSuccess, CoreResource.entity_country));
            logData.ReturnCode = response.ReturnCode;
            return response;
        }
    }

    #endregion
}





