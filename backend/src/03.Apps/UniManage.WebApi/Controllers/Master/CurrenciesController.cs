using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Modules.Master.Application.Queries.Currencies;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.Master
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
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox(CancellationToken cancellationToken)
        {
            var query = new GetCurrencyComboboxQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}


