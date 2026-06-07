using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UniManage.Shared.Infrastructure.Services;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.WebApi.Controllers.System
{
    [Route("api/v1/system")]
    [ApiController]
    public class SystemController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        public SystemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region GET: /api/v1/system/resources

        [HttpGet("resources")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResources(string lang = "vi-VN")
        {
            var culture = CultureInfo.GetCultureInfo(lang);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            var resourceManager = new ResourceManager();
            Dictionary<string, string?> resources = resourceManager.GetResources();
            var response = ResponseHelper.Success(resources, CoreResource.common_success);
            return Ok(response);
        }

        #endregion


    }
}

