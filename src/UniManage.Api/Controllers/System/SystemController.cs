using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UniManage.Api.Domains.Query.System;
using UniManage.Core.Helpers;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Controllers.System
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

        #region GET: /api/v1/system/resource

        [HttpGet("resource")]
        public async Task<IActionResult> GetResources(string lang = "vi-VN")
        {
            var culture = CultureInfo.GetCultureInfo(lang);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            var resourceManager = new ResourceManager();
            Dictionary<string, string?> resources = resourceManager.GetResources();
            var response = new CoreResponse(CoreApiReturnCode.Succeed, CoreResource.Common_msg_Success, resources);
            return Ok(response);
        }

        #endregion

        #region GET: /api/v1/system/languages

        [HttpGet("languages")]
        public async Task<IActionResult> GetLanguageList([FromQuery] GetLanguageListQuery request, CancellationToken cancellationToken)
        {
            request ??= new GetLanguageListQuery();
            request.HeaderInfo = HeaderInfo;

            var validation = await new GetLanguageListQueryValidator().ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(new CoreResponse(CoreApiReturnCode.InvalidData, CoreResource.Common_msg_InvalidData)
                {
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}
