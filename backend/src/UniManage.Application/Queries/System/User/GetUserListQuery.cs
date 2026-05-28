using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Core.Constant;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Queries.System.User
{
    #region Query

    /// <summary>
    /// Truy vấn lấy danh sách người dùng có phân trang và lọc theo điều kiện
    /// </summary>
    public sealed class GetUserListQuery : BaseListQuery, IRequest<PagedResponse<GetUserListQuery.Response>>
    {
        /// <summary>
        /// Trạng thái người dùng để lọc (Active/Inactive)
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// DTO kết quả trả về cho mỗi dòng người dùng
        /// </summary>
        public sealed record Response
        {
            public Guid Uuid { get; set; }
            public string Username { get; set; } = default!;
            public string EmployeeCode { get; set; } = default!;
            public string RoleCode { get; set; } = default!;
            public string Status { get; set; } = default!;
            public DateTime CreatedAt { get; set; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn danh sách người dùng
    /// </summary>
    public sealed class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
    {
        public GetUserListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage(string.Format(CoreResource.validation_invalidFormat, "PageIndex"));

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage(string.Format(CoreResource.validation_maxLength, "PageSize", 100));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn lấy danh sách người dùng
    /// </summary>
    public sealed class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, PagedResponse<GetUserListQuery.Response>>
    {
        /// <summary>
        /// Hàm xử lý lấy dữ liệu danh sách có phân trang từ Database
        /// </summary>
        public async Task<PagedResponse<GetUserListQuery.Response>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ với các tham số phân trang và tìm kiếm
            var logData = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.Keyword), request.Keyword),
                    new(nameof(request.Status), request.Status),
                    new(nameof(request.PageIndex), request.PageIndex),
                    new(nameof(request.PageSize), request.PageSize)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    // Khởi tạo truy vấn từ bảng sy_users
                    var query = dbContext.Set<sy_users>().AsQueryable();

                    // BƯỚC 1: Áp dụng các bộ lọc nghiệp vụ
                    if (!string.IsNullOrEmpty(request.Status))
                    {
                        query = query.Where(u => u.Status == request.Status);
                    }

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        var keyword = request.Keyword.Trim().ToLower();
                        query = query.Where(u => 
                            u.Username.ToLower().Contains(keyword) ||
                            u.Email.ToLower().Contains(keyword) ||
                            (u.EmployeeCode != null && u.EmployeeCode.ToLower().Contains(keyword)));
                    }

                    // BƯỚC 2: Lấy tổng số bản ghi trước khi phân trang để tính toán UI
                    var totalItems = await query.CountAsync(cancellationToken);

                    // BƯỚC 3: Áp dụng sắp xếp và phân trang trực tiếp từ tham số Query
                    // Sử dụng request.Offset và request.PageSize đã được BaseListQuery tính toán sẵn
                    var items = await query
                        .OrderByDescending(u => u.CreatedAt)
                        .Skip(request.Offset)
                        .Take(request.PageSize)
                        .Select(u => new GetUserListQuery.Response
                        {
                            Uuid = u.Uuid,
                            Username = u.Username,
                            EmployeeCode = u.EmployeeCode ?? string.Empty,
                            RoleCode = u.RoleCode ?? string.Empty,
                            Status = u.Status,
                            CreatedAt = u.CreatedAt
                        })
                        .ToListAsync(cancellationToken);

                    // BƯỚC 4: Đóng gói kết quả trả về theo chuẩn PagedResult
                    var result = new PagedResult<GetUserListQuery.Response>
                    {
                        Items = items,
                        Paging = new PagingInfo
                        {
                            PageIndex = request.PageIndex,
                            PageSize = request.PageSize,
                            TotalItems = totalItems
                        }
                    };

                    var response = ResponseHelper.PagedSuccess(result, string.Format(CoreResource.common_listSuccess, CoreResource.entity_user));

                    logData.Result = new { ItemCount = items.Count, TotalItems = totalItems };
                    logData.ReturnCode = response.ReturnCode;
                    logData.Message = response.Message;

                    return response;
                }
                catch (Exception ex)
                {
                    logData.Message = ex.Message;
                    logData.IsException = true;
                    logData.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                    
                    return ResponseHelper.PagedError<GetUserListQuery.Response>(CoreResource.common_error);
                }
                finally
                {
                    // Luôn ghi log API trong mọi trường hợp
                    UniLogManager.WriteApiLog(logData);
                }
            }
        }
    }

    #endregion
}