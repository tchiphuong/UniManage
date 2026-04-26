using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using DbContext = UniManage.Core.Database.DbContext;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Core.Constant;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Departments
{
    #region Command

    /// <summary>
    /// Lệnh xóa hàng loạt phòng ban
    /// </summary>
    public sealed class DeleteDepartmentCommand : BaseCommand, IRequest<ApiResponse<DeleteDepartmentCommand.Response>>
    {
        /// <summary>
        /// Danh sách ID các phòng ban cần xóa
        /// </summary>
        public List<int> Ids { get; set; } = new();

        public sealed class Response
        {
            public bool Success { get; init; }
            public int DeletedCount { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh xóa phòng ban
    /// </summary>
    public sealed class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "Ids"))
                .MustAsync(async (ids, cancel) => await AreDepartmentsExistByIdsAsync(ids))
                .WithMessage(string.Format(CoreResource.common_notFound, CoreResource.entity_department));
        }

        private static async Task<bool> AreDepartmentsExistByIdsAsync(List<int> ids)
        {
            if (ids == null || !ids.Any()) return false;
            using (var dbContext = new DbContext())
            {
                var count = await dbContext.Set<hr_departments>().CountAsync(x => ids.Contains(x.Id));
                return count == ids.Distinct().Count();
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh xóa hàng loạt phòng ban
    /// </summary>
    public sealed class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ApiResponse<DeleteDepartmentCommand.Response>>
    {
        public async Task<ApiResponse<DeleteDepartmentCommand.Response>> Handle(DeleteDepartmentCommand request, CancellationToken ct)
        {
            // Khởi tạo log với danh sách ID ngăn cách bởi dấu phẩy
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.Ids), string.Join(",", request.Ids))
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                // Truy vấn danh sách thực thể cần xóa
                var departmentsToDelete = await dbContext.Set<hr_departments>()
                    .Where(x => request.Ids.Contains(x.Id))
                    .ToListAsync(ct);

                var deletedCount = departmentsToDelete.Count;
                if (deletedCount > 0)
                {
                    // Thực hiện xóa hàng loạt (Bulk Delete)
                    dbContext.Set<hr_departments>().RemoveRange(departmentsToDelete);
                    await dbContext.SaveChangesAsync(ct);
                }

                await dbContext.CommitAsync();

                var responseData = new DeleteDepartmentCommand.Response { Success = true, DeletedCount = deletedCount };
                var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, CoreResource.entity_department));
                
                log.Result = responseData;
                log.Message = response.Message;
                log.ReturnCode = response.ReturnCode;

                return response;
            }
            catch (Exception ex)
            {
                // Ghi nhận lỗi ngoại lệ vào hệ thống log
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<DeleteDepartmentCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
