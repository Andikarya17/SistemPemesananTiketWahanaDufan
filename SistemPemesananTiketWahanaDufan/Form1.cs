using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace SistemPemesananTiketWahanaDufan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupReportViewer(); // Setup data dan laporan
            this.reportViewer1.RefreshReport(); // Tampilkan laporan
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            // (Optional) Tambahan konfigurasi saat ReportViewer load
        }

        private void SetupReportViewer()
        {
            // Connection string ke database
            string connectionString = @"Data Source=ANDIKARYA\ANDIKAARYA;Initial Catalog=TiketwahanDufan2;User ID=sa;Password=Rodamas17;";

            // Query untuk mengambil data pesanan beserta nama wahana dan pengunjung
            string query = @"
                SELECT
                    p.PesananID,
                    p.TanggalKunjungan,
                    p.JumlahTiket,
                    p.TotalHarga,
                    w.NamaWahana,
                    g.Nama AS NamaPengunjung,
                    p.MetodePembayaran,
                    p.StatusPesanan
                FROM
                    Pesanan AS p
                INNER JOIN
                    Wahana AS w ON p.WahanaID = w.WahanaID
                INNER JOIN
                    Pengunjung AS g ON p.PengunjungID = g.PengunjungID;";

            // Buat DataTable dan isi dengan data dari query
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            // Buat ReportDataSource dan hubungkan ke ReportViewer
            ReportDataSource rds = new ReportDataSource("DataSet1", dt); // Pastikan "DataSet1" sesuai dengan nama dataset di RDLC

            // Bersihkan dan tambahkan sumber data ke ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Set path ke file .rdlc
            reportViewer1.LocalReport.ReportPath = @"E:\Kuliah SMT 4\Pengembangan Aplikasi Basis Data\SistemPemesananTiketWahanaDufan\SistemPemesananTiketWahanaDufan\AdminReport.rdlc";

            // Refresh laporan
            reportViewer1.RefreshReport();
        }
    }
}
