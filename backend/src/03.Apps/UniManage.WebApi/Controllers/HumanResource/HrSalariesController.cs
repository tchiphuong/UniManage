using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.HrSalary.Commands;
using UniManage.Shared.Application.Modules.HrSalary.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.HumanResource
{
    [ApiController]
    [Route("api/v1/salaries")]
    public class HrSalariesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public HrSalariesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/salaries

        [HttpGet]
        [PermissionAuthorize(CoreFunction.HrPayslip, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetSalaryListQuery.Response>>>> GetList([FromQuery] GetSalaryListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetSalaryListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/salaries/{id}

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.HrPayslip, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetSalaryByIdQuery.Response>>> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetSalaryByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/salaries

        [HttpPost]
        [PermissionAuthorize(CoreFunction.HrPayslip, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateSalaryCommand.Response>>> Create([FromBody] CreateSalaryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/salaries/{id}

        [HttpPut("{id}")]
        [PermissionAuthorize(CoreFunction.HrPayslip, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateSalaryCommand.Response>>> Update(int id, [FromBody] UpdateSalaryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateSalaryCommand.Response>("ID mismatch"));
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/salaries

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.HrPayslip, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteSalaryCommand.Response>>> Delete([FromBody] DeleteSalaryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

