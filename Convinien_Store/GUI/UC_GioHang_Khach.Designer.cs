﻿using System;
using System.Drawing;
using System.Windows.Forms;
namespace Convenience_Store_Management.GUI
{
    partial class UC_GioHang_Khach
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
            this.btnXoaGioHang = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.TongTien_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXoaGioHang
            // 
            this.btnXoaGioHang.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaGioHang.Location = new System.Drawing.Point(10, 297);
            this.btnXoaGioHang.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoaGioHang.Name = "btnXoaGioHang";
            this.btnXoaGioHang.Size = new System.Drawing.Size(118, 36);
            this.btnXoaGioHang.TabIndex = 20;
            this.btnXoaGioHang.Text = "Xóa khỏi giỏ hàng";
            this.btnXoaGioHang.UseVisualStyleBackColor = true;
            this.btnXoaGioHang.Click += new System.EventHandler(this.btnXoaGioHang_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(374, 297);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 36);
            this.button1.TabIndex = 21;
            this.button1.Text = "Thanh toán";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 54);
            this.label1.TabIndex = 22;
            this.label1.Text = "Giỏ hàng:";
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Location = new System.Drawing.Point(2, 68);
            this.dgvGioHang.Margin = new System.Windows.Forms.Padding(2);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.RowHeadersWidth = 51;
            this.dgvGioHang.Size = new System.Drawing.Size(512, 214);
            this.dgvGioHang.TabIndex = 23;
            // 
            // TongTien_label
            // 
            this.TongTien_label.AutoSize = true;
            this.TongTien_label.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TongTien_label.Location = new System.Drawing.Point(255, 12);
            this.TongTien_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TongTien_label.Name = "TongTien_label";
            this.TongTien_label.Size = new System.Drawing.Size(96, 54);
            this.TongTien_label.TabIndex = 24;
            this.TongTien_label.Text = "Tổng tiền:";
            // 
            // UC_GioHang_Khach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TongTien_label);
            this.Controls.Add(this.dgvGioHang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnXoaGioHang);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UC_GioHang_Khach";
            this.Size = new System.Drawing.Size(514, 366);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnXoaGioHang;
        private Button button1;
        private Label label1;
        private DataGridView dgvGioHang;
        private Label TongTien_label;
    }
}