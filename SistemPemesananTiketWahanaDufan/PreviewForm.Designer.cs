namespace TiketWahanaApp
{
    partial class PreviewData
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Button btnSimpan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.btnSimpan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.SuspendLayout();

            // 
            // dgvPreview
            // 
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Location = new System.Drawing.Point(12, 12);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.Size = new System.Drawing.Size(560, 300);
            this.dgvPreview.TabIndex = 0;

            // 
            // btnSimpan
            // 
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.Location = new System.Drawing.Point(480, 320);
            this.btnSimpan.Size = new System.Drawing.Size(80, 30);
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);

            // 
            // PreviewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.dgvPreview);
            this.Controls.Add(this.btnSimpan);
            this.Name = "PreviewData";
            this.Text = "Preview Data Kunjungan";
            this.Load += new System.EventHandler(this.PreviewData_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.ResumeLayout(false);
        }

    }
}

