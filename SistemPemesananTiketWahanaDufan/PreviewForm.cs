using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class PreviewData : Form
    {
        private DataTable importedData;

        // ✅ Ambil koneksi dari class koneksi.cs
        private readonly koneksi konn = new koneksi();
        private readonly string connString;

        public PreviewData(DataTable dt)
        {
            InitializeComponent();
            importedData = dt;
            connString = konn.connectionString(); // Inisialisasi koneksi
            dgvPreview.DataSource = importedData;
        }

        private void PreviewData_Load(object sender, EventArgs e)
        {
            dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Simpan data ke database?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                SimpanKeDatabase();
        }

        private void SimpanKeDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    foreach (DataRow row in importedData.Rows)
                    {
                        string query = @"
                            INSERT INTO PesananPreview (PesananID, NamaPengunjung, NamaWahana, TanggalKunjungan)
                            VALUES (@id, @nama, @wahana, @tanggal)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", row["PesananID"]);
                            cmd.Parameters.AddWithValue("@nama", row["NamaPengunjung"]);
                            cmd.Parameters.AddWithValue("@wahana", row["NamaWahana"]);
                            cmd.Parameters.AddWithValue("@tanggal", row["TanggalKunjungan"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Data berhasil disimpan.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan: " + ex.Message);
            }
        }
    }
}
