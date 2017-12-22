using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SuperCom.Infrastructure
{
    public class DataTableContext<T> : IDataTableContext<T>
    {
        private readonly ISqlConnection _sqlconnection;

        public DataTableContext(ISqlConnection sqlconnection)
        {
            _sqlconnection = sqlconnection;
        }

        public IList<T> ReadAll(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            try
            {
                var command = CreateCommand(query, commandType, param);
                _sqlconnection.Open();

                var reader = command.ExecuteReader();

                return null;
            }
            finally
            {
                _sqlconnection.Close();
            }
        }

        public object Insert(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            throw new NotImplementedException();
        }

        public void Update(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param)
        {
            throw new NotImplementedException();
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
