using System;
using System.Drawing;
using System.Windows.Forms;

namespace Convenience_Store_Management
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Thi?t l?p ch? ?? hi?n th? cho ?ng d?ng
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ch?y ?ng d?ng v?i FormLogin
            Application.Run(new FormKhachHang());
        }
    }
}
