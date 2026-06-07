using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Modules.Master.Application.Queries.Provinces;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.Master
{
    [ApiController]
    [Route("api/v1/provinces")]
    public class ProvincesController : BaseController
    {
        private readonly IMediator _mediator;

        public ProvincesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromQuery] string? countryCode, CancellationToken cancellationToken)
        {
            var query = new GetProvinceComboboxQuery { CountryCode = countryCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}


