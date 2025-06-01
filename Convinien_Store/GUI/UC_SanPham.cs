using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        // Phuong thuc tai va hien thi du lieu hang hoa (chi cac san pham dang kinh doanh - IsActive=1)
        public void LoadHangHoaData()
        {
            try
            {
                DataSet ds = blHangHoa.LayHangHoa();
                dataGridView1.DataSource = ds.Tables[0]; 

                if (dataGridView1.Columns.Contains("MaSanPham"))
                    dataGridView1.Columns["MaSanPham"].HeaderText = "Ma San Pham";
                if (dataGridView1.Columns.Contains("TenSP"))
                    dataGridView1.Columns["TenSP"].HeaderText = "Ten San Pham";
                if (dataGridView1.Columns.Contains("SoLuong"))
                    dataGridView1.Columns["SoLuong"].HeaderText = "So Luong Ton";
                if (dataGridView1.Columns.Contains("Gia"))
                    dataGridView1.Columns["Gia"].HeaderText = "Gia Ban"; 
                if (dataGridView1.Columns.Contains("GiaNhap"))
                    dataGridView1.Columns["GiaNhap"].HeaderText = "Gia Nhap"; 

                if (dataGridView1.Columns.Contains("Gia"))
                {
                    dataGridView1.Columns["Gia"].DefaultCellStyle.Format = "N0";
                }
                if (dataGridView1.Columns.Contains("GiaNhap"))
                {
                    dataGridView1.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                }
                // An cot IsActive neu co 
                if (dataGridView1.Columns.Contains("IsActive"))
                {
                    dataGridView1.Columns["IsActive"].Visible = false;
                }

                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi tai du lieu san pham: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiem tra xem co phai la hang du lieu hop le khong
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaHHXoa.Text = row.Cells["MaSanPham"].Value.ToString(); 
            }
        }

        private void btnThemHH_Click(object sender, EventArgs e)
        {
            // Lay thong tin 
            string maSanPham = txtMaHHThem.Text.Trim();
            string tenSP = txtTenHH.Text.Trim();
            string soLuongStr = txtSoLuong.Text.Trim();
            string giaStr = txtGiaBan.Text.Trim(); 
            string giaNhapStr = txtGiaNhap.Text.Trim(); 

            // Kiem tra thong tin nhap vao co day du khong
            if (string.IsNullOrEmpty(maSanPham) || string.IsNullOrEmpty(tenSP) ||
                string.IsNullOrEmpty(soLuongStr) || string.IsNullOrEmpty(giaStr) ||
                string.IsNullOrEmpty(giaNhapStr))
            {
                MessageBox.Show("Vui long nhap day du thong tin san pham (Ma Ten So luong Gia ban Gia nhap)", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong;
            if (!int.TryParse(soLuongStr, out soLuong) || soLuong < 0)
            {
                MessageBox.Show("So luong phai la mot so nguyen khong am", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal gia; 
            if (!decimal.TryParse(giaStr, out gia) || gia <= 0)
            {
                MessageBox.Show("Gia ban phai la mot so duong", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal giaNhap;
            if (!decimal.TryParse(giaNhapStr, out giaNhap) || giaNhap < 0)
            {
                MessageBox.Show("Gia nhap phai la mot so khong am", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string error = "";
            bool success = false;

            blHangHoa.db.BeginTransaction(); 
            try
            {
                success = blHangHoa.ThemHangHoa(maSanPham, tenSP, soLuong, gia, giaNhap, ref error);

                if (success)
                {
                    blHangHoa.db.CommitTransaction();
                    MessageBox.Show("Them hang hoa thanh cong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    txtMaHHThem.Clear();
                    txtTenHH.Clear();
                    txtSoLuong.Clear();
                    txtGiaBan.Clear();
                    txtGiaNhap.Clear();
                    LoadHangHoaData(); 
                }
                else
                {
                    blHangHoa.db.RollbackTransaction(); 
                    MessageBox.Show($"Them hang hoa that bai: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                blHangHoa.db.RollbackTransaction(); 
                MessageBox.Show($"Da xay ra loi khong mong muon trong qua trinh them hang hoa: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  (Thuc hien xoa mem - soft delete)
        private void btnXoaHH_Click(object sender, EventArgs e)
        {
            string maSanPhamXoa = txtMaHHXoa.Text.Trim(); // Lay MaSanPham tu TextBox

            // Kiem tra MaSanPham co duoc nhap khong
            if (string.IsNullOrEmpty(maSanPhamXoa))
            {
                MessageBox.Show("Vui long nhap Ma hang hoa can xoa hoac chon tu danh sach", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hien thi hop thoai xac nhan truoc khi xoa mem
            var confirmResult = MessageBox.Show($"Ban co chac chan muon xoa hang hoa co ma '{maSanPhamXoa}' (Thao tac nay se an san pham khoi danh sach)",
                                                 "Xac nhan Xoa Mem", 
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                string error = "";
                bool success = false;

                blHangHoa.db.BeginTransaction();
                try
                {
                    success = blHangHoa.XoaHangHoa(maSanPhamXoa, ref error);

                    if (success)
                    {
                        blHangHoa.db.CommitTransaction(); 
                        MessageBox.Show("Xoa mem hang hoa thanh cong San pham da duoc an khoi danh sach", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMaHHXoa.Clear(); 
                        LoadHangHoaData();
                    }
                    else
                    {
                        blHangHoa.db.RollbackTransaction(); 
                        MessageBox.Show($"Xoa mem hang hoa that bai: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    blHangHoa.db.RollbackTransaction(); 
                    MessageBox.Show($"Da xay ra loi khong mong muon trong qua trinh xoa mem hang hoa: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTenHH_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}