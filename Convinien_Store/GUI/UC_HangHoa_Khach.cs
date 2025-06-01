using System;
using System.Data;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;

namespace Convenience_Store_Management.GUI
{
    public partial class UC_HangHoa_Khach : UserControl 
    {
        private BLHangHoa blHangHoa = new BLHangHoa();

        public delegate void AddToCartEventHandler(object sender, string maSanPham, string tenSP, int soLuong, decimal gia);
        
        public event AddToCartEventHandler OnAddToCart;

        public UC_HangHoa_Khach()
        {
            InitializeComponent();
            soluongText.Text = "1"; // Mac din
        }

        private void UC_HangHoa_Khach_Load(object sender, EventArgs e)
        {
            LoadHangHoaData();
        }

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
                    dataGridView1.Columns["Gia"].HeaderText = "Gia";
                if (dataGridView1.Columns.Contains("GiaNhap")) 
                {
                    dataGridView1.Columns["GiaNhap"].Visible = false; //an cot gia nhap
                }

                // Dinh dang hien thi cho cot Gia (them dau phay ngan cach hang nghin)
                if (dataGridView1.Columns.Contains("Gia"))
                {
                    dataGridView1.Columns["Gia"].DefaultCellStyle.Format = "N0";
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
                tensanpham_label.Text = row.Cells["TenSP"].Value.ToString();
                soluongText.Text = "1";
            }
        }

        private void btnThemGioHang_Click(object sender, EventArgs e)
        {
            // Kiem tra xem co san pham nao dang duoc chon kh
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                // Lay thong tin san pham tu hang duoc chon
                string maSanPham = selectedRow.Cells["MaSanPham"].Value.ToString();
                string tenSP = selectedRow.Cells["TenSP"].Value.ToString();
                int soLuongTon = Convert.ToInt32(selectedRow.Cells["SoLuong"].Value);
                decimal gia = Convert.ToDecimal(selectedRow.Cells["Gia"].Value);

                int quantityToAdd;
                if (!int.TryParse(soluongText.Text, out quantityToAdd) || quantityToAdd <= 0)
                {
                    MessageBox.Show("So luong phai la mot so nguyen duong", "Loi nhap lieu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                // Kiem tra so luong muon them co vuot qua so luong ton kho khong
                if (quantityToAdd > soLuongTon)
                {
                    MessageBox.Show($"San pham '{tenSP}' chi con {soLuongTon} san pham Khong du so luong ban yeu cau", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kich hoat su kien OnAddToCart de thong bao cho UC_GioHang_Khach 
                // Truyen thong tin san pham (maSP, tenSP, so luong them, gia)
                OnAddToCart?.Invoke(this, maSanPham, tenSP, quantityToAdd, gia);

                MessageBox.Show($"{quantityToAdd} x '{tenSP}' da duoc them vao gio hang", "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("Vui long chon mot san pham de them vao gio hang", "Chua Chon San Pham", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}