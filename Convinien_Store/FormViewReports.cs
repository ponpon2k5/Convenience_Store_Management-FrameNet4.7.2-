using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convinien_Store
{
    public partial class FormViewReports : Form // hoặc UC_ViewReports : UserControl
    {
        public FormViewReports()
        {
            InitializeComponent();
        }

        private void FormViewReports_Load(object sender, EventArgs e)
        {
            // Khởi tạo DataSet và TableAdapter
            StoreReportsDataSet ds = new StoreReportsDataSet();
            StoreReportsDataSetTableAdapters.HoaDonBanOverviewTableAdapter adapter = new StoreReportsDataSetTableAdapters.HoaDonBanOverviewTableAdapter();

            // Đổ dữ liệu vào TableAdapter (nếu có tham số, truyền vào đây)
            adapter.Fill(ds.HoaDonBanOverview); // Gọi phương thức Fill của TableAdapter

            // Cấu hình ReportViewer
            ReportDataSource rds = new ReportDataSource("DataSet1", (DataTable)ds.HoaDonBanOverview); // "DataSet1" phải khớp với tên Dataset trong .rdlc file của bạn
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Convinien_Store.ReportDoanhThu.rdlc"; // Thay đổi nếu tên project hoặc đường dẫn khác

            this.reportViewer1.RefreshReport();
        }
    }
}
