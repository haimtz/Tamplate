using System.Data;
using System.Data.SqlClient;

namespace Sql.Infrastructure
{
    public class SqlAdapterWrapper : ISqlAdapter
    {
        private readonly SqlDataAdapter _adapter;

        public SqlAdapterWrapper()
        {
            _adapter = new SqlDataAdapter();
        }

        public ISqlCommand SelectCommand
        {
            set => _adapter.SelectCommand = value.Command;
        }

        public SqlDataAdapter Adapter => _adapter;
        
        public int FillTable(DataTable table)
        {
            if (table == null)
                table = new DataTable();

            return _adapter.Fill(table);
        }
    }
}
