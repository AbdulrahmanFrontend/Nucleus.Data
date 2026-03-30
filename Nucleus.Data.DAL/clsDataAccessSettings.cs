using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.DAL
{
    public class clsDataAccessSettings
    {
        public static SqlConnection GetConnection()
        {
            string ConnectionString =
                "Server=.;Database=SmartDataDB;User Id=sa;Password=sa123456;" +
                "TrustServerCertificate=True;";

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException("invalid connection string");

            return new SqlConnection(ConnectionString);
        }
    }
}
