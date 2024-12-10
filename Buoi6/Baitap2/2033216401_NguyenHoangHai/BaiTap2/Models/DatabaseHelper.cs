using System.Data.SqlClient;

namespace BaiTap2.Models
{
    public class DatabaseHelper
    {
        private static readonly string ConnectionString = "Data Source=DESKTOP-PGC7MIC\\HOANGHAI;Database=SACH;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}