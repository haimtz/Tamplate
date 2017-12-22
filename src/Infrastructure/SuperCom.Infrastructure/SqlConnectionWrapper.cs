using System.Data.SqlClient;

namespace SuperCom.Infrastructure
{
    public class SqlConnectionWrapper : ISqlConnection
    {
        private readonly SqlConnection _connection;

        public SqlConnectionWrapper(string connectionString)
        {
            _connection = new SqlConnection()
            {
                ConnectionString = connectionString
            };
        }

        public SqlConnection Connection => _connection;

        public void Open()
        {
            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
        }
    }
}
