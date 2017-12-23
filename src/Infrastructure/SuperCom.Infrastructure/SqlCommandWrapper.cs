using System.Data;
using System.Data.SqlClient;

namespace Sql.Infrastructure
{
    public class SqlCommandWrapper : ISqlCommand
    {
        private readonly SqlCommand _command;

        public SqlCommandWrapper()
        {
            _command = new SqlCommand();
        }

        public SqlCommandWrapper(string query, ISqlConnection connection) : this()
        {
            _command.CommandText = query;
            _command.Connection = connection.Connection;
        }

        public SqlCommand Command => _command;

        public void AddConnection(ISqlConnection connection)
        {
            _command.Connection = connection.Connection;
        }

        public void AddParameters(SqlParameter[] sqlParameters)
        {
            _command.Parameters.AddRange(sqlParameters);
        }

        public int ExecuteNonQuery()
        {
            return _command.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            return _command.ExecuteScalar();
        }

        public void SetCommandType(CommandType type)
        {
            _command.CommandType = type;
        }
    }
}
