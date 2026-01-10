using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.HR.Attendance;
using UniManage.Application.Queries.HR.Attendance;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.HR
{
    [ApiController]
    [Route("api/v1/attendance")]
    public class AttendanceController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/attendance

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetAttendanceListQuery.Response>>>> GetList([FromQuery] GetAttendanceListQuery query, CancellationToken ct)
        {
            query ??= new GetAttendanceListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/attendance/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetAttendanceByIdQuery.Response>>> GetById(int id, CancellationToken ct)
        {
            var query = new GetAttendanceByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/attendance/check-in

        [HttpPost("check-in")]
        public async Task<ActionResult<ApiResponse<CheckInCommand.Response>>> CheckIn([FromBody] CheckInCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/attendance/check-out

        [HttpPost("check-out")]
        public async Task<ActionResult<ApiResponse<CheckOutCommand.Response>>> CheckOut([FromBody] CheckOutCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/attendance/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateAttendanceCommand.Response>>> Update(int id, [FromBody] UpdateAttendanceCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateAttendanceCommand.Response>("ID mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/attendance

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteAttendanceCommand.Response>>> Delete([FromBody] DeleteAttendanceCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
