using System;
using System.Windows.Forms;

namespace TiketWahanaApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Tampilkan form login terlebih dahulu
            FormLogin loginForm = new FormLogin();
            DialogResult result = loginForm.ShowDialog();

            // Jika login sebagai admin berhasil, buka MainForm sebagai form utama
            if (result == DialogResult.OK && loginForm.IsAdminLogin)
            {
                Application.Run(new MainForm());
            }
            else
            {
                // Jika login gagal atau ditutup, keluar dari aplikasi
                Application.Exit();
            }
        }
    }
}
