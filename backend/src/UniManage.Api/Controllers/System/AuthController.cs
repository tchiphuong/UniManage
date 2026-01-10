using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.System.Auth;
using UniManage.Application.Queries.System.Auth;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.System
{
    /// <summary>
    /// Authentication Controller - Xử lý đăng nhập, đăng xuất, quản lý token
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region POST: /api/v1/auth/login

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="request">Thông tin đăng nhập (username, password)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Access token, refresh token và thông tin user</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<LoginCommand.Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<LoginCommand.Response>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<LoginCommand.Response>>> Login([FromBody] LoginCommand request, CancellationToken ct)
        {
            request ??= new();
            request.HeaderInfo = HeaderInfo;

            var response = await _mediator.Send(request, ct);

            if (response.ReturnCode != 0)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        #endregion

        #region POST: /api/v1/auth/refresh

        /// <summary>
        /// Làm mới access token bằng refresh token
        /// </summary>
        /// <param name="request">Refresh token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Access token và refresh token mới</returns>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<RefreshTokenCommand.Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<RefreshTokenCommand.Response>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<RefreshTokenCommand.Response>>> RefreshToken([FromBody] RefreshTokenCommand request, CancellationToken ct)
        {
            request ??= new();
            request.HeaderInfo = HeaderInfo;

            var response = await _mediator.Send(request, ct);

            if (response.ReturnCode != 0)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        #endregion

        #region POST: /api/v1/auth/logout

        /// <summary>
        /// Đăng xuất hệ thống - revoke refresh token
        /// </summary>
        /// <param name="request">Optional refresh token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Logout result</returns>
        [HttpPost("logout")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> Logout([FromBody] LogoutCommand? request, CancellationToken ct)
        {
            request ??= new LogoutCommand();
            request.HeaderInfo = HeaderInfo;

            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        #endregion

        #region POST: /api/v1/auth/change-password

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="request">Old password và new password</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Password change result</returns>
        [HttpPost("change-password")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> ChangePassword([FromBody] ChangePasswordCommand request, CancellationToken ct)
        {
            request ??= new();
            request.HeaderInfo = HeaderInfo;

            var response = await _mediator.Send(request, ct);

            if (response.ReturnCode != 0)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        #endregion

        #region GET: /api/v1/auth/me

        /// <summary>
        /// Lấy thông tin user hiện tại (từ JWT token)
        /// </summary>
        /// <param name="username">Username từ JWT claims</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Current user information</returns>
        [HttpGet("me")]
        [ProducesResponseType(typeof(ApiResponse<GetCurrentUserQuery.Result>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<GetCurrentUserQuery.Result>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<GetCurrentUserQuery.Result>>> GetCurrentUser([FromQuery] string username, CancellationToken ct)
        {
            var request = new GetCurrentUserQuery { Username = username, HeaderInfo = HeaderInfo };

            var response = await _mediator.Send(request, ct);

            if (response.ReturnCode != 0)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }

        #endregion

        #region POST: /api/v1/auth/forgot-password

        /// <summary>
        /// Quên mật khẩu - Gửi email reset password
        /// </summary>
        /// <param name="request">Email hoặc username</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Forgot password result</returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> ForgotPassword([FromBody] ForgotPasswordCommand request, CancellationToken ct)
        {
            request ??= new();
            request.HeaderInfo = HeaderInfo;

            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        #endregion

        #region POST: /api/v1/auth/reset-password

        /// <summary>
        /// Reset mật khẩu với token từ email
        /// </summary>
        /// <param name="request">Token và mật khẩu mới</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Reset password result</returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> ResetPassword([FromBody] ResetPasswordCommand request, CancellationToken ct)
        {
            request ??= new();
            request.HeaderInfo = HeaderInfo;

            var response = await _mediator.Send(request, ct);

            if (response.ReturnCode != 0)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        #endregion

        #region GET: /api/v1/auth/check-username/{username}

        /// <summary>
        /// Kiểm tra username có tồn tại
        /// </summary>
        /// <param name="username">Username cần kiểm tra</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Username exists result</returns>
        [HttpGet("check-username/{username}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<CheckUsernameExistsQuery.Result>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<CheckUsernameExistsQuery.Result>>> CheckUsername([FromRoute] string username, CancellationToken ct)
        {
            var request = new CheckUsernameExistsQuery { Username = username, HeaderInfo = HeaderInfo };

            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        #endregion

        #region GET: /api/v1/auth/check-email/{email}

        /// <summary>
        /// Kiểm tra email có tồn tại
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Email exists result</returns>
        [HttpGet("check-email/{email}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<CheckEmailExistsQuery.Result>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<CheckEmailExistsQuery.Result>>> CheckEmail([FromRoute] string email, CancellationToken ct)
        {
            var request = new CheckEmailExistsQuery { Email = email, HeaderInfo = HeaderInfo };

            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        #endregion

        #region GET: /api/v1/auth/permissions

        /// <summary>
        /// Lấy danh sách permissions và roles của user hiện tại
        /// </summary>
        /// <param name="username">Username từ JWT claims</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>User permissions and roles</returns>
        [HttpGet("permissions")]
        [ProducesResponseType(typeof(ApiResponse<GetUserPermissionsQuery.Result>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<GetUserPermissionsQuery.Result>>> GetPermissions([FromQuery] string username, CancellationToken ct)
        {
            var request = new GetUserPermissionsQuery { Username = username, HeaderInfo = HeaderInfo };

            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        #endregion
    }
}
