using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
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
        public async Task<ApiResponse<bool>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameters = new List<LogParamModel>
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

                var user = await dbContext.Set<SyUsers>()
                    .Include(u => u.HrEmployees)
                    .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

                if (user == null)
                {
                    return ResponseHelper.NotFound<bool>(CoreResource.entity_user);
                }

                user.Email = request.Email;
                user.Avatar = request.Avatar;
                user.UpdatedAt = DateTimeHelper.Now;
                user.UpdatedBy = username;

                if (user.HrEmployees != null)
                {
                    user.HrEmployees.PhoneNumber = request.PhoneNumber;
                    user.HrEmployees.Address = request.Address;
                    user.HrEmployees.UpdatedAt = DateTimeHelper.Now;
                    user.HrEmployees.UpdatedBy = username;
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                // Clear cache if needed (e.g., current user profile)
                if (user.HrEmployees != null)
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




