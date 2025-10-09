using Newtonsoft.Json;

namespace UniManage.Core.Models
{
    public class CoreResponse
    {
        public CoreApiReturnCode ReturnCode { get; set; }
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<FieldErrorModel> Errors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PagingResult Paging { get; set; }

        public CoreResponse(CoreApiReturnCode returnCode, string message = null, object result = null, PagingResult paging = null)
        {
            ReturnCode = returnCode;
            Message = message;
            Data = result;
            Paging = paging;
        }
    }

    public class FieldErrorModel
    {
        public string Field { get; set; }
        public List<string> Messages { get; set; }

        public FieldErrorModel(string field, string message)
        {
            Field = field;
            Messages = new List<string> { message };
        }

        public FieldErrorModel(string field, List<string> messages)
        {
            Field = field;
            Messages = messages;
        }
    }


    public enum CoreApiReturnCode
    {
        /// <summary>Thành công</summary>
        Succeed = 0,

        /// <summary>Lỗi không xác định (lỗi hệ thống, exception...)</summary>
        ExceptionOccurred = 1,

        /// <summary>Dữ liệu không hợp lệ</summary>
        InvalidData = 2,

        /// <summary>Không tìm thấy</summary>
        NotFound = 3,

        /// <summary>Không được phép truy cập</summary>
        Unauthorized = 4,

        /// <summary>Lỗi nghiệp vụ</summary>
        BusinessError = 5
    }
}