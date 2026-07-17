using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Modules.System.SyAuth.Services;
using UniManage.Shared.Resource;
using UniManage.Shared.Infrastructure.Services;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    #region Command

    /// <summary>
    /// Command c?p nh?t FCM Token cho thi?t b? ngu?i d�ng.
    /// </summary>
    public sealed class UpdateFcmTokenCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public string DeviceId { get; set; } = default!;
        public string? FcmToken { get; set; }
        public string? DeviceType { get; set; }
        public long UserId { get; set; } // L?y t? token/context
    }

    #endregion

    #region Validator

    public sealed class UpdateFcmTokenValidator : AbstractValidator<UpdateFcmTokenCommand>
    {
        public UpdateFcmTokenValidator()
        {
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, "DeviceId"));
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateFcmTokenCommandHandler : IRequestHandler<UpdateFcmTokenCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateFcmTokenCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo());
            await AuthHelper.UpdateDeviceTokenAsync(request.UserId, request.DeviceId, request.FcmToken, request.DeviceType, log, cancellationToken);

            var response = ResponseHelper.Success(true, CoreResource.common_success);
            return response;
        }
    }

    #endregion
}








