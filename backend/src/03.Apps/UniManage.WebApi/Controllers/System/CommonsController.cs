using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManage.Modules.System.Application.Queries.Commons;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.System
{
    [ApiController]
    [Route("api/v1/commons")]
    [Authorize]
    public class CommonsController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public CommonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/commons/combobox/{typeKey}

        [HttpGet("combobox/{typeKey}")]
        public async Task<ActionResult<ApiResponse<List<ComboboxItem>>>> GetCombobox([FromRoute] string typeKey, CancellationToken cancellationToken)
        {
            var query = new GetCommonComboboxQuery { TypeKey = typeKey };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}


