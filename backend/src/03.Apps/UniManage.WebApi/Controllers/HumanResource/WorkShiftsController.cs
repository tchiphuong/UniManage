using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.HumanResource.Application.Commands.WorkShifts;
using UniManage.Modules.HumanResource.Application.Queries.WorkShifts;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Application.Models;

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
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromQuery] string? keyword, CancellationToken cancellationToken)
        {
            var query = new GetWorkShiftComboboxQuery { Keyword = keyword };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/work-shifts

        [HttpGet]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetWorkShiftListQuery.Response>>>> GetList([FromQuery] GetWorkShiftListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetWorkShiftListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/work-shifts/{code}

        [HttpGet("{code}")]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetWorkShiftByCodeQuery.Response>>> GetByCode(string code, CancellationToken cancellationToken)
        {
            var query = new GetWorkShiftByCodeQuery { Code = code };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/work-shifts

        [HttpPost]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateWorkShiftCommand.Response>>> Create([FromBody] CreateWorkShiftCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/work-shifts/{id}

        [HttpPut("{id}")]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateWorkShiftCommand.Response>>> Update(int id, [FromBody] UpdateWorkShiftCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateWorkShiftCommand.Response>("ID mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/work-shifts

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.Hr, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteWorkShiftCommand.Response>>> Delete([FromBody] DeleteWorkShiftCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}


