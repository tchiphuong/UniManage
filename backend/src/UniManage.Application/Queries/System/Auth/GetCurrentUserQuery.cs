using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Auth
{
    /// <summary>
    /// Get Current User Query - Lấy thông tin user hiện tại
    /// </summary>
    public sealed class GetCurrentUserQuery : BaseQuery, IRequest<ApiResponse<GetCurrentUserQuery.Result>>
    {

        public class Result
        {
            /// <summary>
            /// User ID
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// User Code
            /// </summary>
            public string UserCode { get; set; } = string.Empty;
            /// <summary>
            /// Display Name
            /// </summary>
            public string DisplayName { get; set; } = string.Empty;
            /// <summary>
            /// Email
            /// </summary>
            public string? Email { get; set; }
            /// <summary>
            /// Role Code
            /// </summary>
            public string? RoleCode { get; set; }
            /// <summary>
            /// Employee Code
            /// </summary>
            public string? EmployeeCode { get; set; }
            /// <summary>
            /// Created At
            /// </summary>
            public DateTime CreatedAt { get; set; }
        }
    }

    /// <summary>
    /// Get Current User Query Validator
    /// </summary>
    public sealed class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
    {
        public GetCurrentUserQueryValidator()
        {
            RuleFor(x => x.HeaderInfo.Username)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_userIdentity));
        }
    }

    /// <summary>
    /// Get Current User Query Handler
    /// </summary>
    public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ApiResponse<GetCurrentUserQuery.Result>>
    {
        public async Task<ApiResponse<GetCurrentUserQuery.Result>> Handle(GetCurrentUserQuery request, CancellationToken ct)
        {
            var username = request.HeaderInfo.Username ?? string.Empty;
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.HeaderInfo.Username), username)
                }
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    var sql = @"
                        SELECT TOP 1
                            [Id],
                            [UserName] AS UserCode,
                            [EmployeeCode],
                            COALESCE([EmployeeCode], [UserName]) AS DisplayName,
                            [RoleCode],
                            [Email],
                            [CreatedAt]
                        FROM [dbo].[sy_users]
                        WHERE [UserName] = @Username";

                    var user = await dbContext.QueryFirstOrDefaultAsync<GetCurrentUserQuery.Result>(
                        sql,
                        new { Username = username });

                    if (user == null)
                    {
                        var errorResponse = ResponseHelper.NotFound<GetCurrentUserQuery.Result>("User not found");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = $"User not found in database: {username}";
                        return errorResponse;
                    }

                    var response = ResponseHelper.Success(user, string.Format(CoreResource.crud_getSuccess, CoreResource.entity_user));
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = $"Retrieved user info successfully for: {username}";
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<GetCurrentUserQuery.Result>(CoreResource.common_exceptionOccurred);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }
}
