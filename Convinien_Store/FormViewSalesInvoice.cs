using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms; // Required for ReportViewer
using QLBanHang_3Tang.BS_layer; // Assuming QLBanHang_3Tang.BS_layer is where your BLHoaDonBan resides

namespace Convinien_Store
{
    public partial class FormViewSalesInvoice : Form
    {
        private BLHoaDonBan blHoaDonBan = new BLHoaDonBan(); // Instantiate your Business Logic Layer for reports

        public FormViewSalesInvoice()
        {
            InitializeComponent();
        }

        private void FormViewReports_Load(object sender, EventArgs e)
        {
            try
            {
                // Clear any existing data sources
                this.reportViewer1.LocalReport.DataSources.Clear();

                // Fetch data using your Business Logic Layer
                string error = "";
                // You might want to pass parameters to LayDoanhThu if you have report filtering (e.g., "Tháng", "Tuần")
                // For simplicity, let's assume we are fetching all sales data for now,
                // or you can add a constructor to FormViewReports to accept a filter type.
                // For this example, let's fetch 'monthly' data, or adjust as per your report's need.
                // If the report should show ALL sales, you might need a new method in BLHoaDonBan or modify existing one.
                DataSet dsReportData = blHoaDonBan.LayDoanhThu("Tháng", ref error); // Example: Fetch monthly revenue

                if (dsReportData != null && dsReportData.Tables.Count > 0)
                {
                    // Create a ReportDataSource. "DataSet1" must match the name of the dataset in your .rdlc file.
                    ReportDataSource rds = new ReportDataSource("DataSet1", dsReportData.Tables[0]);
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                }
                else
                {
                    MessageBox.Show($"Không có dữ liệu để hiển thị báo cáo: {error}", "Lỗi Tải Báo Cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Exit if no data
                }

                // Set the reportEmbeddedResource property.
                // This path refers to the location of your .rdlc file within your project.
                // Make sure the "Build Action" of your .rdlc file is set to "Embedded Resource".
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Convinien_Store.ReportDoanhThu.rdlc"; //

                // Refresh the report viewer
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}