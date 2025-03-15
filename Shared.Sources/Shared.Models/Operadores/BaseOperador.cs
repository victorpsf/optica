namespace Shared.Models.Operadores;

public class BaseOperador<T>
{
    public string Property { get; set; }
    public string Operator { get; set; }
    public T? Value { get; set; }

    public BaseOperador(string property, string _operator, string value)
    {
        this.Property = property;
        this.Operator = _operator;
        this.Value = (T) Convert.ChangeType(value, typeof(T));
    }
}