using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Domains.Command.Master.Employee;
using UniManage.Api.Domains.Query.Master.Employee;
using UniManage.Core.Helpers;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Controllers
{
    [ApiController]
    [Route("api/v1/employees")]
    public class EmployeeController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region POST: /api/v1/employees

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            request ??= new CreateEmployeeCommand();
            request.HeaderInfo = HeaderInfo;

            var validation = await new CreateEmployeeCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/employees/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetEmployeeByIdQuery { Id = id, HeaderInfo = HeaderInfo };

            var validation = await new GetEmployeeByIdQueryValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/employees

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            request ??= new GetEmployeeListQuery();
            request.HeaderInfo = HeaderInfo;

            var validation = await new GetEmployeeListQueryValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/employees/{id}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            request ??= new();
            request.Id = id;
            request.HeaderInfo = HeaderInfo;

            var validation = await new UpdateEmployeeCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region DELETE: /api/v1/employees/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] List<int> ids, CancellationToken cancellationToken)
        {
            var request = new DeleteEmployeeCommand{ Ids = ids, HeaderInfo = HeaderInfo };

            var validation = await new DeleteEmployeeCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}
