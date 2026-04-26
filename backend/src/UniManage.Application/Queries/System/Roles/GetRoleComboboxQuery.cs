using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Roles
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách vai trò cho Combobox
    /// </summary>
    public sealed class GetRoleComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxModel>>>
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
    public sealed class GetRoleComboboxQueryHandler : IRequestHandler<GetRoleComboboxQuery, ApiResponse<List<ComboboxModel>>>
    {
        public async Task<ApiResponse<List<ComboboxModel>>> Handle(GetRoleComboboxQuery request, CancellationToken ct)
        {
            // Xác định cột ngôn ngữ động (Zero Hardcode)
            var nameCol = TranslateHelper.GetLocalizedColumnName(CoreConstant.Column.Name, request.HeaderInfo?.Language);

            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
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
                            {nameof(sy_roles.Code)} AS Code,
                            {nameCol} AS Name
                        FROM {nameof(sy_roles)}
                        WHERE 1=1");

                    var parameters = new DynamicParameters();

                    if (request.IsActive.HasValue)
                    {
                        sql.AppendLine($@"AND {nameof(sy_roles.Status)} = @Status");
                        parameters.Add("Status", request.IsActive.Value == 1 ? "Active" : "Inactive");
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        sql.AppendLine($@"AND ({nameof(sy_roles.Code)} LIKE @Keyword OR {nameCol} LIKE @Keyword)");
                        parameters.Add("Keyword", $"%{request.Keyword}%");
                    }

                    sql.AppendLine($@"ORDER BY {nameof(sy_roles.Code)}");

                    var items = await dbContext.QueryAsync<ComboboxModel>(sql.ToString(), parameters, ct);
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

                    return ResponseHelper.Error<List<ComboboxModel>>(CoreResource.common_error);
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
