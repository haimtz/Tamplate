namespace Sql.Infrastructure
{
    public interface ISqlAdapterFactory
    {
        ISqlAdapter Create();
    }
}