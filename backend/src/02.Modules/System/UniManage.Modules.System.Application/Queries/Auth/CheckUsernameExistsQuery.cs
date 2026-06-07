using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Queries.Auth
{
    #region Query

    /// <summary>
    /// Truy vấn kiểm tra sự tồn tại của tên đăng nhập (Username) trong hệ thống
    /// </summary>
    public sealed class CheckUsernameExistsQuery : BaseQuery, IRequest<ApiResponse<CheckUsernameExistsQuery.Result>>
    {
        /// <summary>
        /// Tên đăng nhập cần kiểm tra
        /// </summary>
        public string Username { get; set; } = string.Empty;

        public class Result
        {
            /// <summary>
            /// Kết quả kiểm tra (True: đã tồn tại, False: chưa tồn tại)
            /// </summary>
            public bool Exists { get; set; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn kiểm tra tồn tại username
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

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn kiểm tra tồn tại username
    /// </summary>
    public sealed class CheckUsernameExistsQueryHandler : IRequestHandler<CheckUsernameExistsQuery, ApiResponse<CheckUsernameExistsQuery.Result>>
    {
        public async Task<ApiResponse<CheckUsernameExistsQuery.Result>> Handle(CheckUsernameExistsQuery request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Username), request.Username)
                }
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // Sử dụng EF Core LINQ để kiểm tra tồn tại một cách nhanh chóng
                    var exists = await dbContext.Set<SyUsers>()
                        .AnyAsync(u => u.Username == request.Username, cancellationToken);

                    var result = new CheckUsernameExistsQuery.Result
                    {
                        Exists = exists
                    };

                    var response = ResponseHelper.Success(result, string.Format(CoreResource.common_getSuccess, CoreResource.entity_user));
                    
                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = "Check username existence success";

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<CheckUsernameExistsQuery.Result>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




