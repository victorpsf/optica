using Shared.Models.Annotation;
using System.Reflection;
using System.Text.Json;

namespace Shared.Extensions;

public static class ClassConverter
{
    //public static object? ConvertTo<T>(
    //    this T source, 
    //    Type reference
    //) where T : class, new() {
    //    var instance = Activator.CreateInstance(reference);

    //    if (instance is null)
    //        throw new Exception("Invalid construtor for class");

    //    var newProperties = instance.GetType().GetProperties();
    //    var refProperties = source.GetType().GetProperties();

    //    if (newProperties is not null)
    //        foreach (var newProperty in newProperties)
    //        {
    //            if (!newProperty.CanWrite)
    //                continue;

    //            var refProperty = refProperties.FirstOrDefault(x => x.Name == newProperty.Name);
    //            if (refProperty == null || !refProperty.CanRead)
    //                continue;

    //            var converter = newProperty.GetCustomAttribute<ConverterAnnotation>();
    //            if (converter is null)
    //            {
    //                newProperty.SetValue(instance, refProperty.GetValue(source, null));
    //                continue;
    //            }


    //            switch (converter.Type)
    //            {
    //                case ConverterAnnotationType.LIST:
    //                    var values = new List<object?>();

    //                    foreach (var _value in (List<object?>)refProperty.GetValue(source, null))
    //                        values.Add(_value?.ConvertTo(converter.Reference));

    //                    newProperty.SetValue(instance, values);
    //                    break;
    //                default:
    //                    var value2 = refProperty.GetValue(source, null);

    //                    if (value2 is null)
    //                        break;

    //                    newProperty.SetValue(instance, value2.ConvertTo(converter.Reference));
    //                    break;
    //            }
    //        }

    //    return instance;
    //}

    public static object? ConvertTo<T>(
        this T source,
        Type reference
    ) where T : class, new()
    {
        var instance = Activator.CreateInstance(reference);

        if (instance is null)
            throw new Exception("Invalid constructor for class");

        var newProperties = instance.GetType().GetProperties();
        var refProperties = source.GetType().GetProperties();

        foreach (var newProperty in newProperties)
        {
            if (!newProperty.CanWrite)
                continue;

            var refProperty = refProperties.FirstOrDefault(x => x.Name == newProperty.Name);
            if (refProperty == null || !refProperty.CanRead)
                continue;

            var converter = newProperty.GetCustomAttribute<ConverterAnnotation>();
            if (converter is null)
            {
                newProperty.SetValue(instance, refProperty.GetValue(source, null));
                continue;
            }

            try
            {
                switch (converter.Type)
                {
                    case ConverterAnnotationType.LIST:
                        var refValue = refProperty.GetValue(source, null);
                        if (refValue is IEnumerable<object?> refList)
                        {
                            var values = new List<object?>();
                            foreach (var _value in refList)
                                values.Add(_value?.ConvertTo(converter.Reference)); 
                            newProperty.SetValue(instance, values);
                        }
                        break;
                    default:
                        var value2 = refProperty.GetValue(source, null);
                        if (value2 is not null)
                        {
                            newProperty.SetValue(instance, value2.ConvertTo(converter.Reference));
                        }
                        break;
                }
            }
            catch { }
        }

        return instance;
    }
}