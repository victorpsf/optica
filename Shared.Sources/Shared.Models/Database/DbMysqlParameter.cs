using System.Data;

namespace Shared.Models.Database;

public class DbMysqlParameter
{
    public string Name { get; private set; }
    public object? Value { get; private set; }
    public ParameterDirection Direction { get; private set; }

    private DbMysqlParameter(string name, object? value, ParameterDirection direction)
    {
        this.Name = name;
        this.Value = value;
        this.Direction = direction;
    }

    public static DbMysqlParameter Create(string name, object? value)
        => new(name, value, ParameterDirection.Input);
    
    public static DbMysqlParameter Create(string name, object? value, ParameterDirection direction)
        => new(name, value, direction);
}