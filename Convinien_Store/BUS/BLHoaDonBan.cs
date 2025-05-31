// BUS/BLHoaDonBan.cs
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

        // Method to add a sales invoice (HOA_DON_BAN) - 5 arguments
        public bool ThemHoaDonBan(string maHoaDonBan, string maNhanVien, string sdtKhachHang, DateTime ngayBan, ref string error)
        {
            string maNhanVienSql = string.IsNullOrEmpty(maNhanVien) ? "NULL" : $"'{maNhanVien.Replace("'", "''")}'";
            string sdtKhachHangSql = string.IsNullOrEmpty(sdtKhachHang) ? "NULL" : $"'{sdtKhachHang.Replace("'", "''")}'";

            string sql = $"INSERT INTO HOA_DON_BAN (MaHoaDonBan, MaNhanVien, SDTKhachHang, NgayBan) " +
                         $"VALUES ('{maHoaDonBan.Replace("'", "''")}', {maNhanVienSql}, {sdtKhachHangSql}, '{ngayBan.ToString("yyyy-MM-dd")}')";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Method to add a sales detail item (CHI_TIET_BAN)
        public bool ThemChiTietBan(string maHoaDonBan, string maSanPham, int soLuong, decimal giaBan, decimal thanhTien, ref string error)
        {
            string giaBanStr = giaBan.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string thanhTienStr = thanhTien.ToString(System.Globalization.CultureInfo.InvariantCulture);

            string sql = $"INSERT INTO CHI_TIET_BAN (MaHoaDonBan, MaSanPham, SoLuong, GiaBan, ThanhTien) VALUES ('{maHoaDonBan}', '{maSanPham}', {soLuong}, {giaBanStr}, {thanhTienStr})";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Method to update product stock quantity (increase or decrease)
        public bool CapNhatSoLuongHangHoa(string maSanPham, int soLuongThayDoi, ref string error)
        {
            string sql = $"UPDATE HANG_HOA SET SoLuong = SoLuong + {soLuongThayDoi} WHERE MaSanPham = '{maSanPham.Replace("'", "''")}'";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Method to process a full sales transaction (used by UC_GioHang_Khach)
        public bool ProcessSaleTransaction(string maHoaDonBan, string maNhanVien, string sdtKhachHang,
                                           DataTable cartItems, ref string error)
        {
            decimal totalBillAmount = 0;

            db.BeginTransaction();
            try
            {
                if (!ThemHoaDonBan(maHoaDonBan, maNhanVien, sdtKhachHang, DateTime.Now, ref error))
                {
                    db.RollbackTransaction();
                    return false;
                }

                foreach (DataRow row in cartItems.Rows)
                {
                    string maSanPham = row.Field<string>("MaSanPham");
                    int soLuong = row.Field<int>("SoLuong");
                    decimal giaBan = row.Field<decimal>("Gia");
                    decimal thanhTien = row.Field<decimal>("ThanhTien");

                    if (!ThemChiTietBan(maHoaDonBan, maSanPham, soLuong, giaBan, thanhTien, ref error))
                    {
                        db.RollbackTransaction();
                        return false;
                    }

                    if (!CapNhatSoLuongHangHoa(maSanPham, -soLuong, ref error))
                    {
                        db.RollbackTransaction();
                        return false;
                    }
                    totalBillAmount += thanhTien;
                }

                db.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                error = "Lỗi giao dịch bán hàng: " + ex.Message;
                return false;
            }
        }

        // Method to get MaSanPham from TenSP (used by UC_HoaDon)
        public string LayMaSanPhamTuTen(string tenSP)
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
                Console.WriteLine("Lỗi khi lấy mã sản phẩm: " + ex.Message);
            }
            return maSanPham;
        }

        // Method to get GiaBan from MaSanPham (used by UC_HoaDon)
        public decimal? LayGiaBan(string maSP)
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
                Console.WriteLine("Lỗi khi lấy giá bán: " + ex.Message);
            }
            return giaBan;
        }

        // NEW: Method to get details of a specific sales invoice (hiện tại đã có)
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

        // NEW: Phương thức để lấy TẤT CẢ các chi tiết hóa đơn
        public DataSet LayTatCaChiTietHoaDon(ref string error)
        {
            string sql = $"SELECT HDB.MaHoaDonBan, HDB.NgayBan, HDB.MaNhanVien, HDB.SDTKhachHang, " +
                         $"CTB.MaSanPham, HH.TenSP, CTB.SoLuong, CTB.GiaBan, CTB.ThanhTien " +
                         $"FROM HOA_DON_BAN AS HDB " +
                         $"JOIN CHI_TIET_BAN AS CTB ON HDB.MaHoaDonBan = CTB.MaHoaDonBan " +
                         $"JOIN HANG_HOA AS HH ON CTB.MaSanPham = HH.MaSanPham " +
                         $"ORDER BY HDB.NgayBan DESC, HDB.MaHoaDonBan ASC"; // Sắp xếp để dễ nhìn
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


        // Statistics methods (used by UC_ThongKe)
        public DataSet LayDoanhThu(string filterType, ref string error)
        {
            string dateFilter = "";
            DateTime now = DateTime.Now;

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

        public DataSet LayLoiNhuan(string filterType, ref string error)
        {
            string dateFilter = "";
            DateTime now = DateTime.Now;

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

            // Correct SQL to calculate profit: (Selling Price - Import Price) * Quantity
            string sql = $"SELECT HDB.MaHoaDonBan, HDB.NgayBan, " +
                         $"SUM(CTB.SoLuong * (CTB.GiaBan - HH.GiaNhap)) AS TongLoiNhuan " + // Calculate profit per item and sum
                         $"FROM HOA_DON_BAN AS HDB " +
                         $"JOIN CHI_TIET_BAN AS CTB ON HDB.MaHoaDonBan = CTB.MaHoaDonBan " +
                         $"JOIN HANG_HOA AS HH ON CTB.MaSanPham = HH.MaSanPham " + // Join with HANG_HOA to get GiaNhap
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

        public DataSet LayCacMatHangDaBan(string filterType, ref string error)
        {
            string dateFilter = "";
            DateTime now = DateTime.Now;

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

            string sql = $"SELECT HH.MaSanPham, HH.TenSP, SUM(CTB.SoLuong) AS TongSoLuongDaBan, SUM(CTB.ThanhTien) AS TongThanhTien " +
                         $"FROM CHI_TIET_BAN AS CTB JOIN HANG_HOA AS HH ON CTB.MaSanPham = HH.MaSanPham " +
                         $"JOIN HOA_DON_BAN AS HDB ON CTB.MaHoaDonBan = HDB.MaHoaDonBan " +
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

        // Search methods (used by UC_TimKiem)
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