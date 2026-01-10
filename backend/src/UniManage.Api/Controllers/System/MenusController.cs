using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Controllers;
using UniManage.Application.Queries.System.Menus;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.System;

[Authorize]
[ApiController]
[Route("api/v1/menus")]
public class MenusController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public MenusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/menus

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GetMenuListQuery.Response>>>> GetList([FromQuery] GetMenuListQuery query, CancellationToken ct)
    {
        query ??= new GetMenuListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion
}
