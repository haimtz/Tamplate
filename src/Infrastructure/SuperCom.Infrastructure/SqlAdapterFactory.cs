namespace Sql.Infrastructure
{
    public class SqlAdapterFactory : ISqlAdapterFactory
    {
        public ISqlAdapter Create()
        {
            return new SqlAdapterWrapper();
        }
    }
}
