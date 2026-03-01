using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Workflow.Requests
{
    public sealed class CreateRequestCommand : BaseCommand, IRequest<ApiResponse<CreateRequestCommand.Response>>
    {
        public string RequestTypeKey { get; init; } = default!;
        public string ApplicantEmployeeCode { get; init; } = default!;
        public string ApplicantUsername { get; init; } = default!;
        public object? RequestData { get; init; }

        public sealed class Response
        {
            public int Id { get; init; }
        }
    }

    public sealed class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.RequestTypeKey)
                .NotEmpty().WithMessage("Request type key is required")
                .MaximumLength(50).WithMessage("Request type key must not exceed 50 characters");

            RuleFor(x => x.ApplicantEmployeeCode)
                .NotEmpty().WithMessage("Applicant employee code is required")
                .MustAsync(async (code, cancel) => await IsEmployeeExistsAsync(code))
                .WithMessage("Employee does not exist");

            RuleFor(x => x.ApplicantUsername)
                .NotEmpty().WithMessage("Applicant username is required")
                .MaximumLength(100).WithMessage("Applicant username must not exceed 100 characters");
        }

        private static async Task<bool> IsEmployeeExistsAsync(string employeeCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM hr_employees WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                    new { EmployeeCode = employeeCode });
            }
        }
    }

    public sealed class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, ApiResponse<CreateRequestCommand.Response>>
    {
        public async Task<ApiResponse<CreateRequestCommand.Response>> Handle(CreateRequestCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.RequestTypeKey), request.RequestTypeKey),
                    new CoreParamModel(nameof(request.ApplicantEmployeeCode), request.ApplicantEmployeeCode),
                    new CoreParamModel(nameof(request.ApplicantUsername), request.ApplicantUsername)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var requestDataJson = request.RequestData != null ? JsonConvert.SerializeObject(request.RequestData) : null;

                    var sql = @"
                        INSERT INTO wf_request (RequestTypeKey, ApplicantEmployeeCode, ApplicantUsername, Status, RequestData, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                        VALUES (@RequestTypeKey, @ApplicantEmployeeCode, @ApplicantUsername, @Status, @RequestData, @CreatedBy, GETDATE(), @CreatedBy, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<int>(sql, new
                    {
                        request.RequestTypeKey,
                        request.ApplicantEmployeeCode,
                        request.ApplicantUsername,
                        Status = "Draft",
                        RequestData = requestDataJson,
                        CreatedBy = request.HeaderInfo!.Username
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateRequestCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.crud_createSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error creating request: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateRequestCommand.Response>("Error occurred while creating request");

                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}
