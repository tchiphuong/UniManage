namespace UniManage.Core.Models
{
    public class CoreLogModel
    {
        public HeaderInfo HeaderInfo { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
        public int IsException { get; set; }
        public CoreApiReturnCode ReturnCode { get; set; }
        public List<CoreParamModel> Parameter { get; set; }
        public CoreLogModel(HeaderInfo headerInfo)
        {
            HeaderInfo = headerInfo;
        }
    }

    public class CoreParamModel
    {
        // Thêm các thuộc tính nếu cần
    }
}
