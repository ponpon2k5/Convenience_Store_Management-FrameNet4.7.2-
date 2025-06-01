
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

        // Lay danh sach hang hoa dang kinh doanh (IsActive = 1)
        public DataSet LayHangHoa()
        {
            string sql = "SELECT MaSanPham, TenSP, SoLuong, Gia, GiaNhap FROM HANG_HOA WHERE IsActive = 1";
            return db.ExecuteQueryDataSet(sql, CommandType.Text);
        }

        // Them mot hang hoa moi (bao gom GiaNhap, mac dinh IsActive = 1)
        public bool ThemHangHoa(string maSanPham, string tenSP, int soLuong, decimal gia, decimal giaNhap, ref string error)
        {
            string tenSPSafe = tenSP.Replace("'", "''");
            string giaStr = gia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string giaNhapStr = giaNhap.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string sql = $"INSERT INTO HANG_HOA (MaSanPham, TenSP, SoLuong, Gia, GiaNhap, IsActive) " +
                         $"VALUES ('{maSanPham.Replace("'", "''")}', N'{tenSPSafe}', {soLuong}, {giaStr}, {giaNhapStr}, 1)";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Cap nhat thong tin hang hoa (gia ban, gia nhap, so luong)
        public bool CapNhatHangHoa(string maSanPham, decimal giaBanMoi, decimal giaNhapMoi, int soLuongMoi, ref string error)
        {
            string giaBanStr = giaBanMoi.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string giaNhapStr = giaNhapMoi.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string sql = $"UPDATE HANG_HOA SET Gia = {giaBanStr}, GiaNhap = {giaNhapStr}, SoLuong = {soLuongMoi} WHERE MaSanPham = '{maSanPham.Replace("'", "''")}'";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Xoa mem hang hoa (chuyen IsActive = 0 de ngung kinh doanh)
        public bool XoaHangHoa(string maSanPham, ref string error)
        {
            string sql = $"UPDATE HANG_HOA SET IsActive = 0 WHERE MaSanPham = '{maSanPham.Replace("'", "''")}'";
            return db.MyExecuteNonQuery(sql, CommandType.Text, ref error);
        }

        // Lay MaSanPham tu TenSP (chi cho san pham dang kinh doanh)
        public string LayMaSanPhamTuTen(string tenSP)
        {
            string sql = $"SELECT MaSanPham FROM HANG_HOA WHERE TenSP = N'{tenSP.Replace("'", "''")}' AND IsActive = 1";
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
                Console.WriteLine("Loi khi lay ma san pham: " + ex.Message);
            }
            return maSanPham;
        }

        // Lay gia ban cua mot san pham (chi cho san pham dang kinh doanh)
        public decimal? LayGiaBan(string maSP)
        {
            string sql = $"SELECT Gia FROM HANG_HOA WHERE MaSanPham = '{maSP.Replace("'", "''")}' AND IsActive = 1";
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
                Console.WriteLine("Loi khi lay gia ban: " + ex.Message);
            }
            return giaBan;
        }

        // Tim kiem hang hoa theo MaSanPham (cho san pham dang kinh doanh)
        public DataSet TimHangHoa(string maSanPham, ref string error)
        {
            string sql = $"SELECT MaSanPham, TenSP, SoLuong, Gia, GiaNhap FROM HANG_HOA WHERE MaSanPham LIKE '%{maSanPham.Replace("'", "''")}%' AND IsActive = 1";
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

        // Lay tat ca hang hoa (bao gom ca hang ngung kinh doanh - IsActive = 0)
        public DataSet LayTatCaHangHoa(ref string error)
        {
            string sql = "SELECT MaSanPham, TenSP, SoLuong, Gia, GiaNhap, IsActive FROM HANG_HOA";
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