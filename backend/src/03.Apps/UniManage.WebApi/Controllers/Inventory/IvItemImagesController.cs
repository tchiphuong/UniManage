using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.IvItemImage.Commands;
using UniManage.Shared.Application.Modules.IvItemImage.Queries;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/item-images")]
    public class IvItemImagesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public IvItemImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/item-images

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemImageListQuery.Result>>>> GetList([FromQuery] GetItemImageListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetItemImageListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/item-images

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemImageCommand.Response>>> Create([FromBody] CreateItemImageCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/item-images/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateItemImageCommand.Response>>> Update([FromRoute] int id, [FromBody] UpdateItemImageCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemImageCommand.Response>("Id mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/item-images

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemImageCommand.Response>>> Delete([FromBody] DeleteItemImageCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

