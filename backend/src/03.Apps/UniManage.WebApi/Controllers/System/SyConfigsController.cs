using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.SyConfig.Commands;
using UniManage.Shared.Application.Modules.SyConfig.Queries;
using UniManage.Shared.Domain.Entities;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Domain;

namespace UniManage.WebApi.Controllers.System
{
    [ApiController]
    [Route("api/v1/system-configs")]
    public class SyConfigsController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SyConfigsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/system-configs

        [HttpGet]
        [PermissionAuthorize(CoreFunction.SySystemParameters, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SystemConfig>>>> GetList(CancellationToken cancellationToken)
        {
            var query = new GetSystemConfigListQuery { HeaderInfo = HeaderInfo };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/system-configs/company-profile

        [HttpGet("company-profile")]
        [PermissionAuthorize(CoreFunction.SySystemParameters, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<GetCompanyProfileQuery.Response>>> GetCompanyProfile(CancellationToken cancellationToken)
        {
            var query = new GetCompanyProfileQuery { HeaderInfo = HeaderInfo };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/system-configs/company-profile

        [HttpPut("company-profile")]
        [PermissionAuthorize(CoreFunction.SySystemParameters, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateCompanyProfile([FromBody] UpdateCompanyProfileCommand command, CancellationToken cancellationToken)
        {
            command ??= new UpdateCompanyProfileCommand();
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region GET: /api/v1/system-configs/{code}

        [HttpGet("{code}")]
        [PermissionAuthorize(CoreFunction.SySystemParameters, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<SystemConfig>>> GetByCode(string code, CancellationToken cancellationToken)
        {
            var query = new GetSystemConfigByCodeQuery { Code = code, HeaderInfo = HeaderInfo };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region PUT: /api/v1/system-configs/{uuid}

        [HttpPut("{uuid}")]
        [PermissionAuthorize(CoreFunction.SySystemParameters, CoreAction.Update)]
        public async Task<ActionResult<ApiResponse<bool>>> Update(Guid uuid, [FromBody] UpdateSystemConfigCommand command, CancellationToken cancellationToken)
        {
            command ??= new UpdateSystemConfigCommand();
            var finalCommand = new UpdateSystemConfigCommand
            {
                Uuid = uuid,
                ConfigValue = command.ConfigValue,
                HeaderInfo = HeaderInfo
            };

            var result = await _mediator.Send(finalCommand, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

