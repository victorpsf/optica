namespace Shared.Extensions;

public static class PrimitiveExtension
{
    public static bool IsNullOrEmpty(this string? value)
    {
        bool result = string.IsNullOrEmpty(value);
        
        if (!result)
            result = string.IsNullOrEmpty(value?.Trim());
        
        return result;
    }
}