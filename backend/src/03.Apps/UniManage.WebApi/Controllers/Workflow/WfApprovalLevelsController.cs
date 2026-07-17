using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.WfApprovalRoute.Commands;
using UniManage.Shared.Application.Modules.WfApprovalRoute.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Workflow
{
    [ApiController]
    [Route("api/v1/approval-routes")]
    public class ApprovalRoutesController : BaseController
    {
        private readonly IMediator _mediator;

        public ApprovalRoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [PermissionAuthorize(CoreFunction.WfApprovalLevel, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetApprovalRouteListQuery.Result>>>> GetList([FromQuery] GetApprovalRouteListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetApprovalRouteListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.WfApprovalLevel, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetApprovalRouteByIdQuery.Result>>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetApprovalRouteByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [PermissionAuthorize(CoreFunction.WfApprovalLevel, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateApprovalRouteCommand.Response>>> Create([FromBody] CreateApprovalRouteCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.WfApprovalLevel, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteApprovalRouteCommand.Response>>> Delete([FromBody] DeleteApprovalRouteCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}

