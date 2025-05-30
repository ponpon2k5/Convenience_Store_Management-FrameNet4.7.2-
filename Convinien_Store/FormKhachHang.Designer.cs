using System.Drawing;
using System.Windows.Forms;

namespace Convenience_Store_Management
{
    partial class FormKhachHang
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
            this.pnlKhachHang = new System.Windows.Forms.Panel();
            this.panelNhanVien = new System.Windows.Forms.Panel();
            this.btnThanhVien = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGioHang = new System.Windows.Forms.Button();
            this.btnSanPham = new System.Windows.Forms.Button();
            this.panelNhanVien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlKhachHang
            // 
            this.pnlKhachHang.Location = new System.Drawing.Point(157, 0);
            this.pnlKhachHang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlKhachHang.Name = "pnlKhachHang";
            this.pnlKhachHang.Size = new System.Drawing.Size(691, 417);
            this.pnlKhachHang.TabIndex = 5;
            // 
            // panelNhanVien
            // 
            this.panelNhanVien.BackColor = System.Drawing.Color.Red;
            this.panelNhanVien.Controls.Add(this.btnThanhVien);
            this.panelNhanVien.Controls.Add(this.btnExit);
            this.panelNhanVien.Controls.Add(this.pictureBox1);
            this.panelNhanVien.Controls.Add(this.btnGioHang);
            this.panelNhanVien.Controls.Add(this.btnSanPham);
            this.panelNhanVien.Location = new System.Drawing.Point(-1, -1);
            this.panelNhanVien.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelNhanVien.Name = "panelNhanVien";
            this.panelNhanVien.Size = new System.Drawing.Size(156, 420);
            this.panelNhanVien.TabIndex = 4;
            // 
            // btnThanhVien
            // 
            this.btnThanhVien.Location = new System.Drawing.Point(21, 238);
            this.btnThanhVien.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThanhVien.Name = "btnThanhVien";
            this.btnThanhVien.Size = new System.Drawing.Size(114, 34);
            this.btnThanhVien.TabIndex = 6;
            this.btnThanhVien.Text = "Thành viên";
            this.btnThanhVien.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(21, 365);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(114, 34);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Convinien_Store.Properties.Resources.convenience_store;
            this.pictureBox1.InitialImage = global::Convinien_Store.Properties.Resources.convenience_store;
            this.pictureBox1.Location = new System.Drawing.Point(21, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(114, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnGioHang
            // 
            this.btnGioHang.Location = new System.Drawing.Point(21, 181);
            this.btnGioHang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGioHang.Name = "btnGioHang";
            this.btnGioHang.Size = new System.Drawing.Size(114, 34);
            this.btnGioHang.TabIndex = 1;
            this.btnGioHang.Text = "Giỏ hàng";
            this.btnGioHang.UseVisualStyleBackColor = true;
            // 
            // btnSanPham
            // 
            this.btnSanPham.Location = new System.Drawing.Point(21, 129);
            this.btnSanPham.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSanPham.Name = "btnSanPham";
            this.btnSanPham.Size = new System.Drawing.Size(114, 34);
            this.btnSanPham.TabIndex = 0;
            this.btnSanPham.Text = "Sản phẩm";
            this.btnSanPham.UseVisualStyleBackColor = true;
            // 
            // FormKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 418);
            this.Controls.Add(this.pnlKhachHang);
            this.Controls.Add(this.panelNhanVien);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormKhachHang";
            this.Text = "FormKhachHang";
            this.panelNhanVien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlKhachHang;
        private Panel panelNhanVien;
        private Button btnExit;
        private PictureBox pictureBox1;
        private Button btnThongKe;
        private Button btnTimKiem;
        private Button btnGioHang;
        private Button btnSanPham;
        private Button btnThanhVien;
    }
}