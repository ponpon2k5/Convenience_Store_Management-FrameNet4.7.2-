using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data; // Cần thiết cho DataTable

namespace Convenience_Store_Management // Thay bằng namespace chính xác của project bạn
{
    public partial class FormViewSalesInvoice : Form
    {
        // Có thể thêm constructor để nhận MaHoaDonBan nếu bạn muốn lọc
        private string _maHoaDonBanFilter = null;

        public FormViewSalesInvoice(string maHoaDonBan = null)
        {
            InitializeComponent();
            _maHoaDonBanFilter = maHoaDonBan;
        }

        private void FormViewSalesInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo DataSet và TableAdapter
                // Thay 'Convinien_Store.StoreReportsDataSet' bằng namespace và tên DataSet của bạn
                Convinien_Store.StoreReportsDataSet ds = new Convinien_Store.StoreReportsDataSet();
                Convinien_Store.StoreReportsDataSetTableAdapters.InvoiceDetailsTableAdapter adapter = new Convinien_Store.StoreReportsDataSetTableAdapters.InvoiceDetailsTableAdapter();

                if (!string.IsNullOrEmpty(_maHoaDonBanFilter))
                {
                    // Đổ dữ liệu vào TableAdapter với tham số nếu có
                    adapter.FillByInvoiceData(ds.InvoiceDetails, _maHoaDonBanFilter); // Giả sử bạn đã thêm tham số @MaHoaDonBan vào FillByInvoiceData
                }
                else
                {
                    // Đổ tất cả dữ liệu nếu không có bộ lọc
                    adapter.Fill(ds.InvoiceDetails);
                }

                // Cấu hình ReportDataSource. "dsInvoiceDetails" phải khớp với tên Dataset bạn đặt trong .rdlc file!
                ReportDataSource rds = new ReportDataSource("dsInvoiceDetails", (DataTable)ds.InvoiceDetails);

                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);

                // Đặt đường dẫn tới file .rdlc của bạn
                // Thay 'Convinien_Store.ReportSalesInvoice.rdlc' bằng đường dẫn chính xác
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Convinien_Store.ReportSalesInvoice.rdlc";

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}