using System.Data;

namespace Shared.Models.Database;

public class DbMysqlParameterCollection
{
    public List<DbMysqlParameter> Parameters { get; private set; } = new();
    
    private DbMysqlParameterCollection() { }

    public DbMysqlParameterCollection Add(string name, object? value)
    {
        this.Parameters.Add(DbMysqlParameter.Create(name, value));
        return this;
    }

    public DbMysqlParameterCollection Add(string name, object? value, ParameterDirection direction)
    {
        this.Parameters.Add(DbMysqlParameter.Create(name, value, direction));
        return this;
    }
    
    public static DbMysqlParameterCollection Create()
        => new();
}