using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.WebApi.Controllers;
using UniManage.Shared.Application.Modules.MsUnit.Commands;
using UniManage.Shared.Application.Modules.MsUnit.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Master;

[ApiController]
[Route("api/v1/units")]
public class MsUnitsController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public MsUnitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/units/combobox

    [HttpGet("combobox")]
    [PermissionAuthorize(CoreFunction.MsUnit, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromQuery] GetUnitComboboxQuery query, CancellationToken cancellationToken)
    {
        query ??= new GetUnitComboboxQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/units

    [HttpGet]
    [PermissionAuthorize(CoreFunction.MsUnit, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<PagedResult<GetUnitListQuery.Response>>>> GetList([FromQuery] GetUnitListQuery query, CancellationToken cancellationToken)
    {
        query ??= new GetUnitListQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/units/{code}

    [HttpGet("{code}")]
    [PermissionAuthorize(CoreFunction.MsUnit, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetUnitByCodeQuery.Response?>>> GetByCode(string code, CancellationToken cancellationToken)
    {
        var query = new GetUnitByCodeQuery { Code = code, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/units

    [HttpPost]
    [PermissionAuthorize(CoreFunction.MsUnit, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreateUnitCommand.Response>>> Create([FromBody] CreateUnitCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/units/{code}

    [HttpPut("{code}")]
    [PermissionAuthorize(CoreFunction.MsUnit, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdateUnitCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateUnitCommand command, CancellationToken cancellationToken)
    {
        command ??= new UpdateUnitCommand();
        command.Code = code;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/units

    [HttpDelete]
    [PermissionAuthorize(CoreFunction.MsUnit, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeleteUnitCommand.Response>>> Delete([FromBody] DeleteUnitCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion
}


