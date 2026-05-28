using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Authorization;
using UniManage.Api.Extensions;
using UniManage.Application.Commands.System.User;
using UniManage.Application.Queries.System.User;
using UniManage.Core.Constant;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.System;

[ApiController]
[Route("api/v1/users")]
public class UsersController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
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
    public async Task<ActionResult<ApiResponse<PagedResult<GetUserListQuery.Response>>>> GetUserList([FromQuery] GetUserListQuery query, CancellationToken ct)
    {
        query ??= new GetUserListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/users/combobox

    /// <summary>
    /// Get users for combobox
    /// </summary>
    [HttpGet("combobox")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<ComboboxModel<long>>>>> GetCombobox([FromQuery] GetUserComboboxQuery query, CancellationToken ct)
    {
        query ??= new GetUserComboboxQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/users/{id}

    /// <summary>
    /// Get user by id
    /// </summary>
    [HttpGet("{uuid}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetUserByIdQuery.Response>>> GetUserById(Guid uuid, CancellationToken ct)
    {
        var query = new GetUserByIdQuery { Uuid = uuid, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/users/{id}/roles

    /// <summary>
    /// Get user's roles
    /// </summary>
    [HttpGet("{uuid}/roles")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<UserRoleDto>>>> GetRoles(Guid uuid, CancellationToken ct)
    {
        var query = new GetUserRolesQuery { Uuid = uuid, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/users

    /// <summary>
    /// Create new user
    /// </summary>
    [HttpPost]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreateUserCommand.Response>>> CreateUser([FromBody] CreateUserCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/users/change-password

    /// <summary>
    /// Change current user password
    /// </summary>
    [HttpPost("change-password")]
    public async Task<ActionResult<ApiResponse<bool>>> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        command.Username = this.Username; // [SECURITY] REQUIRED: Lấy username từ Token để tránh IDOR
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/users/{id}

    /// <summary>
    /// Update user information
    /// </summary>
    [HttpPut("{uuid}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdateUserCommand.Response>>> UpdateUser([FromRoute] Guid uuid, [FromBody] UpdateUserCommand command, CancellationToken ct)
    {
        command ??= new UpdateUserCommand();
        command.Uuid = uuid;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/users

    /// <summary>
    /// Delete users (soft delete)
    /// </summary>
    [HttpDelete]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeleteUserCommand.Response>>> DeleteUser([FromBody] List<Guid> uuids, CancellationToken ct)
    {
        var command = new DeleteUserCommand { Uuids = uuids, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/users/{id}/roles

    /// <summary>
    /// Assign role to user
    /// </summary>
    [HttpPost("{uuid}/roles")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<AssignUserRoleCommand.Response>>> AssignRole([FromRoute] Guid uuid, [FromBody] AssignUserRoleCommand command, CancellationToken ct)
    {
        command.UserUuid = uuid;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/users/{id}/roles/{roleCode}

    /// <summary>
    /// Remove role from user
    /// </summary>
    [HttpDelete("{uuid}/roles/{roleCode}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<RemoveUserRoleCommand.Response>>> RemoveRole(Guid uuid, string roleCode, CancellationToken ct)
    {
        var command = new RemoveUserRoleCommand { UserUuid = uuid, RoleCode = roleCode, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}