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
    /// Check Email Exists Query - Kiểm tra email có tồn tại
    /// </summary>
    public sealed class CheckEmailExistsQuery : BaseQuery, IRequest<ApiResponse<CheckEmailExistsQuery.Result>>
    {
        /// <summary>
        /// Email cần kiểm tra
        /// </summary>
        public string Email { get; set; } = string.Empty;

        public class Result
        {
            /// <summary>
            /// Email có tồn tại hay không
            /// </summary>
            public bool Exists { get; set; }
        }
    }

    /// <summary>
    /// Check Email Exists Query Validator
    /// </summary>
    public sealed class CheckEmailExistsQueryValidator : AbstractValidator<CheckEmailExistsQuery>
    {
        public CheckEmailExistsQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .Must(ValidationHelper.IsValidEmail).WithMessage(CoreResource.validation_invalidEmail)
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters");
        }
    }

    /// <summary>
    /// Check Email Exists Query Handler
    /// </summary>
    public sealed class CheckEmailExistsQueryHandler : IRequestHandler<CheckEmailExistsQuery, ApiResponse<CheckEmailExistsQuery.Result>>
    {
        public async Task<ApiResponse<CheckEmailExistsQuery.Result>> Handle(CheckEmailExistsQuery request, CancellationToken ct)
        {
            try
            {
                using (var dbContext = new DbContext())
                {
                    var sql = @"
                        SELECT CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT)
                        FROM [dbo].[sy_users]
                        WHERE [Email] = @Email";

                    var exists = await dbContext.ExecuteScalarAsync<bool>(
                        sql,
                        new { request.Email });

                    var result = new CheckEmailExistsQuery.Result
                    {
                        Exists = exists
                    };

                    return ResponseHelper.Success(result);
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"[CheckEmailExists] Error checking email: {request.Email}", ex);
                return ResponseHelper.Error<CheckEmailExistsQuery.Result>(CoreResource.common_exceptionOccurred);
            }
        }
    }
}
