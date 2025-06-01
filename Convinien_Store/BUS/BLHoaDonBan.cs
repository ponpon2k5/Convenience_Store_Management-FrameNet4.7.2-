
using System;
using System.Data;
using Convenience_Store_Management.DAL; 
using Microsoft.Data.SqlClient;

namespace QLBanHang_3Tang.BS_layer
{
    public class BLHoaDonBan 
    {
        public ConnectDB db;

        public BLHoaDonBan()
        {
            db = new ConnectDB();
        }

        // Them mot hoa don ban (bang HOA_DON_BAN)
        public bool ThemHoaDonBan(string maHoaDonBan, string maNhanVien, string sdtKhachHang, DateTime ngayBan, ref string error)
        {
            //MaNhanVien va SDTKhachHang co the null
            string maNhanVienSql = string.IsNullOrEmpty(maNhanVien) ? "NULL" : $"'{maNhanVien.Replace("'", "''")}'";
            string sdtKhachHangSql = string.IsNullOrEmpty(sdtKhachHang) ? "NULL" : $"'{sdtKhachHang.Replace("'", "''")}'";

            // Dinh dang NgayBan 
            string sql = $"INSERT INTO HOA_DON_BAN (MaHoaDonBan, MaNhanVien, SDTKhachHang, NgayBan) " +
                         $"VALUES ('{maHoaDonBan.Replace("'", "''")}', {maNhanVienSql}, {sdtKhachHangSql}, '{ngayBan.ToString("yyyy-MM-dd")}')";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Them mot chi tiet ban hang (bang CHI_TIET_BAN)
        public bool ThemChiTietBan(string maHoaDonBan, string maSanPham, int soLuong, decimal giaBan, decimal thanhTien, ref string error)
        {
            // Chuyen doi gia tri 
            string giaBanStr = giaBan.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string thanhTienStr = thanhTien.ToString(System.Globalization.CultureInfo.InvariantCulture);
            // System.Globalization.CultureInfo.InvariantCulture la tranh su khac bien dau thap phan

            string sql = $"INSERT INTO CHI_TIET_BAN (MaHoaDonBan, MaSanPham, SoLuong, GiaBan, ThanhTien) VALUES ('{maHoaDonBan}', '{maSanPham}', {soLuong}, {giaBanStr}, {thanhTienStr})";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Cap nhat so luong ton kho cua hang hoa (co the tang hoac giam)
        public bool CapNhatSoLuongHangHoa(string maSanPham, int soLuongThayDoi, ref string error)
        {
            // soLuongThayDoi co the am (khi ban hang) hoac duong (khi nhap them/huy don)
            string sql = $"UPDATE HANG_HOA SET SoLuong = SoLuong + {soLuongThayDoi} WHERE MaSanPham = '{maSanPham.Replace("'", "''")}'";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Xu ly toan bo giao dich ban hang: them hoa don, them chi tiet, cap nhat ton kho
        public bool ProcessSaleTransaction(string maHoaDonBan, string maNhanVien, string sdtKhachHang,
                                           DataTable cartItems, ref string error)
        {
            decimal totalBillAmount = 0;

            db.BeginTransaction();
            try
            {
                // 1 Them hoa don chinh
                if (!ThemHoaDonBan(maHoaDonBan, maNhanVien, sdtKhachHang, DateTime.Now, ref error))
                {
                    db.RollbackTransaction(); // Huy giao dich neu them hoa don loi
                    return false;
                }

                // 2 Lap qua cac mat hang trong gio de them chi tiet va cap nhat so luong
                foreach (DataRow row in cartItems.Rows)
                {
                    string maSanPham = row.Field<string>("MaSanPham");
                    int soLuong = row.Field<int>("SoLuong");
                    decimal giaBan = row.Field<decimal>("Gia"); // Lay Gia tu DataTable 
                    decimal thanhTien = row.Field<decimal>("ThanhTien");

                    // 2a Them chi tiet ban hang
                    if (!ThemChiTietBan(maHoaDonBan, maSanPham, soLuong, giaBan, thanhTien, ref error))
                    {
                        db.RollbackTransaction();
                        return false;
                    }

                    // 2b Cap nhat (giam) so luong ton kho
                    if (!CapNhatSoLuongHangHoa(maSanPham, -soLuong, ref error)) // Tru so luong da ban
                    {
                        db.RollbackTransaction();
                        return false;
                    }
                    totalBillAmount += thanhTien; // Tinh tong tien hoa don, hien chua su dung gia tri nay
                }

                db.CommitTransaction(); // Xac nhan toan bo giao dich neu moi thu thanh cong
                return true;
            }
            catch (Exception ex)
            {
                db.RollbackTransaction(); // Huy giao dich neu co loi ngoai le
                error = "Loi giao dich ban hang: " + ex.Message;
                return false;
            }
        }

        // Lay MaSanPham tu TenSP (tuong tu BLHangHoa)
        // CHU Y: Co nguy co SQL Injection
        public string LayMaSanPhamTuTen(string tenSP, ref string error)
        {
            string sql = $"SELECT MaSanPham FROM HANG_HOA WHERE TenSP = N'{tenSP.Replace("'", "''")}'";
            DataSet ds = null;
            string maSanPham = null;
            try
            {
                ds = db.ExecuteQueryDataSet(sql, CommandType.Text);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    maSanPham = ds.Tables[0].Rows[0]["MaSanPham"].ToString();
                }
            }
            catch (Exception ex)
            {
                error = "Loi khi lay ma san pham: " + ex.Message;
            }
            return maSanPham;
        }

        // Lay GiaBan tu MaSanPham (tuong tu BLHangHoa)
        // CHU Y: Co nguy co SQL Injection
        public decimal? LayGiaBan(string maSP, ref string error)
        {
            string sql = $"SELECT Gia FROM HANG_HOA WHERE MaSanPham = '{maSP.Replace("'", "''")}'";
            DataSet ds = null;
            decimal? giaBan = null;
            try
            {
                ds = db.ExecuteQueryDataSet(sql, CommandType.Text);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    giaBan = Convert.ToDecimal(ds.Tables[0].Rows[0]["Gia"]);
                }
            }
            catch (Exception ex)
            {
                error = "Loi khi lay gia ban: " + ex.Message;
            }
            return giaBan;
        }

