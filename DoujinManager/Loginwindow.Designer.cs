namespace DoujinManager
{
    partial class Loginwindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loginwindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_soulplus = new System.Windows.Forms.TabPage();
            this.ll_soulplus_reg = new System.Windows.Forms.LinkLabel();
            this.tb_soulplus_password = new System.Windows.Forms.TextBox();
            this.tb_soulplus_username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tp_hentai = new System.Windows.Forms.TabPage();
            this.ll_hentai_reg = new System.Windows.Forms.LinkLabel();
            this.tb_hentai_password = new System.Windows.Forms.TextBox();
            this.tb_hentai_username = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tp_soulplus.SuspendLayout();
            this.tp_hentai.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.btn_login);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(494, 190);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_soulplus);
            this.tabControl1.Controls.Add(this.tp_hentai);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(494, 125);
            this.tabControl1.TabIndex = 0;
            // 
            // tp_soulplus
            // 
            this.tp_soulplus.Controls.Add(this.ll_soulplus_reg);
            this.tp_soulplus.Controls.Add(this.tb_soulplus_password);
            this.tp_soulplus.Controls.Add(this.tb_soulplus_username);
            this.tp_soulplus.Controls.Add(this.label3);
            this.tp_soulplus.Controls.Add(this.label2);
            this.tp_soulplus.Location = new System.Drawing.Point(4, 25);
            this.tp_soulplus.Name = "tp_soulplus";
            this.tp_soulplus.Padding = new System.Windows.Forms.Padding(3);
            this.tp_soulplus.Size = new System.Drawing.Size(486, 96);
            this.tp_soulplus.TabIndex = 0;
            this.tp_soulplus.Text = "魂+(soulplus)";
            this.tp_soulplus.UseVisualStyleBackColor = true;
            // 
            // ll_soulplus_reg
            // 
            this.ll_soulplus_reg.AutoSize = true;
            this.ll_soulplus_reg.Location = new System.Drawing.Point(280, 21);
            this.ll_soulplus_reg.Name = "ll_soulplus_reg";
            this.ll_soulplus_reg.Size = new System.Drawing.Size(37, 15);
            this.ll_soulplus_reg.TabIndex = 4;
            this.ll_soulplus_reg.TabStop = true;
            this.ll_soulplus_reg.Text = "注册";
            this.ll_soulplus_reg.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ll_soulplus_reg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_soulplus_reg_LinkClicked);
            // 
            // tb_soulplus_password
            // 
            this.tb_soulplus_password.Location = new System.Drawing.Point(79, 57);
            this.tb_soulplus_password.Name = "tb_soulplus_password";
            this.tb_soulplus_password.PasswordChar = '*';
            this.tb_soulplus_password.Size = new System.Drawing.Size(185, 25);
            this.tb_soulplus_password.TabIndex = 3;
            this.tb_soulplus_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_soulplus_password_KeyDown);
            // 
            // tb_soulplus_username
            // 
            this.tb_soulplus_username.Location = new System.Drawing.Point(79, 16);
            this.tb_soulplus_username.Name = "tb_soulplus_username";
            this.tb_soulplus_username.Size = new System.Drawing.Size(185, 25);
            this.tb_soulplus_username.TabIndex = 2;
            this.tb_soulplus_username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_soulplus_username_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名";
            // 
            // tp_hentai
            // 
            this.tp_hentai.Controls.Add(this.ll_hentai_reg);
            this.tp_hentai.Controls.Add(this.tb_hentai_password);
            this.tp_hentai.Controls.Add(this.tb_hentai_username);
            this.tp_hentai.Controls.Add(this.label4);
            this.tp_hentai.Controls.Add(this.label5);
            this.tp_hentai.Location = new System.Drawing.Point(4, 25);
            this.tp_hentai.Name = "tp_hentai";
            this.tp_hentai.Padding = new System.Windows.Forms.Padding(3);
            this.tp_hentai.Size = new System.Drawing.Size(486, 96);
            this.tp_hentai.TabIndex = 1;
            this.tp_hentai.Text = "E-Hentai";
            this.tp_hentai.UseVisualStyleBackColor = true;
            // 
            // ll_hentai_reg
            // 
            this.ll_hentai_reg.AutoSize = true;
            this.ll_hentai_reg.Location = new System.Drawing.Point(280, 21);
            this.ll_hentai_reg.Name = "ll_hentai_reg";
            this.ll_hentai_reg.Size = new System.Drawing.Size(207, 15);
            this.ll_hentai_reg.TabIndex = 9;
            this.ll_hentai_reg.TabStop = true;
            this.ll_hentai_reg.Text = "注册（21:00~2:00关闭注册）";
            this.ll_hentai_reg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_hentai_reg_LinkClicked);
            // 
            // tb_hentai_password
            // 
            this.tb_hentai_password.Location = new System.Drawing.Point(79, 57);
            this.tb_hentai_password.Name = "tb_hentai_password";
            this.tb_hentai_password.PasswordChar = '*';
            this.tb_hentai_password.Size = new System.Drawing.Size(185, 25);
            this.tb_hentai_password.TabIndex = 8;
            this.tb_hentai_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_hentai_password_KeyDown);
            // 
            // tb_hentai_username
            // 
            this.tb_hentai_username.Location = new System.Drawing.Point(79, 16);
            this.tb_hentai_username.Name = "tb_hentai_username";
            this.tb_hentai_username.Size = new System.Drawing.Size(185, 25);
            this.tb_hentai_username.TabIndex = 7;
            this.tb_hentai_username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_hentai_username_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "密码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "用户名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(348, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "在国内IP下E-hentai 21:00~2:00 无法登录、注册";
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(391, 7);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 23);
            this.btn_login.TabIndex = 1;
            this.btn_login.Text = "登录";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(322, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "只有你拥有对应的账户，才能使用该网站的功能";
            // 
            // Loginwindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(494, 190);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Loginwindow";
            this.Text = "登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Loginwindow_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tp_soulplus.ResumeLayout(false);
            this.tp_soulplus.PerformLayout();
            this.tp_hentai.ResumeLayout(false);
            this.tp_hentai.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_soulplus;
        private System.Windows.Forms.LinkLabel ll_soulplus_reg;
        private System.Windows.Forms.TextBox tb_soulplus_password;
        private System.Windows.Forms.TextBox tb_soulplus_username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tp_hentai;
        private System.Windows.Forms.LinkLabel ll_hentai_reg;
        private System.Windows.Forms.TextBox tb_hentai_password;
        private System.Windows.Forms.TextBox tb_hentai_username;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;


    }
}