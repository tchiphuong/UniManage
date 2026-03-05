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
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Username)
                        .MaximumLength(50)
                        .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_username, 50));
                });
        }
    }

    /// <summary>
    /// Check Username Exists Query Handler
    /// </summary>
    public sealed class CheckUsernameExistsQueryHandler : IRequestHandler<CheckUsernameExistsQuery, ApiResponse<CheckUsernameExistsQuery.Result>>
    {
        public async Task<ApiResponse<CheckUsernameExistsQuery.Result>> Handle(CheckUsernameExistsQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Username), request.Username)
                }
            };

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

                    var response = ResponseHelper.Success(result);
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = $"Username exists check for '{request.Username}': {exists}";
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<CheckUsernameExistsQuery.Result>(CoreResource.common_exceptionOccurred);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }
}
