namespace TiketWahanaApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbAdmin;


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
            this.components = new System.ComponentModel.Container();
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
            this.cmbAdmin.FormattingEnabled = true;
            this.cmbAdmin.Location = new System.Drawing.Point(150, 50); // Sesuaikan lokasi
            this.cmbAdmin.Name = "cmbAdmin";
            this.cmbAdmin.Size = new System.Drawing.Size(200, 21);
            this.cmbAdmin.TabIndex = 5;
            this.Controls.Add(this.cmbAdmin);


            // Label Section
            this.lblTitle.Text = "Admin Dashboard - Tiket Wahana Dufan";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(250, 10);
            this.lblTitle.Size = new System.Drawing.Size(400, 30);

            this.lblWahana.Text = "Kelola Wahana";
            this.lblWahana.Location = new System.Drawing.Point(20, 50);
            this.lblWahana.Size = new System.Drawing.Size(150, 20);

            this.lblPengunjung.Text = "Kelola Pengunjung";
            this.lblPengunjung.Location = new System.Drawing.Point(20, 270);
            this.lblPengunjung.Size = new System.Drawing.Size(150, 20);

            this.lblPesanan.Text = "Kelola Pesanan";
            this.lblPesanan.Location = new System.Drawing.Point(20, 490);
            this.lblPesanan.Size = new System.Drawing.Size(150, 20);

            // Wahana Section
            this.txtNamaWahana.Location = new System.Drawing.Point(20, 80);
            this.txtNamaWahana.Size = new System.Drawing.Size(150, 22);
            this.txtHargaTiket.Location = new System.Drawing.Point(180, 80);
            this.txtHargaTiket.Size = new System.Drawing.Size(100, 22);
            this.txtKapasitas.Location = new System.Drawing.Point(290, 80);
            this.txtKapasitas.Size = new System.Drawing.Size(80, 22);
            this.cmbTipeTiket.Location = new System.Drawing.Point(380, 80);
            this.cmbTipeTiket.Size = new System.Drawing.Size(120, 22);
            this.btnTambahWahana.Location = new System.Drawing.Point(510, 78);
            this.btnTambahWahana.Text = "Tambah";
            this.btnUbahWahana.Location = new System.Drawing.Point(590, 78);
            this.btnUbahWahana.Text = "Ubah";
            this.btnHapusWahana.Location = new System.Drawing.Point(670, 78);
            this.btnHapusWahana.Text = "Hapus";
            this.dgvWahana.Location = new System.Drawing.Point(20, 110);
            this.dgvWahana.Size = new System.Drawing.Size(760, 140);

            // Pengunjung Section
            this.txtNamaPengunjung.Location = new System.Drawing.Point(20, 300);
            this.txtEmailPengunjung.Location = new System.Drawing.Point(180, 300);
            this.btnUbahPengunjung.Location = new System.Drawing.Point(340, 298);
            this.btnUbahPengunjung.Text = "Ubah";
            this.btnHapusPengunjung.Location = new System.Drawing.Point(420, 298);
            this.btnHapusPengunjung.Text = "Hapus";
            this.dgvPengunjung.Location = new System.Drawing.Point(20, 330);
            this.dgvPengunjung.Size = new System.Drawing.Size(760, 140);

            // Pesanan Section
            this.cmbPengunjung.Location = new System.Drawing.Point(20, 520);
            this.cmbWahanaPesanan.Location = new System.Drawing.Point(180, 520);
            this.dtpTanggal.Location = new System.Drawing.Point(340, 520);
            this.txtJumlahTiket.Location = new System.Drawing.Point(500, 520);
            this.cmbStatusPesanan.Location = new System.Drawing.Point(600, 520);
            this.btnTambahPesanan.Location = new System.Drawing.Point(20, 550);
            this.btnTambahPesanan.Text = "Tambah";
            this.btnHapusPesanan.Location = new System.Drawing.Point(100, 550);
            this.btnHapusPesanan.Text = "Hapus";
            this.btnUpdateStatus.Location = new System.Drawing.Point(180, 550);
            this.btnUpdateStatus.Text = "Update Status";
            this.dgvPesanan.Location = new System.Drawing.Point(20, 580);
            this.dgvPesanan.Size = new System.Drawing.Size(760, 140);

            // Add Controls
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, lblWahana, lblPengunjung, lblPesanan,
                txtNamaWahana, txtHargaTiket, txtKapasitas, cmbTipeTiket,
                btnTambahWahana, btnUbahWahana, btnHapusWahana, dgvWahana,
                txtNamaPengunjung, txtEmailPengunjung,
                btnUbahPengunjung, btnHapusPengunjung, dgvPengunjung,
                cmbPengunjung, cmbWahanaPesanan, dtpTanggal, txtJumlahTiket,
                cmbStatusPesanan, btnTambahPesanan, btnHapusPesanan, btnUpdateStatus, dgvPesanan
            });

            this.Text = "Admin Dashboard";
            this.ClientSize = new System.Drawing.Size(800, 750);
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
    }
}
