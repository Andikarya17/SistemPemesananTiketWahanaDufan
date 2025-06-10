namespace TiketWahanaApp
{
    partial class PreviewForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNamaPengunjung;
        private System.Windows.Forms.Label lblEmailPengunjung;

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
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblNamaPengunjung = new System.Windows.Forms.Label();
            this.lblEmailPengunjung = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNamaPengunjung
            // 
            this.lblNamaPengunjung.Location = new System.Drawing.Point(12, 9);
            this.lblNamaPengunjung.Size = new System.Drawing.Size(400, 20);
            this.lblNamaPengunjung.Text = "Nama:";
            // 
            // lblEmailPengunjung
            // 
            this.lblEmailPengunjung.Location = new System.Drawing.Point(12, 30);
            this.lblEmailPengunjung.Size = new System.Drawing.Size(400, 20);
            this.lblEmailPengunjung.Text = "Email:";
            // 
            // dgvPreview
            // 
            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.AllowUserToDeleteRows = false;
            this.dgvPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Location = new System.Drawing.Point(12, 60);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.RowHeadersWidth = 51;
            this.dgvPreview.Size = new System.Drawing.Size(660, 280);
            this.dgvPreview.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(597, 350);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Tutup";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // PreviewForm
            // 
            this.ClientSize = new System.Drawing.Size(684, 391);
            this.Controls.Add(this.lblNamaPengunjung);
            this.Controls.Add(this.lblEmailPengunjung);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Riwayat Pemesanan Anda";
            this.Load += new System.EventHandler(this.PreviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
