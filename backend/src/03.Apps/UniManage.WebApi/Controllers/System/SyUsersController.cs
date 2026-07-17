using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.WebApi.Extensions;
using UniManage.Shared.Application.Modules.SyUser.Commands;
using UniManage.Shared.Application.Modules.SyUser.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Resource;
using UniManage.Shared.Infrastructure.Utilities;

namespace UniManage.WebApi.Controllers.System;

[ApiController]
[Route("api/v1/users")]
public class SyUsersController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public SyUsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/users

    /// <summary>
    /// Get paginated list of users
    /// </summary>
    [HttpGet]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<GetUserListQuery.Response>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<GetUserListQuery.Response>>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<PagedResult<GetUserListQuery.Response>>>> GetUserList([FromQuery] GetUserListQuery query, CancellationToken cancellationToken)
    {
        query ??= new GetUserListQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/users/combobox

    /// <summary>
    /// Get users for combobox
    /// </summary>
    [HttpGet("combobox")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<ComboboxItem<long>>>>> GetCombobox([FromQuery] GetUserComboboxQuery query, CancellationToken cancellationToken)
    {
        query ??= new GetUserComboboxQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/users/{id}

    /// <summary>
    /// Get user by id
    /// </summary>
    [HttpGet("{uuid}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetUserByIdQuery.Response>>> GetUserById(Guid uuid, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { Uuid = uuid, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/users/{id}/roles

    /// <summary>
    /// Get user's roles
    /// </summary>
    [HttpGet("{uuid}/roles")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<SyUserRoleModel>>>> GetRoles(Guid uuid, CancellationToken cancellationToken)
    {
        var query = new GetUserRolesQuery { Uuid = uuid, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/users

    /// <summary>
    /// Create new user
    /// </summary>
    [HttpPost]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreateUserCommand.Response>>> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/users/change-password

    /// <summary>
    /// Change current user password
    /// </summary>
    [HttpPost("change-password")]
    public async Task<ActionResult<ApiResponse<bool>>> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken cancellationToken)
    {

        var username = this.Username;
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized(ResponseHelper.Error<bool>(CoreResource.common_unauthorized));
        }

        command.Username = username; // [SECURITY] REQUIRED: Lấy username từ Token để tránh IDOR
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/users/{id}

    /// <summary>
    /// Update user information
    /// </summary>
    [HttpPut("{uuid}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdateUserCommand.Response>>> UpdateUser([FromRoute] Guid uuid, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        command ??= new UpdateUserCommand();
        command.Uuid = uuid;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/users

    /// <summary>
    /// Delete users (soft delete)
    /// </summary>
    [HttpDelete]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeleteUserCommand.Response>>> DeleteUser([FromBody] List<Guid> uuids, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand { Uuids = uuids, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/users/{id}/roles

    /// <summary>
    /// Assign role to user
    /// </summary>
    [HttpPost("{uuid}/roles")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<AssignUserRoleCommand.Response>>> AssignRole([FromRoute] Guid uuid, [FromBody] AssignUserRoleCommand command, CancellationToken cancellationToken)
    {
        command.UserUuid = uuid;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/users/{id}/roles/{roleCode}

    /// <summary>
    /// Remove role from user
    /// </summary>
    [HttpDelete("{uuid}/roles/{roleCode}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<RemoveUserRoleCommand.Response>>> RemoveRole(Guid uuid, string roleCode, CancellationToken cancellationToken)
    {
        var command = new RemoveUserRoleCommand { UserUuid = uuid, RoleCode = roleCode, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion
}

