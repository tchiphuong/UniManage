using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.SaCustomer.Commands;
using UniManage.Shared.Application.Modules.SaCustomer.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.Sales
{
    [ApiController]
    [Route("api/v1/customers")]
    public class SaCustomersController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SaCustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/customers

        [HttpGet]
        [PermissionAuthorize(CoreFunction.MsCustomer, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetCustomerListQuery.Result>>>> GetList([FromQuery] GetCustomerListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetCustomerListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/customers/{code}

        [HttpGet("{code}")]
        [PermissionAuthorize(CoreFunction.MsCustomer, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetCustomerByCodeQuery.Result>>> GetByCode([FromRoute] string code, CancellationToken cancellationToken)
        {
            var query = new GetCustomerByCodeQuery { Code = code };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/customers

        [HttpPost]
        [PermissionAuthorize(CoreFunction.MsCustomer, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateCustomerCommand.Response>>> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/customers/{code}

        [HttpPut("{code}")]
        [PermissionAuthorize(CoreFunction.MsCustomer, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateCustomerCommand.Response>>> Update([FromRoute] string code, [FromBody] UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (code != command.Code)
            {
                return BadRequest(ResponseHelper.Error<UpdateCustomerCommand.Response>("Code mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/customers

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.MsCustomer, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteCustomerCommand.Response>>> Delete([FromBody] DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

