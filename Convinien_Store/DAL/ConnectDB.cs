using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Convenience_Store_Management.DAL
{
    public class ConnectDB
    {
        public readonly string strCon = "Data Source= (local);Initial Catalog=QuanLyBanHang;Integrated Security=True;TrustServerCertificate=True";

        public SqlConnection conn = null;
        public SqlCommand comm = null;
        public SqlDataAdapter da = null;
        public SqlTransaction tran = null;

        // Ham khoi tao, chuan bi san doi tuong connection va command
        public ConnectDB()
        {
            conn = new SqlConnection(strCon);
            comm = conn.CreateCommand();
        }

        // Mo ket noi neu dang dong
        public void OpenConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Loi khi mo cong ket noi: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Loi khong xac dinh " + ex.Message, ex);
            }
        }

        // Dong ket noi neu dang mo
        public void CloseConnection()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Loi khi dong ket noi " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Loi khong xac dinh " + ex.Message, ex);
            }
        }

        // Bat dau mot giao dich CSDL
        public void BeginTransaction()
        {
            OpenConnection();
            tran = conn.BeginTransaction();
            comm.Transaction = tran;
        }

        // Thuc hien (commit) giao dich hien tai
        public void CommitTransaction()
        {
            if (tran != null)
            {
                tran.Commit();
                tran = null;
            }
            CloseConnection();
        }

        // Huy bo (rollback) giao dich hien tai
        public void RollbackTransaction()
        {
            if (tran != null)
            {
                tran.Rollback();
                tran = null;
            }
            CloseConnection();
        }

        // Thuc thi truy van tra ve DataSet
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)
        {
            // Quan ly trang thai ket noi
            bool wasClosed = conn.State == ConnectionState.Closed;
            if (wasClosed && tran == null)
            {
                OpenConnection();
            }

            try
            {
                comm.CommandText = strSQL;
                comm.CommandType = ct;
                comm.Connection = conn;
                if (tran != null)
                {
                    comm.Transaction = tran;
                }
                // Su dung SqlDataAdapter de lay du lieu
                da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                throw new Exception("Loi truy van du lieu: " + ex.Message, ex);
            }
            finally
            {
                // Dong ket noi
                if (wasClosed && tran == null)
                {
                    CloseConnection();
                }
            }
        }

        // Thuc thi lenh khong tra ve du lieu 
        public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string error)
        {
            bool f = false;
            // Quan ly trang thai 
            bool wasClosed = conn.State == ConnectionState.Closed;
            if (wasClosed && tran == null)
            {
                OpenConnection();
            }

            try
            {
                comm.CommandText = strSQL;
                comm.CommandType = ct;
                comm.Connection = conn;
                if (tran != null)
                {
                    comm.Transaction = tran;
                }
                // Thuc thi lenh
                comm.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                // Tra ve thong bao loi qua tham chieu
                error = ex.Message;
            }
            catch (Exception ex)
            {
                error = "Loi khong xac dinh: " + ex.Message;
            }
            finally
            {
                // Dong ket noi 
                if (wasClosed && tran == null)
                {
                    CloseConnection();
                }
            }
            return f;
        }
    }
}