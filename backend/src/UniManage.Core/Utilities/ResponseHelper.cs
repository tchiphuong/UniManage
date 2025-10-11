using UniManage.Core.Models;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Helper functions for creating standardized API responses
    /// </summary>
    public static class ResponseHelper
    {
        /// <summary>
        /// Create successful response
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="data">Response data</param>
        /// <param name="message">Success message</param>
        /// <returns>Success response</returns>
        public static ApiResponse<T> Success<T>(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                ReturnCode = 0,
                Message = message,
                Data = data,
                Errors = new List<string>()
            };
        }

        /// <summary>
        /// Create success response without data
        /// </summary>
        /// <param name="message">Success message</param>
        /// <returns>Success response</returns>
        public static ApiResponse<object> Success(string message = "Operation successful")
        {
            return Success<object>(new { }, message);
        }

        /// <summary>
        /// Create error response
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="errors">Error messages</param>
        /// <param name="returnCode">Error code (default 1)</param>
        /// <returns>Error response</returns>
        public static ApiResponse<T> Error<T>(IEnumerable<string> errors, int returnCode = 1)
        {
            return new ApiResponse<T>
            {
                ReturnCode = returnCode,
                Message = "Operation failed",
                Data = default,
                Errors = errors.ToList()
            };
        }

        /// <summary>
        /// Create error response with single error
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="error">Error message</param>
        /// <param name="returnCode">Error code (default 1)</param>
        /// <returns>Error response</returns>
        public static ApiResponse<T> Error<T>(string error, int returnCode = 1)
        {
            return Error<T>(new[] { error }, returnCode);
        }

        /// <summary>
        /// Create validation error response
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="validationErrors">Validation error messages</param>
        /// <returns>Validation error response</returns>
        public static ApiResponse<T> ValidationError<T>(IEnumerable<string> validationErrors)
        {
            return Error<T>(validationErrors, returnCode: 400);
        }

        /// <summary>
        /// Create not found error response
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="entityName">Entity name that was not found</param>
        /// <returns>Not found error response</returns>
        public static ApiResponse<T> NotFound<T>(string entityName = "Resource")
        {
            return Error<T>($"{entityName} not found", returnCode: 404);
        }

        /// <summary>
        /// Create unauthorized error response
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <returns>Unauthorized error response</returns>
        public static ApiResponse<T> Unauthorized<T>()
        {
            return Error<T>("Unauthorized access", returnCode: 401);
        }

        /// <summary>
        /// Create forbidden error response
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <returns>Forbidden error response</returns>
        public static ApiResponse<T> Forbidden<T>()
        {
            return Error<T>("Access forbidden", returnCode: 403);
        }

        /// <summary>
        /// Create successful paged response
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="items">List of items</param>
        /// <param name="totalItems">Total number of items</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="message">Success message</param>
        /// <returns>Paged response</returns>
        public static PagedResponse<T> PagedSuccess<T>(
            IEnumerable<T> items,
            int totalItems,
            int pageIndex,
            int pageSize,
            string message = "Data retrieved successfully")
        {
            var paging = new PagingInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            var pagedResult = new PagedResult<T>
            {
                Items = items.ToList(),
                Paging = paging
            };

            return new PagedResponse<T>
            {
                ReturnCode = 0,
                Message = message,
                Data = pagedResult,
                Errors = new List<string>()
            };
        }        /// <summary>
                 /// Create error paged response
                 /// </summary>
                 /// <typeparam name="T">Item type</typeparam>
                 /// <param name="errors">Error messages</param>
                 /// <param name="returnCode">Error code</param>
                 /// <returns>Error paged response</returns>
        public static PagedResponse<T> PagedError<T>(IEnumerable<string> errors, int returnCode = 1)
        {
            var emptyPaging = new PagingInfo { PageIndex = 1, PageSize = 20, TotalItems = 0 };
            var emptyResult = new PagedResult<T>
            {
                Items = new List<T>(),
                Paging = emptyPaging
            };

            return new PagedResponse<T>
            {
                ReturnCode = returnCode,
                Message = "Operation failed",
                Data = emptyResult,
                Errors = errors.ToList()
            };
        }
    }
}