using System.Data.SqlClient;

namespace SuperCom.Infrastructure
{
    public interface ISqlConnection
    {
        SqlConnection Connection { get; }

        void Open();

        void Close();
    }
}
