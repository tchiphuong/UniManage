using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Domains.Command.System.User;
using UniManage.Api.Domains.Query.System.User;
using UniManage.Core.Helpers;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region POST: /api/v1/users

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            request ??= new();
            request.HeaderInfo = HeaderInfo;

            var validation = await new CreateUserCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/users/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetUserByIdQuery { Id = id, HeaderInfo = HeaderInfo };

            var validation = await new GetUserByIdQueryValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/users

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetUserListQuery request, CancellationToken cancellationToken)
        {
            request ??= new();
            request.HeaderInfo = HeaderInfo;

            var validation = await new GetUserListQueryValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/users/{id}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
        {
            request ??= new();
            request.Id = id;
            request.HeaderInfo = HeaderInfo;

            var validation = await new UpdateUserCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/users/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] List<int> ids, CancellationToken cancellationToken)
        {
            var request = new DeleteUserCommand{ Ids = ids, HeaderInfo = HeaderInfo };

            var validation = await new DeleteUserCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}
