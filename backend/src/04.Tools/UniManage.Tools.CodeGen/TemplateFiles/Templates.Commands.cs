using System.Collections.Generic;
using System.Linq;

namespace UniManage.Tools.CodeGen
{
    public static partial class Templates
    {
        public static string GetCreateCommandTemplate(string module, string entity, string tableName, string screenCode, List<ColumnInfo> allColumns, List<string> editCols)
        {
            var editColumns = editCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .ToList();

            var props = editColumns.Select(c => 
            {
                var suffix = (c!.CSharpType == "string" && !c.IsNullable) ? " = default!;" : "";
                return $"        public {c.CSharpType} {c.Name} {{ get; init; }}{suffix}";
            });

            var logParams = editColumns.Select(c => $"                    new(nameof(request.{c!.Name}), request.{c.Name}?.ToString())");

            return $@"using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using UniManage.Shared.Infrastructure.Database.Repositories;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;
using UniManage.Modules.{module}.Domain;

namespace UniManage.Modules.{module}.Application.Commands.{entity}
{{
    #region Command

    /// <summary>
    /// Lệnh tạo mới {entity}
    /// </summary>
    public sealed class Create{entity}Command : BaseCommand, IRequest<ApiResponse<Create{entity}Command.Response>>
    {{
{string.Join("\n", props)}

        public sealed class Response
        {{
            public long Id {{ get; init; }}
        }}
    }}

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh tạo mới {entity}
    /// </summary>
    public sealed class Create{entity}CommandHandler : IRequestHandler<Create{entity}Command, ApiResponse<Create{entity}Command.Response>>
    {{
        private readonly IMediator _mediator;

        public Create{entity}CommandHandler(IMediator mediator)
        {{
            _mediator = mediator;
        }}

        public async Task<ApiResponse<Create{entity}Command.Response>> Handle(Create{entity}Command request, CancellationToken cancellationToken)
        {{
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {{
                Parameter = new List<LogParamModel>
                {{
{string.Join(",\n", logParams)}
                }}
            }};

            try
            {{
                using (var unitOfWork = new UnitOfWork(new DbContext(openTransaction: true), _mediator))
                {{
                    try
                    {{
                        var entity = new {tableName}
                        {{
{string.Join(",\n", editColumns.Select(c => $"                            {c!.Name} = request.{c.Name}"))},
                            CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                            CreatedAt = DateTimeHelper.Now,
                            DataRowVersion = new byte[8]
                        }};

                        var repository = new Repository<{tableName}>(unitOfWork._dbContext);
                        repository.Add(entity);
                        await unitOfWork.SaveChangesAsync(cancellationToken);
                        await unitOfWork._dbContext.CommitAsync();

                        var responseData = new Create{entity}Command.Response {{ Id = entity.Id }};
                        var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);
                        
                        log.Result = responseData;
                        log.Message = response.Message;
                        log.ReturnCode = response.ReturnCode;

                        return response;
                    }}
                    catch (Exception)
                    {{
                        await unitOfWork._dbContext.RollbackAsync();
                        throw;
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<Create{entity}Command.Response>(CoreResource.common_error);
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

        public static string GetUpdateCommandTemplate(string module, string entity, string tableName, string screenCode, List<ColumnInfo> allColumns, List<string> editCols)
        {
            var editColumns = editCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .ToList();

            var props = editColumns.Select(c => 
            {
                var suffix = (c!.CSharpType == "string" && !c.IsNullable) ? " = default!;" : "";
                return $"        public {c.CSharpType} {c.Name} {{ get; init; }}{suffix}";
            });

            var logParams = editColumns.Select(c => $"                    new(nameof(request.{c!.Name}), request.{c.Name}?.ToString())");

            return $@"using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using UniManage.Shared.Infrastructure.Database.Repositories;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;
using UniManage.Modules.{module}.Domain;

namespace UniManage.Modules.{module}.Application.Commands.{entity}
{{
    #region Command

    /// <summary>
    /// Lệnh cập nhật {entity}
    /// </summary>
    public sealed class Update{entity}Command : BaseCommand, IRequest<ApiResponse<Update{entity}Command.Response>>
    {{
        public long Id {{ get; set; }}
{string.Join("\n", props)}

        public sealed class Response
        {{
            public long Id {{ get; init; }}
        }}
    }}

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh cập nhật {entity}
    /// </summary>
    public sealed class Update{entity}CommandHandler : IRequestHandler<Update{entity}Command, ApiResponse<Update{entity}Command.Response>>
    {{
        private readonly IMediator _mediator;

        public Update{entity}CommandHandler(IMediator mediator)
        {{
            _mediator = mediator;
        }}

        public async Task<ApiResponse<Update{entity}Command.Response>> Handle(Update{entity}Command request, CancellationToken cancellationToken)
        {{
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {{
                Parameter = new List<LogParamModel>
                {{
                    new(nameof(request.Id), request.Id.ToString()),
{string.Join(",\n", logParams)}
                }}
            }};

            try
            {{
                using (var unitOfWork = new UnitOfWork(new DbContext(openTransaction: true), _mediator))
                {{
                    try
                    {{
                        var repository = new Repository<{tableName}>(unitOfWork._dbContext);
                        var entity = await repository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                        if (entity == null)
                        {{
                            var notFoundResponse = ResponseHelper.NotFound<Update{entity}Command.Response>(CoreResource.common_notFound);
                            log.Message = notFoundResponse.Message;
                            log.ReturnCode = notFoundResponse.ReturnCode;
                            return notFoundResponse;
                        }}

{string.Join("\n", editColumns.Select(c => $"                        entity.{c!.Name} = request.{c.Name};"))}
                        entity.UpdatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser;
                        entity.UpdatedAt = DateTimeHelper.Now;

                        repository.Update(entity);
                        await unitOfWork.SaveChangesAsync(cancellationToken);
                        await unitOfWork._dbContext.CommitAsync();

                        var responseData = new Update{entity}Command.Response {{ Id = entity.Id }};
                        var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
                        
                        log.Result = responseData;
                        log.Message = response.Message;
                        log.ReturnCode = response.ReturnCode;

                        return response;
                    }}
                    catch (Exception)
                    {{
                        await unitOfWork._dbContext.RollbackAsync();
                        throw;
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<Update{entity}Command.Response>(CoreResource.common_error);
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

        public static string GetDeleteCommandTemplate(string module, string entity, string tableName, string screenCode) =>
$@"using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using UniManage.Shared.Infrastructure.Database.Repositories;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;
using UniManage.Modules.{module}.Domain;

namespace UniManage.Modules.{module}.Application.Commands.{entity}
{{
    #region Command

    /// <summary>
    /// Lệnh xóa {entity}
    /// </summary>
    public sealed class Delete{entity}Command : BaseCommand, IRequest<ApiResponse<Delete{entity}Command.Response>>
    {{
        public List<long> Ids {{ get; init; }} = default!;

        public sealed class Response
        {{
            public bool Success {{ get; init; }}
        }}
    }}

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh xóa {entity}
    /// </summary>
    public sealed class Delete{entity}CommandHandler : IRequestHandler<Delete{entity}Command, ApiResponse<Delete{entity}Command.Response>>
    {{
        private readonly IMediator _mediator;

        public Delete{entity}CommandHandler(IMediator mediator)
        {{
            _mediator = mediator;
        }}

        public async Task<ApiResponse<Delete{entity}Command.Response>> Handle(Delete{entity}Command request, CancellationToken cancellationToken)
        {{
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
            {{
                Parameter = new List<LogParamModel>
                {{
                    new(nameof(request.Ids), string.Join("","", request.Ids ?? new List<long>()))
                }}
            }};

            try
            {{
                if (request.Ids == null || request.Ids.Count == 0)
                {{
                    return ResponseHelper.Error<Delete{entity}Command.Response>(""No ids provided"");
                }}

                using (var unitOfWork = new UnitOfWork(new DbContext(openTransaction: true), _mediator))
                {{
                    try
                    {{
                        var repository = new Repository<{tableName}>(unitOfWork._dbContext);
                        var entities = await unitOfWork._dbContext.Set<{tableName}>().Where(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken);
                        if (entities.Count == 0)
                        {{
                            return ResponseHelper.NotFound<Delete{entity}Command.Response>(CoreResource.common_notFound);
                        }}

                        repository.RemoveRange(entities);
                        await unitOfWork.SaveChangesAsync(cancellationToken);
                        await unitOfWork._dbContext.CommitAsync();

                        var responseData = new Delete{entity}Command.Response {{ Success = true }};
                        var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
                        
                        log.Result = responseData;
                        log.Message = response.Message;
                        log.ReturnCode = response.ReturnCode;

                        return response;
                    }}
                    catch (Exception)
                    {{
                        await unitOfWork._dbContext.RollbackAsync();
                        throw;
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<Delete{entity}Command.Response>(CoreResource.common_error);
            }}
            finally
            {{
                UniLogManager.WriteApiLog(log);
            }}
        }}
    }}

    #endregion
}}";

        public static string GetImportCommandTemplate(string module, string entity, string tableName, string screenCode, List<ColumnInfo> allColumns, List<string> editCols)
        {
            var editColumns = editCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .ToList();
            
            var dbAssigns = editColumns.Select((c, idx) => $"                            {c!.Name} = row.Cell({idx + 1}).GetString(), // Please convert type properly");

            return $@"using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Collections.Generic;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using UniManage.Shared.Infrastructure.Database.Repositories;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;
using UniManage.Modules.{module}.Domain;

namespace UniManage.Modules.{module}.Application.Commands.{entity}
{{
    #region Command

    public sealed class Import{entity}Command : BaseImportCommand<{entity}ImportModel>
    {{
    }}

    public sealed class {entity}ImportModel
    {{
{string.Join("\n", editColumns.Select(c => $"        public {c!.CSharpType} {c.Name} {{ get; init; }} {(c.CSharpType == "string" && !c.IsNullable ? "= default!;" : "")}"))}
    }}

    #endregion

    #region Handler

    public sealed class Import{entity}CommandHandler : IRequestHandler<Import{entity}Command, ApiResponse<bool>>
    {{
        private readonly IMediator _mediator;

        public Import{entity}CommandHandler(IMediator mediator)
        {{
            _mediator = mediator;
        }}

        public async Task<ApiResponse<bool>> Handle(Import{entity}Command request, CancellationToken cancellationToken)
        {{
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo());
            
            try
            {{
                using (var unitOfWork = new UnitOfWork(new DbContext(openTransaction: true), _mediator))
                {{
                    try
                    {{
                        var repository = new Repository<{tableName}>(unitOfWork._dbContext);
                            if (request.Items == null || request.Items.Count == 0)
                                return ResponseHelper.Error<bool>(""No items to import"");

                            var createValidator = new Validators.{entity}.Create{entity}CommandValidator();
                            var updateValidator = new Validators.{entity}.Update{entity}CommandValidator();

                            foreach (var item in request.Items)
                            {{
                                // TODO: Convert item to Create{entity}Command or Update{entity}Command and validate
                                // if (request.IsOverwrite) {{ var result = await updateValidator.ValidateAsync(updateCmd); }}
                                // else {{ var result = await createValidator.ValidateAsync(createCmd); }}
                                
                                var entity = new {tableName}
                                {{
{string.Join(",\n", editColumns.Select(c => $"                                    {c!.Name} = item.{c.Name}"))},
                                    CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                                    CreatedAt = DateTimeHelper.Now,
                                    DataRowVersion = new byte[8]
                                }};
                                repository.Add(entity);
                            }}

                        await unitOfWork.SaveChangesAsync(cancellationToken);
                        await unitOfWork._dbContext.CommitAsync();

                        var response = ResponseHelper.Success(true, ""Import successful"");
                        log.Result = response;
                        return response;
                    }}
                    catch (Exception)
                    {{
                        await unitOfWork._dbContext.RollbackAsync();
                        throw;
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<bool>(CoreResource.common_error);
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
