// GUI/UC_SanPham.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;

namespace Convenience_Store_Management.GUI
{
    public partial class UC_SanPham : UserControl
    {
        private BLHangHoa blHangHoa = new BLHangHoa();

        public UC_SanPham()
        {
            InitializeComponent();
        }

        private void UC_SanPham_Load(object sender, EventArgs e)
        {
            LoadHangHoaData();
        }

        public void LoadHangHoaData()
        {
            try
            {
                // Now, LayHangHoa() only returns active products.
                DataSet ds = blHangHoa.LayHangHoa(); //
                dataGridView1.DataSource = ds.Tables[0];

                // Đặt header text cho DataGridView
                if (dataGridView1.Columns.Contains("MaSanPham"))
                    dataGridView1.Columns["MaSanPham"].HeaderText = "Mã Sản Phẩm";
                if (dataGridView1.Columns.Contains("TenSP"))
                    dataGridView1.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                if (dataGridView1.Columns.Contains("SoLuong"))
                    dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng Tồn";
                if (dataGridView1.Columns.Contains("Gia"))
                    dataGridView1.Columns["Gia"].HeaderText = "Giá Bán"; // Updated header text
                if (dataGridView1.Columns.Contains("GiaNhap"))
                    dataGridView1.Columns["GiaNhap"].HeaderText = "Giá Nhập"; // Updated header text

                // Định dạng cột Giá và Giá Nhập
                if (dataGridView1.Columns.Contains("Gia"))
                {
                    dataGridView1.Columns["Gia"].DefaultCellStyle.Format = "N0";
                }
                if (dataGridView1.Columns.Contains("GiaNhap"))
                {
                    dataGridView1.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                }
                // Hide IsActive column if it exists and is not needed for display
                if (dataGridView1.Columns.Contains("IsActive"))
                {
                    dataGridView1.Columns["IsActive"].Visible = false;
                }

                // Chặn người dùng chỉnh sửa trực tiếp trên DataGridView
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải là hàng dữ liệu hợp lệ không (không phải header hay hàng trống)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaHHXoa.Text = row.Cells["MaSanPham"].Value.ToString();
            }
        }

        private void btnThemHH_Click(object sender, EventArgs e)
        {
            string maSanPham = txtMaHHThem.Text.Trim();
            string tenSP = txtTenHH.Text.Trim();
            string soLuongStr = txtSoLuong.Text.Trim();
            string giaStr = txtGiaBan.Text.Trim();
            string giaNhapStr = txtGiaNhap.Text.Trim(); // NEW: Get GiaNhap from textbox

            if (string.IsNullOrEmpty(maSanPham) || string.IsNullOrEmpty(tenSP) ||
                string.IsNullOrEmpty(soLuongStr) || string.IsNullOrEmpty(giaStr) ||
                string.IsNullOrEmpty(giaNhapStr)) // NEW: Validate GiaNhap input
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm (Mã, Tên, Số lượng, Giá bán, Giá nhập).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong;
            if (!int.TryParse(soLuongStr, out soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal gia; // This is selling price
            if (!decimal.TryParse(giaStr, out gia) || gia <= 0)
            {
                MessageBox.Show("Giá bán phải là một số dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal giaNhap; // This is import price
            if (!decimal.TryParse(giaNhapStr, out giaNhap) || giaNhap < 0) // GiaNhap can be 0 or positive
            {
                MessageBox.Show("Giá nhập phải là một số không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string error = "";
            bool success = false;

            blHangHoa.db.BeginTransaction();
            try
            {
                // Call ThemHangHoa with 6 arguments (added giaNhap and IsActive implicitly set to 1)
                success = blHangHoa.ThemHangHoa(maSanPham, tenSP, soLuong, gia, giaNhap, ref error); //

                if (success)
                {
                    blHangHoa.db.CommitTransaction();
                    MessageBox.Show("Thêm hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear input fields
                    txtMaHHThem.Clear();
                    txtTenHH.Clear();
                    txtSoLuong.Clear();
                    txtGiaBan.Clear();
                    txtGiaNhap.Clear();
                    LoadHangHoaData(); // Refresh DataGridView after adding
                }
                else
                {
                    blHangHoa.db.RollbackTransaction();
                    MessageBox.Show($"Thêm hàng hóa thất bại: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                blHangHoa.db.RollbackTransaction();
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn trong quá trình thêm hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // btnXoaHH_Click now performs soft delete
        private void btnXoaHH_Click(object sender, EventArgs e)
        {
            string maSanPhamXoa = txtMaHHXoa.Text.Trim();

            if (string.IsNullOrEmpty(maSanPhamXoa))
            {
                MessageBox.Show("Vui lòng nhập Mã hàng hóa cần xóa hoặc chọn từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa hàng hóa có mã '{maSanPhamXoa}'? (Thao tác này sẽ ẩn sản phẩm khỏi danh sách)",
                                                 "Xác nhận Xóa Mềm", // Changed confirmation message
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                string error = "";
                bool success = false;

                blHangHoa.db.BeginTransaction();
                try
                {
                    // Call the modified XoaHangHoa for soft delete
                    success = blHangHoa.XoaHangHoa(maSanPhamXoa, ref error); //

                    if (success)
                    {
                        blHangHoa.db.CommitTransaction();
                        MessageBox.Show("Xóa mềm hàng hóa thành công! Sản phẩm đã được ẩn khỏi danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMaHHXoa.Clear();
                        LoadHangHoaData(); // Refresh DataGridView after soft deletion
                    }
                    else
                    {
                        blHangHoa.db.RollbackTransaction();
                        MessageBox.Show($"Xóa mềm hàng hóa thất bại: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    blHangHoa.db.RollbackTransaction();
                    MessageBox.Show($"Đã xảy ra lỗi không mong muốn trong quá trình xóa mềm hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTenHH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}