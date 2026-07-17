using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.HrAttendance.Commands;
using UniManage.Shared.Application.Modules.HrAttendance.Queries;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.WebApi.Authorization;

namespace UniManage.WebApi.Controllers.HumanResource
{
    [ApiController]
    [Route("api/v1/attendance")]
    public class HrAttendanceController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public HrAttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/attendance

        [HttpGet]
        [PermissionAuthorize(CoreFunction.HrAttendanceLog, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetAttendanceListQuery.Response>>>> GetList([FromQuery] GetAttendanceListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetAttendanceListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/attendance/{id}

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.HrAttendanceLog, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetAttendanceByIdQuery.Response>>> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetAttendanceByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/attendance/check-in

        [HttpPost("check-in")]
        [PermissionAuthorize(CoreFunction.HrAttendanceLog, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CheckInCommand.Response>>> CheckIn([FromBody] CheckInCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/attendance/check-out

        [HttpPost("check-out")]
        [PermissionAuthorize(CoreFunction.HrAttendanceLog, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<CheckOutCommand.Response>>> CheckOut([FromBody] CheckOutCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/attendance/{id}

        [HttpPut("{id}")]
        [PermissionAuthorize(CoreFunction.HrAttendanceLog, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateAttendanceCommand.Response>>> Update(int id, [FromBody] UpdateAttendanceCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateAttendanceCommand.Response>("ID mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/attendance

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.HrAttendanceLog, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteAttendanceCommand.Response>>> Delete([FromBody] DeleteAttendanceCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

