using UniManage.Shared.Domain.Models;

namespace UniManage.Shared.Infrastructure.Utilities
{
    /// <summary>
    /// Helper functions for creating standardized API responses.
    /// </summary>
    public static class ResponseHelper
    {
        /// <summary>
        /// Creates a successful response with data.
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="data">Response data.</param>
        /// <param name="message">Success message.</param>
        /// <returns>Standardized successful ApiResponse object.</returns>
        public static ApiResponse<T> Success<T>(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                ReturnCode = 0,
                Message = message,
                Data = data,
                Errors = new List<FieldError>()
            };
        }

        /// <summary>
        /// Creates a successful response without specific return data.
        /// </summary>
        /// <param name="message">Success message.</param>
        /// <returns>Standardized successful ApiResponse object.</returns>
        public static ApiResponse<object> Success(string message = "Operation successful")
        {
            return Success<object>(new { }, message);
        }

        /// <summary>
        /// Creates an error response with list of FieldError.
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="errors">List of FieldError.</param>
        /// <param name="returnCode">Failure return code (default: 1).</param>
        /// <returns>Standardized error ApiResponse object.</returns>
        public static ApiResponse<T> Error<T>(List<FieldError> errors, int returnCode = 1)
        {
            return new ApiResponse<T>
            {
                ReturnCode = returnCode,
                Message = "Operation failed",
                Errors = errors,
                Data = default
            };
        }

        /// <summary>
        /// Creates an error response with list of error messages (lỗi chung, không gắn field cụ thể).
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="errors">List of error messages.</param>
        /// <param name="returnCode">Failure return code (default: 1).</param>
        /// <returns>Standardized error ApiResponse object.</returns>
        public static ApiResponse<T> Error<T>(IEnumerable<string> errors, int returnCode = 1)
        {
            return new ApiResponse<T>
            {
                ReturnCode = returnCode,
                Message = "Operation failed",
                Errors = errors.Select(e => new FieldError("", e)).ToList(),
                Data = default
            };
        }

        /// <summary>
        /// Creates an error response with a single error message.
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="error">Error message string.</param>
        /// <param name="returnCode">Failure return code (default: 1).</param>
        /// <returns>Standardized error ApiResponse object.</returns>
        public static ApiResponse<T> Error<T>(string error, int returnCode = 1)
        {
            return new ApiResponse<T>
            {
                ReturnCode = returnCode,
                Message = "Operation failed",
                Errors = new List<FieldError> { new FieldError("", error) },
                Data = default
            };
        }

        /// <summary>
        /// Creates an error response for validation failures.
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="validationErrors">List of validation failure messages.</param>
        /// <returns>Standardized validation error ApiResponse object with code 400.</returns>
        public static ApiResponse<T> ValidationError<T>(IEnumerable<string> validationErrors)
        {
            return Error<T>(validationErrors, returnCode: 400);
        }

        /// <summary>
        /// Creates an error response for validation failures with FieldError details.
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="fieldErrors">List of FieldError validation details.</param>
        /// <returns>Standardized validation error ApiResponse object with code 400.</returns>
        public static ApiResponse<T> ValidationError<T>(List<FieldError> fieldErrors)
        {
            return Error<T>(fieldErrors, returnCode: 400);
        }

        /// <summary>
        /// Creates an error response for missing resources (404).
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="entityName">Resource name string.</param>
        /// <returns>Standardized not-found ApiResponse object with code 404.</returns>
        public static ApiResponse<T> NotFound<T>(string entityName = "Resource")
        {
            return Error<T>($"{entityName} not found", returnCode: 404);
        }

        /// <summary>
        /// Creates an error response for authentication failures (401).
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <returns>Standardized unauthorized ApiResponse object with code 401.</returns>
        public static ApiResponse<T> Unauthorized<T>()
        {
            return Error<T>("Unauthorized access", returnCode: 401);
        }

        /// <summary>
        /// Creates an error response for forbidden access (403).
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <returns>Standardized forbidden ApiResponse object with code 403.</returns>
        public static ApiResponse<T> Forbidden<T>()
        {
            return Error<T>("Access forbidden", returnCode: 403);
        }

        /// <summary>
        /// Creates a successful paged response.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="data">Paged result data.</param>
        /// <param name="message">Success message.</param>
        /// <returns>PagedResponse object.</returns>
        public static PagedResponse<T> PagedSuccess<T>(PagedResult<T> data, string message = "Operation successful")
        {
            return new PagedResponse<T>
            {
                ReturnCode = 0,
                Message = message,
                Data = data,
                Errors = new List<FieldError>()
            };
        }

        /// <summary>
        /// Creates an error paged response.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="error">Error message.</param>
        /// <param name="returnCode">Failure return code.</param>
        /// <returns>PagedResponse object.</returns>
        public static PagedResponse<T> PagedError<T>(string error, int returnCode = 1)
        {
            return new PagedResponse<T>
            {
                ReturnCode = returnCode,
                Message = "Operation failed",
                Errors = new List<FieldError> { new FieldError("", error) },
                Data = null
            };
        }
    }
}
