using System.Data;
using System.Data.SqlClient;

namespace Sql.Infrastructure
{
    public class DataTableContext : IDataTableContext
    {
        private readonly ISqlConnection _sqlconnection;

        public DataTableContext(ISqlConnection sqlconnection)
        {
            _sqlconnection = sqlconnection;
        }

        public SqlDataReader Reader(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            try
            {
                var command = CreateCommand(query, commandType, param);
                _sqlconnection.Open();

                return command.ExecuteReader();
            }
            finally
            {
                _sqlconnection.Close();
            }
        }

        public object ExecuteScalar(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            try
            {
                var command = CreateCommand(query, commandType, param);
                _sqlconnection.Open();
                return command.ExecuteScalar();
            }
            finally
            {
                _sqlconnection.Close();
            }
        }

        public int ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            try
            {
                var command = CreateCommand(query, commandType, param);
                _sqlconnection.Open();
                return command.ExecuteNonQuery();
            }
            finally
            {
                _sqlconnection.Close();
            }
        }

        private SqlCommand CreateCommand(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            var command = new SqlCommand
            {
                Connection = _sqlconnection.Connection,
                CommandText = query,
                CommandType = commandType
            };

            if (param.Length > 0)
                command.Parameters.AddRange(param);

            return command;
        }
    }
}
