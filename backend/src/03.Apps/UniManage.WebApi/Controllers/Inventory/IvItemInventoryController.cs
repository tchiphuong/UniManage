using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.IvItemInventory.Commands;
using UniManage.Shared.Application.Modules.IvItemInventory.Queries;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/item-inventory")]
    public class IvItemInventoryController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public IvItemInventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/item-inventory

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemInventoryListQuery.Result>>>> GetList([FromQuery] GetItemInventoryListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetItemInventoryListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/item-inventory

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemInventoryCommand.Response>>> Create([FromBody] CreateItemInventoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/item-inventory/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateItemInventoryCommand.Response>>> Update([FromRoute] int id, [FromBody] UpdateItemInventoryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemInventoryCommand.Response>("Id mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/item-inventory

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemInventoryCommand.Response>>> Delete([FromBody] DeleteItemInventoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

