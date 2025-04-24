using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class MainForm : Form
    {
        private string connString = @"Data Source=Andikarya\\ANDIKAARYA;Initial Catalog=TiketwahanDufan2;Integrated Security=True";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadWahana();
            LoadPengunjung();
            LoadPesanan();
            LoadAdmin();
        }

        #region — CRUD Wahana —
        private void LoadWahana()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Wahana", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvWahana.DataSource = dt;
            }
        }

        private void btnTambahWahana_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Wahana (NamaWahana, TipeTiket, HargaTiket, KapasitasHarian, AdminID) VALUES (@nama, @tipe, @harga, @kapasitas, @adminID)", conn);
                cmd.Parameters.AddWithValue("@nama", txtNamaWahana.Text);
                cmd.Parameters.AddWithValue("@tipe", cmbTipeTiket.Text);
                cmd.Parameters.AddWithValue("@harga", decimal.Parse(txtHargaTiket.Text));
                cmd.Parameters.AddWithValue("@kapasitas", int.Parse(txtKapasitas.Text));
                cmd.Parameters.AddWithValue("@adminID", (int)cmbAdmin.SelectedValue);
                cmd.ExecuteNonQuery();
                LoadWahana();
            }
        }

        private void btnUbahWahana_Click(object sender, EventArgs e)
        {
            if (dgvWahana.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Wahana SET NamaWahana=@nama, TipeTiket=@tipe, HargaTiket=@harga, KapasitasHarian=@kapasitas WHERE WahanaID=@id", conn);
                    cmd.Parameters.AddWithValue("@nama", txtNamaWahana.Text);
                    cmd.Parameters.AddWithValue("@tipe", cmbTipeTiket.Text);
                    cmd.Parameters.AddWithValue("@harga", decimal.Parse(txtHargaTiket.Text));
                    cmd.Parameters.AddWithValue("@kapasitas", int.Parse(txtKapasitas.Text));
                    cmd.Parameters.AddWithValue("@id", dgvWahana.SelectedRows[0].Cells["WahanaID"].Value);
                    cmd.ExecuteNonQuery();
                    LoadWahana();
                }
            }
        }

        private void btnHapusWahana_Click(object sender, EventArgs e)
        {
            if (dgvWahana.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Wahana WHERE WahanaID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", dgvWahana.SelectedRows[0].Cells["WahanaID"].Value);
                    cmd.ExecuteNonQuery();
                    LoadWahana();
                }
            }
        }

        private void LoadAdmin()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT AdminID, NamaAdmin FROM Admin", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbAdmin.DataSource = dt;
                cmbAdmin.DisplayMember = "NamaAdmin";
                cmbAdmin.ValueMember = "AdminID";
            }
        }
        #endregion

        #region — CRUD Pengunjung —
        private void LoadPengunjung()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Pengunjung", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPengunjung.DataSource = dt;
            }
        }

        private void btnUbahPengunjung_Click(object sender, EventArgs e)
        {
            if (dgvPengunjung.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Pengunjung SET Nama=@nama, Email=@email WHERE PengunjungID=@id", conn);
                    cmd.Parameters.AddWithValue("@nama", txtNamaPengunjung.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmailPengunjung.Text);
                    cmd.Parameters.AddWithValue("@id", dgvPengunjung.SelectedRows[0].Cells["PengunjungID"].Value);
                    cmd.ExecuteNonQuery();
                    LoadPengunjung();
                }
            }
        }

        private void btnHapusPengunjung_Click(object sender, EventArgs e)
        {
            if (dgvPengunjung.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Pengunjung WHERE PengunjungID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", dgvPengunjung.SelectedRows[0].Cells["PengunjungID"].Value);
                    cmd.ExecuteNonQuery();
                    LoadPengunjung();
                }
            }
        }
        #endregion

        #region — Kelola Tiket / Pesanan —
        private void LoadPesanan()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT p.PesananID, q.Nama AS Pengunjung, w.NamaWahana, p.TanggalKunjungan, p.JumlahTiket, p.TotalHarga, p.StatusPesanan FROM Pesanan p JOIN Pengunjung q ON p.PengunjungID=q.PengunjungID JOIN Wahana w ON p.WahanaID=w.WahanaID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPesanan.DataSource = dt;
            }
        }

        private void btnTambahPesanan_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Pesanan (PengunjungID, WahanaID, TanggalKunjungan, JumlahTiket, TotalHarga, MetodePembayaran, StatusPesanan) VALUES (@pengunjung, @wahana, @tanggal, @jumlah, @total, 'Tunai', 'Dipesan')", conn);
                int jumlah = int.Parse(txtJumlahTiket.Text);
                decimal harga = GetHargaWahana((int)cmbWahanaPesanan.SelectedValue);

                cmd.Parameters.AddWithValue("@pengunjung", (int)cmbPengunjung.SelectedValue);
                cmd.Parameters.AddWithValue("@wahana", (int)cmbWahanaPesanan.SelectedValue);
                cmd.Parameters.AddWithValue("@tanggal", dtpTanggal.Value.Date);
                cmd.Parameters.AddWithValue("@jumlah", jumlah);
                cmd.Parameters.AddWithValue("@total", harga * jumlah);
                cmd.ExecuteNonQuery();
                LoadPesanan();
            }
        }

        private decimal GetHargaWahana(int wahanaID)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT HargaTiket FROM Wahana WHERE WahanaID = @id", conn);
                cmd.Parameters.AddWithValue("@id", wahanaID);
                return (decimal)cmd.ExecuteScalar();
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvPesanan.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Pesanan SET StatusPesanan=@status WHERE PesananID=@id", conn);
                    cmd.Parameters.AddWithValue("@status", cmbStatusPesanan.Text);
                    cmd.Parameters.AddWithValue("@id", dgvPesanan.SelectedRows[0].Cells["PesananID"].Value);
                    cmd.ExecuteNonQuery();
                    LoadPesanan();
                }
            }
        }

        private void btnHapusPesanan_Click(object sender, EventArgs e)
        {
            if (dgvPesanan.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Pesanan WHERE PesananID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", dgvPesanan.SelectedRows[0].Cells["PesananID"].Value);
                    cmd.ExecuteNonQuery();
                    LoadPesanan();
                }
            }
        }
        #endregion
    }
}