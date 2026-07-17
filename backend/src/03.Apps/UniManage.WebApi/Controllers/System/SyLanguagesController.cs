using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.SyLanguage.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Resource;

namespace UniManage.WebApi.Controllers.System
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
        [PermissionAuthorize(CoreFunction.MsLanguage, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetLanguageListQuery.Response>>>> GetList([FromQuery] GetLanguageListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetLanguageListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}

