using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Queries.Master.Currencies;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Master
{
    [ApiController]
    [Route("api/v1/currencies")]
    public class CurrenciesController : BaseController
    {
        private readonly IMediator _mediator;

        public CurrenciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox(CancellationToken ct)
        {
            var query = new GetCurrencyComboboxQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }
    }
}
