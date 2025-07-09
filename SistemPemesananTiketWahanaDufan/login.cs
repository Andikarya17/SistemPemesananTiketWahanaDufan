using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class FormLogin : Form
    {
        // ✅ Koneksi dari koneksi.cs
        private readonly koneksi konn = new koneksi();
        private readonly string connString;

        // Tambahkan flag untuk mendeteksi login sebagai admin
        public bool IsAdminLogin = false;

        public FormLogin()
        {
            InitializeComponent();
            connString = konn.connectionString(); // Inisialisasi koneksi
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT COUNT(*) FROM Admin WHERE Nama = @nama AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Login berhasil sebagai Admin.");
                        IsAdminLogin = true;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password salah.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnGuestLogin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Login sebagai pengunjung.");
            this.Hide();
            FormPemesanan formGuest = new FormPemesanan();
            formGuest.ShowDialog();
            this.Show();
        }
    }
}
