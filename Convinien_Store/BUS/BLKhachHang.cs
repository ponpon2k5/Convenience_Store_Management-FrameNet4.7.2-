
using System;
using System.Data;
using Convenience_Store_Management.DAL;

namespace QLBanHang_3Tang.BS_layer
{
    public class BLKhachHang
    {
        public ConnectDB db;

        public BLKhachHang()
        {
            db = new ConnectDB();
        }

        // Lay danh sach tat ca khach hang
        public DataSet LayKhachHang(ref string error)
        {
            string sql = "SELECT SDT, TenKH, NgaySinh FROM KHACH_HANG";
            try
            {
                return db.ExecuteQueryDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                // Gan loi vao bien tham chieu
                error = ex.Message;
                return null;
            }
        }

        // Tim kiem khach hang theo sdt
        public DataSet TimKhachHang(string sdt, ref string error)
        {
            // Xu ly SDT de tranh SQL Injection don gian
            string sql = $"SELECT SDT, TenKH, NgaySinh FROM KHACH_HANG WHERE SDT LIKE '%{sdt.Replace("'", "''")}%'";
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