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
    /// Truy vấn lấy thông tin chi tiết của người dùng hiện tại đang đăng nhập
    /// </summary>
    public sealed class GetCurrentUserQuery : BaseQuery, IRequest<ApiResponse<GetCurrentUserQuery.Result>>
    {
        /// <summary>
        /// Kết quả trả về chứa thông tin cơ bản của người dùng
        /// </summary>
        public class Result
        {
            public long Id { get; set; }
            public string UserCode { get; set; } = string.Empty;
            public string DisplayName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string? RoleCode { get; set; }
            public string? EmployeeCode { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn lấy thông tin người dùng hiện tại
    /// </summary>
    public sealed class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
    {
        public GetCurrentUserQueryValidator()
        {
            RuleFor(x => x.HeaderInfo.Username)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy thông tin người dùng hiện tại
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
                    new(nameof(request.HeaderInfo.Username), username)
                }
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // Sử dụng EF Core với Entity sy_users để lấy thông tin
                    var user = await dbContext.Set<sy_users>()
                        .Where(u => u.Username == username)
                        .Select(u => new GetCurrentUserQuery.Result
                        {
                            Id = u.Id,
                            UserCode = u.Username,
                            EmployeeCode = u.EmployeeCode,
                            DisplayName = u.EmployeeCode ?? u.Username, // Ưu tiên hiển thị mã nhân viên
                            RoleCode = u.RoleCode,
                            Email = u.Email,
                            CreatedAt = u.CreatedAt
                        })
                        .FirstOrDefaultAsync(ct);

                    if (user == null)
                    {
                        var errorResponse = ResponseHelper.NotFound<GetCurrentUserQuery.Result>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                        log.Message = "Current user not found in database";
                        log.ReturnCode = errorResponse.ReturnCode;
                        return errorResponse;
                    }

                    var response = ResponseHelper.Success(user, string.Format(CoreResource.common_getSuccess, CoreResource.entity_user));
                    
                    log.Result = user;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = "Get current user profile success";

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<GetCurrentUserQuery.Result>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
