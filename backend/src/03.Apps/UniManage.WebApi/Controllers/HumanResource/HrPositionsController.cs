using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.HrPosition.Commands;
using UniManage.Shared.Application.Modules.HrPosition.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.HumanResource;

/// <summary>
/// Position management controller
/// </summary>
[ApiController]
[Route("api/v1/positions")]
public class HrPositionsController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public HrPositionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/positions/combobox

    /// <summary>
    /// Get list of positions for combobox
    /// </summary>
    [HttpGet("combobox")]
    [PermissionAuthorize(CoreFunction.MsPosition, CoreAction.View)]
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
    [PermissionAuthorize(CoreFunction.MsPosition, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<PagedResult<GetPositionListQuery.Response>>>> GetList(
        [FromQuery] GetPositionListQuery query,
        CancellationToken cancellationToken)
    {
        query ??= new GetPositionListQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/positions/{id}

    /// <summary>
    /// Get position by id
    /// </summary>
    [HttpGet("{id}")]
    [PermissionAuthorize(CoreFunction.MsPosition, CoreAction.View)]
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
    [PermissionAuthorize(CoreFunction.MsPosition, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreatePositionCommand.Response>>> Create(
        [FromBody] CreatePositionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/positions/{id}

    /// <summary>
    /// Update position
    /// </summary>
    [HttpPut("{id}")]
    [PermissionAuthorize(CoreFunction.MsPosition, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdatePositionCommand.Response>>> Update(
        [FromRoute] int id, [FromBody] UpdatePositionCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/positions

    /// <summary>
    /// Delete positions
    /// </summary>
    [HttpDelete]
    [PermissionAuthorize(CoreFunction.MsPosition, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeletePositionCommand.Response>>> Delete(
        [FromBody] List<int> ids, CancellationToken cancellationToken)
    {
        var command = new DeletePositionCommand { Ids = ids, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion
}


