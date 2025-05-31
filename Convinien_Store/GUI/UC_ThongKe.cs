// GUI/UC_ThongKe.cs
using System;
using System.Data;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;

namespace Convenience_Store_Management.GUI
{
    public partial class UC_ThongKe : UserControl
    {
        private BLHoaDonBan blHoaDonBan = new BLHoaDonBan(); // Use BLHoaDonBan for statistics

        public UC_ThongKe()
        {
            InitializeComponent();
            // Initialize ComboBoxes
            cbDoanhThu.Items.AddRange(new string[] { "Tuần", "Tháng" });
            cbLoiNhuan.Items.AddRange(new string[] { "Tuần", "Tháng" });
            cbHangHoa.Items.AddRange(new string[] { "Tuần", "Tháng" });

            // Set default selections
            cbDoanhThu.SelectedIndex = 0;
            cbLoiNhuan.SelectedIndex = 0;
            cbHangHoa.SelectedIndex = 0;
        }

        private void UC_ThongKe_Load(object sender, EventArgs e)
        {
            // Initial load of data for each tab
            LoadDoanhThuData();
            LoadLoiNhuanData();
            LoadHangHoaDaBanData();
        }

        private void LoadDoanhThuData()
        {
            string filter = cbDoanhThu.SelectedItem?.ToString();
            string error = "";
            DataSet ds = blHoaDonBan.LayDoanhThu(filter, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dgvDoanhThu.DataSource = ds.Tables[0];
                // Format total revenue column
                if (dgvDoanhThu.Columns.Contains("TongDoanhThu"))
                {
                    dgvDoanhThu.Columns["TongDoanhThu"].HeaderText = "Tổng Doanh Thu"; // Added HeaderText
                    dgvDoanhThu.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                }
                if (dgvDoanhThu.Columns.Contains("MaHoaDonBan"))
                {
                    dgvDoanhThu.Columns["MaHoaDonBan"].HeaderText = "Mã Hóa Đơn Bán"; // Added HeaderText
                }
                if (dgvDoanhThu.Columns.Contains("NgayBan"))
                {
                    dgvDoanhThu.Columns["NgayBan"].HeaderText = "Ngày Bán"; // Added HeaderText
                }
            }
            else
            {
                MessageBox.Show($"Lỗi tải doanh thu: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvDoanhThu.DataSource = null;
            }
        }

        private void LoadLoiNhuanData()
        {
            string filter = cbLoiNhuan.SelectedItem?.ToString();
            string error = "";
            DataSet ds = blHoaDonBan.LayLoiNhuan(filter, ref error); // Calls the updated method
            if (ds != null && ds.Tables.Count > 0)
            {
                dgvLoiNhuan.DataSource = ds.Tables[0];
                // Format total profit column
                if (dgvLoiNhuan.Columns.Contains("TongLoiNhuan")) // Column name changed to TongLoiNhuan
                {
                    dgvLoiNhuan.Columns["TongLoiNhuan"].HeaderText = "Tổng Lợi Nhuận"; // Set header text for clarity
                    dgvLoiNhuan.Columns["TongLoiNhuan"].DefaultCellStyle.Format = "N0";
                }
                // Set HeaderText for other columns for consistency
                if (dgvLoiNhuan.Columns.Contains("MaHoaDonBan"))
                {
                    dgvLoiNhuan.Columns["MaHoaDonBan"].HeaderText = "Mã Hóa Đơn Bán";
                }
                if (dgvLoiNhuan.Columns.Contains("NgayBan"))
                {
                    dgvLoiNhuan.Columns["NgayBan"].HeaderText = "Ngày Bán";
                }
            }
            else
            {
                MessageBox.Show($"Lỗi tải lợi nhuận: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvLoiNhuan.DataSource = null;
            }
        }

        private void LoadHangHoaDaBanData()
        {
            string filter = cbHangHoa.SelectedItem?.ToString();
            string error = "";
            DataSet ds = blHoaDonBan.LayCacMatHangDaBan(filter, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dgvHHDaBan.DataSource = ds.Tables[0];
                // Format currency columns
                if (dgvHHDaBan.Columns.Contains("TongThanhTien"))
                {
                    dgvHHDaBan.Columns["TongThanhTien"].HeaderText = "Tổng Thành Tiền";
                    dgvHHDaBan.Columns["TongThanhTien"].DefaultCellStyle.Format = "N0";
                }
                if (dgvHHDaBan.Columns.Contains("MaSanPham"))
                {
                    dgvHHDaBan.Columns["MaSanPham"].HeaderText = "Mã Sản Phẩm";
                }
                if (dgvHHDaBan.Columns.Contains("TenSP"))
                {
                    dgvHHDaBan.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                }
                if (dgvHHDaBan.Columns.Contains("TongSoLuongDaBan"))
                {
                    dgvHHDaBan.Columns["TongSoLuongDaBan"].HeaderText = "Tổng Số Lượng Đã Bán";
                }
            }
            else
            {
                MessageBox.Show($"Lỗi tải mặt hàng đã bán: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvHHDaBan.DataSource = null;
            }
        }

        // ComboBox SelectedIndexChanged events
        private void cbDoanhThu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDoanhThuData();
        }

        private void cbLoiNhuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLoiNhuanData();
        }

        private void cbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHangHoaDaBanData();
        }
    }
}