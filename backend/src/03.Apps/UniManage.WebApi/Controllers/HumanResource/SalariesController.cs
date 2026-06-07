using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.HumanResource.Application.Commands.Salaries;
using UniManage.Modules.HumanResource.Application.Queries.Salaries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.HumanResource
{
    [ApiController]
    [Route("api/v1/salaries")]
    public class SalariesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SalariesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/salaries

        [HttpGet]
        [PermissionAuthorize(CoreFunction.HrSalary, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetSalaryListQuery.Response>>>> GetList([FromQuery] GetSalaryListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetSalaryListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/salaries/{id}

        [HttpGet("{id}")]
        [PermissionAuthorize(CoreFunction.HrSalary, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetSalaryByIdQuery.Response>>> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetSalaryByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/salaries

        [HttpPost]
        [PermissionAuthorize(CoreFunction.HrSalary, CoreAction.Create)]
        public async Task<ActionResult<ApiResponse<CreateSalaryCommand.Response>>> Create([FromBody] CreateSalaryCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/salaries/{id}

        [HttpPut("{id}")]
        [PermissionAuthorize(CoreFunction.HrSalary, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<UpdateSalaryCommand.Response>>> Update(int id, [FromBody] UpdateSalaryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateSalaryCommand.Response>("ID mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/salaries

        [HttpDelete]
        [PermissionAuthorize(CoreFunction.HrSalary, CoreAction.Delete)]
        public async Task<ActionResult<ApiResponse<DeleteSalaryCommand.Response>>> Delete([FromBody] DeleteSalaryCommand command, CancellationToken cancellationToken)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

