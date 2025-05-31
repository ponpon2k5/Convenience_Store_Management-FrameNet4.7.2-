// GUI/UC_HoaDon.cs
using System;
using System.Data;
using System.Drawing;
using System.Linq; // Cần cho LINQ (FirstOrDefault, AsEnumerable)
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Convenience_Store_Management.GUI
{
    public partial class UC_HoaDon : UserControl
    {
        private BLHoaDonBan blHoaDonBan = new BLHoaDonBan();
        private BLHangHoa blHangHoa = new BLHangHoa(); // Thêm BLHangHoa để lấy thông tin sản phẩm
        private DataTable currentInvoiceDetailsTable; // Bảng để lưu trữ chi tiết hóa đơn tạm thời

        public UC_HoaDon()
        {
            InitializeComponent();
            SetupInvoiceDetailsTable(); // Khởi tạo cấu trúc DataTable cho dgvInvoiceDetails
        }

        private void UC_HoaDon_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho ngày bán
            dtpNgayBan.Value = DateTime.Now;

            // Optional: Tạo mã hóa đơn mới ngay khi load hoặc khi thêm sản phẩm đầu tiên
            // UNCOMMENT THE FOLLOWING LINE TO AUTOMATICALLY GENERATE INVOICE ID
            txtMaHD.Text = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss"); //
        }

        private void SetupInvoiceDetailsTable()
        {
            currentInvoiceDetailsTable = new DataTable();
            currentInvoiceDetailsTable.Columns.Add("MaSanPham", typeof(string));
            currentInvoiceDetailsTable.Columns.Add("TenSP", typeof(string));
            currentInvoiceDetailsTable.Columns.Add("SoLuong", typeof(int));
            currentInvoiceDetailsTable.Columns.Add("GiaBan", typeof(decimal));
            currentInvoiceDetailsTable.Columns.Add("ThanhTien", typeof(decimal));

            dgvInvoiceDetails.DataSource = currentInvoiceDetailsTable;

            // Đặt header text cho DataGridView
            dgvInvoiceDetails.AutoGenerateColumns = false; // Tắt tự động tạo cột để tự kiểm soát

            if (!dgvInvoiceDetails.Columns.Contains("MaSanPham")) dgvInvoiceDetails.Columns.Add("MaSanPham", "Mã SP");
            dgvInvoiceDetails.Columns["MaSanPham"].DataPropertyName = "MaSanPham";

            if (!dgvInvoiceDetails.Columns.Contains("TenSP")) dgvInvoiceDetails.Columns.Add("TenSP", "Tên Sản Phẩm");
            dgvInvoiceDetails.Columns["TenSP"].DataPropertyName = "TenSP";

            if (!dgvInvoiceDetails.Columns.Contains("SoLuong")) dgvInvoiceDetails.Columns.Add("SoLuong", "Số Lượng");
            dgvInvoiceDetails.Columns["SoLuong"].DataPropertyName = "SoLuong";

            if (!dgvInvoiceDetails.Columns.Contains("GiaBan")) dgvInvoiceDetails.Columns.Add("GiaBan", "Giá Bán");
            dgvInvoiceDetails.Columns["GiaBan"].DataPropertyName = "GiaBan";
            dgvInvoiceDetails.Columns["GiaBan"].DefaultCellStyle.Format = "N0";

            if (!dgvInvoiceDetails.Columns.Contains("ThanhTien")) dgvInvoiceDetails.Columns.Add("ThanhTien", "Thành Tiền");
            dgvInvoiceDetails.Columns["ThanhTien"].DataPropertyName = "ThanhTien";
            dgvInvoiceDetails.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            dgvInvoiceDetails.ReadOnly = true;
            dgvInvoiceDetails.AllowUserToAddRows = false;
        }

        private void btnThemHD_Click(object sender, EventArgs e) // Đã đổi tên từ button1_Click
        {
            // 1. Lấy dữ liệu từ UI controls
            string maHoaDon = txtMaHD.Text.Trim();
            string maSanPham = txtMaSP.Text.Trim();
            string soLuongStr = txtSoLuong.Text.Trim();

            // 2. Xác thực dữ liệu đầu vào
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập Mã hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(maSanPham))
            {
                MessageBox.Show("Vui lòng nhập Mã sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong;
            if (!int.TryParse(soLuongStr, out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là một số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Lấy thông tin sản phẩm từ BLHangHoa
            string error = "";
            DataSet productInfoDs = blHangHoa.TimHangHoa(maSanPham, ref error);

            if (productInfoDs == null || productInfoDs.Tables.Count == 0 || productInfoDs.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy sản phẩm với mã: '{maSanPham}'. Vui lòng kiểm tra lại. Lỗi: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow productRow = productInfoDs.Tables[0].Rows[0];
            string tenSP = productRow["TenSP"].ToString();
            decimal giaBan = Convert.ToDecimal(productRow["Gia"]);
            int soLuongTonKho = Convert.ToInt32(productRow["SoLuong"]);

            // 4. Kiểm tra số lượng tồn kho
            if (soLuong > soLuongTonKho)
            {
                MessageBox.Show($"Sản phẩm '{tenSP}' (Mã: {maSanPham}) chỉ còn {soLuongTonKho} sản phẩm. Không đủ số lượng bạn yêu cầu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal thanhTien = soLuong * giaBan;

            // 5. Thêm/Cập nhật vào DataTable tạm thời
            DataRow existingRow = currentInvoiceDetailsTable.AsEnumerable()
                                                         .FirstOrDefault(row => row.Field<string>("MaSanPham") == maSanPham);

            if (existingRow != null)
            {
                int currentSoLuong = existingRow.Field<int>("SoLuong");
                // Kiểm tra lại số lượng tồn kho nếu thêm vào sản phẩm đã có
                if ((currentSoLuong + soLuong) > soLuongTonKho)
                {
                    MessageBox.Show($"Sản phẩm '{tenSP}' (Mã: {maSanPham}) chỉ còn {soLuongTonKho} sản phẩm. Tổng số lượng trong hóa đơn tạm thời sẽ vượt quá tồn kho.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existingRow["SoLuong"] = currentSoLuong + soLuong;
                existingRow["ThanhTien"] = (currentSoLuong + soLuong) * giaBan;
            }
            else // Sản phẩm chưa có trong hóa đơn tạm thời
            {
                DataRow newRow = currentInvoiceDetailsTable.NewRow();
                newRow["MaSanPham"] = maSanPham;
                newRow["TenSP"] = tenSP;
                newRow["SoLuong"] = soLuong;
                newRow["GiaBan"] = giaBan;
                newRow["ThanhTien"] = thanhTien;
                currentInvoiceDetailsTable.Rows.Add(newRow);
            }

            dgvInvoiceDetails.Refresh(); // Làm mới DataGridView

            // Xóa các trường nhập liệu cho sản phẩm
            txtMaSP.Clear();
            txtSoLuong.Clear();
            txtMaSP.Focus(); // Di chuyển con trỏ về trường mã sản phẩm
        }

        private void btnXuLyHD_Click(object sender, EventArgs e) // Xử lý hoàn tất hóa đơn
        {
            string maHoaDon = txtMaHD.Text.Trim();
            string maNhanVien = txtMaNV.Text.Trim();
            DateTime ngayBan = dtpNgayBan.Value;

            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập Mã hóa đơn để hoàn tất.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên để hoàn tất hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (currentInvoiceDetailsTable.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn hiện tại chưa có sản phẩm nào. Vui lòng thêm sản phẩm trước khi hoàn tất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string error = "";
            bool success = false;

            // Bắt đầu giao dịch lớn cho toàn bộ quá trình xử lý hóa đơn
            blHoaDonBan.db.BeginTransaction();
            try
            {
                // Bước 1: Thêm hóa đơn bán chính
                // SDTKhachHang is explicitly null for employee-generated invoices via this UC.
                string sdtKhachHang = null;
                success = blHoaDonBan.ThemHoaDonBan(maHoaDon, maNhanVien, sdtKhachHang, ngayBan, ref error);
                if (!success)
                {
                    blHoaDonBan.db.RollbackTransaction();
                    MessageBox.Show($"Hoàn tất hóa đơn thất bại (Thêm HĐB): {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Bước 2: Thêm từng chi tiết sản phẩm vào CHI_TIET_BAN và cập nhật số lượng tồn kho
                foreach (DataRow row in currentInvoiceDetailsTable.Rows)
                {
                    string itemMaSP = row.Field<string>("MaSanPham");
                    int itemSoLuong = row.Field<int>("SoLuong");
                    decimal itemGiaBan = row.Field<decimal>("GiaBan");
                    decimal itemThanhTien = row.Field<decimal>("ThanhTien");

                    // Thêm chi tiết bán
                    success = blHoaDonBan.ThemChiTietBan(maHoaDon, itemMaSP, itemSoLuong, itemGiaBan, itemThanhTien, ref error);
                    if (!success)
                    {
                        blHoaDonBan.db.RollbackTransaction();
                        MessageBox.Show($"Hoàn tất hóa đơn thất bại (Thêm CTB cho SP {itemMaSP}): {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Cập nhật số lượng hàng hóa (giảm tồn kho)
                    success = blHoaDonBan.CapNhatSoLuongHangHoa(itemMaSP, -itemSoLuong, ref error);
                    if (!success)
                    {
                        blHoaDonBan.db.RollbackTransaction();
                        MessageBox.Show($"Hoàn tất hóa đơn thất bại (Cập nhật tồn kho SP {itemMaSP}): {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Nếu tất cả thành công, commit giao dịch
                blHoaDonBan.db.CommitTransaction();
                MessageBox.Show("Hoàn tất hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sau khi hoàn tất, xóa dữ liệu trên UI và làm mới bảng tạm
                ClearInvoiceData();
            }
            catch (Exception ex)
            {
                blHoaDonBan.db.RollbackTransaction();
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn trong quá trình hoàn tất hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInvoiceData()
        {
            txtMaHD.Clear();
            txtMaSP.Clear();
            txtSoLuong.Clear();
            txtMaNV.Clear();
            dtpNgayBan.Value = DateTime.Now;
            currentInvoiceDetailsTable.Clear(); // Xóa tất cả các hàng khỏi bảng tạm
            dgvInvoiceDetails.Refresh(); // Làm mới DataGridView
            txtMaHD.Focus();
        }
    }
}