        // Lay chi tiet cua mot hoa don ban cu the (join cac bang)
        // CHU Y: Co nguy co SQL Injection
        public DataSet LayChiTietHoaDon(string maHoaDonBan, ref string error)
        {
            string sql = $"SELECT HDB.MaHoaDonBan, HDB.NgayBan, HDB.MaNhanVien, HDB.SDTKhachHang, " +
                         $"CTB.MaSanPham, HH.TenSP, CTB.SoLuong, CTB.GiaBan, CTB.ThanhTien " +
                         $"FROM HOA_DON_BAN AS HDB " +
                         $"JOIN CHI_TIET_BAN AS CTB ON HDB.MaHoaDonBan = CTB.MaHoaDonBan " +
                         $"JOIN HANG_HOA AS HH ON CTB.MaSanPham = HH.MaSanPham " +
                         $"WHERE HDB.MaHoaDonBan = '{maHoaDonBan.Replace("'", "''")}'";
            try
            {
                return db.ExecuteQueryDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        // Lay tat ca chi tiet cua tat ca hoa don (join cac bang)
        public DataSet LayTatCaChiTietHoaDon(ref string error)
        {
            string sql = $"SELECT HDB.MaHoaDonBan, HDB.NgayBan, HDB.MaNhanVien, HDB.SDTKhachHang, " +
                         $"CTB.MaSanPham, HH.TenSP, CTB.SoLuong, CTB.GiaBan, CTB.ThanhTien " +
                         $"FROM HOA_DON_BAN AS HDB " +
                         $"JOIN CHI_TIET_BAN AS CTB ON HDB.MaHoaDonBan = CTB.MaHoaDonBan " +
                         $"JOIN HANG_HOA AS HH ON CTB.MaSanPham = HH.MaSanPham " +
                         $"ORDER BY HDB.NgayBan DESC, HDB.MaHoaDonBan ASC"; // Sap xep ket qua
            try
            {
                return db.ExecuteQueryDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        // Lay thong ke doanh thu theo tuan, thang hoac tat ca
        public DataSet LayDoanhThu(string filterType, ref string error)
        {
            string dateFilter = ""; // Dieu kien loc theo ngay
            DateTime now = DateTime.Now;

            // Xac dinh khoang thoi gian loc
            if (filterType == "Tuần")
            {
                DateTime startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
                DateTime endOfWeek = startOfWeek.AddDays(7);
                dateFilter = $"WHERE NgayBan >= '{startOfWeek.ToString("yyyy-MM-dd")}' AND NgayBan < '{endOfWeek.ToString("yyyy-MM-dd")}'";
            }
            else if (filterType == "Tháng")
            {
                DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1);
                dateFilter = $"WHERE NgayBan >= '{startOfMonth.ToString("yyyy-MM-dd")}' AND NgayBan < '{endOfMonth.ToString("yyyy-MM-dd")}'";
            }
            // Neu filterType la "Tất cả" thi dateFilter se rong (khong loc theo ngay)

            // Cau SQL tinh tong doanh thu theo hoa don, loc theo ngay
            string sql = $"SELECT HDB.MaHoaDonBan, HDB.NgayBan, SUM(CTB.ThanhTien) AS TongDoanhThu " +
                         $"FROM HOA_DON_BAN AS HDB JOIN CHI_TIET_BAN AS CTB ON HDB.MaHoaDonBan = CTB.MaHoaDonBan " +
                         $"{dateFilter} GROUP BY HDB.MaHoaDonBan, HDB.NgayBan ORDER BY HDB.NgayBan DESC";
            try
            {
                return db.ExecuteQueryDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        // Lay thong ke loi nhuan theo tuan, thang hoac tat ca
        public DataSet LayLoiNhuan(string filterType, ref string error)
        {
            string dateFilter = ""; // Dieu kien loc theo ngay
            DateTime now = DateTime.Now;

            // Xac dinh khoang thoi gian loc
            if (filterType == "Tuần")
            {
                DateTime startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
                DateTime endOfWeek = startOfWeek.AddDays(7);
                dateFilter = $"WHERE HDB.NgayBan >= '{startOfWeek.ToString("yyyy-MM-dd")}' AND HDB.NgayBan < '{endOfWeek.ToString("yyyy-MM-dd")}'";
            }
            else if (filterType == "Tháng")
            {
                DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1);
                dateFilter = $"WHERE HDB.NgayBan >= '{startOfMonth.ToString("yyyy-MM-dd")}' AND HDB.NgayBan < '{endOfMonth.ToString("yyyy-MM-dd")}'";
            }

            // Cau SQL tinh tong loi nhuan (GiaBan - GiaNhap) * SoLuong
            string sql = $"SELECT HDB.MaHoaDonBan, HDB.NgayBan, " +
                         $"SUM(CTB.SoLuong * (CTB.GiaBan - HH.GiaNhap)) AS TongLoiNhuan " +
                         $"FROM HOA_DON_BAN AS HDB " +
                         $"JOIN CHI_TIET_BAN AS CTB ON HDB.MaHoaDonBan = CTB.MaHoaDonBan " +
                         $"JOIN HANG_HOA AS HH ON CTB.MaSanPham = HH.MaSanPham " + // Join de lay GiaNhap
                         $"{dateFilter} GROUP BY HDB.MaHoaDonBan, HDB.NgayBan ORDER BY HDB.NgayBan DESC";
            try
            {
                return db.ExecuteQueryDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        // Lay thong ke cac mat hang da ban theo so luong va thanh tien
        public DataSet LayCacMatHangDaBan(string filterType, ref string error)
        {
            string dateFilter = ""; // Dieu kien loc theo ngay
            DateTime now = DateTime.Now;

            // Xac dinh khoang thoi gian loc
            if (filterType == "Tuần")
            {
                DateTime startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
                DateTime endOfWeek = startOfWeek.AddDays(7);
                dateFilter = $"WHERE HDB.NgayBan >= '{startOfWeek.ToString("yyyy-MM-dd")}' AND HDB.NgayBan < '{endOfWeek.ToString("yyyy-MM-dd")}'";
            }
            else if (filterType == "Tháng")
            {
                DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1);
                dateFilter = $"WHERE HDB.NgayBan >= '{startOfMonth.ToString("yyyy-MM-dd")}' AND HDB.NgayBan < '{endOfMonth.ToString("yyyy-MM-dd")}'";
            }

            // Cau SQL tinh tong so luong ban va tong thanh tien theo tung san pham
            string sql = $"SELECT HH.MaSanPham, HH.TenSP, SUM(CTB.SoLuong) AS TongSoLuongDaBan, SUM(CTB.ThanhTien) AS TongThanhTien " +
                         $"FROM CHI_TIET_BAN AS CTB JOIN HANG_HOA AS HH ON CTB.MaSanPham = HH.MaSanPham " +
                         $"JOIN HOA_DON_BAN AS HDB ON CTB.MaHoaDonBan = HDB.MaHoaDonBan " + // Join de loc theo NgayBan
                         $"{dateFilter} GROUP BY HH.MaSanPham, HH.TenSP ORDER BY TongSoLuongDaBan DESC";
            try
            {
                return db.ExecuteQueryDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        // Tim kiem hoa don theo MaHoaDonBan (su dung LIKE)
        // CHU Y: Co nguy co SQL Injection
        public DataSet TimHoaDon(string maHoaDonBan, ref string error)
        {
            string sql = $"SELECT HDB.MaHoaDonBan, HDB.MaNhanVien, HDB.SDTKhachHang, HDB.NgayBan, SUM(CTB.ThanhTien) AS TongCong " +
                         $"FROM HOA_DON_BAN AS HDB JOIN CHI_TIET_BAN AS CTB ON HDB.MaHoaDonBan = CTB.MaHoaDonBan " +
                         $"WHERE HDB.MaHoaDonBan LIKE '%{maHoaDonBan.Replace("'", "''")}%' " +
                         $"GROUP BY HDB.MaHoaDonBan, HDB.MaNhanVien, HDB.SDTKhachHang, HDB.NgayBan";
            try
            {
                return db.ExecuteQueryDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }
    }
}