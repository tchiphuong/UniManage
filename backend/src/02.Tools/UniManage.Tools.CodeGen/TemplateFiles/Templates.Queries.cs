using System.Collections.Generic;
using System.Linq;

namespace UniManage.Tools.CodeGen
{
    public static partial class Templates
    {
        public static string GetListQueryTemplate(string module, string entity, string tableName, List<ColumnInfo> allColumns, List<string> gridCols, List<string> searchCols)
        {
            var searchProps = searchCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .Select(c =>
                {
                    // Nếu CSharpType đã chứa '?' (ví dụ string? cho nullable string) thì không thêm '?' nữa
                    var nullable = c!.CSharpType.EndsWith("?") ? "" : "?";
                    return $"        public {c.CSharpType}{nullable} {c.Name} {{ get; init; }}";
                });

            var searchConditions = searchCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .Select(c => 
                {
                    // String dùng IsNullOrEmpty, value type dùng HasValue
                    var baseType = c!.CSharpType.TrimEnd('?');
                    if (baseType == "string")
                        return $"                    if (!string.IsNullOrEmpty(request.{c.Name}))\n                        query = query.Where(x => x.{c.Name}.Contains(request.{c.Name}));";
                    else
                        return $"                    if (request.{c.Name}.HasValue)\n                        query = query.Where(x => x.{c.Name} == request.{c.Name});";
                });

            var selectAssigns = gridCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .Select(c => $"                        {c!.Name} = x.{c.Name}");

            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using UniManage.Modules.{module}.Application.Models.{entity};
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;
using UniManage.Modules.{module}.Domain;

namespace UniManage.Modules.{module}.Application.Queries.{entity}
{{
    #region Query

    public sealed class Get{entity}ListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<{entity}ListModel>>>
    {{
{string.Join("\n", searchProps)}
    }}

    #endregion

    #region Handler

    public sealed class Get{entity}ListQueryHandler : IRequestHandler<Get{entity}ListQuery, ApiResponse<PagedResult<{entity}ListModel>>>
    {{
        public async Task<ApiResponse<PagedResult<{entity}ListModel>>> Handle(Get{entity}ListQuery request, CancellationToken cancellationToken)
        {{
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {{
                Parameter = new List<LogParamModel>
                {{
                    new(nameof(request.Keyword), request.Keyword)
                }}
            }};

            try
            {{
                using (var dbContext = new DbContext())
                {{
                    var query = dbContext.Set<{tableName}>().AsQueryable();

{string.Join("\n", searchConditions)}

                    var totalItems = await query.CountAsync(cancellationToken);
                    var items = await query
                        .OrderByDescending(x => x.Id)
                        .Skip(request.Offset)
                        .Take(request.PageSize)
                        .Select(x => new {entity}ListModel
                        {{
                            Id = x.Id,
{string.Join(",\n", selectAssigns)}
                        }})
                        .ToListAsync(cancellationToken);

                    var result = new PagedResult<{entity}ListModel>
                    {{
                        Items = items,
                        Paging = new PagingInfo
                        {{
                            PageIndex = request.PageIndex,
                            PageSize = request.PageSize,
                            TotalItems = totalItems
                        }}
                    }};

                    var response = ResponseHelper.PagedSuccess(result);
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    return response;
                }}
            }}
            catch (Exception ex)
            {{
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<PagedResult<{entity}ListModel>>(CoreResource.common_error);
            }}
            finally
            {{
                UniLogManager.WriteApiLog(log);
            }}
        }}
    }}

    #endregion
}}";
        }

