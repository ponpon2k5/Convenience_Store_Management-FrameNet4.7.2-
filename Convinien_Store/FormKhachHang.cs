using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convenience_Store_Management.GUI;

namespace Convenience_Store_Management
{
    public partial class FormKhachHang : Form
    {
        private UC_GioHang_Khach ucGioHang;
        private UC_HangHoa_Khach ucHangHoa;

        public FormKhachHang()
        {
            InitializeComponent();
            InitializeUserControls();
        }

        private void InitializeUserControls()
        {
            ucGioHang = new UC_GioHang_Khach();
            ucHangHoa = new UC_HangHoa_Khach();

            ucHangHoa.OnAddToCart += ucGioHang.AddItemToCart;

            LoadSubForm(ucHangHoa);
        }

        private void LoadSubForm(UserControl subForm)
        {
            pnlKhachHang.Controls.Clear();
            subForm.Dock = DockStyle.Fill;
            pnlKhachHang.Controls.Add(subForm);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnThanhVien_Click(object sender, EventArgs e)
        {
            LoadSubForm(new GUI.UC_ThanhVien_Khach());
        }

        private void btnGioHang_Click(object sender, EventArgs e)
        {
            LoadSubForm(ucGioHang);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            LoadSubForm(ucHangHoa);
        }

        public void RefreshHangHoaUC()
        {
            ucHangHoa.LoadHangHoaData();
        }
    }
}