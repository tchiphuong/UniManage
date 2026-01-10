using Dapper;
using FluentValidation;
using MediatR;
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
        /// <summary>
        /// Username (từ JWT token)
        /// </summary>
        public string Username { get; set; } = string.Empty;

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
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");
        }
    }

    /// <summary>
    /// Get Current User Query Handler
    /// </summary>
    public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ApiResponse<GetCurrentUserQuery.Result>>
    {
        public async Task<ApiResponse<GetCurrentUserQuery.Result>> Handle(GetCurrentUserQuery request, CancellationToken ct)
        {
            try
            {
                UniLogger.Info($"[GetCurrentUser] Getting user info for: {request.Username}");

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

                    var user = await dbContext.connection.QueryFirstOrDefaultAsync<GetCurrentUserQuery.Result>(
                        sql,
                        new { request.Username });

                    if (user == null)
                    {
                        UniLogger.Warn($"[GetCurrentUser] User not found: {request.Username}");
                        return ResponseHelper.NotFound<GetCurrentUserQuery.Result>("User not found");
                    }

                    UniLogger.Info($"[GetCurrentUser] Retrieved user info successfully for: {request.Username}");
                    return ResponseHelper.Success(user, CoreResource.User_msg_InfoRetrieved);
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"[GetCurrentUser] Error getting user info for: {request.Username}", ex);
                return ResponseHelper.Error<GetCurrentUserQuery.Result>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }
}
