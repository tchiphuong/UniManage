using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Queries.System.Auth
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
        public async Task<ApiResponse<CheckUsernameExistsQuery.Result>> Handle(CheckUsernameExistsQuery request, CancellationToken ct)
        {
            // Khởi tạo log nghiệp vụ
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.Username), request.Username)
                }
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // Sử dụng EF Core LINQ để kiểm tra tồn tại một cách nhanh chóng
                    var exists = await dbContext.Set<sy_users>()
                        .AnyAsync(u => u.Username == request.Username, ct);

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
