using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.HR.Employees;
using UniManage.Application.Queries.HR.Employees;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.HR;

/// <summary>
/// Employee management controller
/// </summary>
[ApiController]
[Route("api/v1/employees")]
public class EmployeesController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/employees

    /// <summary>
    /// Get paginated list of employees
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<GetEmployeeListQuery.Response>>>> List([FromQuery] GetEmployeeListQuery req, CancellationToken ct)
    {
        req ??= new GetEmployeeListQuery();
        req.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(req, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/employees/{id}

    /// <summary>
    /// Get employee by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeByIdQuery.Response>>> GetById(long id, CancellationToken ct)
    {
        var query = new GetEmployeeByIdQuery
        {
            Id = id,
            HeaderInfo = HeaderInfo
        };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/employees

    /// <summary>
    /// Create new employee
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateEmployeeCommand.Response>>> Create([FromBody] CreateEmployeeCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/employees/{id}

    /// <summary>
    /// Update employee information
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UpdateEmployeeCommand.Response>>> Update([FromRoute] long id, [FromBody] UpdateEmployeeCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/employees

    /// <summary>
    /// Delete employees (soft delete)
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult<ApiResponse<DeleteEmployeeCommand.Response>>> DeleteEmployee([FromBody] List<long> ids, CancellationToken ct)
    {
        var command = new DeleteEmployeeCommand
        {
            Id = ids.FirstOrDefault(),
            HeaderInfo = HeaderInfo
        };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}