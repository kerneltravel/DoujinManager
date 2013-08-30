namespace DoujinManager
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.Online = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.cb_o_siteselect = new System.Windows.Forms.ComboBox();
            this.lb_o_search = new System.Windows.Forms.Label();
            this.btn_o_search = new System.Windows.Forms.Button();
            this.tb_o_search = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lv_o = new System.Windows.Forms.ListView();
            this.btn_o_npage = new System.Windows.Forms.Button();
            this.btn_o_ppage = new System.Windows.Forms.Button();
            this.Local = new System.Windows.Forms.TabPage();
            this.Download = new System.Windows.Forms.TabPage();
            this.lv_d = new ListViewEmbeddedControls.ListViewEx();
            this.lv_d_ch_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_d_ch_percent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_d_ch_ProgressBar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_d_ch_downloadurl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_d_ch_realurl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_d_ch_GUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Setting = new System.Windows.Forms.TabPage();
            this.p_s_pic_proxy = new System.Windows.Forms.Panel();
            this.rb_s_pic_proxy_0 = new System.Windows.Forms.RadioButton();
            this.rb_s_pic_proxy_1 = new System.Windows.Forms.RadioButton();
            this.rb_s_pic_proxy_2 = new System.Windows.Forms.RadioButton();
            this.p_s_proxy = new System.Windows.Forms.Panel();
            this.rb_s_proxy_0 = new System.Windows.Forms.RadioButton();
            this.rb_s_proxy_1 = new System.Windows.Forms.RadioButton();
            this.rb_s_proxy_2 = new System.Windows.Forms.RadioButton();
            this.btn_s_defaultFolder = new System.Windows.Forms.Button();
            this.tb_s_defaultFolder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_s_pic_proxy_port = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_s_pic_proxy = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_s_proxy_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_s_proxy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_s_pic_load_timeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_s_pic_load_sametime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cms_lv_item = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_OpenOriPage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_OpenPic = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Download = new System.Windows.Forms.ToolStripMenuItem();
            this.tt_o_lv_lvi = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl.SuspendLayout();
            this.Online.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.Download.SuspendLayout();
            this.Setting.SuspendLayout();
            this.p_s_pic_proxy.SuspendLayout();
            this.p_s_proxy.SuspendLayout();
            this.cms_lv_item.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.Online);
            this.tabControl.Controls.Add(this.Local);
            this.tabControl.Controls.Add(this.Download);
            this.tabControl.Controls.Add(this.Setting);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(981, 597);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseMove);
            // 
            // Online
            // 
            this.Online.Controls.Add(this.splitContainer1);
            this.Online.Location = new System.Drawing.Point(4, 25);
            this.Online.Name = "Online";
            this.Online.Size = new System.Drawing.Size(973, 568);
            this.Online.TabIndex = 2;
            this.Online.Text = "在线";
            this.Online.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.cb_o_siteselect);
            this.splitContainer1.Panel1.Controls.Add(this.lb_o_search);
            this.splitContainer1.Panel1.Controls.Add(this.btn_o_search);
            this.splitContainer1.Panel1.Controls.Add(this.tb_o_search);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(973, 568);
            this.splitContainer1.SplitterDistance = 36;
            this.splitContainer1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(670, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_o_siteselect
            // 
            this.cb_o_siteselect.FormattingEnabled = true;
            this.cb_o_siteselect.Location = new System.Drawing.Point(373, 6);
            this.cb_o_siteselect.Name = "cb_o_siteselect";
            this.cb_o_siteselect.Size = new System.Drawing.Size(169, 23);
            this.cb_o_siteselect.TabIndex = 4;
            // 
            // lb_o_search
            // 
            this.lb_o_search.AutoSize = true;
            this.lb_o_search.Location = new System.Drawing.Point(8, 11);
            this.lb_o_search.Name = "lb_o_search";
            this.lb_o_search.Size = new System.Drawing.Size(37, 15);
            this.lb_o_search.TabIndex = 1;
            this.lb_o_search.Text = "搜索";
            // 
            // btn_o_search
            // 
            this.btn_o_search.Location = new System.Drawing.Point(548, 6);
            this.btn_o_search.Name = "btn_o_search";
            this.btn_o_search.Size = new System.Drawing.Size(75, 23);
            this.btn_o_search.TabIndex = 3;
            this.btn_o_search.Text = "搜索";
            this.btn_o_search.UseVisualStyleBackColor = true;
            this.btn_o_search.Click += new System.EventHandler(this.btn_o_search_Click);
            // 
            // tb_o_search
            // 
            this.tb_o_search.Location = new System.Drawing.Point(51, 5);
            this.tb_o_search.Name = "tb_o_search";
            this.tb_o_search.Size = new System.Drawing.Size(316, 25);
            this.tb_o_search.TabIndex = 2;
            this.tb_o_search.Text = "<--ch-->";
            this.tb_o_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_o_search_KeyDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lv_o);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btn_o_npage);
            this.splitContainer2.Panel2.Controls.Add(this.btn_o_ppage);
            this.splitContainer2.Size = new System.Drawing.Size(973, 528);
            this.splitContainer2.SplitterDistance = 490;
            this.splitContainer2.TabIndex = 0;
            // 
            // lv_o
            // 
            this.lv_o.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_o.Location = new System.Drawing.Point(0, 0);
            this.lv_o.Name = "lv_o";
            this.lv_o.Size = new System.Drawing.Size(973, 490);
            this.lv_o.TabIndex = 5;
            this.lv_o.UseCompatibleStateImageBehavior = false;
            this.lv_o.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_o_MouseDoubleClick);
            this.lv_o.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lv_o_MouseDown);
            this.lv_o.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lv_o_MouseMove);
            // 
            // btn_o_npage
            // 
            this.btn_o_npage.Enabled = false;
            this.btn_o_npage.Location = new System.Drawing.Point(89, 4);
            this.btn_o_npage.Name = "btn_o_npage";
            this.btn_o_npage.Size = new System.Drawing.Size(75, 23);
            this.btn_o_npage.TabIndex = 1;
            this.btn_o_npage.Text = "下一页";
            this.btn_o_npage.UseVisualStyleBackColor = true;
            this.btn_o_npage.Click += new System.EventHandler(this.btn_o_npage_Click);
            // 
            // btn_o_ppage
            // 
            this.btn_o_ppage.Enabled = false;
            this.btn_o_ppage.Location = new System.Drawing.Point(8, 4);
            this.btn_o_ppage.Name = "btn_o_ppage";
            this.btn_o_ppage.Size = new System.Drawing.Size(75, 23);
            this.btn_o_ppage.TabIndex = 0;
            this.btn_o_ppage.Text = "上一页";
            this.btn_o_ppage.UseVisualStyleBackColor = true;
            this.btn_o_ppage.Click += new System.EventHandler(this.btn_o_ppage_Click);
            // 
            // Local
            // 
            this.Local.Location = new System.Drawing.Point(4, 25);
            this.Local.Name = "Local";
            this.Local.Padding = new System.Windows.Forms.Padding(3);
            this.Local.Size = new System.Drawing.Size(973, 568);
            this.Local.TabIndex = 0;
            this.Local.Text = "本地";
            this.Local.UseVisualStyleBackColor = true;
            // 
            // Download
            // 
            this.Download.Controls.Add(this.lv_d);
            this.Download.Location = new System.Drawing.Point(4, 25);
            this.Download.Name = "Download";
            this.Download.Padding = new System.Windows.Forms.Padding(3);
            this.Download.Size = new System.Drawing.Size(973, 568);
            this.Download.TabIndex = 1;
            this.Download.Text = "下载";
            this.Download.UseVisualStyleBackColor = true;
            // 
            // lv_d
            // 
            this.lv_d.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lv_d_ch_name,
            this.lv_d_ch_percent,
            this.lv_d_ch_ProgressBar,
            this.lv_d_ch_downloadurl,
            this.lv_d_ch_realurl,
            this.lv_d_ch_GUID});
            this.lv_d.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_d.FullRowSelect = true;
            this.lv_d.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_d.Location = new System.Drawing.Point(3, 3);
            this.lv_d.MultiSelect = false;
            this.lv_d.Name = "lv_d";
            this.lv_d.Size = new System.Drawing.Size(967, 562);
            this.lv_d.TabIndex = 1;
            this.lv_d.UseCompatibleStateImageBehavior = false;
            this.lv_d.View = System.Windows.Forms.View.Details;
            // 
            // lv_d_ch_name
            // 
            this.lv_d_ch_name.Text = "名称";
            this.lv_d_ch_name.Width = 315;
            // 
            // lv_d_ch_percent
            // 
            this.lv_d_ch_percent.Text = "%";
            this.lv_d_ch_percent.Width = 77;
            // 
            // lv_d_ch_ProgressBar
            // 
            this.lv_d_ch_ProgressBar.Text = "下载进度";
            this.lv_d_ch_ProgressBar.Width = 136;
            // 
            // lv_d_ch_downloadurl
            // 
            this.lv_d_ch_downloadurl.Text = "引用地址";
            this.lv_d_ch_downloadurl.Width = 77;
            // 
            // lv_d_ch_realurl
            // 
            this.lv_d_ch_realurl.Text = "下载地址";
            this.lv_d_ch_realurl.Width = 76;
            // 
            // lv_d_ch_GUID
            // 
            this.lv_d_ch_GUID.Text = "GUID";
            this.lv_d_ch_GUID.Width = 55;
            // 
            // Setting
            // 
            this.Setting.Controls.Add(this.p_s_pic_proxy);
            this.Setting.Controls.Add(this.p_s_proxy);
            this.Setting.Controls.Add(this.btn_s_defaultFolder);
            this.Setting.Controls.Add(this.tb_s_defaultFolder);
            this.Setting.Controls.Add(this.label9);
            this.Setting.Controls.Add(this.label6);
            this.Setting.Controls.Add(this.tb_s_pic_proxy_port);
            this.Setting.Controls.Add(this.label7);
            this.Setting.Controls.Add(this.tb_s_pic_proxy);
            this.Setting.Controls.Add(this.label8);
            this.Setting.Controls.Add(this.label5);
            this.Setting.Controls.Add(this.tb_s_proxy_port);
            this.Setting.Controls.Add(this.label4);
            this.Setting.Controls.Add(this.tb_s_proxy);
            this.Setting.Controls.Add(this.label3);
            this.Setting.Controls.Add(this.tb_s_pic_load_timeout);
            this.Setting.Controls.Add(this.label2);
            this.Setting.Controls.Add(this.tb_s_pic_load_sametime);
            this.Setting.Controls.Add(this.label1);
            this.Setting.Location = new System.Drawing.Point(4, 25);
            this.Setting.Name = "Setting";
            this.Setting.Padding = new System.Windows.Forms.Padding(3);
            this.Setting.Size = new System.Drawing.Size(973, 568);
            this.Setting.TabIndex = 1;
            this.Setting.Text = "设置";
            this.Setting.UseVisualStyleBackColor = true;
            // 
            // p_s_pic_proxy
            // 
            this.p_s_pic_proxy.Controls.Add(this.rb_s_pic_proxy_0);
            this.p_s_pic_proxy.Controls.Add(this.rb_s_pic_proxy_1);
            this.p_s_pic_proxy.Controls.Add(this.rb_s_pic_proxy_2);
            this.p_s_pic_proxy.Location = new System.Drawing.Point(78, 111);
            this.p_s_pic_proxy.Name = "p_s_pic_proxy";
            this.p_s_pic_proxy.Size = new System.Drawing.Size(305, 25);
            this.p_s_pic_proxy.TabIndex = 24;
            // 
            // rb_s_pic_proxy_0
            // 
            this.rb_s_pic_proxy_0.AutoSize = true;
            this.rb_s_pic_proxy_0.Location = new System.Drawing.Point(3, 3);
            this.rb_s_pic_proxy_0.Name = "rb_s_pic_proxy_0";
            this.rb_s_pic_proxy_0.Size = new System.Drawing.Size(73, 19);
            this.rb_s_pic_proxy_0.TabIndex = 13;
            this.rb_s_pic_proxy_0.TabStop = true;
            this.rb_s_pic_proxy_0.Text = "不启用";
            this.rb_s_pic_proxy_0.UseVisualStyleBackColor = true;
            this.rb_s_pic_proxy_0.Click += new System.EventHandler(this.rb_s_pic_proxy_0_Click);
            // 
            // rb_s_pic_proxy_1
            // 
            this.rb_s_pic_proxy_1.AutoSize = true;
            this.rb_s_pic_proxy_1.Location = new System.Drawing.Point(82, 3);
            this.rb_s_pic_proxy_1.Name = "rb_s_pic_proxy_1";
            this.rb_s_pic_proxy_1.Size = new System.Drawing.Size(104, 19);
            this.rb_s_pic_proxy_1.TabIndex = 14;
            this.rb_s_pic_proxy_1.TabStop = true;
            this.rb_s_pic_proxy_1.Text = "使用IE设置";
            this.rb_s_pic_proxy_1.UseVisualStyleBackColor = true;
            this.rb_s_pic_proxy_1.Click += new System.EventHandler(this.rb_s_pic_proxy_1_Click);
            // 
            // rb_s_pic_proxy_2
            // 
            this.rb_s_pic_proxy_2.AutoSize = true;
            this.rb_s_pic_proxy_2.Location = new System.Drawing.Point(191, 3);
            this.rb_s_pic_proxy_2.Name = "rb_s_pic_proxy_2";
            this.rb_s_pic_proxy_2.Size = new System.Drawing.Size(103, 19);
            this.rb_s_pic_proxy_2.TabIndex = 15;
            this.rb_s_pic_proxy_2.TabStop = true;
            this.rb_s_pic_proxy_2.Text = "自定义代理";
            this.rb_s_pic_proxy_2.UseVisualStyleBackColor = true;
            this.rb_s_pic_proxy_2.Click += new System.EventHandler(this.rb_s_pic_proxy_2_Click);
            // 
            // p_s_proxy
            // 
            this.p_s_proxy.Controls.Add(this.rb_s_proxy_0);
            this.p_s_proxy.Controls.Add(this.rb_s_proxy_1);
            this.p_s_proxy.Controls.Add(this.rb_s_proxy_2);
            this.p_s_proxy.Location = new System.Drawing.Point(78, 76);
            this.p_s_proxy.Name = "p_s_proxy";
            this.p_s_proxy.Size = new System.Drawing.Size(305, 25);
            this.p_s_proxy.TabIndex = 23;
            // 
            // rb_s_proxy_0
            // 
            this.rb_s_proxy_0.AutoSize = true;
            this.rb_s_proxy_0.Location = new System.Drawing.Point(3, 3);
            this.rb_s_proxy_0.Name = "rb_s_proxy_0";
            this.rb_s_proxy_0.Size = new System.Drawing.Size(73, 19);
            this.rb_s_proxy_0.TabIndex = 5;
            this.rb_s_proxy_0.TabStop = true;
            this.rb_s_proxy_0.Text = "不启用";
            this.rb_s_proxy_0.UseVisualStyleBackColor = true;
            this.rb_s_proxy_0.Click += new System.EventHandler(this.rb_s_proxy_0_Click);
            // 
            // rb_s_proxy_1
            // 
            this.rb_s_proxy_1.AutoSize = true;
            this.rb_s_proxy_1.Location = new System.Drawing.Point(82, 3);
            this.rb_s_proxy_1.Name = "rb_s_proxy_1";
            this.rb_s_proxy_1.Size = new System.Drawing.Size(104, 19);
            this.rb_s_proxy_1.TabIndex = 6;
            this.rb_s_proxy_1.TabStop = true;
            this.rb_s_proxy_1.Text = "使用IE设置";
            this.rb_s_proxy_1.UseVisualStyleBackColor = true;
            this.rb_s_proxy_1.Click += new System.EventHandler(this.rb_s_proxy_1_Click);
            // 
            // rb_s_proxy_2
            // 
            this.rb_s_proxy_2.AutoSize = true;
            this.rb_s_proxy_2.Location = new System.Drawing.Point(191, 3);
            this.rb_s_proxy_2.Name = "rb_s_proxy_2";
            this.rb_s_proxy_2.Size = new System.Drawing.Size(103, 19);
            this.rb_s_proxy_2.TabIndex = 7;
            this.rb_s_proxy_2.TabStop = true;
            this.rb_s_proxy_2.Text = "自定义代理";
            this.rb_s_proxy_2.UseVisualStyleBackColor = true;
            this.rb_s_proxy_2.Click += new System.EventHandler(this.rb_s_proxy_2_Click);
            // 
            // btn_s_defaultFolder
            // 
            this.btn_s_defaultFolder.Location = new System.Drawing.Point(459, 145);
            this.btn_s_defaultFolder.Name = "btn_s_defaultFolder";
            this.btn_s_defaultFolder.Size = new System.Drawing.Size(75, 23);
            this.btn_s_defaultFolder.TabIndex = 22;
            this.btn_s_defaultFolder.Text = "选择";
            this.btn_s_defaultFolder.UseVisualStyleBackColor = true;
            this.btn_s_defaultFolder.Click += new System.EventHandler(this.btn_s_defaultFolder_Click);
            // 
            // tb_s_defaultFolder
            // 
            this.tb_s_defaultFolder.Location = new System.Drawing.Point(111, 144);
            this.tb_s_defaultFolder.Name = "tb_s_defaultFolder";
            this.tb_s_defaultFolder.Size = new System.Drawing.Size(338, 25);
            this.tb_s_defaultFolder.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 15);
            this.label9.TabIndex = 20;
            this.label9.Text = "默认保存路径";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(544, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "端口";
            // 
            // tb_s_pic_proxy_port
            // 
            this.tb_s_pic_proxy_port.Location = new System.Drawing.Point(586, 111);
            this.tb_s_pic_proxy_port.Name = "tb_s_pic_proxy_port";
            this.tb_s_pic_proxy_port.Size = new System.Drawing.Size(53, 25);
            this.tb_s_pic_proxy_port.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(392, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "地址";
            // 
            // tb_s_pic_proxy
            // 
            this.tb_s_pic_proxy.Location = new System.Drawing.Point(434, 111);
            this.tb_s_pic_proxy.Name = "tb_s_pic_proxy";
            this.tb_s_pic_proxy.Size = new System.Drawing.Size(100, 25);
            this.tb_s_pic_proxy.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 12;
            this.label8.Text = "图片代理";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(544, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "端口";
            // 
            // tb_s_proxy_port
            // 
            this.tb_s_proxy_port.Location = new System.Drawing.Point(586, 76);
            this.tb_s_proxy_port.Name = "tb_s_proxy_port";
            this.tb_s_proxy_port.Size = new System.Drawing.Size(53, 25);
            this.tb_s_proxy_port.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(392, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "地址";
            // 
            // tb_s_proxy
            // 
            this.tb_s_proxy.Location = new System.Drawing.Point(434, 76);
            this.tb_s_proxy.Name = "tb_s_proxy";
            this.tb_s_proxy.Size = new System.Drawing.Size(100, 25);
            this.tb_s_proxy.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "全局代理";
            // 
            // tb_s_pic_load_timeout
            // 
            this.tb_s_pic_load_timeout.Location = new System.Drawing.Point(196, 41);
            this.tb_s_pic_load_timeout.Name = "tb_s_pic_load_timeout";
            this.tb_s_pic_load_timeout.Size = new System.Drawing.Size(63, 25);
            this.tb_s_pic_load_timeout.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "图片加载等待时间（毫秒）";
            // 
            // tb_s_pic_load_sametime
            // 
            this.tb_s_pic_load_sametime.Location = new System.Drawing.Point(126, 7);
            this.tb_s_pic_load_sametime.Name = "tb_s_pic_load_sametime";
            this.tb_s_pic_load_sametime.Size = new System.Drawing.Size(36, 25);
            this.tb_s_pic_load_sametime.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "同时加载图片数";
            // 
            // cms_lv_item
            // 
            this.cms_lv_item.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_OpenOriPage,
            this.tsmi_OpenPic,
            this.tsmi_Copy,
            this.toolStripSeparator1,
            this.tsmi_Download});
            this.cms_lv_item.Name = "cms_lv_item";
            this.cms_lv_item.Size = new System.Drawing.Size(154, 106);
            // 
            // tsmi_OpenOriPage
            // 
            this.tsmi_OpenOriPage.Name = "tsmi_OpenOriPage";
            this.tsmi_OpenOriPage.Size = new System.Drawing.Size(153, 24);
            this.tsmi_OpenOriPage.Text = "打开原网页";
            this.tsmi_OpenOriPage.Click += new System.EventHandler(this.tsmi_OpenOriPage_Click);
            // 
            // tsmi_OpenPic
            // 
            this.tsmi_OpenPic.Name = "tsmi_OpenPic";
            this.tsmi_OpenPic.Size = new System.Drawing.Size(153, 24);
            this.tsmi_OpenPic.Text = "打开图片";
            this.tsmi_OpenPic.Click += new System.EventHandler(this.tsmi_OpenPic_Click);
            // 
            // tsmi_Copy
            // 
            this.tsmi_Copy.Name = "tsmi_Copy";
            this.tsmi_Copy.Size = new System.Drawing.Size(153, 24);
            this.tsmi_Copy.Text = "复制";
            this.tsmi_Copy.Click += new System.EventHandler(this.tsmi_Copy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // tsmi_Download
            // 
            this.tsmi_Download.Name = "tsmi_Download";
            this.tsmi_Download.Size = new System.Drawing.Size(153, 24);
            this.tsmi_Download.Text = "下载";
            this.tsmi_Download.Click += new System.EventHandler(this.tsmi_Download_Click);
            // 
            // tt_o_lv_lvi
            // 
            this.tt_o_lv_lvi.AutoPopDelay = 5000;
            this.tt_o_lv_lvi.InitialDelay = 500;
            this.tt_o_lv_lvi.ReshowDelay = 100;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 597);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Doujin Manager";
            this.tabControl.ResumeLayout(false);
            this.Online.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.Download.ResumeLayout(false);
            this.Setting.ResumeLayout(false);
            this.Setting.PerformLayout();
            this.p_s_pic_proxy.ResumeLayout(false);
            this.p_s_pic_proxy.PerformLayout();
            this.p_s_proxy.ResumeLayout(false);
            this.p_s_proxy.PerformLayout();
            this.cms_lv_item.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage Online;
        private System.Windows.Forms.TabPage Local;
        private System.Windows.Forms.TabPage Download;
        private System.Windows.Forms.TabPage Setting;
        private System.Windows.Forms.Label lb_o_search;
        private System.Windows.Forms.TextBox tb_o_search;
        private System.Windows.Forms.Button btn_o_search;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView lv_o;
        private System.Windows.Forms.Button btn_o_npage;
        private System.Windows.Forms.Button btn_o_ppage;
        private System.Windows.Forms.ComboBox cb_o_siteselect;
        private System.Windows.Forms.ContextMenuStrip cms_lv_item;
        private System.Windows.Forms.ToolStripMenuItem tsmi_OpenOriPage;
        private System.Windows.Forms.ToolStripMenuItem tsmi_OpenPic;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Copy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Download;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip tt_o_lv_lvi;
        private ListViewEmbeddedControls.ListViewEx lv_d;
        private System.Windows.Forms.ColumnHeader lv_d_ch_name;
        private System.Windows.Forms.ColumnHeader lv_d_ch_percent;
        private System.Windows.Forms.ColumnHeader lv_d_ch_ProgressBar;
        private System.Windows.Forms.ColumnHeader lv_d_ch_downloadurl;
        private System.Windows.Forms.ColumnHeader lv_d_ch_realurl;
        private System.Windows.Forms.ColumnHeader lv_d_ch_GUID;
        private System.Windows.Forms.TextBox tb_s_pic_load_timeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_s_pic_load_sametime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_s_proxy_port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_s_proxy;
        private System.Windows.Forms.RadioButton rb_s_proxy_2;
        private System.Windows.Forms.RadioButton rb_s_proxy_1;
        private System.Windows.Forms.RadioButton rb_s_proxy_0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_s_pic_proxy_port;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_s_pic_proxy;
        private System.Windows.Forms.RadioButton rb_s_pic_proxy_2;
        private System.Windows.Forms.RadioButton rb_s_pic_proxy_1;
        private System.Windows.Forms.RadioButton rb_s_pic_proxy_0;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_s_defaultFolder;
        private System.Windows.Forms.TextBox tb_s_defaultFolder;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel p_s_proxy;
        private System.Windows.Forms.Panel p_s_pic_proxy;


    }
}

