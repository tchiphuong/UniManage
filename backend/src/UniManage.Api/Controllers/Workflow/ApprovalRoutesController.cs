using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Authorization;
using UniManage.Application.Commands.Workflow.ApprovalRoutes;
using UniManage.Application.Queries.Workflow.ApprovalRoutes;
using UniManage.Core.Constant;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Workflow
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
        public async Task<ActionResult<ApiResponse<PagedResult<GetApprovalRouteListQuery.Result>>>> GetList([FromQuery] GetApprovalRouteListQuery query, CancellationToken ct)
        {
            query ??= new GetApprovalRouteListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.Main, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetApprovalRouteByIdQuery.Result>>> GetById([FromRoute] int id, CancellationToken ct)
        {
            var query = new GetApprovalRouteByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpPost]
        [PermissionAuthorize(CoreFunction.Main, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateApprovalRouteCommand.Response>>> Create([FromBody] CreateApprovalRouteCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.Main, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteApprovalRouteCommand.Response>>> Delete([FromBody] DeleteApprovalRouteCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }
    }
}
