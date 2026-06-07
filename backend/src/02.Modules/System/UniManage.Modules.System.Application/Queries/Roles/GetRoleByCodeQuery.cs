using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.System.Application.Queries.Roles
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
        public async Task<ApiResponse<GetRoleByCodeQuery.Result>> Handle(GetRoleByCodeQuery request, CancellationToken cancellationToken)
        {
            var nameCol = TranslateHelper.GetLocalizedColumnName(CoreConstant.Column.Name, request.HeaderInfo?.Language);
            var descCol = TranslateHelper.GetLocalizedColumnName(CoreConstant.Column.Description, request.HeaderInfo?.Language);

            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.RoleCode), request.RoleCode)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = $@"
                        SELECT 
                            Id,
                            Code AS RoleCode,
                            {nameCol} AS RoleName,
                            {descCol} AS Description,
                            CASE WHEN Status = 'Active' THEN 1 ELSE 0 END AS IsActive,
                            CreatedBy,
                            CreatedAt,
                            UpdatedBy,
                            UpdatedAt,
                            DataRowVersion
                        FROM sy_roles
                        WHERE Code = @RoleCode";

                    var result = await dbContext.QueryFirstOrDefaultAsync<GetRoleByCodeQuery.Result>(sql, new { request.RoleCode }, cancellationToken);

                    if (result == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetRoleByCodeQuery.Result>("Role not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(result, CoreResource.common_getSuccess);

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

                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}




