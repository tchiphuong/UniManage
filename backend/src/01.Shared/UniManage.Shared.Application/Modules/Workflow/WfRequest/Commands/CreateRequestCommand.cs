using Newtonsoft.Json;

namespace UniManage.Shared.Application.Modules.WfRequest.Commands
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
                .Must(key => CoreCommon.Value.Requesttype.All.Contains(key))
                .WithMessage("Invalid or inactive request type");

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
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM HrEmployees WHERE EmployeeCode = @EmployeeCode) THEN 1 ELSE 0 END",
                    new { EmployeeCode = employeeCode });
            }
        }
    }

    public sealed class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, ApiResponse<CreateRequestCommand.Response>>
    {
        public async Task<ApiResponse<CreateRequestCommand.Response>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
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
                    }, cancellationToken);

                    

                    var responseData = new CreateRequestCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);
return response;
        }
    }
}




