using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.SyMenu.Queries;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.System
{
    [ApiController]
    [Route("api/v1/menus")]
    public class SyMenusController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SyMenusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/menus

        [HttpGet]
        [PermissionAuthorize(CoreFunction.SyMenu, CoreAction.View)]
        public async Task<ActionResult<ApiResponse<List<GetMenuListQuery.Response>>>> GetList([FromQuery] GetMenuListQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetMenuListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}
