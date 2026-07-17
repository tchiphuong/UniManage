using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    #region Command

    /// <summary>
    /// Refresh Token Command - L�m m?i access token
    /// </summary>
    public sealed class RefreshTokenCommand : BaseCommand, IRequest<ApiResponse<RefreshTokenCommand.Response>>
    {
        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        public class Response
        {
            public string AccessToken { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
            public int ExpiresIn { get; set; }
            public string TokenType { get; set; } = "Bearer";
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Refresh Token Command Validator
    /// </summary>
    public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required");
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Refresh Token Command Handler � uses shared IIdentityServerClient + ApiLogModel
    /// </summary>
    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<RefreshTokenCommand.Response>>
    {
        private readonly IIdentityServerClient _identityClient;

        public RefreshTokenCommandHandler(IIdentityServerClient identityClient)
        {
            _identityClient = identityClient;
        }

        public async Task<ApiResponse<RefreshTokenCommand.Response>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log v?i HeaderInfo t? BaseCommand
            

            try
            {
                var (success, tokenData, error) = await _identityClient.RefreshTokenAsync(
                    request.RefreshToken, cancellationToken);

                if (!success || tokenData == null)
                {
                    var errorResponse = ResponseHelper.Error<RefreshTokenCommand.Response>("Invalid or expired refresh token");
                    return errorResponse;
                }

                var responseData = new RefreshTokenCommand.Response
                {
                    AccessToken = tokenData.access_token,
                    RefreshToken = tokenData.refresh_token,
                    ExpiresIn = tokenData.expires_in,
                    TokenType = tokenData.token_type
                };

                var response = ResponseHelper.Success(responseData, CoreResource.auth_tokenRefreshed);
                return response;
            }
            finally
            {
}
        }
    }

    #endregion
}







