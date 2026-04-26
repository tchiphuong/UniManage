using System.Text.Json.Serialization;

namespace UniManage.Model.Common
{
    /// <summary>
    /// Model ghi log API tập trung
    /// </summary>
    public class CoreLogModel
    {
        #region Properties

        public HeaderInfo? HeaderInfo { get; set; }

        /// <summary>
        /// Danh sách tham số đầu vào (Chuẩn mới: Parameter)
        /// </summary>
        public List<CoreParamModel> Parameter { get; set; } = new();

        /// <summary>
        /// Alias cho Parameter để tương thích với các Handler cũ
        /// </summary>
        [JsonIgnore]
        public List<CoreParamModel> Parameters 
        { 
            get => Parameter; 
            set => Parameter = value; 
        }

        public object? Result { get; set; }
        
        public string? Message { get; set; }

        /// <summary>
        /// Đánh dấu ngoại lệ xảy ra
        /// </summary>
        public bool IsException { get; set; }

        /// <summary>
        /// Mã phản hồi API
        /// </summary>
        public int ReturnCode { get; set; }

        public string? Method { get; set; }
        
        public string? Path { get; set; }

        #endregion

        #region Constructors

        public CoreLogModel() { }

        public CoreLogModel(HeaderInfo? headerInfo)
        {
            HeaderInfo = headerInfo;
        }

        #endregion
    }
}
