using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Authorization;
using UniManage.Api.Controllers;
using UniManage.Application.Commands.Master.Units;
using UniManage.Application.Queries.Master.Units;
using UniManage.Core.Constant;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Master;

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
    [PermissionAuthorize(CoreFunction.Main, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<ComboboxModel>>>> GetCombobox([FromQuery] GetUnitComboboxQuery query, CancellationToken ct)
    {
        query ??= new GetUnitComboboxQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/units

    [HttpGet]
    [PermissionAuthorize(CoreFunction.Main, CoreAction.View)]
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
    [PermissionAuthorize(CoreFunction.Main, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetUnitByCodeQuery.Response?>>> GetByCode(string code, CancellationToken ct)
    {
        var query = new GetUnitByCodeQuery { Code = code, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/units

    [HttpPost]
    [PermissionAuthorize(CoreFunction.Main, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreateUnitCommand.Response>>> Create([FromBody] CreateUnitCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/units/{code}

    [HttpPut("{code}")]
    [PermissionAuthorize(CoreFunction.Main, CoreAction.Update)]
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
    [PermissionAuthorize(CoreFunction.Main, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeleteUnitCommand.Response>>> Delete([FromBody] DeleteUnitCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}
