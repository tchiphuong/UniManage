using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.Sales.Application.Commands.Customers;
using UniManage.Modules.Sales.Application.Queries.Customers;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.Sales
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
        [PermissionAuthorize(CoreFunction.SaCustomer, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetCustomerListQuery.Result>>>> GetList([FromQuery] GetCustomerListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetCustomerListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/customers/{code}

        [HttpGet("{code}")]
        [PermissionAuthorize(CoreFunction.SaCustomer, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetCustomerByCodeQuery.Result>>> GetByCode([FromRoute] string code, CancellationToken cancellationToken)
        {
            var query = new GetCustomerByCodeQuery { Code = code };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/customers

        [HttpPost]
        [PermissionAuthorize(CoreFunction.SaCustomer, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateCustomerCommand.Response>>> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/customers/{code}

        [HttpPut("{code}")]
        [PermissionAuthorize(CoreFunction.SaCustomer, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateCustomerCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (code != command.Code)
            {
                return BadRequest(ResponseHelper.Error<UpdateCustomerCommand.Response>("Code mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/customers

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.SaCustomer, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteCustomerCommand.Response>>> Delete([FromBody] DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

