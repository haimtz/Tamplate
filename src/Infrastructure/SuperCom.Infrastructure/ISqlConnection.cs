using System.Data.SqlClient;

namespace Sql.Infrastructure
{
    public interface ISqlConnection
    {
        SqlConnection Connection { get; }

        void Open();

        void Close();
    }
}
