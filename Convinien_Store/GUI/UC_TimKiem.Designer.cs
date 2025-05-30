using System;
using System.Drawing;
using System.Windows.Forms;
namespace Convenience_Store_Management.GUI
{
    partial class UC_TimKiem
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSuaHH = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTimHH = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnTimHD = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnTimKH = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.txtMaHH = new System.Windows.Forms.TextBox();
            this.txtGiaBanMoi = new System.Windows.Forms.TextBox();
            this.txtGiaNhapMoi = new System.Windows.Forms.TextBox();
            this.txtSoLuongMoi = new System.Windows.Forms.TextBox();
            this.btnViewInvoiceDetails = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(686, 412);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSoLuongMoi);
            this.tabPage1.Controls.Add(this.txtGiaNhapMoi);
            this.tabPage1.Controls.Add(this.txtGiaBanMoi);
            this.tabPage1.Controls.Add(this.txtMaHH);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btnSuaHH);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnTimHH);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(678, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hàng hóa";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(305, 106);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 68);
            this.label7.TabIndex = 15;
            this.label7.Text = "Số lượng mới:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(305, 58);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 68);
            this.label6.TabIndex = 13;
            this.label6.Text = "Giá nhập mới:";
            // 
            // btnSuaHH
            // 
            this.btnSuaHH.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaHH.Location = new System.Drawing.Point(544, 150);
            this.btnSuaHH.Margin = new System.Windows.Forms.Padding(2);
            this.btnSuaHH.Name = "btnSuaHH";
            this.btnSuaHH.Size = new System.Drawing.Size(114, 43);
            this.btnSuaHH.TabIndex = 12;
            this.btnSuaHH.Text = "Sửa";
            this.btnSuaHH.UseVisualStyleBackColor = true;
            this.btnSuaHH.Click += new System.EventHandler(this.btnSuaHH_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(304, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 68);
            this.label3.TabIndex = 10;
            this.label3.Text = "Giá bán mới:";
            // 
            // btnTimHH
            // 
            this.btnTimHH.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimHH.Location = new System.Drawing.Point(163, 50);
            this.btnTimHH.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimHH.Name = "btnTimHH";
            this.btnTimHH.Size = new System.Drawing.Size(109, 47);
            this.btnTimHH.TabIndex = 10;
            this.btnTimHH.Text = "Tìm kiếm";
            this.btnTimHH.UseVisualStyleBackColor = true;
            this.btnTimHH.Click += new System.EventHandler(this.btnTimHH_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 68);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mã hàng hóa:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 198);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(670, 184);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnViewInvoiceDetails);
            this.tabPage3.Controls.Add(this.btnTimHD);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txtMaHD);
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(678, 383);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Hóa đơn";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnTimHD
            // 
            this.btnTimHD.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimHD.Location = new System.Drawing.Point(468, 55);
            this.btnTimHD.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimHD.Name = "btnTimHD";
            this.btnTimHD.Size = new System.Drawing.Size(128, 56);
            this.btnTimHD.TabIndex = 13;
            this.btnTimHD.Text = "Tìm kiếm";
            this.btnTimHD.UseVisualStyleBackColor = true;
            this.btnTimHD.Click += new System.EventHandler(this.btnTimHD_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 68);
            this.label1.TabIndex = 12;
            this.label1.Text = "Mã hóa đơn";
            // 
            // txtMaHD
            // 
            this.txtMaHD.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaHD.Location = new System.Drawing.Point(168, 50);
            this.txtMaHD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(274, 61);
            this.txtMaHD.TabIndex = 11;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(5, 198);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.Size = new System.Drawing.Size(670, 184);
            this.dataGridView3.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnTimKH);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.textBox1);
            this.tabPage4.Controls.Add(this.dataGridView4);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(678, 383);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Khách hàng";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnTimKH
            // 
            this.btnTimKH.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKH.Location = new System.Drawing.Point(202, 141);
            this.btnTimKH.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimKH.Name = "btnTimKH";
            this.btnTimKH.Size = new System.Drawing.Size(94, 34);
            this.btnTimKH.TabIndex = 13;
            this.btnTimKH.Text = "Tìm kiếm";
            this.btnTimKH.UseVisualStyleBackColor = true;
            this.btnTimKH.Click += new System.EventHandler(this.btnTimKH_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 68);
            this.label2.TabIndex = 12;
            this.label2.Text = "SDT khách hàng:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(202, 53);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(274, 61);
            this.textBox1.TabIndex = 11;
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(5, 198);
            this.dataGridView4.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 51;
            this.dataGridView4.Size = new System.Drawing.Size(670, 186);
            this.dataGridView4.TabIndex = 1;
            // 
            // txtMaHH
            // 
            this.txtMaHH.Location = new System.Drawing.Point(37, 58);
            this.txtMaHH.Name = "txtMaHH";
            this.txtMaHH.Size = new System.Drawing.Size(100, 22);
            this.txtMaHH.TabIndex = 17;
            // 
            // txtGiaBanMoi
            // 
            this.txtGiaBanMoi.Location = new System.Drawing.Point(474, 25);
            this.txtGiaBanMoi.Name = "txtGiaBanMoi";
            this.txtGiaBanMoi.Size = new System.Drawing.Size(175, 22);
            this.txtGiaBanMoi.TabIndex = 18;
            // 
            // txtGiaNhapMoi
            // 
            this.txtGiaNhapMoi.Location = new System.Drawing.Point(474, 75);
            this.txtGiaNhapMoi.Name = "txtGiaNhapMoi";
            this.txtGiaNhapMoi.Size = new System.Drawing.Size(175, 22);
            this.txtGiaNhapMoi.TabIndex = 19;
            // 
            // txtSoLuongMoi
            // 
            this.txtSoLuongMoi.Location = new System.Drawing.Point(474, 123);
            this.txtSoLuongMoi.Name = "txtSoLuongMoi";
            this.txtSoLuongMoi.Size = new System.Drawing.Size(175, 22);
            this.txtSoLuongMoi.TabIndex = 20;
            // 
            // btnViewInvoiceDetails
            // 
            this.btnViewInvoiceDetails.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewInvoiceDetails.Location = new System.Drawing.Point(14, 126);
            this.btnViewInvoiceDetails.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewInvoiceDetails.Name = "btnViewInvoiceDetails";
            this.btnViewInvoiceDetails.Size = new System.Drawing.Size(200, 56);
            this.btnViewInvoiceDetails.TabIndex = 14;
            this.btnViewInvoiceDetails.Text = "Xem Hóa Đơn";
            this.btnViewInvoiceDetails.UseVisualStyleBackColor = true;
            this.btnViewInvoiceDetails.Click += new System.EventHandler(this.btnViewInvoiceDetails_Click);
            // 
            // UC_TimKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UC_TimKiem";
            this.Size = new System.Drawing.Size(691, 417);
            this.Load += new System.EventHandler(this.UC_TimKiem_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private DataGridView dataGridView1;
        private TabPage tabPage3;
        private DataGridView dataGridView3;
        private TabPage tabPage4;
        private DataGridView dataGridView4;
        private Button btnTimHH;
        private Label label5;
        private Button btnTimHD;
        private Label label1;
        private TextBox txtMaHD;
        private Button btnTimKH;
        private Label label2;
        private TextBox textBox1;
        private Label label7;
        private Label label6;
        private Button btnSuaHH;
        private Label label3;
        private TextBox txtMaHH;
        private TextBox txtSoLuongMoi;
        private TextBox txtGiaNhapMoi;
        private TextBox txtGiaBanMoi;
        private Button btnViewInvoiceDetails;
    }
}