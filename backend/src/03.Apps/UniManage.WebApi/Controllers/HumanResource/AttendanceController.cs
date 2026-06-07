using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Modules.HumanResource.Application.Commands.Attendance;
using UniManage.Modules.HumanResource.Application.Queries.Attendance;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.WebApi.Authorization;

namespace UniManage.WebApi.Controllers.HumanResource
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
        [PermissionAuthorize(CoreFunction.HrAttendance, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetAttendanceListQuery.Response>>>> GetList([FromQuery] GetAttendanceListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetAttendanceListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/attendance/{id}

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.HrAttendance, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetAttendanceByIdQuery.Response>>> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetAttendanceByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/attendance/check-in

        [HttpPost("check-in")]
        [PermissionAuthorize(CoreFunction.HrAttendance, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CheckInCommand.Response>>> CheckIn([FromBody] CheckInCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/attendance/check-out

        [HttpPost("check-out")]
        [PermissionAuthorize(CoreFunction.HrAttendance, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<CheckOutCommand.Response>>> CheckOut([FromBody] CheckOutCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/attendance/{id}

        [HttpPut("{id}")]
        [PermissionAuthorize(CoreFunction.HrAttendance, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateAttendanceCommand.Response>>> Update(int id, [FromBody] UpdateAttendanceCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateAttendanceCommand.Response>("ID mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/attendance

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.HrAttendance, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteAttendanceCommand.Response>>> Delete([FromBody] DeleteAttendanceCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

