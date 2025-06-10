using SistemPemesananTiketWahanaDufan;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;


namespace TiketWahanaApp
{
    public partial class MainForm : Form
    {
        private string connString = @"Data Source=ANDIKARYA\ANDIKAARYA;Initial Catalog=TiketwahanDufan2;User ID=sa;Password=Rodamas17;";

        
        private readonly MemoryCache _cache = MemoryCache.Default;

        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) // TTL 5 menit
        };

        private const string CacheKeyWahana = "DataWahana";

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
            LoadTipeTiket();
            LoadStatusPesanan();
        }

        #region — CRUD Wahana —
        private void LoadWahana()
        {
            DataTable dt;

            if (_cache.Contains(CacheKeyWahana))
            {
                dt = _cache.Get(CacheKeyWahana) as DataTable;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Wahana", conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    _cache.Add(CacheKeyWahana, dt, _policy);
                }
            }

            dgvWahana.DataSource = dt;
        }

        private void dgvWahana_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvWahana.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvWahana.SelectedRows[0];
                txtNamaWahana.Text = row.Cells["NamaWahana"].Value.ToString();
                cmbTipeTiket.Text = row.Cells["TipeTiket"].Value.ToString();
                txtHargaTiket.Text = row.Cells["HargaTiket"].Value.ToString();
                txtKapasitas.Text = row.Cells["KapasitasHarian"].Value.ToString();
                cmbAdmin.SelectedValue = row.Cells["AdminID"].Value;
            }
        }


        private void btnTambahWahana_Click(object sender, EventArgs e)
        {
            try
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
                    
                    _cache.Remove(CacheKeyWahana);  // Invalidate cache
                    
                    LoadWahana();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah wahana: " + ex.Message);
            }
        }


        private void btnUbahWahana_Click(object sender, EventArgs e)
        {
            try
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
                        _cache.Remove(CacheKeyWahana);  // Invalidate cache
                        
                        LoadWahana();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengubah wahana: " + ex.Message);
            }
        }

        private void LoadTipeTiket()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT TipeTiket FROM Wahana", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbTipeTiket.Items.Clear(); // Bersihkan item sebelumnya
                cmbTipeTiket.Items.Add("Fast Track");
                cmbTipeTiket.Items.Add("Reguler");
                cmbTipeTiket.SelectedIndex = 0;
            }
        }


        private void btnHapusWahana_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvWahana.SelectedRows.Count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Wahana WHERE WahanaID=@id", conn);
                        cmd.Parameters.AddWithValue("@id", dgvWahana.SelectedRows[0].Cells["WahanaID"].Value);
                        cmd.ExecuteNonQuery();
                        
                        _cache.Remove(CacheKeyWahana);  // Invalidate cache
                        
                        LoadWahana();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus wahana: " + ex.Message);
            }
        }


        private void LoadAdmin()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT AdminID, Nama FROM Admin", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbAdmin.DataSource = dt;
                cmbAdmin.DisplayMember = "NamaAdmin";
                cmbAdmin.ValueMember = "AdminID";
            }
        }
        private void LoadStatusPesanan()
        {
            cmbStatusPesanan.Items.Clear();
            cmbStatusPesanan.Items.Add("Dipesan");
            cmbStatusPesanan.Items.Add("Dibatalkan");
            cmbStatusPesanan.Items.Add("Selesai");
            cmbStatusPesanan.SelectedIndex = 0;
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

        private void dgvPengunjung_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPengunjung.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPengunjung.SelectedRows[0];
                txtNamaPengunjung.Text = row.Cells["Nama"].Value.ToString();
                txtEmailPengunjung.Text = row.Cells["Email"].Value.ToString();
                txtPasswordPengunjung.Text = row.Cells["Password"].Value.ToString();
            }
        }

        private void btnUbahPengunjung_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengubah pengunjung: " + ex.Message);
            }
        }


        private void btnHapusPengunjung_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus pengunjung: " + ex.Message);
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

        private void dgvPesanan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPesanan.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPesanan.SelectedRows[0];

                // Misalnya kontrolnya seperti ini:
                cmbPengunjung.Text = row.Cells["Pengunjung"].Value.ToString();
                cmbWahanaPesanan.Text = row.Cells["NamaWahana"].Value.ToString();
                dtpTanggal.Value = Convert.ToDateTime(row.Cells["TanggalKunjungan"].Value);
                txtJumlahTiket.Text = row.Cells["JumlahTiket"].Value.ToString();
                txtTotalHarga.Text = row.Cells["TotalHarga"].Value.ToString();
                cmbStatusPesanan.Text = row.Cells["StatusPesanan"].Value.ToString();
            }
        }


        private void btnTambahPesanan_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah pesanan: " + ex.Message);
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengupdate status pesanan: " + ex.Message);
            }
        }


        private void btnHapusPesanan_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus pesanan: " + ex.Message);
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery = @"
        SELECT 
            p.PesananID, 
            q.Nama AS Pengunjung, 
            w.NamaWahana, 
            p.TanggalKunjungan, 
            p.JumlahTiket, 
            p.TotalHarga, 
            p.StatusPesanan 
        FROM Pesanan p 
        JOIN Pengunjung q ON p.PengunjungID = q.PengunjungID 
        JOIN Wahana w ON p.WahanaID = w.WahanaID 
        WHERE p.StatusPesanan = 'Dipesan'";
                AnalyzeQuery(sqlQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menganalisis query: " + ex.Message);
            }
        }


        private void AnalyzeQuery(string sqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.InfoMessage += (s, e) =>
                {
                    MessageBox.Show(e.Message, "STATISTICS INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };

                conn.Open();
                string wrapped = $@"
            SET STATISTICS IO ON;
            SET STATISTICS TIME ON;
            {sqlQuery};
            SET STATISTICS IO OFF;
            SET STATISTICS TIME OFF;";

                using (SqlCommand cmd = new SqlCommand(wrapped, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            Form1 laporan = new Form1(); // Form1 adalah form yang punya ReportViewer
            laporan.Show(); // Tampilkan laporan
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var openFile = new OpenFileDialog())
            {
                openFile.Filter = "Excel Files|*.xlsx;*.xlsm";
                if (openFile.ShowDialog() == DialogResult.OK)
                    PreviewData(openFile.FileName);
            }
        }
        private void PreviewData(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    DataTable dt = new DataTable();

                    // Ambil baris pertama sebagai header
                    IRow headerRow = sheet.GetRow(0);
                    foreach (var cell in headerRow.Cells)
                    {
                        dt.Columns.Add(cell.ToString());
                    }

                    // Ambil semua baris setelah header
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow dataRow = sheet.GetRow(i);
                        if (dataRow == null) continue;

                        DataRow newRow = dt.NewRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = dataRow.GetCell(j);
                            newRow[j] = cell != null ? cell.ToString() : string.Empty;
                        }
                        dt.Rows.Add(newRow);
                    }

                    // Tambahkan debug jumlah baris
                    MessageBox.Show($"Berhasil membaca {dt.Rows.Count} baris dari Excel.");

                    // Kirim ke PreviewForm
                    PreviewData previewForm = new PreviewData(dt);
                    previewForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error membaca file Excel: " + ex.Message);
            }
        }







        #endregion

        private void txtNamaPengunjung_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvPengunjung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvWahana_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNamaWahana_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var openFile = new OpenFileDialog())
            {
                openFile.Filter = "Excel Files|*.xlsx;*.xlsm";
                if (openFile.ShowDialog() == DialogResult.OK)
                    PreviewData(openFile.FileName);
            }
        }
    }
}