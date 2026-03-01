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

    #region GET: /api/v1/users/{id}

    /// <summary>
    /// Get user by id
    /// </summary>
    [HttpGet("{id}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetUserByIdQuery.Response>>> GetUserById(long id, CancellationToken ct)
    {
        var query = new GetUserByIdQuery { Id = id, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/users/{id}/roles

    /// <summary>
    /// Get user's roles
    /// </summary>
    [HttpGet("{id}/roles")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<UserRoleDto>>>> GetRoles(long id, CancellationToken ct)
    {
        var query = new GetUserRolesQuery { Id = id, HeaderInfo = HeaderInfo };
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

    #region PUT: /api/v1/users/{id}

    /// <summary>
    /// Update user information
    /// </summary>
    [HttpPut("{id}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdateUserCommand.Response>>> UpdateUser([FromRoute] long id, [FromBody] UpdateUserCommand command, CancellationToken ct)
    {
        command ??= new UpdateUserCommand();
        command.Id = id;
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
    public async Task<ActionResult<ApiResponse<DeleteUserCommand.Response>>> DeleteUser([FromBody] List<long> ids, CancellationToken ct)
    {
        // [SECURITY] H6 — Always assign HeaderInfo for audit trail
        var command = new DeleteUserCommand
        {
            Ids = ids,
            HeaderInfo = HeaderInfo,
        };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/users/{id}/roles

    /// <summary>
    /// Assign role to user
    /// </summary>
    [HttpPost("{id}/roles")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<AssignUserRoleCommand.Response>>> AssignRole([FromRoute] long id, [FromBody] AssignUserRoleCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/users/{id}/roles/{roleCode}

    /// <summary>
    /// Remove role from user
    /// </summary>
    [HttpDelete("{id}/roles/{roleCode}")]
    [PermissionAuthorize(CoreFunction.SyUser, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<RemoveUserRoleCommand.Response>>> RemoveRole(long id, string roleCode, CancellationToken ct)
    {
        var command = new RemoveUserRoleCommand
        {
            UserId = id,
            RoleCode = roleCode,
            HeaderInfo = HeaderInfo
        };

        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}