using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.HrWorkShift.Commands;
using UniManage.Shared.Application.Modules.HrWorkShift.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.HumanResource
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
        [PermissionAuthorize(CoreFunction.MsShift, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromQuery] string? keyword, CancellationToken cancellationToken)
        {
            var query = new GetWorkShiftComboboxQuery { Keyword = keyword };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/work-shifts

        [HttpGet]
        [PermissionAuthorize(CoreFunction.MsShift, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetWorkShiftListQuery.Response>>>> GetList([FromQuery] GetWorkShiftListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetWorkShiftListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/work-shifts/{code}

        [HttpGet("{code}")]
        [PermissionAuthorize(CoreFunction.MsShift, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetWorkShiftByCodeQuery.Response>>> GetByCode(string code, CancellationToken cancellationToken)
        {
            var query = new GetWorkShiftByCodeQuery { Code = code };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/work-shifts

        [HttpPost]
        [PermissionAuthorize(CoreFunction.MsShift, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateWorkShiftCommand.Response>>> Create([FromBody] CreateWorkShiftCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/work-shifts/{id}

        [HttpPut("{id}")]
        [PermissionAuthorize(CoreFunction.MsShift, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateWorkShiftCommand.Response>>> Update(int id, [FromBody] UpdateWorkShiftCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateWorkShiftCommand.Response>("ID mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/work-shifts

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.MsShift, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteWorkShiftCommand.Response>>> Delete([FromBody] DeleteWorkShiftCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}


