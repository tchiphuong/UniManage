using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UniManage.Shared.Infrastructure.Services;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using UniManage.WebApi.Authorization;
using UniManage.Shared.Application.Modules.SySystem.Queries;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Infrastructure.Constant;

namespace UniManage.WebApi.Controllers.System
{
    [Route("api/v1/system")]
    [ApiController]
    public class SySystemController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SySystemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/system/resources

        [HttpGet("resources")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<Dictionary<string, string?>>>> GetResources(string lang = "vi-VN")
        {
            var culture = CultureInfo.GetCultureInfo(lang);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            var resourceManager = new ResourceManager();
            Dictionary<string, string?> resources = resourceManager.GetResources();
            var response = ResponseHelper.Success(resources, CoreResource.common_success);
            return ToActionResult(response);
        }

        #endregion

        #region GET: /api/v1/system/navbars

        /// <summary>
        /// Get user navbar tree
        /// </summary>
        [HttpGet("navbars")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<List<GetNavbarTreeQuery.ModuleNode>>>> GetNavbar([FromQuery] GetNavbarTreeQuery query, CancellationToken cancellationToken)
        {
            query ??= new GetNavbarTreeQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        #endregion

    }
}

