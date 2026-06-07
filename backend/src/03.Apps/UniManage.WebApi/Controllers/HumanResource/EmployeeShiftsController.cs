using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Modules.HumanResource.Application.Commands.EmployeeShifts;
using UniManage.Modules.HumanResource.Application.Queries.EmployeeShifts;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.WebApi.Authorization;

namespace UniManage.WebApi.Controllers.HumanResource
{
    [ApiController]
    [Route("api/v1/employee-shifts")]
    public class EmployeeShiftsController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public EmployeeShiftsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/employee-shifts

        [HttpGet]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetEmployeeShiftListQuery.Result>>>> GetList([FromQuery] GetEmployeeShiftListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetEmployeeShiftListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetEmployeeShiftByIdQuery.Result>>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetEmployeeShiftByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/employee-shifts

        [HttpPost]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateEmployeeShiftCommand.Response>>> Create([FromBody] CreateEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/employee-shifts/{id}

        [HttpPut("{id}")]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateEmployeeShiftCommand.Response>>> Update([FromRoute] int id, [FromBody] UpdateEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateEmployeeShiftCommand.Response>("Id mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/employee-shifts

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteEmployeeShiftCommand.Response>>> Delete([FromBody] DeleteEmployeeShiftCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

