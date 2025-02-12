using System.Linq;

namespace Shared.Extensions;

public static class EnumerableExtensions
{
    public static T? getAnnotation<T>(this Enum value) where T : Attribute
    {
        foreach (var enumValue in value.GetType().GetFields())
        {
            if (!value.Equals(enumValue.GetValue(value)))
                continue;

            var attribute = enumValue.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            if (attribute is not null)
                return (T) attribute;
        }
        
        return null;
    }
}
