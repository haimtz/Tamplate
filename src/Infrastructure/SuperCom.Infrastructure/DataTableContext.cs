using System.Data;
using System.Data.SqlClient;

namespace Sql.Infrastructure
{
    public class DataTableContext : IDataTableContext
    {
        private readonly ISqlConnection _sqlconnection;
        private readonly ISqlCommandFactory _commandFactory;
        private readonly ISqlAdapterFactory _adapterFactory;

        public DataTableContext(ISqlConnection sqlconnection, ISqlCommandFactory commandFactory, ISqlAdapterFactory adapterFactory)
        {
            _sqlconnection = sqlconnection;
            _commandFactory = commandFactory;
            _adapterFactory = adapterFactory;
        }

        public DataTable Reader(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            DataTable table = new DataTable();
            try
            {
                var adatpter = _adapterFactory.Create();
                adatpter.SelectCommand = CreteCommand(query, commandType, param);
                _sqlconnection.Open();
                adatpter.FillTable(table);
                return table;
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
                var command = CreteCommand(query, commandType, param);

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
                var command = CreteCommand(query, commandType, param);

                _sqlconnection.Open();
                return command.ExecuteNonQuery();
            }
            finally
            {
                _sqlconnection.Close();
            }
        }

        private ISqlCommand CreteCommand(string query, CommandType commandType, SqlParameter[] param)
        {
            var command = _commandFactory.CreateCommand(query, _sqlconnection);
            command.SetCommandType(commandType);
            if (param.Length > 0)
                command.AddParameters(param);
            return command;
        }
    }
}
