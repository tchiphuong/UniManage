using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.SyFile.Commands;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers.System
{
    /// <summary>
    /// Controller qu?n lý các thao tác v? file và tài li?u
    /// Controller for centralized file management
    /// </summary>
    [ApiController]
    [Route("api/v1/files")]
    public class SyFilesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        /// <summary>
        /// Kh?i t?o FilesController v?i Mediator
        /// </summary>
        public SyFilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region POST: /api/v1/files/upload

        /// <summary>
        /// Endpoint t?i lên file duy nh?t cho toàn h? th?ng (?nh, Tài li?u, Video...)
        /// Unified upload endpoint for all file types
        /// </summary>
        /// <param name="command">D? li?u file và thông tin b? sung</param>
        /// <param name="ct">Token h?y b? yêu c?u</param>
        /// <returns>Tr? v? FileCode n?u thành công</returns>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse<string>>> Upload([FromForm] UploadFileCommand command, CancellationToken cancellationToken)
        {
            // Gán HeaderInfo t? BaseController cho Command
            
            // G?i l?nh x? lý qua Mediator
            var result = await _mediator.Send(command, cancellationToken);
            
            return Ok(result);
        }

        #endregion
    }
}

