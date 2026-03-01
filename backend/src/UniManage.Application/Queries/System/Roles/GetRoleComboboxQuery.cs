using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Roles
{
    public sealed class GetRoleComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItemDto>>>
    {
        public byte? IsActive { get; set; }
        public string? Keyword { get; set; }
    }

    public sealed class GetRoleComboboxQueryValidator : AbstractValidator<GetRoleComboboxQuery>
    {
        public GetRoleComboboxQueryValidator()
        {
        }
    }

    public sealed class GetRoleComboboxQueryHandler : IRequestHandler<GetRoleComboboxQuery, ApiResponse<List<ComboboxItemDto>>>
    {
        public async Task<ApiResponse<List<ComboboxItemDto>>> Handle(GetRoleComboboxQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword),
                    new CoreParamModel(nameof(request.IsActive), request.IsActive)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine(@"
                        SELECT 
                            RoleCode AS Value,
                            RoleName AS Label,
                            RoleName AS LabelEn,
                            RoleName AS LabelVi
                        FROM sy_roles
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (request.IsActive.HasValue)
                    {
                        sql.AppendLine("AND IsActive = @IsActive");
                        parameters.Add("IsActive", request.IsActive.Value);
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine("AND (RoleCode LIKE @Keyword OR RoleName LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    sql.AppendLine("ORDER BY RoleCode");

                    var items = await dbContext.QueryAsync<ComboboxItemDto>(sql.ToString(), parameters, ct);
                    var result = items.ToList();

                    var response = ResponseHelper.Success(result, CoreResource.crud_getSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving role combobox: {ex.Message}", ex);

                    var response = ResponseHelper.Error<List<ComboboxItemDto>>("Error occurred while retrieving role combobox");

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
