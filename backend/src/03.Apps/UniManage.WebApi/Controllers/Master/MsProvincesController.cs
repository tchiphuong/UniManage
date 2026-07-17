using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.MsProvince.Queries;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Master
{
    [ApiController]
    [Route("api/v1/provinces")]
    public class MsProvincesController : BaseController
    {
        private readonly IMediator _mediator;

        public MsProvincesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromQuery] string? countryCode, CancellationToken cancellationToken)
        {
            var query = new GetProvinceComboboxQuery { CountryCode = countryCode };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}


