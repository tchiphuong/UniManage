using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.Sales.Application.Commands.Orders;
using UniManage.Modules.Sales.Application.Queries.Orders;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.Sales
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrdersController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/orders

        [HttpGet]
        [PermissionAuthorize(CoreFunction.SaOrder, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetOrderListQuery.Result>>>> GetList([FromQuery] GetOrderListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetOrderListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/orders/{orderCode}

        [HttpGet("{orderCode}")]
        [PermissionAuthorize(CoreFunction.SaOrder, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetOrderByCodeQuery.Result>>> GetByCode([FromRoute] string orderCode, CancellationToken cancellationToken)
        {
            var query = new GetOrderByCodeQuery { OrderCode = orderCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/orders

        [HttpPost]
        [PermissionAuthorize(CoreFunction.SaOrder, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateOrderCommand.Response>>> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PATCH: /api/v1/orders/{orderCode}/status

        [HttpPatch("{orderCode}/status")]
        [PermissionAuthorize(CoreFunction.SaOrder, CoreAction.Update)]
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
        [PermissionAuthorize(CoreFunction.SaOrder, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteOrderCommand.Response>>> Delete([FromBody] DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

