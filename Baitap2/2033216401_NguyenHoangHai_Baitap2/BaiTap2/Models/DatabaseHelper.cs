using System.Data.SqlClient;

namespace BaiTap2.Models
{
    public class DatabaseHelper
    {
        private static readonly string ConnectionString = "workstation id=hoanghaidb.mssql.somee.com;packet size=4096;user id=hoanghai07077;pwd=samuraix;data source=hoanghaidb.mssql.somee.com;persist security info=False;initial catalog=hoanghaidb;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
