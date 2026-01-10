using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.Inventory.ItemInventory;
using UniManage.Application.Queries.Inventory.ItemInventory;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/item-inventory")]
    public class ItemInventoryController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public ItemInventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/item-inventory

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemInventoryListQuery.Result>>>> GetList([FromQuery] GetItemInventoryListQuery query, CancellationToken ct)
        {
            query ??= new GetItemInventoryListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/item-inventory

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemInventoryCommand.Response>>> Create([FromBody] CreateItemInventoryCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/item-inventory/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateItemInventoryCommand.Response>>> Update([FromRoute] int id, [FromBody] UpdateItemInventoryCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemInventoryCommand.Response>("Id mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/item-inventory

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemInventoryCommand.Response>>> Delete([FromBody] DeleteItemInventoryCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
