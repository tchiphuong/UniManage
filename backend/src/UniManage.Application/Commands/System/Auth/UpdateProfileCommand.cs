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
using UniManage.Core.Constant;

namespace UniManage.Application.Commands.System.Auth
{
    #region Command

    public sealed class UpdateProfileCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public string? Email { get; init; }
        public string? Avatar { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Address { get; init; }
    }

    #endregion

    #region Validator

    public sealed class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage(CoreResource.validation_invalidFormat);
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateProfileCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameters = new List<CoreParamModel>
                {
                    new(nameof(request.Email), request.Email),
                    new(nameof(request.Avatar), request.Avatar),
                    new(nameof(request.PhoneNumber), request.PhoneNumber),
                    new(nameof(request.Address), request.Address)
                }
            };

            var username = request.HeaderInfo?.Username;
            if (string.IsNullOrEmpty(username))
            {
                return ResponseHelper.Error<bool>(CoreResource.common_unauthorized);
            }

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var user = await dbContext.Set<sy_users>()
                    .Include(u => u.hr_employees)
                    .FirstOrDefaultAsync(u => u.Username == username, ct);

                if (user == null)
                {
                    return ResponseHelper.NotFound<bool>(CoreResource.entity_user);
                }

                user.Email = request.Email;
                user.Avatar = request.Avatar;
                user.UpdatedAt = DateTimeHelper.Now;
                user.UpdatedBy = username;

                if (user.hr_employees != null)
                {
                    user.hr_employees.PhoneNumber = request.PhoneNumber;
                    user.hr_employees.Address = request.Address;
                    user.hr_employees.UpdatedAt = DateTimeHelper.Now;
                    user.hr_employees.UpdatedBy = username;
                }

                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync();

                // Clear cache if needed (e.g., current user profile)
                if (user.hr_employees != null)
                {
                    await CacheHelper.RemoveByPatternAsync(string.Format(CacheKeyConstant.System.CurrentUserProfile, username));
                }

                var response = ResponseHelper.Success(true, CoreResource.common_updateSuccess);
                log.ReturnCode = response.ReturnCode;
                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
