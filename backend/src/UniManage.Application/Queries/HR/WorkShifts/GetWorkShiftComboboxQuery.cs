using MediatR;
using System.Text;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.HR.WorkShifts
{
    #region Query

    public sealed class GetWorkShiftComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItemDto>>>
    {
        public string? Keyword { get; set; }
    }

    #endregion

    #region Handler

    public sealed class GetWorkShiftComboboxQueryHandler : IRequestHandler<GetWorkShiftComboboxQuery, ApiResponse<List<ComboboxItemDto>>>
    {
        public async Task<ApiResponse<List<ComboboxItemDto>>> Handle(GetWorkShiftComboboxQuery request, CancellationToken ct)
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
                    var query = new StringBuilder();
                    query.AppendLine(@"
                        SELECT 
                            Code, 
                            Name,
                            StartTime,
                            EndTime
                        FROM hr_work_shifts
                        WHERE 1 = 1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine("AND (Code LIKE @Keyword OR Name LIKE @Keyword)");
                    }

                    query.AppendLine("ORDER BY Name");

                    var workShifts = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new { Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%" },
                        ct);

                    var items = workShifts.Select(ws => new ComboboxItemDto
                    {
                        Value = ws.Code,
                        Label = $"{ws.Name} ({ws.StartTime:hh\\:mm} - {ws.EndTime:hh\\:mm})",
                        Status = 1,
                        Metadata = new Dictionary<string, object>
                        {
                            ["StartTime"] = ws.StartTime.ToString(),
                            ["EndTime"] = ws.EndTime.ToString()
                        }
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

                UniLogger.Error($"Error getting work shift combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItemDto>>(CoreResource.common_exceptionOccurred);
            }
        }
    }

    #endregion
}
