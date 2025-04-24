using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    public partial class FormLogin : Form
    {
        private string connectionString = "Data Source=Andikarya\\ANDIKAARYA;Initial Catalog=TiketwahanDufan2;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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
                        this.Hide();
                        MainForm formAdmin = new MainForm();
                        formAdmin.Show();
                    }
                    else
                    {
                        MessageBox.Show("Email atau Password salah.");
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
            formGuest.Show();
        }
    }
}
