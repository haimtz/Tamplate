namespace Sql.Infrastructure
{
    public interface ISqlCommandFactory
    {
        ISqlCommand CreateCommand();

        ISqlCommand CreateCommand(string query, ISqlConnection connection);
    }
}