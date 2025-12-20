using System.Collections.Generic;

namespace UniManage.Model.Common
{
    public class CoreLogModel
    {
        public HeaderInfo HeaderInfo { get; set; }
        public List<CoreParamModel> Parameter { get; set; } = new();
        public object? Result { get; set; }
        public string? Message { get; set; }
        public int IsException { get; set; }
        public int ReturnCode { get; set; }

        public CoreLogModel() { }

        public CoreLogModel(HeaderInfo headerInfo)
        {
            HeaderInfo = headerInfo;
        }
    }
}
