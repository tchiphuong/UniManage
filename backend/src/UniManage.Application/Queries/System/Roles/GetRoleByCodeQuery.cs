using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Roles
{
    public sealed class GetRoleByCodeQuery : BaseQuery, IRequest<ApiResponse<GetRoleByCodeQuery.Result>>
    {
        public string RoleCode { get; set; } = default!;

        public sealed class Result
        {
            public int Id { get; set; }
            public string RoleCode { get; set; } = default!;
            public string RoleName { get; set; } = default!;
            public string? Description { get; set; }
            public byte IsActive { get; set; }
            public string CreatedBy { get; set; } = default!;
            public DateTime CreatedAt { get; set; }
            public string UpdatedBy { get; set; } = default!;
            public DateTime UpdatedAt { get; set; }
            public byte[] DataRowVersion { get; set; } = default!;
        }
    }

    public sealed class GetRoleByCodeQueryValidator : AbstractValidator<GetRoleByCodeQuery>
    {
        public GetRoleByCodeQueryValidator()
        {
            RuleFor(x => x.RoleCode)
                .NotEmpty().WithMessage("Role code is required");
        }
    }

    public sealed class GetRoleByCodeQueryHandler : IRequestHandler<GetRoleByCodeQuery, ApiResponse<GetRoleByCodeQuery.Result>>
    {
        public async Task<ApiResponse<GetRoleByCodeQuery.Result>> Handle(GetRoleByCodeQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.RoleCode), request.RoleCode)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = @"
                        SELECT 
                            Id,
                            RoleCode,
                            RoleName,
                            Description,
                            IsActive,
                            CreatedBy,
                            CreatedAt,
                            UpdatedBy,
                            UpdatedAt,
                            DataRowVersion
                        FROM sy_roles
                        WHERE RoleCode = @RoleCode";

                    var result = await dbContext.QueryFirstOrDefaultAsync<GetRoleByCodeQuery.Result>(sql, new { request.RoleCode }, ct);

                    if (result == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetRoleByCodeQuery.Result>("Role not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(result, CoreResource.Common_msg_GetSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving role by code: {ex.Message}", ex);

                    var response = ResponseHelper.Error<GetRoleByCodeQuery.Result>("Error occurred while retrieving role");

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
