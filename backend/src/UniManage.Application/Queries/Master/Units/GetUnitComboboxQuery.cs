using MediatR;
using System.Text;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Master.Units
{
    #region Query

    public sealed class GetUnitComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItemDto>>>
    {
    }

    #endregion

    #region Handler

    public sealed class GetUnitComboboxQueryHandler : IRequestHandler<GetUnitComboboxQuery, ApiResponse<List<ComboboxItemDto>>>
    {
        public async Task<ApiResponse<List<ComboboxItemDto>>> Handle(GetUnitComboboxQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Keyword), request.Keyword)
                }
            };

            try
            {
                using (var db = new DbContext())
                {
                    var isEnglish = request.HeaderInfo?.Language?.StartsWith("en", StringComparison.OrdinalIgnoreCase) == true;
                    var nameColumn = isEnglish ? "NameEn" : "NameVi";

                    var query = new StringBuilder();
                    query.AppendLine($@"
                        SELECT 
                            Code, 
                            {nameColumn} AS Name
                        FROM ms_units
                        WHERE Code IS NOT NULL");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine($"AND (Code LIKE @Keyword OR {nameColumn} LIKE @Keyword)");
                    }

                    query.AppendLine($"ORDER BY {nameColumn}");

                    var units = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new { Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%" },
                        ct);

                    var items = units.Select(u => new ComboboxItemDto
                    {
                        Value = u.Code,
                        Label = u.Name,
                        Status = 1
                    }).ToList();

                    var response = ResponseHelper.Success(items);
                    log.Result = new { Count = items.Count };
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = 1;
                log.Message = ex.ToString();
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                UniLogger.Error($"Error getting unit combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItemDto>>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }

    #endregion
}
