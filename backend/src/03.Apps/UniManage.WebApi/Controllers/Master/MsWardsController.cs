using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.MsWard.Queries;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Master
{
    [ApiController]
    [Route("api/v1/wards")]
    public class MsWardsController : BaseController
    {
        private readonly IMediator _mediator;

        public MsWardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromQuery] string? provinceCode, CancellationToken cancellationToken)
        {
            var query = new GetWardComboboxQuery { ProvinceCode = provinceCode };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}


