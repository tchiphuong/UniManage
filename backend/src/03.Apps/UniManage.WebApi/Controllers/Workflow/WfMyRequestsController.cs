using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.WfRequest.Commands;
using UniManage.Shared.Application.Modules.WfRequest.Queries;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.WebApi.Authorization;

namespace UniManage.WebApi.Controllers.Workflow
{
    [ApiController]
    [Route("api/v1/requests")]
    public class RequestsController : BaseController
    {
        private readonly IMediator _mediator;

        public RequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [PermissionAuthorize(CoreFunction.WfMyRequests, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetRequestListQuery.Result>>>> GetList([FromQuery] GetRequestListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetRequestListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.WfMyRequests, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetRequestByIdQuery.Result>>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetRequestByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [PermissionAuthorize(CoreFunction.WfMyRequests, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateRequestCommand.Response>>> Create([FromBody] CreateRequestCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{id}/submissions")]
        [PermissionAuthorize(CoreFunction.WfMyRequests, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<SubmitRequestCommand.Response>>> Submit([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new SubmitRequestCommand { RequestId = id };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{id}/approvals")]
        [PermissionAuthorize(CoreFunction.WfMyRequests, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<ApproveRequestCommand.Response>>> Approve([FromRoute] int id, [FromBody] ApproveRequestCommand command, CancellationToken cancellationToken)
        {
            if (id != command.RequestId)
            {
                return BadRequest(ResponseHelper.Error<ApproveRequestCommand.Response>("RequestId mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{id}/rejections")]
        [PermissionAuthorize(CoreFunction.WfMyRequests, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<RejectRequestCommand.Response>>> Reject([FromRoute] int id, [FromBody] RejectRequestCommand command, CancellationToken cancellationToken)
        {
            if (id != command.RequestId)
            {
                return BadRequest(ResponseHelper.Error<RejectRequestCommand.Response>("RequestId mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.WfMyRequests, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteRequestCommand.Response>>> Delete([FromBody] DeleteRequestCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}

