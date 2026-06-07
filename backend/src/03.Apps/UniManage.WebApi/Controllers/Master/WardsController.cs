using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Modules.Master.Application.Queries.Wards;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.Master
{
    [ApiController]
    [Route("api/v1/wards")]
    public class WardsController : BaseController
    {
        private readonly IMediator _mediator;

        public WardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromQuery] string? provinceCode, CancellationToken cancellationToken)
        {
            var query = new GetWardComboboxQuery { ProvinceCode = provinceCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}


