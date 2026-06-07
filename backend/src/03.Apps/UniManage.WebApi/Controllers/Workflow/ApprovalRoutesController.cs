using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.Workflow.Application.Commands.ApprovalRoutes;
using UniManage.Modules.Workflow.Application.Queries.ApprovalRoutes;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Application.Models;

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
        [PermissionAuthorize(CoreFunction.Main, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetApprovalRouteListQuery.Result>>>> GetList([FromQuery] GetApprovalRouteListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetApprovalRouteListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.Main, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetApprovalRouteByIdQuery.Result>>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetApprovalRouteByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [PermissionAuthorize(CoreFunction.Main, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateApprovalRouteCommand.Response>>> Create([FromBody] CreateApprovalRouteCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.Main, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteApprovalRouteCommand.Response>>> Delete([FromBody] DeleteApprovalRouteCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}

