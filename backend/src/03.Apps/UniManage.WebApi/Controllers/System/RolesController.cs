using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.System.Application.Commands.Roles;
using UniManage.Modules.System.Application.Queries.Roles;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.System
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
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromQuery] GetRoleComboboxQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetRoleComboboxQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/roles

        /// <summary>
        /// Get paginated list of roles
        /// </summary>
        [HttpGet]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetRoleListQuery.Result>>>> GetList([FromQuery] GetRoleListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetRoleListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/roles/{roleCode}

        /// <summary>
        /// Get role by code
        /// </summary>
        [HttpGet("{roleCode}")]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetRoleByCodeQuery.Result>>> GetByCode([FromRoute] string roleCode, CancellationToken cancellationToken)
        {
            var query = new GetRoleByCodeQuery { RoleCode = roleCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/roles

        /// <summary>
        /// Create new role
        /// </summary>
        [HttpPost]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateRoleCommand.Response>>> Create([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/roles/{roleCode}

        /// <summary>
        /// Update role
        /// </summary>
        [HttpPut("{roleCode}")]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateRoleCommand.Response>>> Update([FromRoute] string roleCode, [FromBody] UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            if (roleCode != command.RoleCode)
            {
                return BadRequest(ResponseHelper.Error<UpdateRoleCommand.Response>("RoleCode mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/roles/{roleCode}/permissions

        /// <summary>
        /// Get role permission matrix
        /// </summary>
        [HttpGet("{roleCode}/permissions")]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<List<GetRolePermissionsQuery.Result>>>> GetPermissions([FromRoute] string roleCode, CancellationToken cancellationToken)
        {
            var query = new GetRolePermissionsQuery { RoleCode = roleCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/roles/{roleCode}/permissions

        /// <summary>
        /// Update role permission matrix
        /// </summary>
        [HttpPut("{roleCode}/permissions")]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<bool>>> UpdatePermissions([FromRoute] string roleCode, [FromBody] UpdateRolePermissionsCommand command, CancellationToken cancellationToken)
        {
            if (roleCode != command.RoleCode)
            {
                return BadRequest(ResponseHelper.Error<bool>("RoleCode mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/roles

        /// <summary>
        /// Delete roles
        /// </summary>
        [HttpDelete]
        [PermissionAuthorize(CoreFunction.SyRole, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteRoleCommand.Response>>> Delete([FromBody] DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}


