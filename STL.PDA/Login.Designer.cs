namespace STL.PDA
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.libSet = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular);
            this.chkRememberMe.Location = new System.Drawing.Point(52, 149);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(100, 20);
            this.chkRememberMe.TabIndex = 0;
            this.chkRememberMe.Text = "记住密码";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular);
            this.txtUserID.Location = new System.Drawing.Point(110, 66);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(154, 28);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserID_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular);
            this.txtPassword.Location = new System.Drawing.Point(110, 110);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(154, 28);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btn_login.Location = new System.Drawing.Point(52, 192);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(72, 35);
            this.btn_login.TabIndex = 3;
            this.btn_login.Text = "登录";
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            this.btn_login.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_login_KeyDown);
            // 
            // btn_exit
            // 
            this.btn_exit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btn_exit.Location = new System.Drawing.Point(192, 192);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(72, 35);
            this.btn_exit.TabIndex = 4;
            this.btn_exit.Text = "退出";
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // libSet
            // 
            this.libSet.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Underline);
            this.libSet.Location = new System.Drawing.Point(213, 149);
            this.libSet.Name = "libSet";
            this.libSet.Size = new System.Drawing.Size(51, 20);
            this.libSet.TabIndex = 6;
            this.libSet.Text = "设置";
            this.libSet.Click += new System.EventHandler(this.libSet_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(32, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.Text = "密   码";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(32, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 25);
            this.label2.Text = "用户名";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(318, 270);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.libSet);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.chkRememberMe);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Text = "登录";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.LinkLabel libSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

