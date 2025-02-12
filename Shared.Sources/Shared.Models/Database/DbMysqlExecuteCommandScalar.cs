namespace Shared.Models.Database;

public class DbMysqlExecuteCommandScalar: DbMysqlExecuteCommand
{
    public string PrimaryKey { get; set; }
}