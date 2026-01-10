using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.Master.Countries;
using UniManage.Application.Queries.Master.Countries;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Master;

/// <summary>
/// Country management controller - Master Data
/// </summary>
[ApiController]
[Route("api/v1/countries")]
public class CountriesController : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    #endregion

    #region Constructor

    public CountriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET: /api/v1/master/countries/combobox

    /// <summary>
    /// Get list of countries for combobox
    /// </summary>
    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox(CancellationToken ct)
    {
        var query = new GetCountryComboboxQuery { HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/master/countries

    /// <summary>
    /// Get list of countries with pagination
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<GetCountryListQuery.Response>>>> GetList([FromQuery] GetCountryListQuery query, CancellationToken ct)
    {
        query ??= new GetCountryListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/master/countries/{code}

    /// <summary>
    /// Get country by code
    /// </summary>
    [HttpGet("{code}")]
    public async Task<ActionResult<ApiResponse<GetCountryByCodeQuery.Response>>> GetByCode(string code, CancellationToken ct)
    {
        var query = new GetCountryByCodeQuery { Code = code, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region POST: /api/v1/master/countries

    /// <summary>
    /// Create new country
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateCountryCommand.Response>>> Create([FromBody] CreateCountryCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/master/countries/{code}

    /// <summary>
    /// Update country
    /// </summary>
    [HttpPut("{code}")]
    public async Task<ActionResult<ApiResponse<UpdateCountryCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateCountryCommand command, CancellationToken ct)
    {
        command.Code = code;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/master/countries

    /// <summary>
    /// Delete countries
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult<ApiResponse<DeleteCountryCommand.Response>>> Delete([FromBody] List<string> codes, CancellationToken ct)
    {
        var command = new DeleteCountryCommand { Codes = codes, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}
