using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.HR.Departments;
using UniManage.Application.Queries.HR.Departments;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.HR;

/// <summary>
/// Department management controller
/// </summary>
[ApiController]
[Route("api/v1/departments")]
public class DepartmentsController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/departments/combobox

    /// <summary>
    /// Get list of departments for combobox
    /// </summary>
    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox(
        CancellationToken ct)
    {
        var query = new GetDepartmentComboboxQuery { HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/departments

    /// <summary>
    /// Get list of departments for page
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<GetDepartmentListQuery.Response>>>> GetList(
        [FromQuery] GetDepartmentListQuery query,
        CancellationToken ct)
    {
        query ??= new GetDepartmentListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/departments/{id}

    /// <summary>
    /// Get department by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDepartmentByIdQuery.Response>>> GetById(int id, CancellationToken ct)
    {
        var query = new GetDepartmentByIdQuery { Id = id, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/departments

    /// <summary>
    /// Create new department
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateDepartmentCommand.Response>>> Create(
        [FromBody] CreateDepartmentCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/departments/{id}

    /// <summary>
    /// Update department
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UpdateDepartmentCommand.Response>>> Update(
        [FromRoute] int id, [FromBody] UpdateDepartmentCommand command, CancellationToken ct)
    {
        command.Id = id;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/departments

    /// <summary>
    /// Delete departments
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult<ApiResponse<DeleteDepartmentCommand.Response>>> Delete(
        [FromBody] List<int> ids, CancellationToken ct)
    {
        var command = new DeleteDepartmentCommand { Ids = ids, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}
