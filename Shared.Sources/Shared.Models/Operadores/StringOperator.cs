namespace Shared.Models.Operadores;

public class StringOperator: BaseOperador<string>
{
    public StringOperator(string property, string _operator, string value) : base(property, _operator, value) 
    { }
}