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
    public partial class UC_ThanhVien_Khach : UserControl
    {
        private BLTaiKhoan blTaiKhoan = new BLTaiKhoan();

        public UC_ThanhVien_Khach()
        {
            InitializeComponent();
        }

        private void UC_ThanhVien_Khach_Load(object sender, EventArgs e)
        {
            string error = "";
            string currentUsername = blTaiKhoan.LayTenDangNhapTuSDTKhachHang(SessionManager.CurrentLoggedInCustomerSdt, ref error);

            if (!string.IsNullOrEmpty(currentUsername))
            {
                txtCurrentUsername.Text = currentUsername;
                txtCurrentUsername.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Khong the tai thong tin ten dang nhap Vui long dang nhap lai", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thay doi trang thai hien/an mat khau
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtCurrentPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
            txtNewPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
            txtConfirmNewPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        private void btnChangeCredentials_Click(object sender, EventArgs e)
        {
            string currentUsername = txtCurrentUsername.Text.Trim();
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmNewPassword = txtConfirmNewPassword.Text.Trim();
            string sdtKhachHang = SessionManager.CurrentLoggedInCustomerSdt;
            string error = "";

            // 1 Kiem tra cac truong mat khau co duoc dien day du va hop le 
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
            {
                MessageBox.Show("Vui long dien day du tat ca cac truong mat khau", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newPassword.Length < 6)
            {
                MessageBox.Show("Mat khau moi phai co it nhat 6 ky tu", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Mat khau moi va xac nhan mat khau khong khop", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2 Kiem tra mat khau cu
            if (!blTaiKhoan.KiemTraDangNhap(currentUsername, currentPassword, "Customer", ref error))
            {
                MessageBox.Show("Mat khau cu khong dung Vui long nhap lai", "Loi Xac Thuc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3 Goi phuong thuc cap nhat mat khau
            if (blTaiKhoan.CapNhatMatKhauKhachHang(sdtKhachHang, currentUsername, newPassword, ref error))
            {
                MessageBox.Show("Cap nhat mat khau thanh cong", "Thanh Cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmNewPassword.Clear();
                chkShowPassword.Checked = false;
            }
            else
            {
                MessageBox.Show($"Cap nhat mat khau that bai: {error}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}