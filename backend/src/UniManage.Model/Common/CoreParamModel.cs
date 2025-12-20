namespace UniManage.Model.Common
{
    public class CoreParamModel
    {
        public string Name { get; set; }
        public object? Value { get; set; }

        public CoreParamModel(string name, object? value)
        {
            Name = name;
            Value = value;
        }

        public CoreParamModel() { }
    }
}
