using System;
using System.Drawing;
using System.Windows.Forms;
namespace Convenience_Store_Management
{
    partial class FormLogin
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 106);
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
            this.label2.Location = new System.Drawing.Point(102, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            //
            // txtAccount
            //
            this.txtAccount.Location = new System.Drawing.Point(266, 106);
            this.txtAccount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(157, 22);
            this.txtAccount.TabIndex = 2;
            //
            // txtPwd
            //
            this.txtPwd.Location = new System.Drawing.Point(266, 159);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(157, 22);
            this.txtPwd.TabIndex = 3;
            //
            // cbShowPwd
            //
            this.cbShowPwd.AutoSize = true;
            this.cbShowPwd.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowPwd.Location = new System.Drawing.Point(465, 150);
            this.cbShowPwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbShowPwd.Name = "cbShowPwd";
            this.cbShowPwd.Size = new System.Drawing.Size(189, 38);
            this.cbShowPwd.TabIndex = 4;
            this.cbShowPwd.Text = "Show password";
            this.cbShowPwd.UseVisualStyleBackColor = true;
            this.cbShowPwd.CheckedChanged += new System.EventHandler(this.cbShowPwd_CheckedChanged); // ADDED
            //
            // btnLogin
            //
            this.btnLogin.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(108, 261);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(87, 47);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click); // ADDED
            //
            // btnExit
            //
            this.btnExit.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(486, 259);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(68, 49);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click); // ADDED
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(641, 77);
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
            this.NhanVienCb.Location = new System.Drawing.Point(157, 206);
            this.NhanVienCb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NhanVienCb.Name = "NhanVienCb";
            this.NhanVienCb.Size = new System.Drawing.Size(89, 20);
            this.NhanVienCb.TabIndex = 11;
            this.NhanVienCb.Text = "Nhân viên";
            this.NhanVienCb.UseVisualStyleBackColor = true;
            this.NhanVienCb.CheckedChanged += new System.EventHandler(this.NhanVienCb_CheckedChanged); // ADDED
            //
            // KhachHangCb
            //
            this.KhachHangCb.AutoSize = true;
            this.KhachHangCb.Location = new System.Drawing.Point(392, 206);
            this.KhachHangCb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.KhachHangCb.Name = "KhachHangCb";
            this.KhachHangCb.Size = new System.Drawing.Size(102, 20);
            this.KhachHangCb.TabIndex = 12;
            this.KhachHangCb.Text = "Khách Hàng";
            this.KhachHangCb.UseVisualStyleBackColor = true;
            this.KhachHangCb.CheckedChanged += new System.EventHandler(this.KhachHangCb_CheckedChanged); // ADDED
            //
            // button1
            //
            this.button1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(285, 261);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 47);
            this.button1.TabIndex = 13;
            this.button1.Text = "Sign up";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click); // ADDED
            //
            // FormLogin
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 346);
            this.Controls.Add(this.button1);
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
            this.Name = "FormLogin";
            this.Text = "FormLogin";
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
        private Button button1;
    }
}