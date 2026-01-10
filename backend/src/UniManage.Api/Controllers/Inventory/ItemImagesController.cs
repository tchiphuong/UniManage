using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.Inventory.ItemImages;
using UniManage.Application.Queries.Inventory.ItemImages;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/item-images")]
    public class ItemImagesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public ItemImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/item-images

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemImageListQuery.Result>>>> GetList([FromQuery] GetItemImageListQuery query, CancellationToken ct)
        {
            query ??= new GetItemImageListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/item-images

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemImageCommand.Response>>> Create([FromBody] CreateItemImageCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/item-images/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateItemImageCommand.Response>>> Update([FromRoute] int id, [FromBody] UpdateItemImageCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemImageCommand.Response>("Id mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/item-images

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemImageCommand.Response>>> Delete([FromBody] DeleteItemImageCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
