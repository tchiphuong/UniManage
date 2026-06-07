using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Modules.HumanResource.Application.Commands.Employees;
using UniManage.Modules.HumanResource.Application.Queries.Employees;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.WebApi.Authorization;

namespace UniManage.WebApi.Controllers.HumanResource;

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
    [PermissionAuthorize(CoreFunction.HrEmployee, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<PagedResult<GetEmployeeListQuery.Response>>>> List([FromQuery] GetEmployeeListQuery req, CancellationToken cancellationToken)
    {
        req ??= new GetEmployeeListQuery();
        req.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(req, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/employees/{id}

    /// <summary>
    /// Get employee by id
    /// </summary>
    [HttpGet("{id}")]
    [PermissionAuthorize(CoreFunction.HrEmployee, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetEmployeeByIdQuery.Response>>> GetById(long id, CancellationToken cancellationToken)
    {
        var query = new GetEmployeeByIdQuery
        {
            Id = id,
            HeaderInfo = HeaderInfo
        };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/employees

    /// <summary>
    /// Create new employee
    /// </summary>
    [HttpPost]
    [PermissionAuthorize(CoreFunction.HrEmployee, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreateEmployeeCommand.Response>>> Create([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/employees/{id}

    /// <summary>
    /// Update employee information
    /// </summary>
    [HttpPut("{id}")]
    [PermissionAuthorize(CoreFunction.HrEmployee, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdateEmployeeCommand.Response>>> Update([FromRoute] long id, [FromBody] UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/employees

    /// <summary>
    /// Delete employees (soft delete)
    /// </summary>
    [HttpDelete]
    [PermissionAuthorize(CoreFunction.HrEmployee, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeleteEmployeeCommand.Response>>> DeleteEmployee([FromBody] List<long> ids, CancellationToken cancellationToken)
    {
        var command = new DeleteEmployeeCommand
        {
            Id = ids.FirstOrDefault(),
            HeaderInfo = HeaderInfo
        };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion
}
