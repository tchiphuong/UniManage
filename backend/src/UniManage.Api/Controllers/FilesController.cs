using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Application.Commands.System.File;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers
{
    /// <summary>
    /// Controller quản lý các thao tác về file và tài liệu
    /// Controller for centralized file management
    /// </summary>
    [ApiController]
    [Route("api/v1/files")]
    public class FilesController : BaseController
    {
        #region Properties

        private readonly IMediator _mediator;

        /// <summary>
        /// Khởi tạo FilesController với Mediator
        /// </summary>
        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region POST: /api/v1/files/upload

        /// <summary>
        /// Endpoint tải lên file duy nhất cho toàn hệ thống (Ảnh, Tài liệu, Video...)
        /// Unified upload endpoint for all file types
        /// </summary>
        /// <param name="command">Dữ liệu file và thông tin bổ sung</param>
        /// <param name="ct">Token hủy bỏ yêu cầu</param>
        /// <returns>Trả về FileCode nếu thành công</returns>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse<string>>> Upload([FromForm] UploadFileCommand command, CancellationToken ct)
        {
            // Gán HeaderInfo từ BaseController cho Command
            command.HeaderInfo = HeaderInfo;
            
            // Gửi lệnh xử lý qua Mediator
            var result = await _mediator.Send(command, ct);
            
            return Ok(result);
        }

        #endregion
    }
}
