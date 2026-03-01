using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Authorization;
using UniManage.Application.Commands.System.Roles;
using UniManage.Application.Queries.System.Roles;
using UniManage.Core.Constant;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.System
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RolesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/roles/combobox

        /// <summary>
        /// Get roles combobox items
        /// </summary>
        [HttpGet("combobox")]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox([FromQuery] GetRoleComboboxQuery query, CancellationToken ct)
        {
            query ??= new GetRoleComboboxQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/roles

        /// <summary>
        /// Get paginated list of roles
        /// </summary>
        [HttpGet]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetRoleListQuery.Result>>>> GetList([FromQuery] GetRoleListQuery query, CancellationToken ct)
        {
            query ??= new GetRoleListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/roles/{roleCode}

        /// <summary>
        /// Get role by code
        /// </summary>
        [HttpGet("{roleCode}")]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetRoleByCodeQuery.Result>>> GetByCode([FromRoute] string roleCode, CancellationToken ct)
        {
            var query = new GetRoleByCodeQuery { RoleCode = roleCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/roles

        /// <summary>
        /// Create new role
        /// </summary>
        [HttpPost]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateRoleCommand.Response>>> Create([FromBody] CreateRoleCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/roles/{roleCode}

        /// <summary>
        /// Update role
        /// </summary>
        [HttpPut("{roleCode}")]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.Update)]
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

        #endregion

        #region DELETE: /api/v1/roles

        /// <summary>
        /// Delete roles
        /// </summary>
        [HttpDelete]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteRoleCommand.Response>>> Delete([FromBody] DeleteRoleCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
