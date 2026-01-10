using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Controllers;
using UniManage.Application.Commands.Master.Units;
using UniManage.Application.Queries.Master.Units;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Master;

[Authorize]
[ApiController]
[Route("api/v1/units")]
public class UnitsController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public UnitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/units/combobox

    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox([FromQuery] GetUnitComboboxQuery query, CancellationToken ct)
    {
        query ??= new GetUnitComboboxQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/units

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<GetUnitListQuery.Response>>>> GetList([FromQuery] GetUnitListQuery query, CancellationToken ct)
    {
        query ??= new GetUnitListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/units/{code}

    [HttpGet("{code}")]
    public async Task<ActionResult<ApiResponse<GetUnitByCodeQuery.Response?>>> GetByCode(string code, CancellationToken ct)
    {
        var query = new GetUnitByCodeQuery { Code = code, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/units

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateUnitCommand.Response>>> Create([FromBody] CreateUnitCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/units/{code}

    [HttpPut("{code}")]
    public async Task<ActionResult<ApiResponse<UpdateUnitCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateUnitCommand command, CancellationToken ct)
    {
        command ??= new UpdateUnitCommand();
        command.Code = code;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/units

    [HttpDelete]
    public async Task<ActionResult<ApiResponse<DeleteUnitCommand.Response>>> Delete([FromBody] DeleteUnitCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}
