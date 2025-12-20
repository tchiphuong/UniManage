using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;
using UniManage.Application.Queries.System.Language;
using UniManage.Core.Constant;

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
            var response = ResponseHelper.Success(resources, CoreResource.Common_msg_Success);
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
                return BadRequest(new ApiResponse<object>
                {
                    ReturnCode = CoreApiReturnCode.InvalidData,
                    Message = CoreResource.Common_msg_InvalidData,
                    Errors = validation.Errors.ToFieldErrorModels()
                });

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}
