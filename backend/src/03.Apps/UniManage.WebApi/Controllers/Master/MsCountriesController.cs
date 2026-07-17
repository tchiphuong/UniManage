using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.MsCountry.Commands;
using UniManage.Shared.Application.Modules.MsCountry.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Master;

/// <summary>
/// Country management controller - Master Data
/// </summary>
[ApiController]
[Route("api/v1/countries")]
public class MsCountriesController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    #endregion

    #region Constructor

    public MsCountriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/countries/combobox

    /// <summary>
    /// Get list of countries for combobox
    /// </summary>
    [HttpGet("combobox")]
    [PermissionAuthorize(CoreFunction.MsCountry, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox(CancellationToken cancellationToken)
    {
        var query = new GetCountryComboboxQuery { HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/countries

    /// <summary>
    /// Get list of countries with pagination
    /// </summary>
    [HttpGet]
    [PermissionAuthorize(CoreFunction.MsCountry, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<PagedResult<GetCountryListQuery.Response>>>> GetList([FromQuery] GetCountryListQuery query, CancellationToken cancellationToken)
    {
        query ??= new GetCountryListQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/countries/{code}

    /// <summary>
    /// Get country by code
    /// </summary>
    [HttpGet("{code}")]
    [PermissionAuthorize(CoreFunction.MsCountry, CoreAction.View)]
    public async Task<ActionResult<ApiResponse<GetCountryByCodeQuery.Response>>> GetByCode(string code, CancellationToken cancellationToken)
    {
        var query = new GetCountryByCodeQuery { Code = code, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/countries

    /// <summary>
    /// Create new country
    /// </summary>
    [HttpPost]
    [PermissionAuthorize(CoreFunction.MsCountry, CoreAction.Create)]
    public async Task<ActionResult<ApiResponse<CreateCountryCommand.Response>>> Create([FromBody] CreateCountryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/countries/{code}

    /// <summary>
    /// Update country
    /// </summary>
    [HttpPut("{code}")]
    [PermissionAuthorize(CoreFunction.MsCountry, CoreAction.Update)]
    public async Task<ActionResult<ApiResponse<UpdateCountryCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateCountryCommand command, CancellationToken cancellationToken)
    {
        command.Code = code;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/countries

    /// <summary>
    /// Delete countries
    /// </summary>
    [HttpDelete]
    [PermissionAuthorize(CoreFunction.MsCountry, CoreAction.Delete)]
    public async Task<ActionResult<ApiResponse<DeleteCountryCommand.Response>>> Delete([FromBody] List<string> codes, CancellationToken cancellationToken)
    {
        var command = new DeleteCountryCommand { Codes = codes, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    #endregion
}