        public static string GetByIdQueryTemplate(string module, string entity, string tableName, string screenCode, List<ColumnInfo> allColumns)
        {
            var selectAssigns = allColumns.Select(c => $"                        {c.Name} = x.{c.Name}");

            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using UniManage.Modules.{module}.Application.Models.{entity};
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;
using UniManage.Modules.{module}.Domain;

namespace UniManage.Modules.{module}.Application.Queries.{entity}
{{
    #region Query

    public sealed class Get{entity}ByIdQuery : BaseQuery, IRequest<ApiResponse<{entity}DetailModel>>
    {{
        public long Id {{ get; init; }}
    }}

    #endregion

    #region Handler

    public sealed class Get{entity}ByIdQueryHandler : IRequestHandler<Get{entity}ByIdQuery, ApiResponse<{entity}DetailModel>>
    {{
        public async Task<ApiResponse<{entity}DetailModel>> Handle(Get{entity}ByIdQuery request, CancellationToken cancellationToken)
        {{
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {{
                Parameter = new List<LogParamModel>
                {{
                    new(nameof(request.Id), request.Id.ToString())
                }}
            }};

            try
            {{
                using (var dbContext = new DbContext())
                {{
                    var item = await dbContext.Set<{tableName}>()
                        .Where(x => x.Id == request.Id)
                        .Select(x => new {entity}DetailModel
                        {{
{string.Join(",\n", selectAssigns)}
                        }})
                        .FirstOrDefaultAsync(cancellationToken);

                    if (item == null)
                    {{
                        var notFound = ResponseHelper.NotFound<{entity}DetailModel>(CoreResource.common_notFound);
                        log.ReturnCode = notFound.ReturnCode;
                        return notFound;
                    }}

                    var response = ResponseHelper.Success(item);
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    return response;
                }}
            }}
            catch (Exception ex)
            {{
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<{entity}DetailModel>(CoreResource.common_error);
            }}
            finally
            {{
                UniLogManager.WriteApiLog(log);
            }}
        }}
    }}

    #endregion
}}";
        }

        public static string GetExportQueryTemplate(string module, string entity, string tableName, List<ColumnInfo> allColumns, List<string> gridCols, List<string> searchCols)
        {
            var searchProps = searchCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .Select(c =>
                {
                    var nullable = c!.CSharpType.EndsWith("?") ? "" : "?";
                    return $"        public {c.CSharpType}{nullable} {c.Name} {{ get; init; }}";
                });

            var searchConditions = searchCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .Select(c => 
                {
                    var baseType = c!.CSharpType.TrimEnd('?');
                    if (baseType == "string")
                        return $"                    if (!string.IsNullOrEmpty(request.{c.Name}))\n                        query = query.Where(x => x.{c.Name}.Contains(request.{c.Name}));";
                    else
                        return $"                    if (request.{c.Name}.HasValue)\n                        query = query.Where(x => x.{c.Name} == request.{c.Name});";
                });

            var headerAssigns = gridCols.Select((col, idx) => $"                    worksheet.Cell(1, {idx + 1}).Value = \"{col}\";");
            var rowAssigns = gridCols.Select((col, idx) => $"                        worksheet.Cell(row, {idx + 1}).Value = item.{col}?.ToString();");

            return $@"using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;
using UniManage.Modules.{module}.Domain;

namespace UniManage.Modules.{module}.Application.Queries.{entity}
{{
    #region Query

    public sealed class Export{entity}Query : BaseExportQuery, IRequest<ApiResponse<byte[]>>
    {{
{string.Join("\n", searchProps)}
    }}

    #endregion

    #region Handler

    public sealed class Export{entity}QueryHandler : IRequestHandler<Export{entity}Query, ApiResponse<byte[]>>
    {{
        public async Task<ApiResponse<byte[]>> Handle(Export{entity}Query request, CancellationToken cancellationToken)
        {{
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {{
                Parameter = new List<LogParamModel>
                {{
                    new(nameof(request.Keyword), request.Keyword)
                }}
            }};

            try
            {{
                using (var dbContext = new DbContext())
                {{
                    var query = dbContext.Set<{tableName}>().AsQueryable();

{string.Join("\n", searchConditions)}

                    var items = await query.OrderByDescending(x => x.Id).ToListAsync(cancellationToken);

                    using (var workbook = new XLWorkbook())
                    {{
                        var worksheet = workbook.Worksheets.Add(""{entity}s"");
                        
                        // Header
{string.Join("\n", headerAssigns)}

                        // Data
                        int row = 2;
                        foreach (var item in items)
                        {{
{string.Join("\n", rowAssigns)}
                            row++;
                        }}

                        worksheet.Columns().AdjustToContents();

                        using (var stream = new MemoryStream())
                        {{
                            workbook.SaveAs(stream);
                            var response = ResponseHelper.Success(stream.ToArray(), ""Export successful"");
                            log.Result = ""Excel bytes"";
                            return response;
                        }}
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<byte[]>(CoreResource.common_error);
            }}
            finally
            {{
                UniLogManager.WriteApiLog(log);
            }}
        }}
    }}

    #endregion
}}";
        }
    }
}
