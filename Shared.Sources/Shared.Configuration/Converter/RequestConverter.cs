using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Configuration.Converter;

public class RequestConverter: IActionFilter
{
    private record QueryOperator(string Property, string Operator, string? Value);

    public void OnActionExecuting(ActionExecutingContext context)
    {
        List<QueryOperator> operators = new List<QueryOperator>();
        foreach (var item in context.HttpContext.Request.Query.Keys)
        {
            var value = context.HttpContext.Request.Query[item];

            if (item.Contains("."))
            {
                var parts = item.Split('.');
                operators.Add(new QueryOperator(parts[0], parts[1], value));
            }
            
            else operators.Add(new QueryOperator(item, "eq", value));
        }

        foreach (var parameter in context.ActionDescriptor.Parameters)
            try
            {
                if (!(string.Equals(parameter?.BindingInfo?.BindingSource?.Id.ToUpperInvariant(), "QUERY")))
                    continue;
                
                if (parameter?.ParameterType is null)
                    continue;

                var instance = Activator.CreateInstance(parameter.ParameterType);
                if (instance is null)
                    continue;

                foreach (var property in instance.GetType().GetProperties())
                {
                    var _operator = operators.FirstOrDefault(a => string.Equals(a.Property.ToUpperInvariant(), property.Name.ToUpperInvariant()));
                    
                    if (_operator is null)
                        continue;
                    
                    try { property.SetValue(instance, Activator.CreateInstance(property.PropertyType, _operator.Property, _operator.Operator, _operator.Value)); }
                    catch { }
                    
                    try { property.SetValue(instance, Convert.ChangeType(_operator.Value, property.PropertyType)); }
                    catch { }
                }
                
                context.ActionArguments[parameter.Name] = instance;
            }
        
            catch { }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    { }
}