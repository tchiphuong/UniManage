using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.IvItemDetail.Commands;
using UniManage.Shared.Application.Modules.IvItemDetail.Queries;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/item-details")]
    public class IvItemDetailsController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public IvItemDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/item-details

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemDetailListQuery.Result>>>> GetList([FromQuery] GetItemDetailListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetItemDetailListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/item-details/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetItemDetailByIdQuery.Result>>> GetById([FromRoute] long id, CancellationToken cancellationToken)
        {
            var query = new GetItemDetailByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/item-details

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemDetailCommand.Response>>> Create([FromBody] CreateItemDetailCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/item-details/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateItemDetailCommand.Response>>> Update([FromRoute] long id, [FromBody] UpdateItemDetailCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemDetailCommand.Response>("Id mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/item-details

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemDetailCommand.Response>>> Delete([FromBody] DeleteItemDetailCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

