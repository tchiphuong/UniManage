using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.Inventory.ItemDetails;
using UniManage.Application.Queries.Inventory.ItemDetails;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/item-details")]
    public class ItemDetailsController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public ItemDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/item-details

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemDetailListQuery.Result>>>> GetList([FromQuery] GetItemDetailListQuery query, CancellationToken ct)
        {
            query ??= new GetItemDetailListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/item-details/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetItemDetailByIdQuery.Result>>> GetById([FromRoute] long id, CancellationToken ct)
        {
            var query = new GetItemDetailByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/item-details

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemDetailCommand.Response>>> Create([FromBody] CreateItemDetailCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/item-details/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateItemDetailCommand.Response>>> Update([FromRoute] long id, [FromBody] UpdateItemDetailCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemDetailCommand.Response>("Id mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/item-details

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemDetailCommand.Response>>> Delete([FromBody] DeleteItemDetailCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
