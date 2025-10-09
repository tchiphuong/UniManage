using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Domains.Command.System.Auth;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Controllers.System
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            CoreResponse response;
            command ??= new LoginCommand();
            command.HeaderInfo = HeaderInfo;

            try
            {
                var validationResult = await new LoginCommandValidator().ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    response = new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData);
                    response.Errors = validationResult.Errors.Select(response => new FieldErrorModel(response.PropertyName, response.ErrorMessage)).ToList();
                    return BadRequest(response);
                }
                response = await _mediator.Send(command, cancellationToken);
            }
            catch (Exception ex)
            {
                response = new CoreResponse(CoreApiReturnCode.ExceptionOccurred, CoreResource.Common_msg_ExceptionOccurred);
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
