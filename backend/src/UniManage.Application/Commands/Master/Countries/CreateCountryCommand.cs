using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.Master.Countries
{
    #region Command

    /// <summary>
    /// Command to create a new country
    /// </summary>
    public sealed class CreateCountryCommand : BaseCommand, IRequest<ApiResponse<CreateCountryCommand.Response>>
    {
        public string Code { get; init; } = default!;
        public string NameVi { get; init; } = default!;
        public string NameEn { get; init; } = default!;
        public string? FullNameVi { get; init; }
        public string? FullNameEn { get; init; }
        public string? PhoneCode { get; init; }
        public int SortOrder { get; init; } = 0;
        public bool IsActive { get; init; } = true;

        public sealed class Response
        {
            public bool Success => !string.IsNullOrEmpty(Code);
            public string Code { get; init; } = default!;
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for CreateCountryCommand
    /// </summary>
    public sealed class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .MaximumLength(20).WithMessage(string.Format(CoreResource.validation_maxLength, 20))
                .Matches("^[A-Z]+$").WithMessage(CoreResource.validation_uppercaseAlphanumericOnly)
                .MustAsync(async (code, cancel) => !await IsCodeExistsAsync(code))
                .WithMessage(CoreResource.validation_alreadyExists);

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

        private static async Task<bool> IsCodeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<ad_countries>().AnyAsync(x => x.Code == code);
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for CreateCountryCommand
    /// </summary>
    public sealed class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, ApiResponse<CreateCountryCommand.Response>>
    {
        public async Task<ApiResponse<CreateCountryCommand.Response>> Handle(CreateCountryCommand request, CancellationToken ct)
        {
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Code), request.Code),
                    new CoreParamModel(nameof(request.NameVi), request.NameVi),
                    new CoreParamModel(nameof(request.NameEn), request.NameEn)
                }
            };

            using var dbContext = new DbContext(openTransaction: true);
            var country = new ad_countries
            {
                Code = request.Code,
                NameVi = request.NameVi,
                NameEn = request.NameEn,
                FullNameVi = request.FullNameVi,
                FullNameEn = request.FullNameEn,
                CodeName = StringHelper.ToSlug(request.NameEn),
                PhoneCode = request.PhoneCode,
                SortOrder = request.SortOrder,
                IsActive = request.IsActive,
                CreatedAt = DateTimeHelper.Now
            };

            dbContext.Set<ad_countries>().Add(country);
            await dbContext.SaveChangesAsync(ct);
            await dbContext.CommitAsync();

            var response = ResponseHelper.Success(new CreateCountryCommand.Response { Code = country.Code }, string.Format(CoreResource.common_createSuccess, CoreResource.entity_country));
            logData.ReturnCode = response.ReturnCode;
            return response;
        }
    }

    #endregion
}
