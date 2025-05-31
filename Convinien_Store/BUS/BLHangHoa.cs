// BUS/BLHangHoa.cs
using System;
using System.Data;
using Convenience_Store_Management.DAL;

namespace QLBanHang_3Tang.BS_layer
{
    public class BLHangHoa
    {
        public ConnectDB db;

        public BLHangHoa()
        {
            db = new ConnectDB();
        }

        public DataSet LayHangHoa()
        {
            // Include GiaNhap in SELECT statement
            // ONLY SELECT active items
            string sql = "SELECT MaSanPham, TenSP, SoLuong, Gia, GiaNhap FROM HANG_HOA WHERE IsActive = 1"; //
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
        }

        // ThemHangHoa: Added giaNhap parameter and its inclusion in SQL
        public bool ThemHangHoa(string maSanPham, string tenSP, int soLuong, decimal gia, decimal giaNhap, ref string error) // Added 'decimal giaNhap'
        {
            // IMPORTANT: SQL Injection Vulnerability!
            string tenSPSafe = tenSP.Replace("'", "''");
            string giaStr = gia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string giaNhapStr = giaNhap.ToString(System.Globalization.CultureInfo.InvariantCulture); // Convert decimal GiaNhap

            // SQL INSERT statement with GiaNhap and IsActive
            string sql = $"INSERT INTO HANG_HOA (MaSanPham, TenSP, SoLuong, Gia, GiaNhap, IsActive) " + //
                         $"VALUES ('{maSanPham.Replace("'", "''")}', N'{tenSPSafe}', {soLuong}, {giaStr}, {giaNhapStr}, 1)"; //

            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // NEW: Method to update HangHoa (selling price, import price, quantity)
        public bool CapNhatHangHoa(string maSanPham, decimal giaBanMoi, decimal giaNhapMoi, int soLuongMoi, ref string error)
        {
            string giaBanStr = giaBanMoi.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string giaNhapStr = giaNhapMoi.ToString(System.Globalization.CultureInfo.InvariantCulture);

            string sql = $"UPDATE HANG_HOA SET Gia = {giaBanStr}, GiaNhap = {giaNhapStr}, SoLuong = {soLuongMoi} WHERE MaSanPham = '{maSanPham.Replace("'", "''")}'";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // XoaHangHoa: Modified for soft delete
        public bool XoaHangHoa(string maSanPham, ref string error)
        {
            string sql = $"UPDATE HANG_HOA SET IsActive = 0 WHERE MaSanPham = '{maSanPham.Replace("'", "''")}'"; //
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // LayMaSanPhamTuTen remains unchanged
        public string LayMaSanPhamTuTen(string tenSP)
        {
            // Only consider active products
            string sql = $"SELECT MaSanPham FROM HANG_HOA WHERE TenSP = N'{tenSP.Replace("'", "''")}' AND IsActive = 1"; //

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

        // LayGiaBan remains unchanged (as it specifically gets selling price)
        public decimal? LayGiaBan(string maSP)
        {
            // Only get price for active products
            string sql = $"SELECT Gia FROM HANG_HOA WHERE MaSanPham = '{maSP.Replace("'", "''")}' AND IsActive = 1"; //

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

        // TimHangHoa: Included GiaNhap in SELECT statement
        // Only search active items by default
        public DataSet TimHangHoa(string maSanPham, ref string error)
        {
            string sql = $"SELECT MaSanPham, TenSP, SoLuong, Gia, GiaNhap FROM HANG_HOA WHERE MaSanPham LIKE '%{maSanPham.Replace("'", "''")}%' AND IsActive = 1"; //
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

        // NEW: Method to retrieve ALL HangHoa (including inactive) for an admin view, if needed
        public DataSet LayTatCaHangHoa(ref string error)
        {
            string sql = "SELECT MaSanPham, TenSP, SoLuong, Gia, GiaNhap, IsActive FROM HANG_HOA"; //
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