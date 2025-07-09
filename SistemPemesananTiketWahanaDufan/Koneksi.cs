using System.Data.SqlClient;

namespace TiketWahanaApp
{
    public class koneksi
    {
        private readonly string connstr = @"Data Source=172.20.10.4,1433;Initial Catalog=TiketwahanDufan2;User ID=sa;Password=Rodamas17;";

        /// <summary>
        /// Mengembalikan string koneksi ke SQL Server menggunakan SQL Authentication.
        /// </summary>
        public string connectionString()
        {
            return connstr;
        }

        /// <summary>
        /// Mengembalikan objek SqlConnection berdasarkan string koneksi tetap.
        /// </summary>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString());
        }
    }
}
