using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.MsCurrency.Queries;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Master
{
    [ApiController]
    [Route("api/v1/currencies")]
    public class MsCurrenciesController : BaseController
    {
        private readonly IMediator _mediator;

        public MsCurrenciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox(CancellationToken cancellationToken)
        {
            var query = new GetCurrencyComboboxQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}


