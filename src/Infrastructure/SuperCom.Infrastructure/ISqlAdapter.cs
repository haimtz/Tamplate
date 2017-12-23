using System.Data;
using System.Data.SqlClient;

namespace Sql.Infrastructure
{
    public interface ISqlAdapter
    {
        ISqlCommand SelectCommand { set; }

        int FillTable(DataTable table);

        SqlDataAdapter Adapter { get;  }
    }
}
