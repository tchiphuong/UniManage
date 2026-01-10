using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.Sales.Customers;
using UniManage.Application.Queries.Sales.Customers;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.Sales
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomersController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/customers

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetCustomerListQuery.Result>>>> GetList([FromQuery] GetCustomerListQuery query, CancellationToken ct)
        {
            query ??= new GetCustomerListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/customers/{code}

        [HttpGet("{code}")]
        public async Task<ActionResult<ApiResponse<GetCustomerByCodeQuery.Result>>> GetByCode([FromRoute] string code, CancellationToken ct)
        {
            var query = new GetCustomerByCodeQuery { Code = code };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/customers

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateCustomerCommand.Response>>> Create([FromBody] CreateCustomerCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/customers/{code}

        [HttpPut("{code}")]
        public async Task<ActionResult<ApiResponse<UpdateCustomerCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateCustomerCommand command, CancellationToken ct)
        {
            if (code != command.Code)
            {
                return BadRequest(ResponseHelper.Error<UpdateCustomerCommand.Response>("Code mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/customers

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteCustomerCommand.Response>>> Delete([FromBody] DeleteCustomerCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
