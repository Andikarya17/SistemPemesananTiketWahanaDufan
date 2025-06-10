using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class PreviewForm : Form
    {
        private readonly string connString = "Data Source=Andikarya\\ANDIKAARYA;Initial Catalog=TiketwahanDufan2;Integrated Security=True";
        private readonly string pengunjungEmail;

        public PreviewForm(string email)
        {
            InitializeComponent();
            pengunjungEmail = email;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            LoadPesanan();
        }

        private void LoadPesanan()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string getIdQuery = "SELECT PengunjungID FROM Pengunjung WHERE Email = @Email";
                    SqlCommand getIdCmd = new SqlCommand(getIdQuery, conn);
                    getIdCmd.Parameters.AddWithValue("@Email", pengunjungEmail);
                    object result = getIdCmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("Data pengunjung tidak ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    int pengunjungID = Convert.ToInt32(result);

                    // Tampilkan Nama & Email
                    string infoQuery = "SELECT Nama, Email FROM Pengunjung WHERE PengunjungID = @PengunjungID";
                    SqlCommand infoCmd = new SqlCommand(infoQuery, conn);
                    infoCmd.Parameters.AddWithValue("@PengunjungID", pengunjungID);
                    SqlDataReader reader = infoCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblNamaPengunjung.Text = "Nama: " + reader["Nama"].ToString();
                        lblEmailPengunjung.Text = "Email: " + reader["Email"].ToString();
                    }
                    reader.Close();

                    // Load tabel pesanan
                    string pesananQuery = @"
                        SELECT w.NamaWahana, w.TipeTiket, p.TanggalKunjungan, 
                               p.JumlahTiket, p.TotalHarga, p.StatusPesanan
                        FROM Pesanan p
                        INNER JOIN Wahana w ON p.WahanaID = w.WahanaID
                        WHERE p.PengunjungID = @PengunjungID";

                    SqlDataAdapter da = new SqlDataAdapter(pesananQuery, conn);
                    da.SelectCommand.Parameters.AddWithValue("@PengunjungID", pengunjungID);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPreview.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data pesanan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
