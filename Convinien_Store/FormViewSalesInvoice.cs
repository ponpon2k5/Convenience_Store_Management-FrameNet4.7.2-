// Convinien_Store/FormViewSalesInvoice.cs
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

        public FormViewSalesInvoice(string maHoaDonBan)
        {
            InitializeComponent();
            _maHoaDonBan = maHoaDonBan;
        }

        private void FormViewReports_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.LocalReport.DataSources.Clear();

                string error = "";
                // Hiển thị MaHoaDonBan được truyền vào để gỡ lỗi
                MessageBox.Show($"Đang cố gắng tải báo cáo cho MaHoaDonBan: '{_maHoaDonBan}'", "Thông tin gỡ lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sử dụng invoice ID đã lưu trữ để lấy chi tiết hóa đơn cụ thể
                DataSet dsReportData = blHoaDonBan.LayChiTietHoaDon(_maHoaDonBan, ref error);

                if (dsReportData != null && dsReportData.Tables.Count > 0 && dsReportData.Tables[0].Rows.Count > 0)
                {
                    // Hiển thị số lượng hàng được trả về để gỡ lỗi
                    MessageBox.Show($"Tìm thấy {dsReportData.Tables[0].Rows.Count} hàng dữ liệu cho báo cáo.", "Thông tin gỡ lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // "DataTable1" phải khớp với tên của dataset trong tệp .rdlc của bạn.
                    ReportDataSource rds = new ReportDataSource("DataTable1", dsReportData.Tables[0]);
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                }
                else
                {
                    // Thêm chi tiết lỗi vào thông báo nếu không có dữ liệu
                    MessageBox.Show($"Không có dữ liệu để hiển thị báo cáo cho hóa đơn {_maHoaDonBan}. Lỗi chi tiết: {error}", "Lỗi Tải Báo Cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Đảm bảo "Build Action" của tệp .rdlc của bạn được đặt thành "Embedded Resource".
                // CẬP NHẬT TÊN TỆP RDLC ĐÃ ĐỔI TÊN Ở ĐÂY
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Convinien_Store.ReportSalesInvoice.rdlc"; // Đã sửa tên file .rdlc

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}