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
    /// Check Username Exists Query - Kiểm tra username có tồn tại
    /// </summary>
    public sealed class CheckUsernameExistsQuery : BaseQuery, IRequest<ApiResponse<CheckUsernameExistsQuery.Result>>
    {
        /// <summary>
        /// Username cần kiểm tra
        /// </summary>
        public string Username { get; set; } = string.Empty;

        public class Result
        {
            /// <summary>
            /// Username có tồn tại hay không
            /// </summary>
            public bool Exists { get; set; }
        }
    }

    /// <summary>
    /// Check Username Exists Query Validator
    /// </summary>
    public sealed class CheckUsernameExistsQueryValidator : AbstractValidator<CheckUsernameExistsQuery>
    {
        public CheckUsernameExistsQueryValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters");
        }
    }

    /// <summary>
    /// Check Username Exists Query Handler
    /// </summary>
    public sealed class CheckUsernameExistsQueryHandler : IRequestHandler<CheckUsernameExistsQuery, ApiResponse<CheckUsernameExistsQuery.Result>>
    {
        public async Task<ApiResponse<CheckUsernameExistsQuery.Result>> Handle(CheckUsernameExistsQuery request, CancellationToken ct)
        {
            try
            {
                using (var dbContext = new DbContext())
                {
                    var sql = @"
                        SELECT CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT)
                        FROM [dbo].[sy_users]
                        WHERE [UserName] = @Username";

                    var exists = await dbContext.ExecuteScalarAsync<bool>(
                        sql,
                        new { request.Username });

                    var result = new CheckUsernameExistsQuery.Result
                    {
                        Exists = exists
                    };

                    return ResponseHelper.Success(result);
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"[CheckUsernameExists] Error checking username: {request.Username}", ex);
                return ResponseHelper.Error<CheckUsernameExistsQuery.Result>(CoreResource.common_exceptionOccurred);
            }
        }
    }
}
