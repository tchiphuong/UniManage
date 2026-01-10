using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Roles
{
    public sealed class CreateRoleCommand : BaseCommand, IRequest<ApiResponse<CreateRoleCommand.Response>>
    {
        public string RoleCode { get; init; } = default!;
        public string RoleName { get; init; } = default!;
        public string? Description { get; init; }
        public byte IsActive { get; init; } = 1;

        public sealed class Response
        {
            public int Id { get; init; }
        }
    }

    public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleCode)
                .NotEmpty().WithMessage("Role code is required")
                .Length(2, 50).WithMessage("Role code must be between 2 and 50 characters")
                .Must(ValidationHelper.IsValidUserCode).WithMessage("Role code allows only alphanumeric and underscore")
                .MustAsync(async (code, cancel) => !await IsRoleCodeExistsAsync(code))
                .WithMessage("Role code already exists");

            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required")
                .Length(2, 200).WithMessage("Role name must be between 2 and 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

            RuleFor(x => x.IsActive)
                .InclusiveBetween((byte)0, (byte)1).WithMessage("IsActive must be 0 or 1");
        }

        private static async Task<bool> IsRoleCodeExistsAsync(string roleCode)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_roles WHERE RoleCode = @RoleCode) THEN 1 ELSE 0 END",
                    new { RoleCode = roleCode });
            }
        }
    }

    public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApiResponse<CreateRoleCommand.Response>>
    {
        public async Task<ApiResponse<CreateRoleCommand.Response>> Handle(CreateRoleCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.RoleCode), request.RoleCode),
                    new CoreParamModel(nameof(request.RoleName), request.RoleName),
                    new CoreParamModel(nameof(request.IsActive), request.IsActive)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        INSERT INTO sy_roles (RoleCode, RoleName, Description, IsActive, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                        VALUES (@RoleCode, @RoleName, @Description, @IsActive, @CreatedBy, GETDATE(), @CreatedBy, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<int>(sql, new
                    {
                        request.RoleCode,
                        request.RoleName,
                        request.Description,
                        request.IsActive,
                        CreatedBy = request.HeaderInfo!.Username
                    }, ct);

                    await dbContext.CommitAsync(ct);

                    var responseData = new CreateRoleCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_CreateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error creating role: {ex.Message}", ex);

                    var response = ResponseHelper.Error<CreateRoleCommand.Response>("Error occurred while creating role");

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
