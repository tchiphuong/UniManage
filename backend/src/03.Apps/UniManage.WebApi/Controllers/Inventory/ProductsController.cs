using Microsoft.AspNetCore.Mvc;
using UniManage.Modules.Inventory.Application.Commands.Product;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers.Inventory
{
    /// <summary>
    /// Controller quản lý sản phẩm (Products) thuộc module Kho (Inventory).
    /// </summary>
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : BaseController
    {
        #region Properties
        #endregion

        #region CreateProduct
        /// <summary>
        /// Tạo mới một sản phẩm trong module Inventory.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateProductCommand.Response>>> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            // BẮT BUỘC: Lấy HeaderInfo từ BaseController
            command.HeaderInfo = HeaderInfo;

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        #endregion
    }
}
