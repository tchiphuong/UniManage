using Microsoft.AspNetCore.Mvc;
using UniManage.Shared.Application.Modules.IvProduct.Commands;
using UniManage.Shared.Domain.Models;

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

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        #endregion
    }
}
