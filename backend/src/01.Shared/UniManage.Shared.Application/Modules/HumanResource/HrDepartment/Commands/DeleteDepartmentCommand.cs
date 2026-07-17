using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;


namespace UniManage.Shared.Application.Modules.HrDepartment.Commands
{
    #region Command

    /// <summary>
    /// L?nh x?a h?ng lo?t ph?ng ban
    /// </summary>
    public sealed class DeleteDepartmentCommand : BaseCommand, IRequest<ApiResponse<DeleteDepartmentCommand.Response>>
    {
        /// <summary>
        /// Danh s?ch ID c?c ph?ng ban c?n x?a
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
    /// B? ki?m tra d? li?u cho l?nh x?a ph?ng ban
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
                var count = await dbContext.Set<HrDepartments>().CountAsync(x => ids.Contains(x.Id));
                return count == ids.Distinct().Count();
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l? l?nh x?a h?ng lo?t ph?ng ban
    /// </summary>
    public sealed class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ApiResponse<DeleteDepartmentCommand.Response>>
    {
        public async Task<ApiResponse<DeleteDepartmentCommand.Response>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log v?i danh s?ch ID ngan c?ch b?i d?u ph?y
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Ids), string.Join(",", request.Ids))
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                // Truy v?n danh s?ch th?c th? c?n x?a
                var departmentsToDelete = await dbContext.Set<HrDepartments>()
                    .Where(x => request.Ids.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                var deletedCount = departmentsToDelete.Count;
                if (deletedCount > 0)
                {
                    // Th?c hi?n x?a h?ng lo?t (Bulk Delete)
                    dbContext.Set<HrDepartments>().RemoveRange(departmentsToDelete);
                    await dbContext.SaveChangesAsync(cancellationToken);
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
                // Ghi nh?n l?i ngo?i l? v?o h? th?ng log
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



