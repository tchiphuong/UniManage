namespace UniManage.Tools.CodeGen
{
    public static partial class Templates
    {
        public static string GetControllerTemplate(string module, string entity, string routeName, string functionCode) =>
$@"using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UniManage.Shared.Application.Models;
using UniManage.Modules.{module}.Application.Commands.{entity};
using UniManage.Modules.{module}.Application.Queries.{entity};
using UniManage.Modules.{module}.Application.Models.{entity};

namespace UniManage.WebApi.Controllers.{module}
{{
    /// <summary>
    /// Controller quản lý {entity}
    /// </summary>
    [ApiController]
    [Route(""api/v1/{routeName}"")]
    [Authorize]
    public class {entity}sController : BaseController
    {{
        #region Properties

        private readonly IMediator _mediator;

        /// <summary>Constructor</summary>
        public {entity}sController(IMediator mediator)
        {{
            _mediator = mediator;
        }}

        #endregion

        #region GET: /api/v1/{routeName}

        /// <summary>Lấy danh sách phân trang</summary>
        [HttpGet]
        [PermissionAuthorize(""{functionCode}"", CoreActionCode.View)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<{entity}ListModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<{entity}ListModel>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResult<{entity}ListModel>>>> GetList([FromQuery] Get{entity}ListQuery query, CancellationToken ct)
        {{
            query ??= new Get{entity}ListQuery();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }}

        #endregion

        #region GET: /api/v1/{routeName}/{{id}}

        /// <summary>Lấy chi tiết theo Id</summary>
        [HttpGet(""{{id}}"")]
        [PermissionAuthorize(""{functionCode}"", CoreActionCode.View)]
        public async Task<ActionResult<ApiResponse<{entity}DetailModel>>> GetById(long id, CancellationToken ct)
        {{
            var query = new Get{entity}ByIdQuery {{ Id = id, HeaderInfo = HeaderInfo }};
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }}

        #endregion

        #region POST: /api/v1/{routeName}

        /// <summary>Tạo mới</summary>
        [HttpPost]
        [PermissionAuthorize(""{functionCode}"", CoreActionCode.Create)]
        public async Task<ActionResult<ApiResponse<Create{entity}Command.Response>>> Create([FromBody] Create{entity}Command command, CancellationToken ct)
        {{
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }}

        #endregion

        #region PUT: /api/v1/{routeName}/{{id}}

        /// <summary>Cập nhật</summary>
        [HttpPut(""{{id}}"")]
        [PermissionAuthorize(""{functionCode}"", CoreActionCode.Update)]
        public async Task<ActionResult<ApiResponse<bool>>> Update(long id, [FromBody] Update{entity}Command command, CancellationToken ct)
        {{
            command ??= new Update{entity}Command();
            command.Id = id;
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }}

        #endregion

        #region DELETE: /api/v1/{routeName}/{{id}}

        /// <summary>Xóa</summary>
        [HttpDelete(""{{id}}"")]
        [PermissionAuthorize(""{functionCode}"", CoreActionCode.Delete)]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id, CancellationToken ct)
        {{
            var command = new Delete{entity}Command {{ Id = id, HeaderInfo = HeaderInfo }};
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }}

        #endregion

        #region GET: /api/v1/{routeName}/export

        /// <summary>Xuất Excel</summary>
        [HttpGet(""export"")]
        [PermissionAuthorize(""{functionCode}"", CoreActionCode.Export)]
        public async Task<IActionResult> Export([FromQuery] Export{entity}Query query, CancellationToken ct)
        {{
            query ??= new Export{entity}Query();
            query.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(query, ct);
            
            if (result.ReturnCode != 0 || result.Data == null)
                return BadRequest(result);

            return File(result.Data, ""application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"", ""{entity}s_Export.xlsx"");
        }}

        #endregion

        #region POST: /api/v1/{routeName}/import

        /// <summary>Nhập dữ liệu</summary>
        [HttpPost(""import"")]
        [PermissionAuthorize(""{functionCode}"", CoreActionCode.Import)]
        public async Task<ActionResult<ApiResponse<bool>>> Import([FromBody] Import{entity}Command command, CancellationToken ct)
        {{
            command ??= new Import{entity}Command();
            command.HeaderInfo = HeaderInfo;
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }}

        #endregion
    }}
}}";
    }
}
