using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Text.Json.Nodes;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Filters
{
    /// <summary>
    /// Filter tự động thêm các ví dụ response mẫu (400, 401, 403, 404, 409, 429, 500) vào Swagger cho các API tương ứng.
    /// Sử dụng JsonSerializer.SerializeToNode từ model thực tế ApiResponse, đảm bảo 100% khớp cấu trúc JSON với API runtime.
    /// </summary>
    public class SwaggerAutoResponseFilter : IOperationFilter
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Constructor nhận cấu hình JSON mặc định từ hệ thống
        /// </summary>
        public SwaggerAutoResponseFilter(IOptions<JsonOptions> jsonOptions)
        {
            _jsonSerializerOptions = jsonOptions.Value.JsonSerializerOptions;
        }

        /// <summary>
        /// Áp dụng filter cho từng API operation
        /// </summary>
#pragma warning disable S3776 // Refactor this method to reduce its Cognitive Complexity
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null) return;
            operation.Responses ??= new OpenApiResponses();

            // 1. Tự động thêm mã lỗi 400 (Bad Request - Validation Error) cho mọi API
            if (!operation.Responses.ContainsKey("400"))
            {
                var response400 = new ApiResponse<object>
                {
                    ReturnCode = 400,
                    Message = "Validation failed",
                    Errors = new List<FieldError>
                    {
                        new FieldError("username", new List<string> { "Tên đăng nhập không được bỏ trống." }),
                        new FieldError("email", new List<string> { "Email không đúng định dạng.", "Email đã được sử dụng." })
                    },
                    Data = null
                };

                AddResponse(operation, "400", "Dữ liệu gửi lên không hợp lệ (Lỗi Validation)", response400);
            }

            // 2. Tự động kiểm tra xem API có yêu cầu xác thực hay không để thêm 401 & 403
            var hasAuthorize = context.MethodInfo != null && context.MethodInfo.DeclaringType != null &&
                (context.MethodInfo.DeclaringType.GetCustomAttributes(true).Any(a => a.GetType().Name == "AuthorizeAttribute") ||
                 context.MethodInfo.GetCustomAttributes(true).Any(a => a.GetType().Name == "AuthorizeAttribute"));

            var hasAllowAnonymous = context.MethodInfo != null && 
                context.MethodInfo.GetCustomAttributes(true).Any(a => a.GetType().Name == "AllowAnonymousAttribute");

            if (hasAuthorize && !hasAllowAnonymous)
            {
                if (!operation.Responses.ContainsKey("401"))
                {
                    var response401 = new ApiResponse<object>
                    {
                        ReturnCode = 401,
                        Message = "Không có quyền truy cập (Chưa xác thực)",
                        Errors = new List<FieldError> { new FieldError("", "Token không hợp lệ hoặc đã hết hạn") },
                        Data = null
                    };

                    AddResponse(operation, "401", "Không có quyền truy cập (Chưa xác thực token)", response401);
                }

                if (!operation.Responses.ContainsKey("403"))
                {
                    var response403 = new ApiResponse<object>
                    {
                        ReturnCode = 403,
                        Message = "Từ chối truy cập",
                        Errors = new List<FieldError> { new FieldError("", "Bạn không có quyền thực hiện hành động này") },
                        Data = null
                    };

                    AddResponse(operation, "403", "Từ chối truy cập (Không có quyền hạn thực hiện hành động này)", response403);
                }
            }

            // 3. Tự động thêm mã lỗi 404 (Not Found)
            if (!operation.Responses.ContainsKey("404"))
            {
                var response404 = new ApiResponse<object>
                {
                    ReturnCode = 404,
                    Message = "Không tìm thấy dữ liệu",
                    Errors = new List<FieldError> { new FieldError("", "Tài nguyên yêu cầu không tồn tại trên hệ thống") },
                    Data = null
                };

                AddResponse(operation, "404", "Không tìm thấy tài nguyên yêu cầu", response404);
            }

            // 4. Tự động thêm mã lỗi 409 (Conflict) cho các API POST/PUT/PATCH
            var httpMethod = context.ApiDescription?.HttpMethod;
            var isWriteOperation = httpMethod == "POST" || httpMethod == "PUT" || httpMethod == "PATCH";
            if (isWriteOperation && !operation.Responses.ContainsKey("409"))
            {
                var response409 = new ApiResponse<object>
                {
                    ReturnCode = 409,
                    Message = "Dữ liệu đã tồn tại",
                    Errors = new List<FieldError> { new FieldError("", "Mã hoặc thông tin tài nguyên đã được sử dụng") },
                    Data = null
                };

                AddResponse(operation, "409", "Dữ liệu đã tồn tại hoặc xảy ra xung đột", response409);
            }

            // 5. Tự động thêm mã lỗi 429 (Too Many Requests)
            if (!operation.Responses.ContainsKey("429"))
            {
                var response429 = new ApiResponse<object>
                {
                    ReturnCode = 429,
                    Message = "Yêu cầu quá nhiều",
                    Errors = new List<FieldError> { new FieldError("", "Số lượng request vượt quá giới hạn cho phép") },
                    Data = null
                };

                AddResponse(operation, "429", "Yêu cầu quá nhiều (Rate limit bị vượt quá)", response429);
            }

            // 6. Tự động thêm mã lỗi 500 (Internal Server Error)
            if (!operation.Responses.ContainsKey("500"))
            {
                var response500 = new ApiResponse<object>
                {
                    ReturnCode = 500,
                    Message = "Lỗi hệ thống",
                    Errors = new List<FieldError> { new FieldError("", "Có lỗi xảy ra trên máy chủ") },
                    Data = null
                };

                AddResponse(operation, "500", "Lỗi hệ thống không mong muốn", response500);
            }
        }

        /// <summary>
        /// Helper method tạo OpenApiResponse từ model thực tế và thêm vào operation
        /// </summary>
        private void AddResponse(OpenApiOperation operation, string statusCode, string description, ApiResponse<object> responseModel)
        {
            operation.Responses!.Add(statusCode, new OpenApiResponse
            {
                Description = description,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Example = JsonSerializer.SerializeToNode(responseModel, _jsonSerializerOptions)
                    }
                }
            });
        }
    }
}
