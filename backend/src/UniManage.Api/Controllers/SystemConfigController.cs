using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.System;
using UniManage.Application.Queries.System;
using UniManage.Model.Common;
using UniManage.Model.Entities;

namespace UniManage.Api.Controllers
{
    [ApiController]
    [Route("api/v1/system-configs")]
    public class SystemConfigController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SystemConfigController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/system-configs

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SystemConfig>>>> GetList(CancellationToken ct)
        {
            var query = new GetSystemConfigListQuery { HeaderInfo = HeaderInfo };
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/system-configs/{code}

        [HttpGet("{code}")]
        public async Task<ActionResult<ApiResponse<SystemConfig>>> GetByCode(string code, CancellationToken ct)
        {
            var query = new GetSystemConfigByCodeQuery { Code = code, HeaderInfo = HeaderInfo };
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/system-configs/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] UpdateSystemConfigCommand command, CancellationToken ct)
        {
            command ??= new UpdateSystemConfigCommand();
            var finalCommand = new UpdateSystemConfigCommand
            {
                Id = id,
                ConfigValue = command.ConfigValue,
                HeaderInfo = HeaderInfo
            };

            var result = await _mediator.Send(finalCommand, ct);
            return Ok(result);
        }

        #endregion
    }
}
