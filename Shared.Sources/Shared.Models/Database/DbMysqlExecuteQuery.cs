using System.Data;

namespace Shared.Models.Database;

public class DbMysqlExecuteQuery
{
    public string Sql { get; set; } = string.Empty;
    public DbMysqlParameterCollection Collection { get; set; } = DbMysqlParameterCollection.Create();
    public int CommandTimeOut { get; set; } = 22000;
    public CommandType CommandType { get; set; } = CommandType.Text;
}