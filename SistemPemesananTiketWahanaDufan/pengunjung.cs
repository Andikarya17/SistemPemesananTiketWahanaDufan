using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class FormPemesanan : Form
    {
        // ✅ Tambahkan koneksi dari class koneksi.cs
        private readonly koneksi konn = new koneksi();
        private readonly string connString;

        public FormPemesanan()
        {
            InitializeComponent();
            connString = konn.connectionString(); // Inisialisasi koneksi
            LoadWahana(); // WAJIB: supaya dropdown wahana terisi saat form dibuka
        }

        private void LoadWahana()
        {
            using (SqlConnection conn = new SqlConnection(connString))
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
            // ✅ Validasi: Nama tidak diisi
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validasi: Email tidak diisi
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validasi: Wahana tidak dipilih
            if (cmbWahana.SelectedIndex == -1 || cmbWahana.SelectedValue == null)
            {
                MessageBox.Show("Pilih wahana terlebih dahulu.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validasi: Tipe Tiket tidak dipilih
            if (string.IsNullOrWhiteSpace(cmbTipeTiket.Text))
            {
                MessageBox.Show("Pilih tipe tiket terlebih dahulu.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validasi: Tipe Tiket tidak sesuai pilihan (harus Reguler / Fast Track)
            string tipeTiket = cmbTipeTiket.Text.Trim();
            if (tipeTiket != "Reguler" && tipeTiket != "Fast Track")
            {
                MessageBox.Show("Tipe tiket hanya boleh Reguler atau Fast Track.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validasi: Tanggal tidak dipilih (dianggap kosong jika default 1/1/0001 atau tahun terlalu lama)
            if (dtpTanggal.Value.Year < 2000)
            {
                MessageBox.Show("Tanggal kunjungan tidak valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validasi: Jumlah tiket kosong atau bukan angka positif
            if (!int.TryParse(txtJumlahTiket.Text, out int jumlahTiket) || jumlahTiket <= 0)
            {
                MessageBox.Show("Jumlah tiket harus berupa angka lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection conn = new SqlConnection(connString))
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
