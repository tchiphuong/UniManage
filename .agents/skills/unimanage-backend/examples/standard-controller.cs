// =============================================================================
// STANDARD CONTROLLER TEMPLATE — UniManage RESTful Pattern
// =============================================================================
// Rules:
//   - Inherit BaseController (NOT ControllerBase)
//   - Thin controllers: only routing + _mediator.Send()
//   - MUST assign HeaderInfo before Send
//   - Region per endpoint: #region GET: /api/v1/items
//   - Parameters on same line as method signature
// =============================================================================

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UniManage.WebApi.Controllers;

/// <summary>
/// RESTful API controller for item management
/// </summary>
[ApiController]
[Route("api/v1/items")]
public class ItemsController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor with MediatR injection
    /// </summary>
    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/items

    /// <summary>
    /// Get paginated list of items
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResponse<GetItemListQuery.Response>>> GetList([FromQuery] GetItemListQuery query, CancellationToken ct)
    {
        query ??= new GetItemListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/items/{uuid}

    /// <summary>
    /// Get item details by UUID
    /// </summary>
    [HttpGet("{uuid}")]
    public async Task<ActionResult<ApiResponse<GetItemByIdQuery.Response>>> GetById([FromRoute] Guid uuid, CancellationToken ct)
    {
        var query = new GetItemByIdQuery { Uuid = uuid, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/items

    /// <summary>
    /// Create a new item
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateItemCommand.Response>>> Create([FromBody] CreateItemCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/items/{uuid}

    /// <summary>
    /// Update an existing item
    /// </summary>
    [HttpPut("{uuid}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update([FromRoute] Guid uuid, [FromBody] UpdateItemCommand command, CancellationToken ct)
    {
        command ??= new UpdateItemCommand();
        command.Uuid = uuid;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/items

    /// <summary>
    /// Delete one or more items
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult<ApiResponse<bool>>> Delete([FromBody] DeleteItemCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}
