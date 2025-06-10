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
            LoadWahana(); // WAJIB: supaya dropdown wahana terisi saat form dibuka
        }

        private void LoadWahana()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT WahanaID, NamaWahana FROM Wahana";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbWahana.DataSource = dt;
                    cmbWahana.DisplayMember = "NamaWahana";
                    cmbWahana.ValueMember = "WahanaID";
                    cmbWahana.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal load wahana: " + ex.Message);
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            txtNama.Text = "";
            txtEmail.Text = "";
            cmbWahana.SelectedIndex = -1;
            cmbTipeTiket.SelectedIndex = -1;
            dtpTanggal.Value = DateTime.Now;
            txtJumlahTiket.Text = "";
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fungsi hapus data belum diimplementasikan.");
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fungsi ubah data belum diimplementasikan.");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int pengunjungID = GetPengunjungIDByEmail(conn, txtEmail.Text);
                    if (pengunjungID == -1)
                    {
                        string insertPengunjung = "INSERT INTO Pengunjung (Nama, Email, Password) OUTPUT INSERTED.PengunjungID VALUES (@Nama, @Email, 'default123')";
                        SqlCommand cmdInsert = new SqlCommand(insertPengunjung, conn);
                        cmdInsert.Parameters.AddWithValue("@Nama", txtNama.Text);
                        cmdInsert.Parameters.AddWithValue("@Email", txtEmail.Text);
                        pengunjungID = (int)cmdInsert.ExecuteScalar();
                    }

                    decimal hargaPerTiket = GetHargaTiket(conn, (int)cmbWahana.SelectedValue, cmbTipeTiket.Text);
                    int jumlahTiket = int.Parse(txtJumlahTiket.Text);
                    decimal totalHarga = hargaPerTiket * jumlahTiket;

                    string insertPesanan = "INSERT INTO Pesanan (PengunjungID, WahanaID, TanggalKunjungan, JumlahTiket, TotalHarga, MetodePembayaran, StatusPesanan) " +
                                           "VALUES (@PengunjungID, @WahanaID, @Tanggal, @Jumlah, @Total, 'Tunai', 'Dipesan')";
                    SqlCommand cmd = new SqlCommand(insertPesanan, conn);
                    cmd.Parameters.AddWithValue("@PengunjungID", pengunjungID);
                    cmd.Parameters.AddWithValue("@WahanaID", cmbWahana.SelectedValue);
                    cmd.Parameters.AddWithValue("@Tanggal", dtpTanggal.Value);
                    cmd.Parameters.AddWithValue("@Jumlah", jumlahTiket);
                    cmd.Parameters.AddWithValue("@Total", totalHarga);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Pesanan berhasil disimpan.");

                    // 🔥 INI DIA TAMBAHAN UNTUK MENAMPILKAN FORM PREVIEW
                    PreviewForm preview = new PreviewForm(txtEmail.Text);
                    preview.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private int GetPengunjungIDByEmail(SqlConnection conn, string email)
        {
            string query = "SELECT PengunjungID FROM Pengunjung WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }

        private decimal GetHargaTiket(SqlConnection conn, int wahanaID, string tipeTiket)
        {
            string query = "SELECT HargaTiket FROM Wahana WHERE WahanaID = @WahanaID AND TipeTiket = @TipeTiket";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@WahanaID", wahanaID);
            cmd.Parameters.AddWithValue("@TipeTiket", tipeTiket);
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToDecimal(result) : 50000m;
        }
    }
}
