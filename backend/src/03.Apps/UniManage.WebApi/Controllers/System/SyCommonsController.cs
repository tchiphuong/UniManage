using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.SyCommon.Queries;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.System
{
    [ApiController]
    [Route("api/v1/commons")]
    [Authorize]
    public class SyCommonsController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SyCommonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/commons/combobox/{typeKey}

        [HttpGet("combobox/{typeKey}")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromRoute] string typeKey, CancellationToken cancellationToken)
        {
            var query = new GetCommonComboboxQuery { TypeKey = typeKey };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}


