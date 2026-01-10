using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.HR.Positions;
using UniManage.Application.Queries.HR.Positions;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.HR;

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
    public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox(
        CancellationToken ct)
    {
        var query = new GetPositionComboboxQuery { HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/positions

    /// <summary>
    /// Get list of positions for page
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<GetPositionListQuery.Response>>>> GetList(
        [FromQuery] GetPositionListQuery query,
        CancellationToken ct)
    {
        query ??= new GetPositionListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/positions/{id}

    /// <summary>
    /// Get position by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetPositionByIdQuery.Response>>> GetById(int id, CancellationToken ct)
    {
        var query = new GetPositionByIdQuery { Id = id, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/positions

    /// <summary>
    /// Create new position
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreatePositionCommand.Response>>> Create(
        [FromBody] CreatePositionCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/positions/{id}

    /// <summary>
    /// Update position
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UpdatePositionCommand.Response>>> Update(
        [FromRoute] int id, [FromBody] UpdatePositionCommand command, CancellationToken ct)
    {
        command.Id = id;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/positions

    /// <summary>
    /// Delete positions
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult<ApiResponse<DeletePositionCommand.Response>>> Delete(
        [FromBody] List<int> ids, CancellationToken ct)
    {
        var command = new DeletePositionCommand { Ids = ids, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}
