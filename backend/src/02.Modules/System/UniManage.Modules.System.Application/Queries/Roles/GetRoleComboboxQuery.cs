using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.System.Application.Queries.Roles
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách vai trò cho Combobox
    /// </summary>
    public sealed class GetRoleComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
    {
        public byte? IsActive { get; set; }
        public string? Keyword { get; set; }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn Combobox vai trò
    /// </summary>
    public sealed class GetRoleComboboxQueryValidator : AbstractValidator<GetRoleComboboxQuery>
    {
        public GetRoleComboboxQueryValidator()
        {
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách vai trò cho Combobox
    /// </summary>
    public sealed class GetRoleComboboxQueryHandler : IRequestHandler<GetRoleComboboxQuery, ApiResponse<List<ComboboxItem>>>
    {
        public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetRoleComboboxQuery request, CancellationToken cancellationToken)
        {
            // Xác định cột ngôn ngữ động (Zero Hardcode)
            var nameCol = TranslateHelper.GetLocalizedColumnName(CoreConstant.Column.Name, request.HeaderInfo?.Language);

            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Keyword), request.Keyword),
                    new(nameof(request.IsActive), request.IsActive)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = new StringBuilder();
                    sql.AppendLine($@"
                        SELECT 
                            {nameof(SyRoles.Code)} AS Code,
                            {nameCol} AS Name
                        FROM {nameof(SyRoles)}
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (request.IsActive.HasValue)
                    {
                        sql.AppendLine($@"AND {nameof(SyRoles.Status)} = @Status");
                        parameters.Add("Status", request.IsActive.Value == 1 ? "Active" : "Inactive");
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine($@"AND ({nameof(SyRoles.Code)} LIKE @Keyword OR {nameCol} LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    sql.AppendLine($@"ORDER BY {nameof(SyRoles.Code)}");

                    var items = await dbContext.QueryAsync<ComboboxItem>(sql.ToString(), parameters, cancellationToken);
                    var result = items.ToList();

                    var response = ResponseHelper.Success(result, string.Format(CoreResource.common_getSuccess, CoreResource.entity_role));

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;

                    return response;
                }
                catch (Exception ex)
                {
                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;

                    return ResponseHelper.Error<List<ComboboxItem>>(CoreResource.common_error);
                }
                finally
                {
                    UniLogManager.WriteApiLog(log);
                }
            }
        }
    }

    #endregion
}





