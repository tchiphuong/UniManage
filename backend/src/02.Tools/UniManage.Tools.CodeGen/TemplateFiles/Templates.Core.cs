namespace UniManage.Tools.CodeGen
{
    public static partial class Templates
    {
        private static string ToResourceProp(string screenCode, string type, string name)
        {
            return $"{screenCode}_{type}_{name}";
        }
    }
}
