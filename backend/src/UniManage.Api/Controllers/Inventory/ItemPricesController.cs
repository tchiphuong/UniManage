using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.Inventory.ItemPrices;
using UniManage.Application.Queries.Inventory.ItemPrices;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/item-prices")]
    public class ItemPricesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public ItemPricesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/item-prices

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemPriceListQuery.Result>>>> GetList([FromQuery] GetItemPriceListQuery query, CancellationToken ct)
        {
            query ??= new GetItemPriceListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/item-prices

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemPriceCommand.Response>>> Create([FromBody] CreateItemPriceCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/item-prices/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateItemPriceCommand.Response>>> Update([FromRoute] int id, [FromBody] UpdateItemPriceCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemPriceCommand.Response>("Id mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/item-prices

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemPriceCommand.Response>>> Delete([FromBody] DeleteItemPriceCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
