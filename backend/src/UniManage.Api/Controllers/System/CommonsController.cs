using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Queries.System.Commons;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.System
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
        public async Task<ActionResult<ApiResponse<List<ComboboxItemDto>>>> GetCombobox([FromRoute] string typeKey, CancellationToken ct)
        {
            var query = new GetCommonComboboxQuery { TypeKey = typeKey };
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion
    }
}
