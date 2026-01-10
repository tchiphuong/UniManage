using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.Inventory.Items;
using UniManage.Application.Queries.Inventory.Items;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Inventory
{
    [ApiController]
    [Route("api/v1/items")]
    public class ItemsController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/items

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemListQuery.Result>>>> GetList([FromQuery] GetItemListQuery query, CancellationToken ct)
        {
            query ??= new GetItemListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/items/{code}

        [HttpGet("{code}")]
        public async Task<ActionResult<ApiResponse<GetItemByCodeQuery.Result>>> GetByCode([FromRoute] string code, CancellationToken ct)
        {
            var query = new GetItemByCodeQuery { Code = code };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/items

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateItemCommand.Response>>> Create([FromBody] CreateItemCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/items/{code}

        [HttpPut("{code}")]
        public async Task<ActionResult<ApiResponse<UpdateItemCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateItemCommand command, CancellationToken ct)
        {
            if (code != command.Code)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemCommand.Response>("Code mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/items

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteItemCommand.Response>>> Delete([FromBody] DeleteItemCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
