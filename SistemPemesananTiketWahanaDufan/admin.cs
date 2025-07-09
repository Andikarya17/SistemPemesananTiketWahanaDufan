using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SistemPemesananTiketWahanaDufan;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Caching;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class MainForm : Form
    {
        private readonly koneksi konn = new koneksi();
        private readonly string connString;

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };

        private const string CacheKeyWahana = "DataWahana";

        public MainForm()
        {
            InitializeComponent();
            connString = konn.connectionString();
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

        #region
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
                    SqlCommand cmd = new SqlCommand("GetWahanaList", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    _cache.Add(CacheKeyWahana, dt, _policy);
                }
            }

            dgvWahana.DataSource = dt;
        }

        private void btnTambahWahana_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaWahana.Text))
            {
                MessageBox.Show("Nama Wahana tidak boleh kosong.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHargaTiket.Text))
            {
                MessageBox.Show("Harga Tiket tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtHargaTiket.Text, out decimal hargaTiket) || hargaTiket < 0)
            {
                MessageBox.Show("Harga Tiket harus berupa angka positif.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKapasitas.Text) || !int.TryParse(txtKapasitas.Text, out int kapasitas) || kapasitas <= 0)
            {
                MessageBox.Show("Kapasitas harian harus diisi dan harus lebih dari 0.");
                return;
            }


            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("AddWahana", conn, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@NamaWahana", txtNamaWahana.Text.Trim());
                    cmd.Parameters.AddWithValue("@TipeTiket", cmbTipeTiket.Text.Trim());
                    cmd.Parameters.AddWithValue("@HargaTiket", decimal.Parse(txtHargaTiket.Text));
                    cmd.Parameters.AddWithValue("@KapasitasHarian", int.Parse(txtKapasitas.Text));

                    if (cmbAdmin.SelectedValue != null)
                        cmd.Parameters.AddWithValue("@AdminID", (int)cmbAdmin.SelectedValue);
                    else
                        cmd.Parameters.AddWithValue("@AdminID", DBNull.Value);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    _cache.Remove(CacheKeyWahana);
                    MessageBox.Show("Wahana berhasil ditambahkan.");
                    LoadWahana();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal menambah wahana: " + ex.Message);
            }
        }

        private void btnUbahWahana_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaWahana.Text))
            {
                MessageBox.Show("Nama Wahana tidak boleh kosong.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHargaTiket.Text))
            {
                MessageBox.Show("Harga Tiket tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtHargaTiket.Text, out decimal hargaTiket) || hargaTiket < 0)
            {
                MessageBox.Show("Harga Tiket harus berupa angka positif.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKapasitas.Text) || !int.TryParse(txtKapasitas.Text, out int kapasitas) || kapasitas <= 0)
            {
                MessageBox.Show("Kapasitas harian harus diisi dan harus lebih dari 0.");
                return;
            }


            SqlTransaction transaction = null;
            try
            {
                if (dgvWahana.SelectedRows.Count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        SqlCommand cmd = new SqlCommand("UpdateWahana", conn, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        int id = Convert.ToInt32(dgvWahana.SelectedRows[0].Cells["WahanaID"].Value);
                        cmd.Parameters.AddWithValue("@WahanaID", id);
                        cmd.Parameters.AddWithValue("@NamaWahana", txtNamaWahana.Text.Trim());
                        cmd.Parameters.AddWithValue("@TipeTiket", cmbTipeTiket.Text.Trim());
                        cmd.Parameters.AddWithValue("@HargaTiket", decimal.Parse(txtHargaTiket.Text));
                        cmd.Parameters.AddWithValue("@KapasitasHarian", int.Parse(txtKapasitas.Text));

                        if (cmbAdmin.SelectedValue != null)
                            cmd.Parameters.AddWithValue("@AdminID", (int)cmbAdmin.SelectedValue);
                        else
                            cmd.Parameters.AddWithValue("@AdminID", DBNull.Value);

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        _cache.Remove(CacheKeyWahana);
                        MessageBox.Show("Wahana berhasil diubah.");
                        LoadWahana();
                    }
                }
                else
                {
                    MessageBox.Show("Pilih data yang ingin diubah.");
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal ubah wahana: " + ex.Message);
            }
        }

        private void btnHapusWahana_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            try
            {
                if (dgvWahana.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Hapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(connString))
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();

                            SqlCommand cmd = new SqlCommand("DeleteWahana", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            int id = Convert.ToInt32(dgvWahana.SelectedRows[0].Cells["WahanaID"].Value);
                            cmd.Parameters.AddWithValue("@WahanaID", id);

                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                            _cache.Remove(CacheKeyWahana);
                            MessageBox.Show("Wahana berhasil dihapus.");
                            LoadWahana();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pilih data yang ingin dihapus.");
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal hapus wahana: " + ex.Message);
            }
        }

        private void LoadTipeTiket()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT TipeTiket FROM Wahana", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbTipeTiket.Items.Clear();
                cmbTipeTiket.Items.Add("Fast Track");
                cmbTipeTiket.Items.Add("Reguler");
                cmbTipeTiket.SelectedIndex = 0;
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
                cmbAdmin.DisplayMember = "Nama";
                cmbAdmin.ValueMember = "AdminID";
            }
        }
        #endregion

        #region — CRUD Pengunjung —
        private void LoadPengunjung()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("GetPengunjungList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPengunjung.DataSource = dt;
            }
        }

        private void btnUbahPengunjung_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaPengunjung.Text))
            {
                MessageBox.Show("Nama tidak boleh kosong.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmailPengunjung.Text))
            {
                MessageBox.Show("Email tidak boleh kosong.");
                return;
            }

            SqlTransaction transaction = null;
            try
            {
                if (dgvPengunjung.SelectedRows.Count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        SqlCommand cmd = new SqlCommand("UpdatePengunjung", conn, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        int id = Convert.ToInt32(dgvPengunjung.SelectedRows[0].Cells["PengunjungID"].Value);
                        cmd.Parameters.AddWithValue("@PengunjungID", id);
                        cmd.Parameters.AddWithValue("@Nama", txtNamaPengunjung.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmailPengunjung.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPasswordPengunjung.Text.Trim());

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        MessageBox.Show("Pengunjung berhasil diubah.");
                        LoadPengunjung();
                    }
                }
                else
                {
                    MessageBox.Show("Pilih pengunjung yang ingin diubah.");
                }
            }
            catch (Exception ex)
            {
                if (transaction != null && transaction.Connection != null)
                {
                    transaction.Rollback();
                }
                MessageBox.Show("Gagal ubah pengunjung: " + ex.Message);
            }

        }

        private void btnHapusPengunjung_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            try
            {
                if (dgvPengunjung.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Hapus pengunjung ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(connString))
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();

                            SqlCommand cmd = new SqlCommand("DeletePengunjung", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            int id = Convert.ToInt32(dgvPengunjung.SelectedRows[0].Cells["PengunjungID"].Value);
                            cmd.Parameters.AddWithValue("@PengunjungID", id);

                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                            MessageBox.Show("Pengunjung berhasil dihapus.");
                            LoadPengunjung();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pilih pengunjung yang ingin dihapus.");
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal hapus pengunjung: " + ex.Message);
            }
        }
        #endregion

        #region — CRUD Pesanan —
        private void LoadPesanan()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("GetPesananList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPesanan.DataSource = dt;
            }
            foreach (DataGridViewColumn col in dgvPesanan.Columns)
            {
                Console.WriteLine(col.Name); // atau tampilkan pakai MessageBox
            }

        }

        private decimal GetHargaWahana(int wahanaID)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetWahanaPriceById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@WahanaID", wahanaID);
                object result = cmd.ExecuteScalar();
                return (result != null && result != DBNull.Value) ? (decimal)result : 0;
            }
        }

        private void btnTambahPesanan_Click(object sender, EventArgs e)
        {
            // 1. Validasi Nama Wahana
            if (cmbWahanaPesanan.SelectedIndex == -1 || cmbWahanaPesanan.SelectedValue == null)
            {
                MessageBox.Show("Nama wahana harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validasi Jumlah Tiket
            if (!int.TryParse(txtJumlahTiket.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah tiket harus diisi dan lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Validasi Pengunjung
            if (cmbPengunjung.SelectedIndex == -1 || cmbPengunjung.SelectedValue == null)
            {
                MessageBox.Show("Nama pengunjung harus dipilih.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("AddPesanan", conn, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    
                    decimal harga = GetHargaWahana((int)cmbWahanaPesanan.SelectedValue);

                    cmd.Parameters.AddWithValue("@PengunjungID", (int)cmbPengunjung.SelectedValue);
                    cmd.Parameters.AddWithValue("@WahanaID", (int)cmbWahanaPesanan.SelectedValue);
                    cmd.Parameters.AddWithValue("@TanggalKunjungan", dtpTanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@JumlahTiket", jumlah);
                    cmd.Parameters.AddWithValue("@TotalHarga", harga * jumlah);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    MessageBox.Show("Pesanan berhasil ditambahkan.");
                    LoadPesanan();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal tambah pesanan: " + ex.Message);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (cmbPengunjung.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmbPengunjung.Text))
            {
                MessageBox.Show("Nama pengunjung harus dipilih.");
                return;
            }

            SqlTransaction transaction = null;
            try
            {
                if (dgvPesanan.SelectedRows.Count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        SqlCommand cmd = new SqlCommand("UpdatePesananStatus", conn, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        int id = Convert.ToInt32(dgvPesanan.SelectedRows[0].Cells["PesananID"].Value);
                        cmd.Parameters.AddWithValue("@PesananID", id);
                        cmd.Parameters.AddWithValue("@StatusPesanan", cmbStatusPesanan.Text.Trim());

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        MessageBox.Show("Status pesanan diperbarui.");
                        LoadPesanan();
                    }
                }
                else
                {
                    MessageBox.Show("Pilih data pesanan.");
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal update status: " + ex.Message);
            }
        }

        private void btnHapusPesanan_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            try
            {
                if (dgvPesanan.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Hapus pesanan ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(connString))
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();

                            SqlCommand cmd = new SqlCommand("DeletePesanan", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            int id = Convert.ToInt32(dgvPesanan.SelectedRows[0].Cells["PesananID"].Value);
                            cmd.Parameters.AddWithValue("@PesananID", id);

                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                            MessageBox.Show("Pesanan dihapus.");
                            LoadPesanan();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pilih data yang ingin dihapus.");
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal hapus pesanan: " + ex.Message);
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


        private void txtNamaWahana_TextChanged(object sender, EventArgs e) { }
        private void dgvWahana_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvWahana_SelectionChanged(object sender, EventArgs e) 
        {
            if (dgvWahana.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvWahana.SelectedRows[0];
                txtNamaWahana.Text = row.Cells["NamaWahana"].Value?.ToString() ?? "";
                cmbTipeTiket.Text = row.Cells["TipeTiket"].Value?.ToString() ?? "";
                txtHargaTiket.Text = row.Cells["HargaTiket"].Value?.ToString() ?? "";
                txtKapasitas.Text = row.Cells["KapasitasHarian"].Value?.ToString() ?? "";

                if (cmbAdmin.DataSource != null)
                {
                    if (row.Cells["AdminID"].Value != DBNull.Value && row.Cells["AdminID"].Value != null)
                    {
                        cmbAdmin.SelectedValue = row.Cells["AdminID"].Value;
                    }
                    else
                    {
                        cmbAdmin.SelectedIndex = -1;
                    }
                }
            }
        }
        private void txtNamaPengunjung_TextChanged(object sender, EventArgs e) { }
        private void dgvPengunjung_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvPengunjung_SelectionChanged(object sender, EventArgs e) 
        {
            if (dgvPengunjung.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPengunjung.SelectedRows[0];
                txtNamaPengunjung.Text = row.Cells["Nama"].Value?.ToString() ?? "";
                txtEmailPengunjung.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtPasswordPengunjung.Text = "";
            }
        }
        private void dgvPesanan_SelectionChanged(object sender, EventArgs e) 
        {
            if (dgvPesanan.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPesanan.SelectedRows[0];

                cmbPengunjung.Text = row.Cells["Pengunjung"].Value?.ToString() ?? "";
                cmbWahanaPesanan.Text = row.Cells["NamaWahana"].Value?.ToString() ?? "";

                if (row.Cells["TanggalKunjungan"].Value != DBNull.Value && row.Cells["TanggalKunjungan"].Value != null)
                {
                    dtpTanggal.Value = Convert.ToDateTime(row.Cells["TanggalKunjungan"].Value);
                }
                else
                {
                    dtpTanggal.Value = DateTime.Now;
                }

                txtJumlahTiket.Text = row.Cells["JumlahTiket"].Value?.ToString() ?? "";
                txtTotalHarga.Text = row.Cells["TotalHarga"].Value?.ToString() ?? "";
                cmbStatusPesanan.Text = row.Cells["StatusPesanan"].Value?.ToString() ?? "";
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
            Form1 laporan = new Form1();
            laporan.Show();
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

                    IRow headerRow = sheet.GetRow(0);
                    foreach (var cell in headerRow.Cells)
                    {
                        dt.Columns.Add(cell.ToString());
                    }

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

                    MessageBox.Show($"Berhasil membaca {dt.Rows.Count} baris dari Excel.");

                    PreviewData previewForm = new PreviewData(dt);
                    previewForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error membaca file Excel: " + ex.Message);
            }
        }

    }
}
#endregion