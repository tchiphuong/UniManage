using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Queries.Master.Wards;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Master
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
        public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox([FromQuery] string? provinceCode, CancellationToken ct)
        {
            var query = new GetWardComboboxQuery { ProvinceCode = provinceCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }
    }
}
