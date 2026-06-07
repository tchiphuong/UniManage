using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.HumanResource.Application.Commands.Positions;
using UniManage.Modules.HumanResource.Application.Queries.Positions;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.HumanResource;

/// <summary>
/// Position management controller
/// </summary>
[ApiController]
[Route("api/v1/positions")]
public class PositionsController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public PositionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/positions/combobox

    /// <summary>
    /// Get list of positions for combobox
    /// </summary>
    [HttpGet("combobox")]
    [PermissionAuthorize(CoreFunction.HrPosition, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox(
        CancellationToken cancellationToken)
    {
        var query = new GetPositionComboboxQuery { HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/positions

    /// <summary>
    /// Get list of positions for page
    /// </summary>
    [HttpGet]
    [PermissionAuthorize(CoreFunction.HrPosition, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<PagedResult<GetPositionListQuery.Response>>>> GetList(
        [FromQuery] GetPositionListQuery query,
        CancellationToken cancellationToken)
    {
        query ??= new GetPositionListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/positions/{id}

    /// <summary>
    /// Get position by id
    /// </summary>
    [HttpGet("{id}")]
    [PermissionAuthorize(CoreFunction.HrPosition, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetPositionByIdQuery.Response>>> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetPositionByIdQuery { Id = id, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/positions

    /// <summary>
    /// Create new position
    /// </summary>
    [HttpPost]
    [PermissionAuthorize(CoreFunction.HrPosition, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreatePositionCommand.Response>>> Create(
        [FromBody] CreatePositionCommand command, CancellationToken cancellationToken)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/positions/{id}

    /// <summary>
    /// Update position
    /// </summary>
    [HttpPut("{id}")]
    [PermissionAuthorize(CoreFunction.HrPosition, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdatePositionCommand.Response>>> Update(
        [FromRoute] int id, [FromBody] UpdatePositionCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/positions

    /// <summary>
    /// Delete positions
    /// </summary>
    [HttpDelete]
    [PermissionAuthorize(CoreFunction.HrPosition, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeletePositionCommand.Response>>> Delete(
        [FromBody] List<int> ids, CancellationToken cancellationToken)
    {
        var command = new DeletePositionCommand { Ids = ids, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion
}


