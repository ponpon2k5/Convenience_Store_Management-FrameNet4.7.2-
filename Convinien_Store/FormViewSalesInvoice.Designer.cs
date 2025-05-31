// Convinien_Store/FormViewSalesInvoice.Designer.cs
namespace Convinien_Store
{
    partial class FormViewSalesInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            //
            // reportViewer1
            //
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Convinien_Store.HoaDonBan.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0); // Đặt lại Location về 0,0 để nó bắt đầu từ góc trên bên trái
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(739, 332); // Kích thước ban đầu, sẽ được tự động điều chỉnh
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill; // <<-- THÊM HOẶC SỬA DÒNG NÀY

            //
            // FormViewSalesInvoice
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450); // Kích thước ban đầu của Form
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormViewSalesInvoice";
            this.Text = "FormViewSalesInvoice";
            this.Load += new System.EventHandler(this.FormViewReports_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}