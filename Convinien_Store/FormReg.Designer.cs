
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Convenience_Store_Management
{
    partial class FormReg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool
        disposing)
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.cbShowPwd = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.NhanVienCb = new System.Windows.Forms.CheckBox();
            this.KhachHangCb = new System.Windows.Forms.CheckBox();
            this.sdt_text = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.manv_text = new System.Windows.Forms.TextBox();
            this.manhanvien_label = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(122, 109);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 159);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(279, 118);
            this.txtAccount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(157, 22);
            this.txtAccount.TabIndex = 2;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(279, 161);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(157, 22);
            this.txtPwd.TabIndex = 3;
            // 
            // cbShowPwd
            // 
            this.cbShowPwd.AutoSize = true;
            this.cbShowPwd.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowPwd.Location = new System.Drawing.Point(486, 161);
            this.cbShowPwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbShowPwd.Name = "cbShowPwd";
            this.cbShowPwd.Size = new System.Drawing.Size(189, 38);
            this.cbShowPwd.TabIndex = 4;
            this.cbShowPwd.Text = "Show password";
            this.cbShowPwd.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(122, 320);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(126, 39);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Sign up";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(355, 320);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(77, 39);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 77);
            this.panel1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(22, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(307, 39);
            this.label3.TabIndex = 0;
            this.label3.Text = "Convenience Store";
            // 
            // NhanVienCb
            // 
            this.NhanVienCb.AutoSize = true;
            this.NhanVienCb.Location = new System.Drawing.Point(186, 206);
            this.NhanVienCb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NhanVienCb.Name = "NhanVienCb";
            this.NhanVienCb.Size = new System.Drawing.Size(89, 20);
            this.NhanVienCb.TabIndex = 10;
            this.NhanVienCb.Text = "Nhân viên";
            this.NhanVienCb.UseVisualStyleBackColor = true;
            // 
            // KhachHangCb
            // 
            this.KhachHangCb.AutoSize = true;
            this.KhachHangCb.Location = new System.Drawing.Point(393, 203);
            this.KhachHangCb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.KhachHangCb.Name = "KhachHangCb";
            this.KhachHangCb.Size = new System.Drawing.Size(102, 20);
            this.KhachHangCb.TabIndex = 11;
            this.KhachHangCb.Text = "Khách Hàng";
            this.KhachHangCb.UseVisualStyleBackColor = true;
            // 
            // sdt_text
            // 
            this.sdt_text.Location = new System.Drawing.Point(279, 238);
            this.sdt_text.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sdt_text.Name = "sdt_text";
            this.sdt_text.Size = new System.Drawing.Size(157, 22);
            this.sdt_text.TabIndex = 13;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(122, 238);
            this.label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(54, 34);
            this.label.TabIndex = 12;
            this.label.Text = "SDT";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // manv_text
            // 
            this.manv_text.Location = new System.Drawing.Point(279, 274);
            this.manv_text.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.manv_text.Name = "manv_text";
            this.manv_text.Size = new System.Drawing.Size(157, 22);
            this.manv_text.TabIndex = 15;
            // 
            // manhanvien_label
            // 
            this.manhanvien_label.AutoSize = true;
            this.manhanvien_label.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manhanvien_label.Location = new System.Drawing.Point(122, 274);
            this.manhanvien_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.manhanvien_label.Name = "manhanvien_label";
            this.manhanvien_label.Size = new System.Drawing.Size(78, 34);
            this.manhanvien_label.TabIndex = 14;
            this.manhanvien_label.Text = "Mã NV";
            this.manhanvien_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 395);
            this.Controls.Add(this.manv_text);
            this.Controls.Add(this.manhanvien_label);
            this.Controls.Add(this.sdt_text);
            this.Controls.Add(this.label);
            this.Controls.Add(this.KhachHangCb);
            this.Controls.Add(this.NhanVienCb);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.cbShowPwd);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormReg";
            this.Text = "FormReg";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtAccount;
        private TextBox txtPwd;
        private CheckBox cbShowPwd;
        private Button btnLogin;
        private Button btnExit;
        private Panel panel1;
        private Label label3;
        private CheckBox NhanVienCb;
        private CheckBox KhachHangCb;
        private TextBox sdt_text;
        private Label label;
        private TextBox manv_text;
        private Label manhanvien_label;
    }
}