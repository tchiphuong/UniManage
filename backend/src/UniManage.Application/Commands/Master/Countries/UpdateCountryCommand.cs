using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Master.Countries
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
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
                .MaximumLength(20).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 20));

            RuleFor(x => x.NameVi)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
                .MaximumLength(100).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 100));

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
                .MaximumLength(100).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 100));

            RuleFor(x => x.FullNameVi)
                .MaximumLength(200).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 200));

            RuleFor(x => x.FullNameEn)
                .MaximumLength(200).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 200));

            RuleFor(x => x.PhoneCode)
                .MaximumLength(20).WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 20));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for UpdateCountryCommand
    /// </summary>
    public sealed class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, ApiResponse<UpdateCountryCommand.Response>>
    {
        public async Task<ApiResponse<UpdateCountryCommand.Response>> Handle(UpdateCountryCommand request, CancellationToken ct)
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

            using (DbContext dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var codeName = StringHelper.ToSlug(request.NameEn);

                    var rowsAffected = await dbContext.ExecuteAsync(
                        @"UPDATE ad_countries
                      SET NameVi = @NameVi,
                          NameEn = @NameEn,
                          FullNameVi = @FullNameVi,
                          FullNameEn = @FullNameEn,
                          CodeName = @CodeName,
                          PhoneCode = @PhoneCode,
                          SortOrder = @SortOrder,
                          IsActive = @IsActive,
                          UpdatedAt = GETDATE()
                      WHERE Code = @Code",
                        new
                        {
                            request.Code,
                            request.NameVi,
                            request.NameEn,
                            request.FullNameVi,
                            request.FullNameEn,
                            CodeName = codeName,
                            request.PhoneCode,
                            request.SortOrder,
                            request.IsActive
                        },
                        ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync();
                        var notFoundResponse = ResponseHelper.NotFound<UpdateCountryCommand.Response>(CoreResource.Country_msg_NotFound);
                        logData.ReturnCode = notFoundResponse.ReturnCode;
                        logData.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(logData);
                        return notFoundResponse;
                    }

                    await dbContext.CommitAsync();

                    var responseData = new UpdateCountryCommand.Response { Success = true, Code = request.Code };
                    var response = ResponseHelper.Success(responseData, CoreResource.Country_msg_UpdateSuccess);
                    logData.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(logData);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error updating country: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateCountryCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
                    logData.Message = ex.ToString();
                    logData.IsException = 1;
                    logData.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(logData);

                    return response;
                }
            }
        }
    }

    #endregion
}
