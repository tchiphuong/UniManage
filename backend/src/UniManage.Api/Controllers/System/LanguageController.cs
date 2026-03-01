using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Authorization;
using UniManage.Application.Queries.System.Language;
using UniManage.Core.Constant;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Api.Controllers.System
{
    [ApiController]
    [Route("api/v1/languages")]
    public class LanguageController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/languages

        [HttpGet]
        [PermissionAuthorize(CoreFunction.System, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetLanguageListQuery.Response>>>> GetList([FromQuery] GetLanguageListQuery query, CancellationToken ct)
        {
            query ??= new GetLanguageListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion
    }
}
