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
        private string _maHoaDonBan; // Giữ lại cho trường hợp xem hóa đơn cụ thể

        // Constructor cũ: Dùng để xem một hóa đơn cụ thể
        public FormViewSalesInvoice(string maHoaDonBan)
        {
            InitializeComponent();
            _maHoaDonBan = maHoaDonBan;
        }

        // Constructor mới: Dùng để xem tất cả hóa đơn
        public FormViewSalesInvoice()
        {
            InitializeComponent();
            _maHoaDonBan = null; // Đặt là null để biểu thị rằng chúng ta muốn lấy tất cả hóa đơn
        }

        private void FormViewReports_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.LocalReport.DataSources.Clear();

                string error = "";
                DataSet dsReportData = null;

                // Logic để quyết định lấy hóa đơn cụ thể hay tất cả hóa đơn
                if (string.IsNullOrEmpty(_maHoaDonBan))
                {
                    // Trường hợp xem tất cả hóa đơn
                    MessageBox.Show($"Đang cố gắng tải báo cáo cho TẤT CẢ hóa đơn.", "Thông tin gỡ lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dsReportData = blHoaDonBan.LayTatCaChiTietHoaDon(ref error); // Gọi phương thức mới để lấy tất cả
                }
                else
                {
                    // Trường hợp xem hóa đơn cụ thể
                    MessageBox.Show($"Đang cố gắng tải báo cáo cho MaHoaDonBan: '{_maHoaDonBan}'", "Thông tin gỡ lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dsReportData = blHoaDonBan.LayChiTietHoaDon(_maHoaDonBan, ref error);
                }


                if (dsReportData != null && dsReportData.Tables.Count > 0 && dsReportData.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show($"Tìm thấy {dsReportData.Tables[0].Rows.Count} hàng dữ liệu cho báo cáo.", "Thông tin gỡ lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // "DataTable1" phải khớp với tên của dataset trong tệp .rdlc của bạn.
                    ReportDataSource rds = new ReportDataSource("DataTable1", dsReportData.Tables[0]);
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                }
                else
                {
                    // Thêm chi tiết lỗi vào thông báo nếu không có dữ liệu
                    MessageBox.Show($"Không có dữ liệu để hiển thị báo cáo. Lỗi chi tiết: {error}", "Lỗi Tải Báo Cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Đảm bảo "Build Action" của tệp .rdlc của bạn được đặt thành "Embedded Resource".
                // CẬP NHẬT TÊN TỆP RDLC ĐÃ ĐỔI TÊN Ở ĐÂY
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Convinien_Store.ReportSalesInvoice.rdlc"; // Đã sửa tên file .rdlc

                // <<-- THÊM CÁC DÒNG NÀY ĐỂ TỐI ƯU HIỂN THỊ
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout); // Chế độ bố cục in
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth; // Tự động zoom theo chiều rộng trang
                // <<-- KẾT THÚC CÁC DÒNG MỚI

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}