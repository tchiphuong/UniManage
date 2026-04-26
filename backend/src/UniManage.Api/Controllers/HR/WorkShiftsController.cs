using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Authorization;
using UniManage.Application.Commands.HR.WorkShifts;
using UniManage.Application.Queries.HR.WorkShifts;
using UniManage.Core.Constant;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.HR
{
    [ApiController]
    [Route("api/v1/work-shifts")]
    public class WorkShiftsController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public WorkShiftsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/work-shifts/combobox

        [HttpGet("combobox")]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<List<ComboboxModel>>>> GetCombobox([FromQuery] string? keyword, CancellationToken ct)
        {
            var query = new GetWorkShiftComboboxQuery { Keyword = keyword };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/work-shifts

        [HttpGet]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetWorkShiftListQuery.Response>>>> GetList([FromQuery] GetWorkShiftListQuery query, CancellationToken ct)
        {
            query ??= new GetWorkShiftListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/work-shifts/{code}

        [HttpGet("{code}")]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetWorkShiftByCodeQuery.Response>>> GetByCode(string code, CancellationToken ct)
        {
            var query = new GetWorkShiftByCodeQuery { Code = code };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/work-shifts

        [HttpPost]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateWorkShiftCommand.Response>>> Create([FromBody] CreateWorkShiftCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/work-shifts/{id}

        [HttpPut("{id}")]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateWorkShiftCommand.Response>>> Update(int id, [FromBody] UpdateWorkShiftCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateWorkShiftCommand.Response>("ID mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/work-shifts

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteWorkShiftCommand.Response>>> Delete([FromBody] DeleteWorkShiftCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
