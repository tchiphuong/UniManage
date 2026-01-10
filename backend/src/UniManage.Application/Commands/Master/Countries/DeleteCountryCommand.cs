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
    /// Command to delete countries
    /// </summary>
    public sealed class DeleteCountryCommand : BaseCommand, IRequest<ApiResponse<DeleteCountryCommand.Response>>
    {
        public List<string> Codes { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
            public int DeletedCount { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for DeleteCountryCommand
    /// </summary>
    public sealed class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(x => x.Codes)
                .NotEmpty().WithMessage(CoreResource.Validation_msg_Required)
                .Must(codes => codes.All(c => !string.IsNullOrEmpty(c)))
                .WithMessage("All codes must be non-empty");
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for DeleteCountryCommand
    /// </summary>
    public sealed class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, ApiResponse<DeleteCountryCommand.Response>>
    {
        public async Task<ApiResponse<DeleteCountryCommand.Response>> Handle(DeleteCountryCommand request, CancellationToken ct)
        {
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Codes), string.Join(", ", request.Codes))
            }
            };

            using (DbContext dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var deletedCount = await dbContext.ExecuteAsync(
                        "DELETE FROM ad_countries WHERE Code IN @Codes",
                        new { Codes = request.Codes },
                        ct);

                    await dbContext.CommitAsync();

                    var responseData = new DeleteCountryCommand.Response { Success = true, DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, string.Format(CoreResource.Country_msg_DeleteSuccess, deletedCount));

                    logData.Result = response.Data;
                    logData.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(logData);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    UniLogger.Error($"Error deleting countries: {ex.Message}", ex);

                    var response = ResponseHelper.Error<DeleteCountryCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
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
