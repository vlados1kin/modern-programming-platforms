namespace Helper;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ExportClassAttribute : Attribute
{
    public string Description { get; }

    public ExportClassAttribute(string description = "")
    {
        Description = description;
    }
}