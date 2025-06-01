
using System;
using System.Data;
using Convenience_Store_Management.DAL;

namespace QLBanHang_3Tang.BS_layer 
{
    public class BLTaiKhoan 
    {
        private ConnectDB db; 

        public BLTaiKhoan()
        {
            db = new ConnectDB();
        }

        // Kiem tra thong tin dang nhap cua NhanVien hoac KhachHang
        public bool KiemTraDangNhap(string tenDangNhap, string matKhau, string loaiTaiKhoan, ref string error)
        {
            string sql = "";
            string tableName = "";

            // Chon bang DANG_NHAP tuong ung voi loai tai khoan
            if (loaiTaiKhoan == "Employee")
            {
                tableName = "DANG_NHAP_NHAN_VIEN";
            }
            else if (loaiTaiKhoan == "Customer")
            {
                tableName = "DANG_NHAP_KHACH_HANG";
            }
            else
            {
                error = "Loai tai khoan khong hop le";
                return false;
            }

            // check xem co dung khong
            sql = $"SELECT COUNT(*) FROM {tableName} WHERE TenDangNhap = '{tenDangNhap.Replace("'", "''")}' AND MatKhau = '{matKhau.Replace("'", "''")}'";

            try
            {
                DataSet ds = db.ExecuteQueryDataSet(sql, CommandType.Text);
                // neu co tai khoaon
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0;
                }
                return false; // Khong tim thay
            }
            catch (Exception ex)
            {
                error = "Loi khi kiem tra dang nhap: " + ex.Message;
                return false;
            }
        }

        // Kiem tra xem TenDangNhap da ton tai trong bang nhan vien hoac khach hang chua
        public bool KiemTraTonTaiTenDangNhap(string tenDangNhap, ref string error)
        {
            // Ktra
            string sqlEmployee = $"SELECT COUNT(*) FROM DANG_NHAP_NHAN_VIEN WHERE TenDangNhap = '{tenDangNhap.Replace("'", "''")}'";
            string sqlCustomer = $"SELECT COUNT(*) FROM DANG_NHAP_KHACH_HANG WHERE TenDangNhap = '{tenDangNhap.Replace("'", "''")}'";

            try
            {
                // Ktra
                DataSet dsEmployee = db.ExecuteQueryDataSet(sqlEmployee, CommandType.Text);
                if (dsEmployee != null && dsEmployee.Tables.Count > 0 && Convert.ToInt32(dsEmployee.Tables[0].Rows[0][0]) > 0)
                {
                    return true;
                }

                
                DataSet dsCustomer = db.ExecuteQueryDataSet(sqlCustomer, CommandType.Text);
                if (dsCustomer != null && dsCustomer.Tables.Count > 0 && Convert.ToInt32(dsCustomer.Tables[0].Rows[0][0]) > 0)
                {
                    return true; 
                }
                return false; //Khong ton tai
            }
            catch (Exception ex)
            {
                error = "Loi kiem tra ten dang nhap ton tai: " + ex.Message;
                return false; 
            }
        }

