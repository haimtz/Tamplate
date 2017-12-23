namespace Sql.Infrastructure
{
    public class SqlCommandFactory : ISqlCommandFactory
    {
        public ISqlCommand CreateCommand()
        {
            return new SqlCommandWrapper();
        }

        public ISqlCommand CreateCommand(string query, ISqlConnection connection)
        {
            return new SqlCommandWrapper(query, connection);
        }
    }
}
