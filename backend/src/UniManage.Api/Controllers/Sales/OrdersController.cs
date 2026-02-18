using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.Sales.Orders;
using UniManage.Application.Queries.Sales.Orders;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Sales
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
        public async Task<ActionResult<ApiResponse<PagedResult<GetOrderListQuery.Result>>>> GetList([FromQuery] GetOrderListQuery query, CancellationToken ct)
        {
            query ??= new GetOrderListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/orders/{orderCode}

        [HttpGet("{orderCode}")]
        public async Task<ActionResult<ApiResponse<GetOrderByCodeQuery.Result>>> GetByCode([FromRoute] string orderCode, CancellationToken ct)
        {
            var query = new GetOrderByCodeQuery { OrderCode = orderCode };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/orders

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateOrderCommand.Response>>> Create([FromBody] CreateOrderCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PATCH: /api/v1/orders/{orderCode}/status

        [HttpPatch("{orderCode}/status")]
        public async Task<ActionResult<ApiResponse<UpdateOrderStatusCommand.Response>>> UpdateStatus([FromRoute] string orderCode, [FromBody] UpdateOrderStatusCommand command, CancellationToken ct)
        {
            var cmd = new UpdateOrderStatusCommand
            {
                OrderCode = orderCode,
                Status = command.Status,
                HeaderInfo = HeaderInfo
            };
            var result = await _mediator.Send(cmd, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/orders

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteOrderCommand.Response>>> Delete([FromBody] DeleteOrderCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
