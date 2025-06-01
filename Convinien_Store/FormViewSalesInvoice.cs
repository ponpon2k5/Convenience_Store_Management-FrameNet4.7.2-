
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using QLBanHang_3Tang.BS_layer; 

namespace Convinien_Store 
{
    public partial class FormViewSalesInvoice : Form
    {
        private BLHoaDonBan blHoaDonBan = new BLHoaDonBan(); 
        private string _maHoaDonBan; 

        // Xem mot hoa don cu the
        public FormViewSalesInvoice(string maHoaDonBan)
        {
            InitializeComponent();
            _maHoaDonBan = maHoaDonBan; // Luu lai ma hoa don can xem
        }

        //  xem TAT CA hoa don
        public FormViewSalesInvoice()
        {
            InitializeComponent();
            _maHoaDonBan = null; // Dat la null de biet la can lay tat ca hoa don
        }

        private void FormViewReports_Load(object sender, EventArgs e)
        {
            try
            {
                // Xoa cac nguon du lieu cu cua ReportViewer (neu co)
                this.reportViewer1.LocalReport.DataSources.Clear();

                string error = ""; 
                DataSet dsReportData = null;

                if (string.IsNullOrEmpty(_maHoaDonBan)) // xem tat ca
                {
                    dsReportData = blHoaDonBan.LayTatCaChiTietHoaDon(ref error); 
                }
                else 
                {
                    dsReportData = blHoaDonBan.LayChiTietHoaDon(_maHoaDonBan, ref error);
                }

                // Kiem tra xem co du lieu de hien thi khong
                if (dsReportData != null && dsReportData.Tables.Count > 0 && dsReportData.Tables[0].Rows.Count > 0)
                {
                    ReportDataSource rds = new ReportDataSource("DataTable1", dsReportData.Tables[0]);
                    this.reportViewer1.LocalReport.DataSources.Add(rds); // Them nguon du lieu vao ReportViewer
                }
                else
                {
                    MessageBox.Show($"Khong co du lieu de hien thi bao cao Loi chi tiet: {error}", "Loi Tai Bao Cao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; 
                }

                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Convinien_Store.ReportSalesInvoice.rdlc"; 

                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout); // Chuyen sang che do xem bo cuc in
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth; // Tu dong dieu chinh zoom theo chieu rong trang

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Da xay ra loi khi tai bao cao: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}