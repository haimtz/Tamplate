using System.Data;
using System.Data.SqlClient;

namespace Sql.Infrastructure
{
    public interface ISqlCommand
    {
        object ExecuteScalar();

        int ExecuteNonQuery();

        void AddConnection(ISqlConnection connection);

        void AddParameters(SqlParameter[] sqlParameter);

        void SetCommandType(CommandType type);

        SqlCommand Command { get; }
    }
}
