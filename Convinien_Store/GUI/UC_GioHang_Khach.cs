using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;
using Convenience_Store_Management.Helper;

namespace Convenience_Store_Management.GUI
{
    public partial class UC_GioHang_Khach : UserControl 
    {
        private DataTable cartTable = new DataTable();
        private BLHangHoa blHangHoa = new BLHangHoa();
        private BLHoaDonBan blHoaDonBan = new BLHoaDonBan();

        // Ham khoi tao UserControl
        public UC_GioHang_Khach()
        {
            InitializeComponent();
            SetupCartTable();      
            UpdateTongTienLabel(); // Cap nhat tong tien ban dau (0 VND)
        }

        // Thiet lap cau truc cho DataTable
        private void SetupCartTable()
        {
            // Dinh nghia cac cot cho DataTable cua gio hang
            cartTable.Columns.Add("MaSanPham", typeof(string));
            cartTable.Columns.Add("TenSP", typeof(string));
            cartTable.Columns.Add("Gia", typeof(decimal));
            cartTable.Columns.Add("SoLuong", typeof(int));
            cartTable.Columns.Add("ThanhTien", typeof(decimal)); 

            dgvGioHang.DataSource = cartTable;

            dgvGioHang.AutoGenerateColumns = false; // Tat tu dong tao cot

            // Them va cau hinh tung cot (MaSanPham, TenSP, Gia, SoLuong, ThanhTien)
            if (!dgvGioHang.Columns.Contains("MaSanPham")) dgvGioHang.Columns.Add("MaSanPham", "Ma SP");
            dgvGioHang.Columns["MaSanPham"].DataPropertyName = "MaSanPham";

            if (!dgvGioHang.Columns.Contains("TenSP")) dgvGioHang.Columns.Add("TenSP", "Ten San Pham");
            dgvGioHang.Columns["TenSP"].DataPropertyName = "TenSP";

            if (!dgvGioHang.Columns.Contains("Gia")) dgvGioHang.Columns.Add("Gia", "Gia");
            dgvGioHang.Columns["Gia"].DataPropertyName = "Gia";
            dgvGioHang.Columns["Gia"].DefaultCellStyle.Format = "N0";

            if (!dgvGioHang.Columns.Contains("SoLuong")) dgvGioHang.Columns.Add("SoLuong", "So Luong");
            dgvGioHang.Columns["SoLuong"].DataPropertyName = "SoLuong";

            if (!dgvGioHang.Columns.Contains("ThanhTien")) dgvGioHang.Columns.Add("ThanhTien", "Thanh Tien");
            dgvGioHang.Columns["ThanhTien"].DataPropertyName = "ThanhTien";
            dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            // Ngan chinh sua truc tiep
            dgvGioHang.ReadOnly = true;
            dgvGioHang.AllowUserToAddRows = false;
        }

        // Tinh toan va cap nhat Label hien thi tong tien cua gio hang
        private void UpdateTongTienLabel()
        {
            decimal tongTien = 0;
            // Duyet qua tung hang trong cartTable
            foreach (DataRow row in cartTable.Rows)
            {
                tongTien += row.Field<decimal>("ThanhTien");
            }
            TongTien_label.Text = $"Tong tien: {tongTien:N0} VND";
        }

        public void AddItemToCart(object sender, string maSanPham, string tenSP, int soLuong, decimal gia)
        {
            DataRow existingRow = null;

            foreach (DataRow row in cartTable.Rows)
            {
                if (row.Field<string>("MaSanPham") == maSanPham) 
                {
                    existingRow = row;
                    break; //neu tim thay thi thoat khoi vong lap
                }
            }

            if (existingRow != null)
            {
                // San pham da co: Tang so luong va cap nhat lai ThanhTien
                int currentSoLuong = existingRow.Field<int>("SoLuong"); 
                decimal currentGia = existingRow.Field<decimal>("Gia");

                existingRow["SoLuong"] = currentSoLuong + soLuong;
                existingRow["ThanhTien"] = (currentSoLuong + soLuong) * currentGia;
            }
            else
            {
                // San pham chua co: Them hang moi vao DataTable
                DataRow newRow = cartTable.NewRow();
                newRow["MaSanPham"] = maSanPham;
                newRow["TenSP"] = tenSP;
                newRow["Gia"] = gia;
                newRow["SoLuong"] = soLuong;
                newRow["ThanhTien"] = soLuong * gia;
                cartTable.Rows.Add(newRow);
            }
            UpdateTongTienLabel(); // Cap nhat lai tong tien 
        }

        private void RefreshCartDisplay()
        {
            dgvGioHang.Refresh(); 
            UpdateTongTienLabel(); 
        }

        // Xoa gia hang
        private void btnXoaGioHang_Click(object sender, EventArgs e)
        {
            // Kiem tra xem co hang nao dang duoc chon  khong
            if (dgvGioHang.SelectedRows.Count > 0)
            {
                int rowIndex = dgvGioHang.SelectedRows[0].Index; // Lay chi so cua hang duoc chon

                // Dam bao chi so hang hop le
                if (rowIndex >= 0 && rowIndex < cartTable.Rows.Count)
                {
                    DataRow rowToRemove = cartTable.Rows[rowIndex]; // Lay DataRow tuong ung trong DataTable
                    string tenSP = rowToRemove.Field<string>("TenSP");

                    cartTable.Rows.Remove(rowToRemove);

                    MessageBox.Show($"Da xoa '{tenSP}' khoi gio hang", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateTongTienLabel(); 
                }
            }
            else
            {
                MessageBox.Show("Vui long chon mot san pham de xoa khoi gio hang", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Thanh toan gio hang
        private void button1_Click(object sender, EventArgs e)
        {
            // Kiem tra gio hang rong
            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Gio hang cua ban dang trong Vui long them san pham de thanh toan", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string errorMessage = "";
            // Lay thong tin can thiet cho viec tao hoa don
            string maNhanVien = null; 
            string sdtKhachHang = SessionManager.CurrentLoggedInCustomerSdt;
            string maHoaDonBanMoi = "HDB" + DateTime.Now.ToString("yyyyMMddHHmmss"); // Tao ma hoa don moi theo thoi gian

            // them hoa don, chi tiet hoa don va cap nhat so luong ton kho trong CSDL
            if (blHoaDonBan.ProcessSaleTransaction(maHoaDonBanMoi, maNhanVien, sdtKhachHang, cartTable, ref errorMessage))
            {
                MessageBox.Show("Thanh toan thanh cong Gio hang da duoc xoa", "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cartTable.Clear(); 
                UpdateTongTienLabel();

                if (this.ParentForm is FormKhachHang mainForm)
                {
                    mainForm.RefreshHangHoaUC(); 
                }
            }
            else
            {
                MessageBox.Show("Co loi xay ra trong qua trinh thanh toan: " + errorMessage, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}