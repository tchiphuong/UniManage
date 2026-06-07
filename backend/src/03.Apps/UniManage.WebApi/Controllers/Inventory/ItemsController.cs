using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.Inventory.Application.Commands.Items;
using UniManage.Modules.Inventory.Application.Queries.Items;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.Inventory
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
        [PermissionAuthorize(CoreFunction.ItItem, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetItemListQuery.Result>>>> GetList([FromQuery] GetItemListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetItemListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/items/{code}

        [HttpGet("{code}")]
        [PermissionAuthorize(CoreFunction.ItItem, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetItemByCodeQuery.Result>>> GetByCode([FromRoute] string code, CancellationToken cancellationToken)
        {
            var query = new GetItemByCodeQuery { Code = code };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/items

        [HttpPost]
        [PermissionAuthorize(CoreFunction.ItItem, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateItemCommand.Response>>> Create([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/items/{code}

        [HttpPut("{code}")]
        [PermissionAuthorize(CoreFunction.ItItem, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateItemCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateItemCommand command, CancellationToken cancellationToken)
        {
            if (code != command.Code)
            {
                return BadRequest(ResponseHelper.Error<UpdateItemCommand.Response>("Code mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/items

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.ItItem, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteItemCommand.Response>>> Delete([FromBody] DeleteItemCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

