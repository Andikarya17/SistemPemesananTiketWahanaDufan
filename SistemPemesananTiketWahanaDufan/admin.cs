using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SistemPemesananTiketWahanaDufan;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Runtime.Caching;
using System.Windows.Forms;
using System;



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

            // Memanfaatkan cache
            if (_cache.Contains(CacheKeyWahana))
            {
                dt = _cache.Get(CacheKeyWahana) as DataTable;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    // Memanggil stored procedure GetWahanaList
                    SqlCommand cmd = new SqlCommand("GetWahanaList", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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
                txtNamaWahana.Text = row.Cells["NamaWahana"].Value?.ToString() ?? "";
                cmbTipeTiket.Text = row.Cells["TipeTiket"].Value?.ToString() ?? "";
                txtHargaTiket.Text = row.Cells["HargaTiket"].Value?.ToString() ?? "";
                txtKapasitas.Text = row.Cells["KapasitasHarian"].Value?.ToString() ?? "";

                // Pastikan cmbAdmin sudah terisi datanya sebelum mencoba SelectedValue
                if (cmbAdmin.DataSource != null)
                {
                    // Cek apakah AdminID tidak null atau DBNull
                    if (row.Cells["AdminID"].Value != DBNull.Value && row.Cells["AdminID"].Value != null)
                    {
                        cmbAdmin.SelectedValue = row.Cells["AdminID"].Value;
                    }
                    else
                    {
                        cmbAdmin.SelectedIndex = -1; // Kosongkan pilihan jika AdminID null
                    }
                }
            }
        }



        private void btnTambahWahana_Click(object sender, EventArgs e)
        {
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
                    {
                        cmd.Parameters.AddWithValue("@AdminID", (int)cmbAdmin.SelectedValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AdminID", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    _cache.Remove(CacheKeyWahana);
                    MessageBox.Show("Wahana berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWahana();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal menambah wahana: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnUbahWahana_Click(object sender, EventArgs e)
        {
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

                        int wahanaID = Convert.ToInt32(dgvWahana.SelectedRows[0].Cells["WahanaID"].Value);

                        cmd.Parameters.AddWithValue("@WahanaID", wahanaID);
                        cmd.Parameters.AddWithValue("@NamaWahana", txtNamaWahana.Text.Trim());
                        cmd.Parameters.AddWithValue("@TipeTiket", cmbTipeTiket.Text.Trim());
                        cmd.Parameters.AddWithValue("@HargaTiket", decimal.Parse(txtHargaTiket.Text));
                        cmd.Parameters.AddWithValue("@KapasitasHarian", int.Parse(txtKapasitas.Text));

                        if (cmbAdmin.SelectedValue != null)
                        {
                            cmd.Parameters.AddWithValue("@AdminID", (int)cmbAdmin.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AdminID", DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        _cache.Remove(CacheKeyWahana);
                        MessageBox.Show("Wahana berhasil diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadWahana();
                    }
                }
                else
                {
                    MessageBox.Show("Pilih wahana yang ingin diubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal mengubah wahana: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SqlTransaction transaction = null;
            try
            {
                if (dgvWahana.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus wahana ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(connString))
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();

                            SqlCommand cmd = new SqlCommand("DeleteWahana", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            int wahanaID = Convert.ToInt32(dgvWahana.SelectedRows[0].Cells["WahanaID"].Value);
                            cmd.Parameters.AddWithValue("@WahanaID", wahanaID);

                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                            _cache.Remove(CacheKeyWahana);
                            MessageBox.Show("Wahana berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadWahana();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pilih wahana yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal menghapus wahana: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SqlCommand cmd = new SqlCommand("GetPengunjungList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

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
                txtNamaPengunjung.Text = row.Cells["Nama"].Value?.ToString() ?? "";
                txtEmailPengunjung.Text = row.Cells["Email"].Value?.ToString() ?? "";
                // KEAMANAN KRITIS: JANGAN MENAMPILKAN PASSWORD DI UI ADMIN
                // Selalu kosongkan textbox password untuk mencegah kebocoran atau pembaruan yang tidak sengaja.
                txtPasswordPengunjung.Text = "";
            }
        }


        private void btnUbahPengunjung_Click(object sender, EventArgs e)
        {
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

                        int pengunjungID = Convert.ToInt32(dgvPengunjung.SelectedRows[0].Cells["PengunjungID"].Value);

                        cmd.Parameters.AddWithValue("@PengunjungID", pengunjungID);
                        cmd.Parameters.AddWithValue("@Nama", txtNamaPengunjung.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmailPengunjung.Text.Trim());

                        // Menangani password: Jika textbox kosong, SP akan mempertahankan password lama.
                        // Jika ada input, SP akan memperbarui.
                        cmd.Parameters.AddWithValue("@Password", txtPasswordPengunjung.Text.Trim());

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        MessageBox.Show("Data pengunjung berhasil diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPengunjung();
                    }
                }
                else
                {
                    MessageBox.Show("Pilih pengunjung yang ingin diubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal mengubah pengunjung: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnHapusPengunjung_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            try
            {
                if (dgvPengunjung.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus pengunjung ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(connString))
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();

                            SqlCommand cmd = new SqlCommand("DeletePengunjung", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            int pengunjungID = Convert.ToInt32(dgvPengunjung.SelectedRows[0].Cells["PengunjungID"].Value);
                            cmd.Parameters.AddWithValue("@PengunjungID", pengunjungID);

                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                            MessageBox.Show("Pengunjung berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPengunjung();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pilih pengunjung yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal menghapus pengunjung: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region — Kelola Tiket / Pesanan —
        private void LoadPesanan()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("GetPesananList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

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



        private void btnTambahPesanan_Click(object sender, EventArgs e)
        {
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

                    int jumlah = int.Parse(txtJumlahTiket.Text);
                    // Panggil stored procedure GetWahanaPriceById
                    decimal harga = GetHargaWahana((int)cmbWahanaPesanan.SelectedValue);

                    cmd.Parameters.AddWithValue("@PengunjungID", (int)cmbPengunjung.SelectedValue);
                    cmd.Parameters.AddWithValue("@WahanaID", (int)cmbWahanaPesanan.SelectedValue);
                    cmd.Parameters.AddWithValue("@TanggalKunjungan", dtpTanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@JumlahTiket", jumlah);
                    cmd.Parameters.AddWithValue("@TotalHarga", harga * jumlah);
                    // Parameter berikut sudah memiliki default di SP AddPesanan,
                    // jadi tidak perlu di-set di sini jika ingin menggunakan default SP.
                    // Jika ingin set manual, tambahkan:
                    // cmd.Parameters.AddWithValue("@MetodePembayaran", "Tunai"); 
                    // cmd.Parameters.AddWithValue("@StatusPesanan", "Dipesan"); 

                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    MessageBox.Show("Pesanan berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPesanan();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal menambah pesanan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal GetHargaWahana(int wahanaID)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // Memanggil stored procedure GetWahanaPriceById
                SqlCommand cmd = new SqlCommand("GetWahanaPriceById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@WahanaID", wahanaID);
                object result = cmd.ExecuteScalar();
                return (result != null && result != DBNull.Value) ? (decimal)result : 0m;
            }
        }




        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
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

                        int pesananID = Convert.ToInt32(dgvPesanan.SelectedRows[0].Cells["PesananID"].Value);

                        cmd.Parameters.AddWithValue("@PesananID", pesananID);
                        cmd.Parameters.AddWithValue("@StatusPesanan", cmbStatusPesanan.Text.Trim());

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        MessageBox.Show("Status pesanan berhasil diupdate.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPesanan();
                    }
                }
                else
                {
                    MessageBox.Show("Pilih pesanan yang ingin diupdate statusnya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal mengupdate status pesanan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnHapusPesanan_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            try
            {
                if (dgvPesanan.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus pesanan ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(connString))
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();

                            SqlCommand cmd = new SqlCommand("DeletePesanan", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            int pesananID = Convert.ToInt32(dgvPesanan.SelectedRows[0].Cells["PesananID"].Value);
                            cmd.Parameters.AddWithValue("@PesananID", pesananID);

                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                            MessageBox.Show("Pesanan berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPesanan();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pilih pesanan yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Gagal menghapus pesanan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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