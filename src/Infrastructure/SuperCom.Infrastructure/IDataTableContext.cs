using System.Data;
using System.Data.SqlClient;

namespace Sql.Infrastructure
{
    public interface IDataTableContext
    {
        DataTable Reader(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param);

        object ExecuteScalar(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param);

        int ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param);
    }
}