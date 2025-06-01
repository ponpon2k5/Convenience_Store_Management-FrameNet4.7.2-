using System;
using System.Data;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;

namespace Convenience_Store_Management.GUI
{
    public partial class UC_ThongKe : UserControl 
    {
        private BLHoaDonBan blHoaDonBan = new BLHoaDonBan();

        public UC_ThongKe()
        {
            InitializeComponent();
            cbDoanhThu.Items.AddRange(new string[] { "Tuần", "Tháng", "Tất cả" });
            cbLoiNhuan.Items.AddRange(new string[] { "Tuần", "Tháng", "Tất cả" });
            cbHangHoa.Items.AddRange(new string[] { "Tuần", "Tháng", "Tất cả" });

            cbDoanhThu.SelectedIndex = 0;
            cbLoiNhuan.SelectedIndex = 0;
            cbHangHoa.SelectedIndex = 0;
        }

        private void UC_ThongKe_Load(object sender, EventArgs e)
        {
            
            LoadDoanhThuData();
            LoadLoiNhuanData();
            LoadHangHoaDaBanData();
        }

        private void LoadDoanhThuData()
        {
            string filter;
            if (cbDoanhThu.SelectedItem != null)
            {
                filter = cbDoanhThu.SelectedItem.ToString();
            }
            else
            {
                filter = null;
            }
            string error = "";
            DataSet ds = blHoaDonBan.LayDoanhThu(filter, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dgvDoanhThu.DataSource = ds.Tables[0];
                if (dgvDoanhThu.Columns.Contains("TongDoanhThu"))
                {
                    dgvDoanhThu.Columns["TongDoanhThu"].HeaderText = "Tong Doanh Thu";
                    dgvDoanhThu.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                }
                if (dgvDoanhThu.Columns.Contains("MaHoaDonBan"))
                {
                    dgvDoanhThu.Columns["MaHoaDonBan"].HeaderText = "Ma Hoa Don Ban";
                }
                if (dgvDoanhThu.Columns.Contains("NgayBan"))
                {
                    dgvDoanhThu.Columns["NgayBan"].HeaderText = "Ngay Ban";
                }
            }
            else
            {
                MessageBox.Show($"Loi tai doanh thu: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvDoanhThu.DataSource = null;
            }
        }

        private void LoadLoiNhuanData()
        {
            string filter;

            if (cbLoiNhuan.SelectedItem != null)
            {
                filter = cbLoiNhuan.SelectedItem.ToString();
            }
            else
            {
                
                filter = null;
            }
            string error = "";
            DataSet ds = blHoaDonBan.LayLoiNhuan(filter, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dgvLoiNhuan.DataSource = ds.Tables[0]; // Gan du lieu
                // Dinh dang va dat ten cot
                if (dgvLoiNhuan.Columns.Contains("TongLoiNhuan")) 
                {
                    dgvLoiNhuan.Columns["TongLoiNhuan"].HeaderText = "Tong Loi Nhuan";
                    dgvLoiNhuan.Columns["TongLoiNhuan"].DefaultCellStyle.Format = "N0"; 
                }
                if (dgvLoiNhuan.Columns.Contains("MaHoaDonBan"))
                {
                    dgvLoiNhuan.Columns["MaHoaDonBan"].HeaderText = "Ma Hoa Don Ban";
                }
                if (dgvLoiNhuan.Columns.Contains("NgayBan"))
                {
                    dgvLoiNhuan.Columns["NgayBan"].HeaderText = "Ngay Ban";
                }
            }
            else
            {
                MessageBox.Show($"Loi tai loi nhuan: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvLoiNhuan.DataSource = null;
            }
        }

        private void LoadHangHoaDaBanData()
        {
            string filter;
            if (cbHangHoa.SelectedItem != null)
            {
                filter = cbHangHoa.SelectedItem.ToString();
            }
            else
            {

                filter = null;
            }
            string error = "";
            DataSet ds = blHoaDonBan.LayCacMatHangDaBan(filter, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dgvHHDaBan.DataSource = ds.Tables[0];
                if (dgvHHDaBan.Columns.Contains("TongThanhTien"))
                {
                    dgvHHDaBan.Columns["TongThanhTien"].HeaderText = "Tong Thanh Tien";
                    dgvHHDaBan.Columns["TongThanhTien"].DefaultCellStyle.Format = "N0"; 
                }
                if (dgvHHDaBan.Columns.Contains("MaSanPham"))
                {
                    dgvHHDaBan.Columns["MaSanPham"].HeaderText = "Ma San Pham";
                }
                if (dgvHHDaBan.Columns.Contains("TenSP"))
                {
                    dgvHHDaBan.Columns["TenSP"].HeaderText = "Ten San Pham";
                }
                if (dgvHHDaBan.Columns.Contains("TongSoLuongDaBan"))
                {
                    dgvHHDaBan.Columns["TongSoLuongDaBan"].HeaderText = "Tong So Luong Da Ban";
                }
            }
            else
            {
                MessageBox.Show($"Loi tai mat hang da ban: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvHHDaBan.DataSource = null;
            }
        }

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