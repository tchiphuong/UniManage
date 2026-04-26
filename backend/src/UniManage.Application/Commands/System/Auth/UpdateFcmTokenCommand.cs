using FluentValidation;
using MediatR;
using UniManage.Core.Services;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Auth
{
    #region Command

    /// <summary>
    /// Command cập nhật FCM Token cho thiết bị người dùng.
    /// </summary>
    public sealed class UpdateFcmTokenCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public string DeviceId { get; set; } = default!;
        public string? FcmToken { get; set; }
        public string? DeviceType { get; set; }
        public long UserId { get; set; } // Lấy từ token/context
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
        public async Task<ApiResponse<bool>> Handle(UpdateFcmTokenCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo());
            await AuthHelper.UpdateDeviceTokenAsync(request.UserId, request.DeviceId, request.FcmToken, request.DeviceType, log, ct);

            var response = ResponseHelper.Success(true, CoreResource.common_success);
            return response;
        }
    }

    #endregion
}


