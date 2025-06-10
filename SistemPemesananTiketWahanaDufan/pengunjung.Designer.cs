namespace TiketWahanaApp
{
    partial class FormPemesanan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblWahana;
        private System.Windows.Forms.Label lblTipeTiket;
        private System.Windows.Forms.Label lblTanggal;
        private System.Windows.Forms.Label lblJumlahTiket;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.ComboBox cmbWahana;
        private System.Windows.Forms.ComboBox cmbTipeTiket;
        private System.Windows.Forms.DateTimePicker dtpTanggal;
        private System.Windows.Forms.TextBox txtJumlahTiket;
        private System.Windows.Forms.Button btnSubmit;

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
            this.lblNama = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblWahana = new System.Windows.Forms.Label();
            this.lblTipeTiket = new System.Windows.Forms.Label();
            this.lblTanggal = new System.Windows.Forms.Label();
            this.lblJumlahTiket = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.cmbWahana = new System.Windows.Forms.ComboBox();
            this.cmbTipeTiket = new System.Windows.Forms.ComboBox();
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.txtJumlahTiket = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Label dan Textbox Nama
            this.lblNama.Text = "Nama";
            this.lblNama.Location = new System.Drawing.Point(30, 25);
            this.txtNama.Location = new System.Drawing.Point(150, 22);
            this.txtNama.Size = new System.Drawing.Size(200, 22);

            // Label dan Textbox Email
            this.lblEmail.Text = "Email";
            this.lblEmail.Location = new System.Drawing.Point(30, 60);
            this.txtEmail.Location = new System.Drawing.Point(150, 57);
            this.txtEmail.Size = new System.Drawing.Size(200, 22);

            // Label dan ComboBox Wahana
            this.lblWahana.Text = "Pilihan Wahana";
            this.lblWahana.Location = new System.Drawing.Point(30, 95);
            this.cmbWahana.Location = new System.Drawing.Point(150, 92);
            this.cmbWahana.Size = new System.Drawing.Size(200, 24);
            this.cmbWahana.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Label dan ComboBox Tipe Tiket
            this.lblTipeTiket.Text = "Tipe Tiket";
            this.lblTipeTiket.Location = new System.Drawing.Point(30, 130);
            this.cmbTipeTiket.Location = new System.Drawing.Point(150, 127);
            this.cmbTipeTiket.Size = new System.Drawing.Size(200, 24);
            this.cmbTipeTiket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipeTiket.Items.AddRange(new object[] { "Reguler", "Fast Track" });

            // Label dan DateTimePicker Tanggal
            this.lblTanggal.Text = "Tanggal";
            this.lblTanggal.Location = new System.Drawing.Point(30, 165);
            this.dtpTanggal.Location = new System.Drawing.Point(150, 162);
            this.dtpTanggal.Size = new System.Drawing.Size(200, 22);

            // Label dan Textbox Jumlah Tiket
            this.lblJumlahTiket.Text = "Jumlah Tiket";
            this.lblJumlahTiket.Location = new System.Drawing.Point(30, 200);
            this.txtJumlahTiket.Location = new System.Drawing.Point(150, 197);
            this.txtJumlahTiket.Size = new System.Drawing.Size(200, 22);

            // Button Submit
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Location = new System.Drawing.Point(150, 235);
            this.btnSubmit.Size = new System.Drawing.Size(90, 30);
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            // FormPemesanan
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lblNama);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblWahana);
            this.Controls.Add(this.cmbWahana);
            this.Controls.Add(this.lblTipeTiket);
            this.Controls.Add(this.cmbTipeTiket);
            this.Controls.Add(this.lblTanggal);
            this.Controls.Add(this.dtpTanggal);
            this.Controls.Add(this.lblJumlahTiket);
            this.Controls.Add(this.txtJumlahTiket);
            this.Controls.Add(this.btnSubmit);
            this.Name = "FormPemesanan";
            this.Text = "Form Pemesanan Tiket";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
