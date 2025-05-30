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
        private string _maHoaDonBan; // Private field to store the invoice ID

        // Modified constructor to accept maHoaDonBan
        public FormViewSalesInvoice(string maHoaDonBan)
        {
            InitializeComponent();
            _maHoaDonBan = maHoaDonBan; // Store the passed invoice ID
        }

        // Removed the parameterless constructor.

        private void FormViewReports_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.LocalReport.DataSources.Clear();

                string error = "";
                // Use the stored invoice ID to fetch specific invoice details
                DataSet dsReportData = blHoaDonBan.LayChiTietHoaDon(_maHoaDonBan, ref error);

                if (dsReportData != null && dsReportData.Tables.Count > 0)
                {
                    // "DataTable1" must match the name of the dataset in your .rdlc file.
                    ReportDataSource rds = new ReportDataSource("DataTable1", dsReportData.Tables[0]);
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                }
                else
                {
                    MessageBox.Show($"Không có dữ liệu để hiển thị báo cáo cho hóa đơn {_maHoaDonBan}: {error}", "Lỗi Tải Báo Cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Make sure the "Build Action" of your .rdlc file is set to "Embedded Resource".
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Convinien_Store.HoaDonBan.rdlc";

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}