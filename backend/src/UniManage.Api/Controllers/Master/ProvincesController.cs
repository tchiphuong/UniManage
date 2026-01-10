using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Queries.Master.Provinces;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Master
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
        public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox([FromQuery] string? countryCode, CancellationToken ct)
        {
            var query = new GetProvinceComboboxQuery { CountryCode = countryCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }
    }
}
