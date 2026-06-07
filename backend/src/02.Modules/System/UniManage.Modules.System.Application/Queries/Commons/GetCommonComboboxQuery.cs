using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.System.Application.Queries.Commons
{
    #region Query

    public sealed class GetCommonComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
    {
        public string TypeKey { get; init; } = default!;
        public string? Keyword { get; set; }
    }

    #endregion

    #region Validator

    public sealed class GetCommonComboboxQueryValidator : AbstractValidator<GetCommonComboboxQuery>
    {
        public GetCommonComboboxQueryValidator()
        {
            RuleFor(x => x.TypeKey)
                .NotEmpty().WithMessage(CoreResource.validation_required);
        }
    }

    #endregion

    #region Handler

    public sealed class GetCommonComboboxQueryHandler : IRequestHandler<GetCommonComboboxQuery, ApiResponse<List<ComboboxItem>>>
    {
        public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetCommonComboboxQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.TypeKey), request.TypeKey),
                    new LogParamModel(nameof(request.Keyword), request.Keyword)
                }
            };

            try
            {
                using (var db = new DbContext())
                {
                    var isEnglish = request.HeaderInfo?.Language?.StartsWith("en", StringComparison.OrdinalIgnoreCase) == true;
                    var nameColumn = isEnglish ? "ValueNameEn" : "ValueNameVi";

                    var query = new StringBuilder();
                    query.AppendLine($@"
                        SELECT 
                            ValueKey, 
                            {nameColumn} AS ValueName,
                            Sort
                        FROM SyCommons
                        WHERE TypeKey = @TypeKey 
                            AND Status = 1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine($"AND (ValueKey LIKE @Keyword OR {nameColumn} LIKE @Keyword)");
                    }

                    query.AppendLine($"ORDER BY Sort, {nameColumn}");

                    var commons = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new
                        {
                            request.TypeKey,
                            Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%"
                        },
                        cancellationToken);

                    var items = commons.Select(c => new ComboboxItem
                    {
                        Code = c.ValueKey,
                        Name = c.ValueName,
                        Status = 1,
                        ExtData = new Dictionary<string, object>
                        {
                            ["Sort"] = c.Sort
                        }
                    }).ToList();

                    var response = ResponseHelper.Success(items);
                    log.Result = new { Count = items.Count, TypeKey = request.TypeKey };
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

                UniLogger.Error($"Error getting common combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxItem>>(CoreResource.common_exceptionOccurred);
            }
        }
    }

    #endregion
}