        // Them tai khoan moi cho NhanVien hoac KhachHang
        public bool ThemTaiKhoan(string tenDangNhap, string matKhau, string loaiTaiKhoan, string identifier, ref string error)
        {
            string sql = "";

            // INSERT tuong ung voi loai tai khoan
            if (loaiTaiKhoan == "Employee")
            {
                sql = $"INSERT INTO DANG_NHAP_NHAN_VIEN (TenDangNhap, MatKhau, MaNhanVien) VALUES ('{tenDangNhap.Replace("'", "''")}', '{matKhau.Replace("'", "''")}', '{identifier.Replace("'", "''")}')";
            }
            else if (loaiTaiKhoan == "Customer")
            {
                sql = $"INSERT INTO DANG_NHAP_KHACH_HANG (TenDangNhap, MatKhau, SDTKhachHang) VALUES ('{tenDangNhap.Replace("'", "''")}', '{matKhau.Replace("'", "''")}', '{identifier.Replace("'", "''")}')";
            }
            else
            {
                error = "Loai tai khoan khong hop le";
                return false;
            }

            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Them mot nhan vien moi vao bang NHAN_VIEN
        public bool ThemNhanVien(string maNhanVien, string hoTenNV, string sdtNV, ref string error)
        {
            string sql = $"INSERT INTO NHAN_VIEN (MaNhanVien, HoTenNV, SdtNV) VALUES ('{maNhanVien.Replace("'", "''")}', N'{hoTenNV.Replace("'", "''")}', '{sdtNV.Replace("'", "''")}')";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Them mot khach hang moi vao bang KHACH_HANG
        public bool ThemKhachHang(string sdtKhachHang, string tenKH, DateTime? ngaySinh, ref string error)
        {
            // ngaysinh co the null, neu co thi chen` null vao
            string ngaySinhStr = ngaySinh.HasValue ? $"'{ngaySinh.Value.ToString("yyyy-MM-dd")}'" : "NULL";
            string sql = $"INSERT INTO KHACH_HANG (SDT, TenKH, NgaySinh) VALUES ('{sdtKhachHang.Replace("'", "''")}', N'{tenKH.Replace("'", "''")}', {ngaySinhStr})";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Lay MaNhanVien tu TenDangNhap cua nhan vien
        public string LayMaNhanVienTuTenDangNhap(string tenDangNhap, ref string error)
        {
            string sql = $"SELECT MaNhanVien FROM DANG_NHAP_NHAN_VIEN WHERE TenDangNhap = '{tenDangNhap.Replace("'", "''")}'";
            try
            {
                DataSet ds = db.ExecuteQueryDataSet(sql, CommandType.Text);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["MaNhanVien"] != DBNull.Value)
                {
                    return ds.Tables[0].Rows[0]["MaNhanVien"].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                error = "Loi lay ma nhan vien tu tai khoan: " + ex.Message;
                return null;
            }
        }

        // Lay SDTKhachHang tu TenDangNhap cua khach hang
        public string LaySDTKhachHangTuTenDangNhap(string tenDangNhap, ref string error)
        {
            string sql = $"SELECT SDTKhachHang FROM DANG_NHAP_KHACH_HANG WHERE TenDangNhap = '{tenDangNhap.Replace("'", "''")}'";
            try
            {
                DataSet ds = db.ExecuteQueryDataSet(sql, CommandType.Text);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["SDTKhachHang"] != DBNull.Value)
                {
                    return ds.Tables[0].Rows[0]["SDTKhachHang"].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                error = "Loi lay SDT khach hang tu tai khoan: " + ex.Message;
                return null;
            }
        }

        // Lay TenDangNhap tu SDTKhachHang cua khach hang
        public string LayTenDangNhapTuSDTKhachHang(string sdtKhachHang, ref string error)
        {
            string sql = $"SELECT TenDangNhap FROM DANG_NHAP_KHACH_HANG WHERE SDTKhachHang = '{sdtKhachHang.Replace("'", "''")}'";
            try
            {
                DataSet ds = db.ExecuteQueryDataSet(sql, CommandType.Text);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["TenDangNhap"] != DBNull.Value)
                {
                    return ds.Tables[0].Rows[0]["TenDangNhap"].ToString();
                }
                return null; 
            }
            catch (Exception ex)
            {
                error = "Loi lay ten dang nhap tu SDT khach hang: " + ex.Message;
                return null;
            }
        }

        // Cap nhat TenDangNhap va MatKhau cho tai khoan khach hang (su dung transaction)
        public bool CapNhatTaiKhoanKhachHang(string sdtKhachHang, string oldUsername, string newUsername, string newPassword, ref string error)
        {
            string sql = "";
            bool success = false; // check

            db.BeginTransaction();
            try
            {
                // Neu TenDangNhap thay doi thi cap nhat
                if (oldUsername != newUsername)
                {
                    sql = $"UPDATE DANG_NHAP_KHACH_HANG SET TenDangNhap = '{newUsername.Replace("'", "''")}' WHERE SDTKhachHang = '{sdtKhachHang.Replace("'", "''")}' AND TenDangNhap = '{oldUsername.Replace("'", "''")}'";
                    success = db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
                    if (!success)
                    {
                        db.RollbackTransaction();
                        error = "Khong the cap nhat ten dang nhap: " + error;
                        return false;
                    }
                }
                else
                {
                    success = true;
                }

                // Luon cap nhat MatKhau (voi TenDangNhap moi neu da doi)
                sql = $"UPDATE DANG_NHAP_KHACH_HANG SET MatKhau = '{newPassword.Replace("'", "''")}' WHERE SDTKhachHang = '{sdtKhachHang.Replace("'", "''")}' AND TenDangNhap = '{newUsername.Replace("'", "''")}'";
                bool passwordUpdateSuccess = db.MyExecuteNonQuery(sql, CommandType.Text, ref error);

                // Neu ca hai buoc (hoac mot buoc neu ten khong doi) thanh cong thi commit
                if (passwordUpdateSuccess && success)
                {
                    db.CommitTransaction();
                    return true;
                }
                else
                {
                    db.RollbackTransaction();
                    error = "Khong the cap nhat mat khau: " + error;
                    return false;
                }
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                error = "Loi trong qua trinh cap nhat tai khoan: " + ex.Message;
                return false;
            }
        }

        // Chi cap nhat MatKhau cho tai khoan khach hang (su dung transaction)
        public bool CapNhatMatKhauKhachHang(string sdtKhachHang, string tenDangNhap, string newPassword, ref string error)
        {
            string sql = "";
            db.BeginTransaction();
            try
            {
                sql = $"UPDATE DANG_NHAP_KHACH_HANG SET MatKhau = '{newPassword.Replace("'", "''")}' WHERE SDTKhachHang = '{sdtKhachHang.Replace("'", "''")}' AND TenDangNhap = '{tenDangNhap.Replace("'", "''")}'";
                bool passwordUpdateSuccess = db.MyExecuteNonQuery(sql, CommandType.Text, ref error);

                if (passwordUpdateSuccess)
                {
                    db.CommitTransaction();
                    return true;
                }
                else
                {
                    db.RollbackTransaction();
                    return false;
                }
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                error = "Loi trong qua trinh cap nhat mat khau: " + ex.Message;
                return false;
            }
        }
    }
}