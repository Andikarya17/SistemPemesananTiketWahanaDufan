namespace TiketWahanaApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbAdmin = new System.Windows.Forms.ComboBox();
            this.cmbPengunjung = new System.Windows.Forms.ComboBox();
            this.cmbWahanaPesanan = new System.Windows.Forms.ComboBox();
            this.cmbTipeTiket = new System.Windows.Forms.ComboBox();
            this.txtJumlahTiket = new System.Windows.Forms.TextBox();
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.dgvPesanan = new System.Windows.Forms.DataGridView();
            this.btnTambahPesanan = new System.Windows.Forms.Button();
            this.btnHapusPesanan = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesanan)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbAdmin
            // 
            this.cmbAdmin.FormattingEnabled = true;
            this.cmbAdmin.Location = new System.Drawing.Point(20, 20);
            this.cmbAdmin.Name = "cmbAdmin";
            this.cmbAdmin.Size = new System.Drawing.Size(200, 24);
            this.cmbAdmin.TabIndex = 0;
            this.cmbAdmin.Text = "Pilih Admin";
            // 
            // cmbPengunjung
            // 
            this.cmbPengunjung.FormattingEnabled = true;
            this.cmbPengunjung.Location = new System.Drawing.Point(20, 60);
            this.cmbPengunjung.Name = "cmbPengunjung";
            this.cmbPengunjung.Size = new System.Drawing.Size(200, 24);
            this.cmbPengunjung.TabIndex = 1;
            this.cmbPengunjung.Text = "Pilih Pengunjung";
            // 
            // cmbWahanaPesanan
            // 
            this.cmbWahanaPesanan.FormattingEnabled = true;
            this.cmbWahanaPesanan.Location = new System.Drawing.Point(20, 100);
            this.cmbWahanaPesanan.Name = "cmbWahanaPesanan";
            this.cmbWahanaPesanan.Size = new System.Drawing.Size(200, 24);
            this.cmbWahanaPesanan.TabIndex = 2;
            this.cmbWahanaPesanan.Text = "Pilih Wahana";
            // 
            // cmbTipeTiket
            // 
            this.cmbTipeTiket.FormattingEnabled = true;
            this.cmbTipeTiket.Location = new System.Drawing.Point(20, 140);
            this.cmbTipeTiket.Name = "cmbTipeTiket";
            this.cmbTipeTiket.Size = new System.Drawing.Size(200, 24);
            this.cmbTipeTiket.TabIndex = 3;
            this.cmbTipeTiket.Text = "Pilih Tipe Tiket";
            // 
            // txtJumlahTiket
            // 
            this.txtJumlahTiket.Location = new System.Drawing.Point(20, 180);
            this.txtJumlahTiket.Name = "txtJumlahTiket";
            this.txtJumlahTiket.Size = new System.Drawing.Size(200, 22);
            this.txtJumlahTiket.TabIndex = 4;
            // 
            // dtpTanggal
            // 
            this.dtpTanggal.Location = new System.Drawing.Point(20, 220);
            this.dtpTanggal.Name = "dtpTanggal";
            this.dtpTanggal.Size = new System.Drawing.Size(200, 22);
            this.dtpTanggal.TabIndex = 5;
            // 
            // dgvPesanan
            // 
            this.dgvPesanan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPesanan.Location = new System.Drawing.Point(250, 20);
            this.dgvPesanan.Name = "dgvPesanan";
            this.dgvPesanan.RowHeadersWidth = 51;
            this.dgvPesanan.Size = new System.Drawing.Size(500, 300);
            this.dgvPesanan.TabIndex = 6;
            // 
            // btnTambahPesanan
            // 
            this.btnTambahPesanan.Location = new System.Drawing.Point(20, 260);
            this.btnTambahPesanan.Name = "btnTambahPesanan";
            this.btnTambahPesanan.Size = new System.Drawing.Size(200, 30);
            this.btnTambahPesanan.TabIndex = 7;
            this.btnTambahPesanan.Text = "Tambah Pesanan";
            this.btnTambahPesanan.UseVisualStyleBackColor = true;
            // 
            // btnHapusPesanan
            // 
            this.btnHapusPesanan.Location = new System.Drawing.Point(20, 300);
            this.btnHapusPesanan.Name = "btnHapusPesanan";
            this.btnHapusPesanan.Size = new System.Drawing.Size(200, 30);
            this.btnHapusPesanan.TabIndex = 8;
            this.btnHapusPesanan.Text = "Hapus Pesanan";
            this.btnHapusPesanan.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(20, 340);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(200, 30);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.cmbAdmin);
            this.Controls.Add(this.cmbPengunjung);
            this.Controls.Add(this.cmbWahanaPesanan);
            this.Controls.Add(this.cmbTipeTiket);
            this.Controls.Add(this.txtJumlahTiket);
            this.Controls.Add(this.dtpTanggal);
            this.Controls.Add(this.dgvPesanan);
            this.Controls.Add(this.btnTambahPesanan);
            this.Controls.Add(this.btnHapusPesanan);
            this.Controls.Add(this.btnRefresh);
            this.Name = "MainForm";
            this.Text = "Sistem Pemesanan Tiket Wahana Dufan";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesanan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbAdmin;
        private System.Windows.Forms.ComboBox cmbPengunjung;
        private System.Windows.Forms.ComboBox cmbWahanaPesanan;
        private System.Windows.Forms.ComboBox cmbTipeTiket;
        private System.Windows.Forms.TextBox txtJumlahTiket;
        private System.Windows.Forms.DateTimePicker dtpTanggal;
        private System.Windows.Forms.DataGridView dgvPesanan;
        private System.Windows.Forms.Button btnTambahPesanan;
        private System.Windows.Forms.Button btnHapusPesanan;
        private System.Windows.Forms.Button btnRefresh;
    }
}
