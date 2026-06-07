using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Modules.System.Application.Queries.Menus;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.System
{
    [ApiController]
    [Route("api/v1/menus")]
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
        [PermissionAuthorize(CoreFunction.System, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<List<GetMenuListQuery.Response>>>> GetList([FromQuery] GetMenuListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetMenuListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}
