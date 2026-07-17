using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.IvItemPrice.Commands;
using UniManage.Shared.Application.Modules.IvItemPrice.Queries;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/item-prices")]
    public class IvItemPricesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public IvItemPricesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/item-prices

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemPriceListQuery.Result>>>> GetList([FromQuery] GetItemPriceListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetItemPriceListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/item-prices

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemPriceCommand.Response>>> Create([FromBody] CreateItemPriceCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/item-prices/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateItemPriceCommand.Response>>> Update([FromRoute] int id, [FromBody] UpdateItemPriceCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemPriceCommand.Response>("Id mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/item-prices

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemPriceCommand.Response>>> Delete([FromBody] DeleteItemPriceCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

