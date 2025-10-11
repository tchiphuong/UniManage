using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.System.Auth;
using UniManage.Core.Models;

namespace UniManage.Api.Controllers.System
{
    /// <summary>
    /// Authentication Controller - Xử lý đăng nhập, đăng xuất
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="request">Thông tin đăng nhập</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Access token và thông tin user</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginCommand.Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<LoginCommand.Response>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<LoginCommand.Response>>> Login(
            [FromBody] LoginCommand request,
            CancellationToken ct)
        {
            var response = await _mediator.Send(request, ct);

            if (response.ReturnCode != 0)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Đăng xuất hệ thống
        /// </summary>
        [HttpPost("logout")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<object>>> Logout()
        {
            // TODO: Implement logout logic (invalidate refresh token, etc.)
            return Ok(ApiResponse<object>.Success(null, "Logged out successfully"));
        }
    }
}
