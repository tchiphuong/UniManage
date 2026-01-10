using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.HR.EmployeeShifts;
using UniManage.Application.Queries.HR.EmployeeShifts;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.HR
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
        public async Task<ActionResult<ApiResponse<PagedResult<GetEmployeeShiftListQuery.Result>>>> GetList([FromQuery] GetEmployeeShiftListQuery query, CancellationToken ct)
        {
            query ??= new GetEmployeeShiftListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeShiftByIdQuery.Result>>> GetById([FromRoute] int id, CancellationToken ct)
        {
            var query = new GetEmployeeShiftByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/employee-shifts

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateEmployeeShiftCommand.Response>>> Create([FromBody] CreateEmployeeShiftCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/employee-shifts/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateEmployeeShiftCommand.Response>>> Update([FromRoute] int id, [FromBody] UpdateEmployeeShiftCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateEmployeeShiftCommand.Response>("Id mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/employee-shifts

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteEmployeeShiftCommand.Response>>> Delete([FromBody] DeleteEmployeeShiftCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
