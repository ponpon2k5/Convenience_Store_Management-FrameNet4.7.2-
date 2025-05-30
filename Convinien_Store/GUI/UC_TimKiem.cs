// GUI/UC_TimKiem.cs
using System;
using System.Data;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;

namespace Convenience_Store_Management.GUI
{
    public partial class UC_TimKiem : UserControl
    {
        private BLHangHoa blHangHoa = new BLHangHoa();
        private BLHoaDonBan blHoaDonBan = new BLHoaDonBan();
        private BLKhachHang blKhachHang = new BLKhachHang();

        public UC_TimKiem()
        {
            InitializeComponent();
            // Hook up the Load event handler
            this.Load += new System.EventHandler(this.UC_TimKiem_Load);
        }

        // NEW: UC_TimKiem_Load event handler to preload data
        private void UC_TimKiem_Load(object sender, EventArgs e)
        {
            // Trigger each search button's click event with null arguments.
            // This will use the current (empty) textbox values to search,
            // effectively loading all data due to the LIKE '%%' in the BL queries.
            btnTimHH_Click(null, null); // Preload all products
            btnTimHD_Click(null, null); // Preload all invoices
            btnTimKH_Click(null, null); // Preload all customers
        }

        // Event handler for "Tìm kiếm" (Search) button on "Hàng hóa" tab
        private void btnTimHH_Click(object sender, EventArgs e)
        {
            string maHangHoa = txtMaHH.Text.Trim();
            string error = "";
            DataSet ds = blHangHoa.TimHangHoa(maHangHoa, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hàng hóa nào khớp với mã đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Format total column if exists
                if (dataGridView1.Columns.Contains("Gia"))
                {
                    dataGridView1.Columns["Gia"].DefaultCellStyle.Format = "N0";
                }
                if (dataGridView1.Columns.Contains("GiaNhap"))
                {
                    dataGridView1.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                }
            }
            else
            {
                MessageBox.Show($"Lỗi tìm kiếm hàng hóa: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        // Event handler for "Sửa" (Edit) button on "Hàng hóa" tab
        private void btnSuaHH_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng hóa để sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected product's MaSanPham
            string maSanPham = dataGridView1.SelectedRows[0].Cells["MaSanPham"].Value.ToString();
            string error = "";

            // Get current values from the selected row and initialize new variables with them
            decimal newGiaBan = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["Gia"].Value);
            decimal newGiaNhap = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["GiaNhap"].Value);
            int newSoLuong = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SoLuong"].Value);

            // Try to parse new values from textboxes if they are not empty
            if (!string.IsNullOrEmpty(txtGiaBanMoi.Text))
            {
                if (!decimal.TryParse(txtGiaBanMoi.Text, out newGiaBan) || newGiaBan <= 0)
                {
                    MessageBox.Show("Giá bán mới không hợp lệ. Vui lòng nhập một số dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtGiaNhapMoi.Text))
            {
                if (!decimal.TryParse(txtGiaNhapMoi.Text, out newGiaNhap) || newGiaNhap < 0)
                {
                    MessageBox.Show("Giá nhập mới không hợp lệ. Vui lòng nhập một số không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtSoLuongMoi.Text))
            {
                if (!int.TryParse(txtSoLuongMoi.Text, out newSoLuong) || newSoLuong < 0)
                {
                    MessageBox.Show("Số lượng mới không hợp lệ. Vui lòng nhập một số nguyên không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Perform the update
            if (blHangHoa.CapNhatHangHoa(maSanPham, newGiaBan, newGiaNhap, newSoLuong, ref error))
            {
                MessageBox.Show("Cập nhật thông tin hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Clear the MaHH textbox to ensure all products are reloaded
                txtMaHH.Clear(); // <-- ADDED THIS LINE
                // Refresh data grid view to show updated information
                btnTimHH_Click(sender, e);
                // Clear the update textboxes
                txtGiaBanMoi.Clear();
                txtGiaNhapMoi.Clear();
                txtSoLuongMoi.Clear();
            }
            else
            {
                MessageBox.Show($"Cập nhật hàng hóa thất bại: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaHH.Text = row.Cells["MaSanPham"].Value.ToString();
                txtGiaBanMoi.Text = row.Cells["Gia"].Value.ToString();
                txtGiaNhapMoi.Text = row.Cells["GiaNhap"].Value.ToString();
                txtSoLuongMoi.Text = row.Cells["SoLuong"].Value.ToString();
            }
        }


        // Event handler for "Tìm kiếm" (Search) button on "Hóa đơn" tab
        private void btnTimHD_Click(object sender, EventArgs e)
        {
            string maHoaDon = txtMaHD.Text.Trim();
            string error = "";
            DataSet ds = blHoaDonBan.TimHoaDon(maHoaDon, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dataGridView3.DataSource = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn nào khớp với mã đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Format total column if exists
                if (dataGridView3.Columns.Contains("TongCong"))
                {
                    dataGridView3.Columns["TongCong"].DefaultCellStyle.Format = "N0";
                }
            }
            else
            {
                MessageBox.Show($"Lỗi tìm kiếm hóa đơn: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView3.DataSource = null;
            }
        }

        // Event handler for "Tìm kiếm" (Search) button on "Khách hàng" tab
        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string sdtKhachHang = textBox1.Text.Trim(); // This is the textbox for SDT
            string error = "";
            DataSet ds = blKhachHang.TimKhachHang(sdtKhachHang, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dataGridView4.DataSource = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào khớp với SĐT đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Lỗi tìm kiếm khách hàng: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView4.DataSource = null;
            }
        }
    }
}