using System;
using System.Data;
using System.Windows.Forms;
using QLBanHang_3Tang.BS_layer;
using Convinien_Store;

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
            this.Load += new System.EventHandler(this.UC_TimKiem_Load);
        }

        // Xu ly su kien
        private void UC_TimKiem_Load(object sender, EventArgs e)
        {
            btnTimHH_Click(null, null);
            btnTimHD_Click(null, null);
            btnTimKH_Click(null, null);
        }

        private void btnTimHH_Click(object sender, EventArgs e)
        {
            string maHangHoa = txtMaHH.Text.Trim();
            string error = "";
            // Goi phuong thuc 
            DataSet ds = blHangHoa.TimHangHoa(maHangHoa, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0]; // Hien thi ket qua 
                if (ds.Tables[0].Rows.Count == 0 && !string.IsNullOrEmpty(maHangHoa))
                {
                    MessageBox.Show("Khong tim thay hang hoa nao khop voi ma da nhap", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                MessageBox.Show($"Loi tim kiem hang hoa: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null; 
            }
        }

        private void btnSuaHH_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui long chon mot hang hoa de sua", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lay MaSanPham tu hang duoc chon
            string maSanPham = dataGridView1.SelectedRows[0].Cells["MaSanPham"].Value.ToString();
            string error = "";

            // Lay gia tri hien tai tu DataGridView de lam gia tri mac dinh neu TextBox de trong
            decimal currentGiaBan = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["Gia"].Value);
            decimal currentGiaNhap = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["GiaNhap"].Value);
            int currentSoLuong = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SoLuong"].Value);

            decimal newGiaBan = currentGiaBan;
            decimal newGiaNhap = currentGiaNhap;
            int newSoLuong = currentSoLuong;

            // Kiem tra va lay gia tri moi tu cac TextBox, neu hop le thi su dung, neu khong thi giu gia tri cu
            if (!string.IsNullOrEmpty(txtGiaBanMoi.Text))
            {
                if (!decimal.TryParse(txtGiaBanMoi.Text, out newGiaBan) || newGiaBan <= 0)
                {
                    MessageBox.Show("Gia ban moi khong hop le Vui long nhap mot so duong", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtGiaNhapMoi.Text))
            {
                if (!decimal.TryParse(txtGiaNhapMoi.Text, out newGiaNhap) || newGiaNhap < 0)
                {
                    MessageBox.Show("Gia nhap moi khong hop le Vui long nhap mot so khong am", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtSoLuongMoi.Text))
            {
                if (!int.TryParse(txtSoLuongMoi.Text, out newSoLuong) || newSoLuong < 0)
                {
                    MessageBox.Show("So luong moi khong hop le Vui long nhap mot so nguyen khong am", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (blHangHoa.CapNhatHangHoa(maSanPham, newGiaBan, newGiaNhap, newSoLuong, ref error))
            {
                MessageBox.Show("Cap nhat thong tin hang hoa thanh cong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHH.Clear(); 
                btnTimHH_Click(sender, e);
                txtGiaBanMoi.Clear();
                txtGiaNhapMoi.Clear();
                txtSoLuongMoi.Clear();
            }
            else
            {
                MessageBox.Show($"Cap nhat hang hoa that bai: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Dam bao rang click vao mot hang
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                // Dien MaSP vao o tim kiem
                txtMaHH.Text = row.Cells["MaSanPham"].Value.ToString();
                txtGiaBanMoi.Text = row.Cells["Gia"].Value.ToString();
                txtGiaNhapMoi.Text = row.Cells["GiaNhap"].Value.ToString();
                txtSoLuongMoi.Text = row.Cells["SoLuong"].Value.ToString();
            }
        }

        private void btnTimHD_Click(object sender, EventArgs e)
        {
            string maHoaDon = txtMaHD.Text.Trim(); 
            string error = "";
            DataSet ds = blHoaDonBan.TimHoaDon(maHoaDon, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dataGridView3.DataSource = ds.Tables[0]; // Hien thi ket qua 
                if (ds.Tables[0].Rows.Count == 0 && !string.IsNullOrEmpty(maHoaDon))
                {
                    MessageBox.Show("Khong tim thay hoa don nao khop voi ma da nhap", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (dataGridView3.Columns.Contains("TongCong"))
                {
                    dataGridView3.Columns["TongCong"].DefaultCellStyle.Format = "N0";
                }
            }
            else
            {
                MessageBox.Show($"Loi tim kiem hoa don: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView3.DataSource = null;
            }
        }

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string sdtKhachHang = textBox1.Text.Trim(); // Lay SDT tu TextBox tim kiem
            string error = "";
            DataSet ds = blKhachHang.TimKhachHang(sdtKhachHang, ref error);
            if (ds != null && ds.Tables.Count > 0)
            {
                dataGridView4.DataSource = ds.Tables[0]; 
                if (ds.Tables[0].Rows.Count == 0 && !string.IsNullOrEmpty(sdtKhachHang))
                {
                    MessageBox.Show("Khong tim thay khach hang nao khop voi SDT da nhap", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Loi tim kiem khach hang: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView4.DataSource = null;
            }
        }

        private void btnViewInvoiceDetails_Click(object sender, EventArgs e)
        {
            // Kiem tra xem co hoa don nao duoc chon
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Lay MaHoaDonBan tu hang duoc chon
                string maHoaDonBan = dataGridView3.SelectedRows[0].Cells["MaHoaDonBan"].Value.ToString();
                // Tao va hien thi FormViewSalesInvoice voi MaHoaDonBan cu the
                FormViewSalesInvoice formView = new FormViewSalesInvoice(maHoaDonBan);
                formView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui long chon mot hoa don de xem chi tiet", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnViewAllInvoices_Click(object sender, EventArgs e)
        {
            
            FormViewSalesInvoice formView = new FormViewSalesInvoice();
            formView.ShowDialog();
        }
    }
}