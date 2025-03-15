namespace Shared.Models.Operadores;

public class NumberOperator<T>: BaseOperador<T>
{
    public NumberOperator(string property, string _operator, string value) : base(property, _operator, value) 
    { }
}