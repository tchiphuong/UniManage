using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.SaOrder.Commands;
using UniManage.Shared.Application.Modules.SaOrder.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Sales
{
    [ApiController]
    [Route("api/v1/orders")]
    public class SaOrdersController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SaOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/orders

        [HttpGet]
        [PermissionAuthorize(CoreFunction.SaSalesOrders, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetOrderListQuery.Result>>>> GetList([FromQuery] GetOrderListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetOrderListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/orders/{orderCode}

        [HttpGet("{orderCode}")]
        [PermissionAuthorize(CoreFunction.SaSalesOrders, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetOrderByCodeQuery.Result>>> GetByCode([FromRoute] string orderCode, CancellationToken cancellationToken)
        {
            var query = new GetOrderByCodeQuery { OrderCode = orderCode };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/orders

        [HttpPost]
        [PermissionAuthorize(CoreFunction.SaSalesOrders, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateOrderCommand.Response>>> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PATCH: /api/v1/orders/{orderCode}/status

        [HttpPatch("{orderCode}/status")]
        [PermissionAuthorize(CoreFunction.SaSalesOrders, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateOrderStatusCommand.Response>>> UpdateStatus([FromRoute] string orderCode, [FromBody] UpdateOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var cmd = new UpdateOrderStatusCommand
            {
                OrderCode = orderCode,
                Status = command.Status,
                HeaderInfo = HeaderInfo
            };
            var result = await _mediator.Send(cmd, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/orders

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.SaSalesOrders, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteOrderCommand.Response>>> Delete([FromBody] DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

