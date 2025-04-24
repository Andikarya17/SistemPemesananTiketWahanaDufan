using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class FormPemesanan : Form
    {
        private string connectionString = "Data Source=Andikarya\\ANDIKAARYA;Initial Catalog=TiketwahanDufan2;Integrated Security=True";

        public FormPemesanan()
        {
            InitializeComponent();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Kosongkan form untuk input baru
            txtNama.Text = "";
            txtID.Text = "";
            txtEmail.Text = "";
            cmbWahana.SelectedIndex = -1;
            dtpTanggal.Value = DateTime.Now;
            txtJumlah.Text = "";
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fungsi hapus data masih belum diimplementasikan.");
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fungsi ubah data masih belum diimplementasikan.");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Pesanan (PengunjungID, WahanaID, TanggalKunjungan, JumlahTiket, TotalHarga, MetodePembayaran, StatusPesanan) " +
                                   "VALUES (@PengunjungID, @WahanaID, @Tanggal, @Jumlah, @Total, 'Tunai', 'Dipesan')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PengunjungID", txtID.Text);
                    cmd.Parameters.AddWithValue("@WahanaID", cmbWahana.SelectedValue); // Pastikan ValueMember sudah diset
                    cmd.Parameters.AddWithValue("@Tanggal", dtpTanggal.Value);
                    cmd.Parameters.AddWithValue("@Jumlah", int.Parse(txtJumlah.Text));
                    cmd.Parameters.AddWithValue("@Total", HitungTotalHarga());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Pesanan berhasil disimpan.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private decimal HitungTotalHarga()
        {
            // Dummy static pricing, bisa disesuaikan ambil dari database
            decimal hargaPerTiket = 50000;
            int jumlah = int.Parse(txtJumlah.Text);
            return hargaPerTiket * jumlah;
        }
    }
}
