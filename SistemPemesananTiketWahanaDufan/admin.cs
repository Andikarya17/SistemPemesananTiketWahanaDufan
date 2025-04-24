using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class MainForm : Form
    {
        private string connString = @"Data Source=Andikarya\ANDIKAARYA;Initial Catalog=TiketwahanDufan2;Integrated Security=True";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadComboAdmins();
            LoadComboPengunjung();
            LoadComboWahana();
            LoadPesanan();
        }

        #region — Load Comboboxes —
        private void LoadComboAdmins()
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT AdminID, Nama FROM Admin", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbAdmin.DataSource = dt;
                cmbAdmin.ValueMember = "AdminID";
                cmbAdmin.DisplayMember = "Nama";
            }
            finally
            {
                conn.Dispose();
            }
        }

        private void LoadComboPengunjung()
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT PengunjungID, Nama FROM Pengunjung", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbPengunjung.DataSource = dt;
                cmbPengunjung.ValueMember = "PengunjungID";
                cmbPengunjung.DisplayMember = "Nama";
            }
            finally
            {
                conn.Dispose();
            }
        }

        private void LoadComboWahana()
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT WahanaID, NamaWahana FROM Wahana", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbWahanaPesanan.DataSource = dt;
                cmbWahanaPesanan.ValueMember = "WahanaID";
                cmbWahanaPesanan.DisplayMember = "NamaWahana";
            }
            finally
            {
                conn.Dispose();
            }
        }
        #endregion

        #region — CRUD Pesanan —
        private void LoadPesanan()
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT p.PesananID, q.Nama AS NamaPengunjung, w.NamaWahana, 
                           p.TanggalKunjungan, p.JumlahTiket, p.TotalHarga, 
                           p.MetodePembayaran, p.StatusPesanan
                    FROM Pesanan p
                    JOIN Pengunjung q ON p.PengunjungID = q.PengunjungID
                    JOIN Wahana w ON p.WahanaID = w.WahanaID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPesanan.DataSource = dt;
            }
            finally
            {
                conn.Dispose();
            }
        }

        private void btnTambahPesanan_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                decimal harga = GetHargaWahana((int)cmbWahanaPesanan.SelectedValue);
                int jumlah = int.Parse(txtJumlahTiket.Text);
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Pesanan (PengunjungID, WahanaID, TanggalKunjungan, 
                                         JumlahTiket, TotalHarga, MetodePembayaran, StatusPesanan)
                    VALUES (@pengunjung, @wahana, @tanggal, @jumlah, @total, @metode, @status)", conn);
                cmd.Parameters.AddWithValue("@pengunjung", (int)cmbPengunjung.SelectedValue);
                cmd.Parameters.AddWithValue("@wahana", (int)cmbWahanaPesanan.SelectedValue);
                cmd.Parameters.AddWithValue("@tanggal", dtpTanggal.Value.Date);
                cmd.Parameters.AddWithValue("@jumlah", jumlah);
                cmd.Parameters.AddWithValue("@total", harga * jumlah);
                cmd.Parameters.AddWithValue("@metode", "Cash"); // Contoh metode pembayaran
                cmd.Parameters.AddWithValue("@status", "Dipesan");
                cmd.ExecuteNonQuery();
                LoadPesanan();
            }
            finally
            {
                conn.Dispose();
            }
        }

        private decimal GetHargaWahana(int wahanaID)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT HargaTiket FROM Wahana WHERE WahanaID = @id", conn);
                cmd.Parameters.AddWithValue("@id", wahanaID);
                return (decimal)cmd.ExecuteScalar();
            }
            finally
            {
                conn.Dispose();
            }
        }
        #endregion
    }
}
