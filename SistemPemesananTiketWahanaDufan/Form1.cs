using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace SistemPemesananTiketWahanaDufan
{
    public partial class Form1 : Form
    {
        // ✅ Tambahkan koneksi dari class koneksi.cs
        private readonly TiketWahanaApp.koneksi konn = new TiketWahanaApp.koneksi();
        private readonly string connString;

        public Form1()
        {
            InitializeComponent();
            connString = konn.connectionString(); // Ambil koneksi dari koneksi.cs
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

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", dt); // Pastikan nama dataset sesuai file RDLC

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Ganti path report sesuai lokasi di output deploy (misalnya satu folder)
            reportViewer1.LocalReport.ReportPath = "AdminReport.rdlc";

            reportViewer1.RefreshReport();
        }
    }
}
