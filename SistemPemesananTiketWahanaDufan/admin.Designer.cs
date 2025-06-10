namespace TiketWahanaApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbAdmin;

        private System.Windows.Forms.Button btnRefresh;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWahana = new System.Windows.Forms.Label();
            this.lblPengunjung = new System.Windows.Forms.Label();
            this.lblPesanan = new System.Windows.Forms.Label();
            this.txtNamaWahana = new System.Windows.Forms.TextBox();
            this.txtHargaTiket = new System.Windows.Forms.TextBox();
            this.txtKapasitas = new System.Windows.Forms.TextBox();
            this.cmbTipeTiket = new System.Windows.Forms.ComboBox();
            this.btnTambahWahana = new System.Windows.Forms.Button();
            this.btnUbahWahana = new System.Windows.Forms.Button();
            this.btnHapusWahana = new System.Windows.Forms.Button();
            this.dgvWahana = new System.Windows.Forms.DataGridView();
            this.txtNamaPengunjung = new System.Windows.Forms.TextBox();
            this.txtEmailPengunjung = new System.Windows.Forms.TextBox();
            this.btnUbahPengunjung = new System.Windows.Forms.Button();
            this.btnHapusPengunjung = new System.Windows.Forms.Button();
            this.dgvPengunjung = new System.Windows.Forms.DataGridView();
            this.cmbPengunjung = new System.Windows.Forms.ComboBox();
            this.cmbWahanaPesanan = new System.Windows.Forms.ComboBox();
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.txtJumlahTiket = new System.Windows.Forms.TextBox();
            this.cmbStatusPesanan = new System.Windows.Forms.ComboBox();
            this.btnTambahPesanan = new System.Windows.Forms.Button();
            this.btnHapusPesanan = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.dgvPesanan = new System.Windows.Forms.DataGridView();
            this.cmbAdmin = new System.Windows.Forms.ComboBox();
            this.txtPasswordPengunjung = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalHarga = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvWahana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPengunjung)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesanan)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(250, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 30);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Admin Dashboard - Tiket Wahana Dufan";
            // 
            // lblWahana
            // 
            this.lblWahana.Location = new System.Drawing.Point(20, 50);
            this.lblWahana.Name = "lblWahana";
            this.lblWahana.Size = new System.Drawing.Size(150, 20);
            this.lblWahana.TabIndex = 7;
            this.lblWahana.Text = "Kelola Wahana";
            // 
            // lblPengunjung
            // 
            this.lblPengunjung.Location = new System.Drawing.Point(20, 270);
            this.lblPengunjung.Name = "lblPengunjung";
            this.lblPengunjung.Size = new System.Drawing.Size(150, 20);
            this.lblPengunjung.TabIndex = 8;
            this.lblPengunjung.Text = "Kelola Pengunjung";
            // 
            // lblPesanan
            // 
            this.lblPesanan.Location = new System.Drawing.Point(20, 490);
            this.lblPesanan.Name = "lblPesanan";
            this.lblPesanan.Size = new System.Drawing.Size(150, 20);
            this.lblPesanan.TabIndex = 9;
            this.lblPesanan.Text = "Kelola Pesanan";
            // 
            // txtNamaWahana
            // 
            this.txtNamaWahana.Location = new System.Drawing.Point(20, 80);
            this.txtNamaWahana.Name = "txtNamaWahana";
            this.txtNamaWahana.Size = new System.Drawing.Size(150, 22);
            this.txtNamaWahana.TabIndex = 10;
            this.txtNamaWahana.TextChanged += new System.EventHandler(this.txtNamaWahana_TextChanged);
            // 
            // txtHargaTiket
            // 
            this.txtHargaTiket.Location = new System.Drawing.Point(180, 80);
            this.txtHargaTiket.Name = "txtHargaTiket";
            this.txtHargaTiket.Size = new System.Drawing.Size(100, 22);
            this.txtHargaTiket.TabIndex = 11;
            // 
            // txtKapasitas
            // 
            this.txtKapasitas.Location = new System.Drawing.Point(290, 80);
            this.txtKapasitas.Name = "txtKapasitas";
            this.txtKapasitas.Size = new System.Drawing.Size(80, 22);
            this.txtKapasitas.TabIndex = 12;
            // 
            // cmbTipeTiket
            // 
            this.cmbTipeTiket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipeTiket.Items.AddRange(new object[] {
            "Reguler",
            "Fast Track"});
            this.cmbTipeTiket.Location = new System.Drawing.Point(376, 77);
            this.cmbTipeTiket.Name = "cmbTipeTiket";
            this.cmbTipeTiket.Size = new System.Drawing.Size(119, 24);
            this.cmbTipeTiket.TabIndex = 13;
            // 
            // btnTambahWahana
            // 
            this.btnTambahWahana.Location = new System.Drawing.Point(510, 78);
            this.btnTambahWahana.Name = "btnTambahWahana";
            this.btnTambahWahana.Size = new System.Drawing.Size(75, 23);
            this.btnTambahWahana.TabIndex = 14;
            this.btnTambahWahana.Text = "Tambah";
            this.btnTambahWahana.Click += new System.EventHandler(this.btnTambahWahana_Click);
            // 
            // btnUbahWahana
            // 
            this.btnUbahWahana.Location = new System.Drawing.Point(590, 78);
            this.btnUbahWahana.Name = "btnUbahWahana";
            this.btnUbahWahana.Size = new System.Drawing.Size(75, 23);
            this.btnUbahWahana.TabIndex = 15;
            this.btnUbahWahana.Text = "Ubah";
            this.btnUbahWahana.Click += new System.EventHandler(this.btnUbahWahana_Click);
            // 
            // btnHapusWahana
            // 
            this.btnHapusWahana.Location = new System.Drawing.Point(670, 78);
            this.btnHapusWahana.Name = "btnHapusWahana";
            this.btnHapusWahana.Size = new System.Drawing.Size(75, 23);
            this.btnHapusWahana.TabIndex = 16;
            this.btnHapusWahana.Text = "Hapus";
            this.btnHapusWahana.Click += new System.EventHandler(this.btnHapusWahana_Click);
            // 
            // dgvWahana
            // 
            this.dgvWahana.ColumnHeadersHeight = 29;
            this.dgvWahana.Location = new System.Drawing.Point(20, 110);
            this.dgvWahana.Name = "dgvWahana";
            this.dgvWahana.RowHeadersWidth = 51;
            this.dgvWahana.Size = new System.Drawing.Size(760, 140);
            this.dgvWahana.TabIndex = 17;
            this.dgvWahana.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWahana_CellContentClick);
            this.dgvWahana.SelectionChanged += new System.EventHandler(this.dgvWahana_SelectionChanged);
            // 
            // txtNamaPengunjung
            // 
            this.txtNamaPengunjung.Location = new System.Drawing.Point(80, 302);
            this.txtNamaPengunjung.Name = "txtNamaPengunjung";
            this.txtNamaPengunjung.Size = new System.Drawing.Size(100, 22);
            this.txtNamaPengunjung.TabIndex = 18;
            this.txtNamaPengunjung.TextChanged += new System.EventHandler(this.txtNamaPengunjung_TextChanged);
            // 
            // txtEmailPengunjung
            // 
            this.txtEmailPengunjung.Location = new System.Drawing.Point(256, 302);
            this.txtEmailPengunjung.Name = "txtEmailPengunjung";
            this.txtEmailPengunjung.Size = new System.Drawing.Size(100, 22);
            this.txtEmailPengunjung.TabIndex = 19;
            // 
            // btnUbahPengunjung
            // 
            this.btnUbahPengunjung.Location = new System.Drawing.Point(590, 299);
            this.btnUbahPengunjung.Name = "btnUbahPengunjung";
            this.btnUbahPengunjung.Size = new System.Drawing.Size(75, 23);
            this.btnUbahPengunjung.TabIndex = 20;
            this.btnUbahPengunjung.Text = "Ubah";
            this.btnUbahPengunjung.Click += new System.EventHandler(this.btnUbahPengunjung_Click);
            // 
            // btnHapusPengunjung
            // 
            this.btnHapusPengunjung.Location = new System.Drawing.Point(670, 299);
            this.btnHapusPengunjung.Name = "btnHapusPengunjung";
            this.btnHapusPengunjung.Size = new System.Drawing.Size(75, 23);
            this.btnHapusPengunjung.TabIndex = 21;
            this.btnHapusPengunjung.Text = "Hapus";
            this.btnHapusPengunjung.Click += new System.EventHandler(this.btnHapusPengunjung_Click);
            // 
            // dgvPengunjung
            // 
            this.dgvPengunjung.ColumnHeadersHeight = 29;
            this.dgvPengunjung.Location = new System.Drawing.Point(20, 330);
            this.dgvPengunjung.Name = "dgvPengunjung";
            this.dgvPengunjung.RowHeadersWidth = 51;
            this.dgvPengunjung.Size = new System.Drawing.Size(760, 140);
            this.dgvPengunjung.TabIndex = 22;
            this.dgvPengunjung.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPengunjung_CellContentClick);
            this.dgvPengunjung.SelectionChanged += new System.EventHandler(this.dgvPengunjung_SelectionChanged);
            // 
            // cmbPengunjung
            // 
            this.cmbPengunjung.Location = new System.Drawing.Point(33, 550);
            this.cmbPengunjung.Name = "cmbPengunjung";
            this.cmbPengunjung.Size = new System.Drawing.Size(121, 24);
            this.cmbPengunjung.TabIndex = 23;
            // 
            // cmbWahanaPesanan
            // 
            this.cmbWahanaPesanan.Location = new System.Drawing.Point(171, 550);
            this.cmbWahanaPesanan.Name = "cmbWahanaPesanan";
            this.cmbWahanaPesanan.Size = new System.Drawing.Size(121, 24);
            this.cmbWahanaPesanan.TabIndex = 24;
            // 
            // dtpTanggal
            // 
            this.dtpTanggal.Location = new System.Drawing.Point(545, 485);
            this.dtpTanggal.Name = "dtpTanggal";
            this.dtpTanggal.Size = new System.Drawing.Size(200, 22);
            this.dtpTanggal.TabIndex = 25;
            // 
            // txtJumlahTiket
            // 
            this.txtJumlahTiket.Location = new System.Drawing.Point(311, 548);
            this.txtJumlahTiket.Name = "txtJumlahTiket";
            this.txtJumlahTiket.Size = new System.Drawing.Size(100, 22);
            this.txtJumlahTiket.TabIndex = 26;
            // 
            // cmbStatusPesanan
            // 
            this.cmbStatusPesanan.Location = new System.Drawing.Point(649, 546);
            this.cmbStatusPesanan.Name = "cmbStatusPesanan";
            this.cmbStatusPesanan.Size = new System.Drawing.Size(121, 24);
            this.cmbStatusPesanan.TabIndex = 27;
            // 
            // btnTambahPesanan
            // 
            this.btnTambahPesanan.Location = new System.Drawing.Point(25, 726);
            this.btnTambahPesanan.Name = "btnTambahPesanan";
            this.btnTambahPesanan.Size = new System.Drawing.Size(75, 23);
            this.btnTambahPesanan.TabIndex = 28;
            this.btnTambahPesanan.Text = "Tambah";
            this.btnTambahPesanan.Click += new System.EventHandler(this.btnTambahPesanan_Click);
            // 
            // btnHapusPesanan
            // 
            this.btnHapusPesanan.Location = new System.Drawing.Point(105, 726);
            this.btnHapusPesanan.Name = "btnHapusPesanan";
            this.btnHapusPesanan.Size = new System.Drawing.Size(75, 23);
            this.btnHapusPesanan.TabIndex = 29;
            this.btnHapusPesanan.Text = "Hapus";
            this.btnHapusPesanan.Click += new System.EventHandler(this.btnHapusPesanan_Click);
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Location = new System.Drawing.Point(185, 726);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateStatus.TabIndex = 30;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // dgvPesanan
            // 
            this.dgvPesanan.ColumnHeadersHeight = 29;
            this.dgvPesanan.Location = new System.Drawing.Point(20, 580);
            this.dgvPesanan.Name = "dgvPesanan";
            this.dgvPesanan.RowHeadersWidth = 51;
            this.dgvPesanan.Size = new System.Drawing.Size(760, 140);
            this.dgvPesanan.TabIndex = 31;
            this.dgvPesanan.SelectionChanged += new System.EventHandler(this.dgvPesanan_SelectionChanged);
            // 
            // cmbAdmin
            // 
            this.cmbAdmin.FormattingEnabled = true;
            this.cmbAdmin.Location = new System.Drawing.Point(150, 50);
            this.cmbAdmin.Name = "cmbAdmin";
            this.cmbAdmin.Size = new System.Drawing.Size(200, 24);
            this.cmbAdmin.TabIndex = 5;
            // 
            // txtPasswordPengunjung
            // 
            this.txtPasswordPengunjung.Location = new System.Drawing.Point(469, 302);
            this.txtPasswordPengunjung.Name = "txtPasswordPengunjung";
            this.txtPasswordPengunjung.Size = new System.Drawing.Size(100, 22);
            this.txtPasswordPengunjung.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "Nama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 34;
            this.label2.Text = "Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 35;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 528);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "Pengunjung";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(177, 528);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 37;
            this.label5.Text = "Nama Wahana";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(308, 528);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 16);
            this.label6.TabIndex = 38;
            this.label6.Text = "Jumlah Wahana";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(682, 524);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "Status";
            // 
            // txtTotalHarga
            // 
            this.txtTotalHarga.Location = new System.Drawing.Point(469, 548);
            this.txtTotalHarga.Name = "txtTotalHarga";
            this.txtTotalHarga.Size = new System.Drawing.Size(100, 22);
            this.txtTotalHarga.TabIndex = 40;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(481, 528);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 16);
            this.label8.TabIndex = 41;
            this.label8.Text = "Total Harga";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(670, 47);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(75, 23);
            this.btnAnalyze.TabIndex = 42;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(565, 47);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(85, 24);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "Laporan";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(484, 49);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 43;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 750);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTotalHarga);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPasswordPengunjung);
            this.Controls.Add(this.cmbAdmin);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblWahana);
            this.Controls.Add(this.lblPengunjung);
            this.Controls.Add(this.lblPesanan);
            this.Controls.Add(this.txtNamaWahana);
            this.Controls.Add(this.txtHargaTiket);
            this.Controls.Add(this.txtKapasitas);
            this.Controls.Add(this.cmbTipeTiket);
            this.Controls.Add(this.btnTambahWahana);
            this.Controls.Add(this.btnUbahWahana);
            this.Controls.Add(this.btnHapusWahana);
            this.Controls.Add(this.dgvWahana);
            this.Controls.Add(this.txtNamaPengunjung);
            this.Controls.Add(this.txtEmailPengunjung);
            this.Controls.Add(this.btnUbahPengunjung);
            this.Controls.Add(this.btnHapusPengunjung);
            this.Controls.Add(this.dgvPengunjung);
            this.Controls.Add(this.cmbPengunjung);
            this.Controls.Add(this.cmbWahanaPesanan);
            this.Controls.Add(this.dtpTanggal);
            this.Controls.Add(this.txtJumlahTiket);
            this.Controls.Add(this.cmbStatusPesanan);
            this.Controls.Add(this.btnTambahPesanan);
            this.Controls.Add(this.btnHapusPesanan);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.dgvPesanan);
            this.Name = "MainForm";
            this.Text = "Admin Dashboard";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWahana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPengunjung)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesanan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWahana;
        private System.Windows.Forms.Label lblPengunjung;
        private System.Windows.Forms.Label lblPesanan;

        private System.Windows.Forms.TextBox txtNamaWahana;
        private System.Windows.Forms.TextBox txtHargaTiket;
        private System.Windows.Forms.TextBox txtKapasitas;
        private System.Windows.Forms.ComboBox cmbTipeTiket;
        private System.Windows.Forms.Button btnTambahWahana;
        private System.Windows.Forms.Button btnUbahWahana;
        private System.Windows.Forms.Button btnHapusWahana;
        private System.Windows.Forms.DataGridView dgvWahana;

        private System.Windows.Forms.TextBox txtNamaPengunjung;
        private System.Windows.Forms.TextBox txtEmailPengunjung;
        private System.Windows.Forms.Button btnUbahPengunjung;
        private System.Windows.Forms.Button btnHapusPengunjung;
        private System.Windows.Forms.DataGridView dgvPengunjung;

        private System.Windows.Forms.ComboBox cmbPengunjung;
        private System.Windows.Forms.ComboBox cmbWahanaPesanan;
        private System.Windows.Forms.DateTimePicker dtpTanggal;
        private System.Windows.Forms.TextBox txtJumlahTiket;
        private System.Windows.Forms.ComboBox cmbStatusPesanan;
        private System.Windows.Forms.Button btnTambahPesanan;
        private System.Windows.Forms.Button btnHapusPesanan;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.DataGridView dgvPesanan;
        private System.Windows.Forms.TextBox txtPasswordPengunjung;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalHarga;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnImport;
    }
}
