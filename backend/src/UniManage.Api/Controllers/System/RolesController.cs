using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.System.Roles;
using UniManage.Application.Queries.System.Roles;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.System
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RolesController : BaseController
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox([FromQuery] GetRoleComboboxQuery query, CancellationToken ct)
        {
            query ??= new GetRoleComboboxQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetRoleListQuery.Result>>>> GetList([FromQuery] GetRoleListQuery query, CancellationToken ct)
        {
            query ??= new GetRoleListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet("{roleCode}")]
        public async Task<ActionResult<ApiResponse<GetRoleByCodeQuery.Result>>> GetByCode([FromRoute] string roleCode, CancellationToken ct)
        {
            var query = new GetRoleByCodeQuery { RoleCode = roleCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateRoleCommand.Response>>> Create([FromBody] CreateRoleCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpPut("{roleCode}")]
        public async Task<ActionResult<ApiResponse<UpdateRoleCommand.Response>>> Update([FromRoute] string roleCode, [FromBody] UpdateRoleCommand command, CancellationToken ct)
        {
            if (roleCode != command.RoleCode)
            {
                return BadRequest(ResponseHelper.Error<UpdateRoleCommand.Response>("RoleCode mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteRoleCommand.Response>>> Delete([FromBody] DeleteRoleCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }
    }
}
