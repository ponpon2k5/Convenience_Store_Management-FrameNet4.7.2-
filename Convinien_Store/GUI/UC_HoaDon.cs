using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;
using Convenience_Store_Management.Helper; 

namespace Convenience_Store_Management.GUI
{
    public partial class UC_HoaDon : UserControl 
    {
        private BLHoaDonBan blHoaDonBan = new BLHoaDonBan();
        private BLHangHoa blHangHoa = new BLHangHoa();
        private DataTable currentInvoiceDetailsTable;

        
        public UC_HoaDon()
        {
            InitializeComponent();
            SetupInvoiceDetailsTable(); 
        }

        private void UC_HoaDon_Load(object sender, EventArgs e)
        {
            dtpNgayBan.Value = DateTime.Now;

            // Tu dong tao ma hoa don
            txtMaHD.Text = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // Tu dong dien MaNhanVien neu co nhan vien dang dang nhap
            if (!string.IsNullOrEmpty(SessionManager.CurrentLoggedInEmployeeId))
            {
                txtMaNV.Text = SessionManager.CurrentLoggedInEmployeeId;
                txtMaNV.Enabled = false;
            }
            else
            {
                MessageBox.Show("Khong tim thay ma nhan vien dang nhap Vui long dang nhap hoac nhap thu cong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Enabled = true;
            }
        }
        private void SetupInvoiceDetailsTable()
        {
            currentInvoiceDetailsTable = new DataTable();
            // Dinh nghia cac cot cho bang chi tiet hoa don tam thoi
            currentInvoiceDetailsTable.Columns.Add("MaSanPham", typeof(string));
            currentInvoiceDetailsTable.Columns.Add("TenSP", typeof(string));
            currentInvoiceDetailsTable.Columns.Add("SoLuong", typeof(int));
            currentInvoiceDetailsTable.Columns.Add("GiaBan", typeof(decimal));
            currentInvoiceDetailsTable.Columns.Add("ThanhTien", typeof(decimal));

            dgvInvoiceDetails.DataSource = currentInvoiceDetailsTable;

            dgvInvoiceDetails.AutoGenerateColumns = false; 

            if (!dgvInvoiceDetails.Columns.Contains("MaSanPham")) dgvInvoiceDetails.Columns.Add("MaSanPham", "Ma SP");
            dgvInvoiceDetails.Columns["MaSanPham"].DataPropertyName = "MaSanPham";

            if (!dgvInvoiceDetails.Columns.Contains("TenSP")) dgvInvoiceDetails.Columns.Add("TenSP", "Ten San Pham");
            dgvInvoiceDetails.Columns["TenSP"].DataPropertyName = "TenSP";

            if (!dgvInvoiceDetails.Columns.Contains("SoLuong")) dgvInvoiceDetails.Columns.Add("SoLuong", "So Luong");
            dgvInvoiceDetails.Columns["SoLuong"].DataPropertyName = "SoLuong";

            if (!dgvInvoiceDetails.Columns.Contains("GiaBan")) dgvInvoiceDetails.Columns.Add("GiaBan", "Gia Ban");
            dgvInvoiceDetails.Columns["GiaBan"].DataPropertyName = "GiaBan";
            dgvInvoiceDetails.Columns["GiaBan"].DefaultCellStyle.Format = "N0";

            if (!dgvInvoiceDetails.Columns.Contains("ThanhTien")) dgvInvoiceDetails.Columns.Add("ThanhTien", "Thanh Tien");
            dgvInvoiceDetails.Columns["ThanhTien"].DataPropertyName = "ThanhTien";
            dgvInvoiceDetails.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            dgvInvoiceDetails.ReadOnly = true;
            dgvInvoiceDetails.AllowUserToAddRows = false;
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            // 1 Lay du lieu tu 
            string maHoaDon = txtMaHD.Text.Trim(); 
            string maSanPham = txtMaSP.Text.Trim();
            string soLuongStr = txtSoLuong.Text.Trim();

            // 2 Kiem tra tinh hop le
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui long nhap Ma hoa don", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(maSanPham))
            {
                MessageBox.Show("Vui long nhap Ma san pham", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong;
            if (!int.TryParse(soLuongStr, out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("So luong phai la mot so nguyen duong", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3 Lay thong tin chi tiet 
            string error = "";
            DataSet productInfoDs = blHangHoa.TimHangHoa(maSanPham, ref error);

            if (productInfoDs == null || productInfoDs.Tables.Count == 0 || productInfoDs.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show($"Khong tim thay san pham voi ma: '{maSanPham}' Vui long kiem tra lai Loi: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow productRow = productInfoDs.Tables[0].Rows[0]; 
            string tenSP = productRow["TenSP"].ToString();
            decimal giaBan = Convert.ToDecimal(productRow["Gia"]); 
            int soLuongTonKho = Convert.ToInt32(productRow["SoLuong"]);

            // 4 Kiem tra so luong
            if (soLuong > soLuongTonKho)
            {
                MessageBox.Show($"San pham '{tenSP}' (Ma: {maSanPham}) chi con {soLuongTonKho} san pham Khong du so luong ban yeu cau", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal thanhTien = soLuong * giaBan; 

            // 5 Them hoac cap nhat san pham vao bang chi tiet hoa don tam thoi 
            DataRow existingRow = null;
            // Kiemm tra ton tai
            foreach (DataRow dr in currentInvoiceDetailsTable.Rows)
            {
                if (dr.Field<string>("MaSanPham") == maSanPham)
                {
                    existingRow = dr;
                    break;
                }
            }

            if (existingRow != null) 
            {
                int currentSoLuong = existingRow.Field<int>("SoLuong");
                // Kiem tra lai so luong ton kho
                if ((currentSoLuong + soLuong) > soLuongTonKho)
                {
                    MessageBox.Show($"San pham '{tenSP}' (Ma: {maSanPham}) chi con {soLuongTonKho} san pham Tong so luong trong hoa don tam thoi se vuot qua ton kho", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existingRow["SoLuong"] = currentSoLuong + soLuong;
                existingRow["ThanhTien"] = (currentSoLuong + soLuong) * giaBan;
            }
            else 
            {
                DataRow newRow = currentInvoiceDetailsTable.NewRow();
                newRow["MaSanPham"] = maSanPham;
                newRow["TenSP"] = tenSP;
                newRow["SoLuong"] = soLuong;
                newRow["GiaBan"] = giaBan;
                newRow["ThanhTien"] = thanhTien;
                currentInvoiceDetailsTable.Rows.Add(newRow);
            }

            dgvInvoiceDetails.Refresh();

            
            txtMaSP.Clear();
            txtSoLuong.Clear();
            txtMaSP.Focus(); 
        }

        private void btnXuLyHD_Click(object sender, EventArgs e)
        {
            // Lay thong tin chung cua hoa don
            string maHoaDon = txtMaHD.Text.Trim();
            string maNhanVien = txtMaNV.Text.Trim();
            DateTime ngayBan = dtpNgayBan.Value;

            // Kiem tra cac dieu kien can thiet truoc khi xu ly
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui long nhap Ma hoa don de hoan tat", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui long nhap Ma nhan vien de hoan tat hoa don", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (currentInvoiceDetailsTable.Rows.Count == 0) // Kiem tra hoa don co san pham nao khong
            {
                MessageBox.Show("Hoa don hien tai chua co san pham nao Vui long them san pham truoc khi hoan tat", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string error = "";
            bool success = false;

            blHoaDonBan.db.BeginTransaction();
            try
            {
                // Buoc 1: Them thong tin hoa don chinh vao bang HOA_DON_BAN
                string sdtKhachHang = null; // Hoa don nay do nhan vien tao, khong co SDT khach hang truc tiep
                success = blHoaDonBan.ThemHoaDonBan(maHoaDon, maNhanVien, sdtKhachHang, ngayBan, ref error);
                if (!success)
                {
                    blHoaDonBan.db.RollbackTransaction(); 
                    MessageBox.Show($"Hoan tat hoa don that bai (Them HDB): {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buoc 2: Duyet qua tung san pham trong hoa don tam thoi
                foreach (DataRow row in currentInvoiceDetailsTable.Rows)
                {
                    string itemMaSP = row.Field<string>("MaSanPham");
                    int itemSoLuong = row.Field<int>("SoLuong");
                    decimal itemGiaBan = row.Field<decimal>("GiaBan");
                    decimal itemThanhTien = row.Field<decimal>("ThanhTien");

                    success = blHoaDonBan.ThemChiTietBan(maHoaDon, itemMaSP, itemSoLuong, itemGiaBan, itemThanhTien, ref error);
                    if (!success)
                    {
                        blHoaDonBan.db.RollbackTransaction();
                        MessageBox.Show($"Hoan tat hoa don that bai (Them CTB cho SP {itemMaSP}): {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Cap nhat (giam) so luong ton kho cua san pham
                    success = blHoaDonBan.CapNhatSoLuongHangHoa(itemMaSP, -itemSoLuong, ref error); // So luong am de tru di
                    if (!success)
                    {
                        blHoaDonBan.db.RollbackTransaction(); 
                        MessageBox.Show($"Hoan tat hoa don that bai (Cap nhat ton kho SP {itemMaSP}): {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                blHoaDonBan.db.CommitTransaction();
                MessageBox.Show("Hoan tat hoa don thanh cong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInvoiceData();
            }
            catch (Exception ex) 
            {
                blHoaDonBan.db.RollbackTransaction(); 
                MessageBox.Show($"Da xay ra loi khong mong muon trong qua trinh hoan tat hoa don: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xoa sach du lieu tren cac truong nhap
        private void ClearInvoiceData()
        {
            txtMaHD.Clear();
            txtMaSP.Clear();
            txtSoLuong.Clear();
            if (string.IsNullOrEmpty(SessionManager.CurrentLoggedInEmployeeId))
            {
                txtMaNV.Clear();
            }
            dtpNgayBan.Value = DateTime.Now;
            currentInvoiceDetailsTable.Clear();
            dgvInvoiceDetails.Refresh();
            txtMaHD.Focus(); 
            txtMaHD.Text = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}