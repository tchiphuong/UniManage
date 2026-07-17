using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.HrDepartment.Commands;
using UniManage.Shared.Application.Modules.HrDepartment.Queries;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.WebApi.Authorization;

namespace UniManage.WebApi.Controllers.HumanResource;

/// <summary>
/// Department management controller
/// </summary>
[ApiController]
[Route("api/v1/departments")]
public class HrDepartmentsController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public HrDepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/departments/combobox

    /// <summary>
    /// Get list of departments for combobox
    /// </summary>
    [HttpGet("combobox")]
    [PermissionAuthorize(CoreFunction.MsDepartment, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox(
        CancellationToken cancellationToken)
    {
        var query = new GetDepartmentComboboxQuery { HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/departments

    /// <summary>
    /// Get list of departments for page
    /// </summary>
    [HttpGet]
    [PermissionAuthorize(CoreFunction.MsDepartment, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<PagedResult<GetDepartmentListQuery.Response>>>> GetList(
        [FromQuery] GetDepartmentListQuery query,
        CancellationToken cancellationToken)
    {
        query ??= new GetDepartmentListQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/departments/{id}

    /// <summary>
    /// Get department by id
    /// </summary>
    [HttpGet("{id}")]
    [PermissionAuthorize(CoreFunction.MsDepartment, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetDepartmentByIdQuery.Response>>> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetDepartmentByIdQuery { Id = id, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/departments

    /// <summary>
    /// Create new department
    /// </summary>
    [HttpPost]
    [PermissionAuthorize(CoreFunction.MsDepartment, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreateDepartmentCommand.Response>>> Create(
        [FromBody] CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/departments/{id}

    /// <summary>
    /// Update department
    /// </summary>
    [HttpPut("{id}")]
    [PermissionAuthorize(CoreFunction.MsDepartment, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdateDepartmentCommand.Response>>> Update(
        [FromRoute] int id, [FromBody] UpdateDepartmentCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/departments

    /// <summary>
    /// Delete departments
    /// </summary>
    [HttpDelete]
    [PermissionAuthorize(CoreFunction.MsDepartment, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeleteDepartmentCommand.Response>>> Delete(
        [FromBody] List<int> ids, CancellationToken cancellationToken)
    {
        var command = new DeleteDepartmentCommand { Ids = ids, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion
}


