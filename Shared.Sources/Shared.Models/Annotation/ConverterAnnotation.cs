namespace Shared.Models.Annotation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ConverterAnnotation : Attribute
{
    public ConverterAnnotationType Type { get; private set; }
    public Type Reference { get; private set; }

    public ConverterAnnotation(ConverterAnnotationType type, Type reference)
    {
        this.Type = type;
        this.Reference = reference;
    }
}
