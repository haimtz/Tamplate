using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SuperCom.Infrastructure
{
    public interface IDataTableContext<T>
    {
        IList<T> ReadAll(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param);

        object Insert(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param);

        void Update(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param);

        bool Delete(string query, CommandType commandType = CommandType.Text, params SqlParameter[] param);
    }
}