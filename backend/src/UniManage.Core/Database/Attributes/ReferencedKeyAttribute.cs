namespace UniManage.Model.Attributes;

/// <summary>
/// Specifies the Principal Key (Unique Key/Alternate Key) column in the referenced table.
/// Use this when the Foreign Key does not target the Primary Key.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ReferencedKeyAttribute : Attribute
{
    public string KeyName { get; }

    public ReferencedKeyAttribute(string keyName)
    {
        KeyName = keyName;
    }
}
