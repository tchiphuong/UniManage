using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.HR.Salaries;
using UniManage.Application.Queries.HR.Salaries;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.HR
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
        public async Task<ActionResult<ApiResponse<PagedResult<GetSalaryListQuery.Response>>>> GetList([FromQuery] GetSalaryListQuery query, CancellationToken ct)
        {
            query ??= new GetSalaryListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/salaries/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetSalaryByIdQuery.Response>>> GetById(int id, CancellationToken ct)
        {
            var query = new GetSalaryByIdQuery { Id = id };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region POST: /api/v1/salaries

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateSalaryCommand.Response>>> Create([FromBody] CreateSalaryCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/salaries/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateSalaryCommand.Response>>> Update(int id, [FromBody] UpdateSalaryCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(ResponseHelper.Error<UpdateSalaryCommand.Response>("ID mismatch"));
            }
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/salaries

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<DeleteSalaryCommand.Response>>> Delete([FromBody] DeleteSalaryCommand command, CancellationToken ct)
        {
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        #endregion
    }
}
