namespace Shared.Extensions;

public static class ClassConverter
{
    public static B ConvertTo<T, B>(this T source)
        where T : class, new()
        where B : class, new()
    {
        var instance = new B();
        var properties = instance.GetType().GetProperties();
        var sourceProperties = source.GetType().GetProperties();

        foreach (var property in properties)
        {
            if (!property.CanWrite)
                continue;

            var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == property.Name);
            if (sourceProperty == null || !sourceProperty.CanRead)
                continue;
            
            property.SetValue(instance, sourceProperty.GetValue(source, null));
        }
        
        return instance;
    }
}