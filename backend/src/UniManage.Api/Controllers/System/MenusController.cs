using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Authorization;
using UniManage.Application.Queries.System.Menus;
using UniManage.Core.Constant;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers.System
{
    [Route("api/v1")]
    [ApiController]
    public class MenusController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public MenusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/menus

        [HttpGet]
        [Authorize]
        [Route("menus")]
        [PermissionAuthorize(CoreFunction.System, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<List<GetMenuListQuery.Response>>>> GetList([FromQuery] GetMenuListQuery query, CancellationToken ct)
        {
            query ??= new GetMenuListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        #endregion
    }
}