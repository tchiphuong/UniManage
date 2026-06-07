namespace UniManage.Shared.Application.Models
{
    public class ApiResponse<T>
    {
        public int ReturnCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<FieldError> Errors { get; set; } = new();
        
        /// <summary>
        /// ID theo vết request (CorrelationId) để dễ dàng trace log
        /// </summary>
        public string TraceId { get; set; } = string.Empty;

        /// <summary>
        /// True nếu ReturnCode == 0
        /// </summary>
        public bool IsSuccess => ReturnCode == 0;
    }
}

