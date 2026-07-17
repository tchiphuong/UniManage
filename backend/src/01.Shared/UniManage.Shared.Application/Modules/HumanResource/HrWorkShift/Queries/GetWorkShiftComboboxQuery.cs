using MediatR;
using System.Text;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.HrWorkShift.Queries
{
    #region Query

    public sealed class GetWorkShiftComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
    {
        public string? Keyword { get; set; }
    }

    #endregion

    #region Handler

    public sealed class GetWorkShiftComboboxQueryHandler : IRequestHandler<GetWorkShiftComboboxQuery, ApiResponse<List<ComboboxItem>>>
    {
        public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetWorkShiftComboboxQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Keyword), request.Keyword)
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
                        FROM HrWorkShifts
                        WHERE 1 = 1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine("AND (Code LIKE @Keyword OR Name LIKE @Keyword)");
                    }

                    query.AppendLine("ORDER BY Name");

                    var workShifts = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new { Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%" },
                        cancellationToken);

                    var items = workShifts.Select(ws => new ComboboxItem
                    {
                        Code = ws.Code,
                        Name = $"{ws.Name} ({ws.StartTime:hh\\:mm} - {ws.EndTime:hh\\:mm})",
                        Status = 1,
                        ExtData = new Dictionary<string, object>
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
                log.IsException = true;
                log.Message = ex.ToString();
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                UniLogger.Error($"Error getting work shift combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItem>>(CoreResource.common_exceptionOccurred);
            }
        }
    }

    #endregion
}




