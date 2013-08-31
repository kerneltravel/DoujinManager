using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;



namespace DoujinManager
{
    public partial class MainWindow : Form
    {
        private ImageList il_o = new ImageList();
        private DoujinSite[] DSList = new DoujinSite[2];
        private List<Doujinshi> search_result_object_list = new List<Doujinshi>();
        private ConcurrentDictionary<Guid, Doujinshi> download_object_dict = new ConcurrentDictionary<Guid, Doujinshi>();
        private Settings setting;
        private String string_doujinsite;
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        private Boolean parIsRunning = false;
        private Int32 parcomp1=0;
        private Int32 parcomp2=0;
        private System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();
        private Object parcompLock = new Object();
        private Object downlistLock = new Object();
        private Boolean tt_active = false;

        public MainWindow()
        {
            Regex.CacheSize = 25;
            InitializeComponent();
            InitializeSetting();
            if (!File.Exists("Configuration/cookie.xml"))
            {
                Loginwindow lw = new Loginwindow();
                lw.SendSettings += (sender, e) =>
                {
                    setting.soulplus_enabled = e.soulplus_enabled;
                    setting.hentai_enabled = e.hentai_enabled;
                };
                lw.ShowDialog();
            }
            if (setting.soulplus_enabled) { cb_o_siteselect.Items.Add("soul-plus.net"); }
            if (setting.hentai_enabled) { cb_o_siteselect.Items.Add("E-hentai.org"); }
            if (cb_o_siteselect.Items.Count != 0) { cb_o_siteselect.Text = cb_o_siteselect.Items[0] as String; }
            //Universe.SaveSettings(setting, "Configuration/settings.xml");
            if (setting.use_proxy == 0) { WebRequest.DefaultWebProxy = null; }
            if (setting.use_proxy == 2) { WebRequest.DefaultWebProxy = new WebProxy(setting.proxy, setting.proxy_port); }
            if (setting.use_proxy == 1) { WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy(); }
            il_o.ImageSize = new System.Drawing.Size(setting.Thumbnail_Width, setting.Thumbnail_Height);
            il_o.ColorDepth = ColorDepth.Depth32Bit;
            DSList[0] = new DoujinSite(Global.urls.hentai, CookieUtility.ReadCookie("Configuration/cookie.xml"));
            DSList[1] = new DoujinSite(Global.urls.soulplus, CookieUtility.ReadCookie("Configuration/cookie.xml"));
            DSList[0].Messager += (sender, e) => MessageBox.Show(e.msg);
            DSList[1].Messager += (sender, e) => MessageBox.Show(e.msg);
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += refreshTimer_Tick;
            refreshTimer.Enabled = true;
            lv_o.LargeImageList = il_o;
        }

        #region Search

        private int read_DoujinSite(string string_doujinsite)
        {
            int i = 0;
            switch (string_doujinsite)
            {
                case "E-hentai.org":
                    i = 0;
                    break;
                case "soul-plus.net":
                    i = 1;
                    break;
            }
            return i;
        }

        private void search(string string_doujinsite)
        {
            Task.Factory.StartNew((o) =>
            {
                this.Invoke((Action)delegate { this.lv_o.Cursor = Cursors.AppStarting; });
                Match f = Regex.Match(tb_o_search.Text, "(?<=^<--).+(?=-->$)");
                bool badkeyword = false;
                List<string[]> search_result;
                if (f.Success) { 
                    string fid = null;
                    switch (f.Value)
                    {
                        case "ch":
                            fid = "36";
                            break;
                        case "C84":
                            fid = "79";
                            break;
                        default:
                            if (Global.IsNum(f.Value)) { fid = f.Value; }
                            else { MessageBox.Show("搜索语法错误！"); badkeyword = true; }
                            break;
                    }
                    if (badkeyword) { search_result = new List<string[]>(); }
                    else { search_result = DSList[(int)o].List(fid, DSList[(int)o].search_currect_page); }
                }
                else { search_result = DSList[(int)o].Search(tb_o_search.Text, DSList[(int)o].search_currect_page); }

                if (DSList[(int)o].search_currect_page > 1) { this.Invoke((Action)delegate { btn_o_ppage.Enabled = true; }); }
                else { this.Invoke((Action)delegate { btn_o_ppage.Enabled = false; }); }

                if (DSList[(int)o].search_currect_page < DSList[(int)o].search_all_page) { this.Invoke((Action)delegate { btn_o_npage.Enabled = true; }); }
                else { this.Invoke((Action)delegate { btn_o_npage.Enabled = false; }); }

                search_result_object_list.Clear();
                foreach (string[] search_resultd in search_result)
                {
                    Doujinshi djs = new Doujinshi();
                    if (DSList[(int)o].NameFormatter(search_resultd[0], ref djs.name, ref djs.author, ref djs.party, ref djs.localization_group, ref djs.tag))
                    {
                        djs.ori_name = search_resultd[0];
                        djs.download_url = search_resultd[1];
                        search_result_object_list.Add(djs);
                    }
                    //else { MessageBox.Show(search_resultd[0]); }
                }
                while (parcomp1 != parcomp2) { Thread.Sleep(100); } 
                this.Invoke((Action)delegate
                {
                    il_o.Images.Clear();
                    this.lv_o.BeginUpdate();
                    for (int i = 0; i < search_result_object_list.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = search_result_object_list[i].name;
                        this.lv_o.Items.Add(lvi);
                        il_o.Images.Add(Image.FromFile("empty.png"));
                    }
                    this.lv_o.EndUpdate();
                });
                
                cancelToken = new CancellationTokenSource(); 
                parcomp1 = search_result_object_list.Count;
                parcomp2 = 0;
                ParallelOptions parOpts = new ParallelOptions();
                parOpts.CancellationToken = cancelToken.Token;
                parOpts.MaxDegreeOfParallelism = setting.pic_load_sametime;
                parIsRunning = true;
                try
                {
                    Parallel.For(0, search_result_object_list.Count, parOpts, i =>
                    {
                        parOpts.CancellationToken.ThrowIfCancellationRequested();
                        search_result_object_list[i].cover_url = DSList[(int)o].Extract_pic(search_result_object_list[i].download_url);
                        Image tmp_cover_data = null;
                        try
                        {
                            HttpWebRequest picrequest = WebRequest.Create(search_result_object_list[i].cover_url) as HttpWebRequest;
                            picrequest.Timeout = setting.pic_load_timeout;
                            if (setting.use_pic_proxy == 0) { picrequest.Proxy = null; }
                            if (setting.use_pic_proxy == 1) { picrequest.Proxy = WebRequest.GetSystemWebProxy(); }
                            if (setting.use_pic_proxy == 2) { picrequest.Proxy = new WebProxy(setting.pic_proxy, setting.pic_proxy_port); }
                            search_result_object_list[i].cover = Image.FromStream(picrequest.GetResponse().GetResponseStream());
                            tmp_cover_data = PicUtility.MakePic(search_result_object_list[i].cover, setting.Thumbnail_Width, setting.Thumbnail_Height, "#FFFFFF");
                        }
                        catch
                        {
                            tmp_cover_data = Image.FromFile("empty.png");
                        }
                        this.Invoke((Action)delegate {
                            try
                            {
                                il_o.Images[i] = tmp_cover_data;
                                lv_o.LargeImageList = il_o;
                                lv_o.RedrawItems(i, i, false);
                            }
                            catch { }
                            lock (parcompLock)
                            {
                                parcomp2 += 1;
                            }
                            if (parcomp2 == parcomp1) { this.lv_o.Cursor = Cursors.Default; }
                        });
                    });
                }
                catch
                {
                    lock (parcompLock)
                    {
                        parcomp2 = parcomp1;
                    }
                }
            }, read_DoujinSite(string_doujinsite));
        }

        private void btn_o_search_Click(object sender, EventArgs e)
        {
            if (setting.use_proxy == 0) { WebRequest.DefaultWebProxy = null; }
            if (setting.use_proxy == 2) { WebRequest.DefaultWebProxy = new WebProxy(setting.proxy, setting.proxy_port); }
            if (setting.use_proxy == 1) { WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy(); }

            string_doujinsite = cb_o_siteselect.Text;
            btn_o_ppage.Enabled = false;
            btn_o_npage.Enabled = false;
            lv_o.Items.Clear();
            int i = read_DoujinSite(string_doujinsite);
            DSList[i].search_currect_page = 1;
            if (parIsRunning) { cancelToken.Cancel(); parIsRunning = false; }
            search(string_doujinsite);
        }

        private void btn_o_ppage_Click(object sender, EventArgs e)
        {
            if (setting.use_proxy == 0) { WebRequest.DefaultWebProxy = null; }
            if (setting.use_proxy == 2) { WebRequest.DefaultWebProxy = new WebProxy(setting.proxy, setting.proxy_port); }
            if (setting.use_proxy == 1) { WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy(); }

            btn_o_ppage.Enabled = false;
            btn_o_npage.Enabled = false;
            int i = read_DoujinSite(string_doujinsite);
            if (DSList[i].search_currect_page > 1)
            {
                DSList[i].search_currect_page -= 1;
                lv_o.Items.Clear();
                if (parIsRunning) { cancelToken.Cancel(); parIsRunning = false; }
                search(string_doujinsite);
            }
        }

        private void btn_o_npage_Click(object sender, EventArgs e)
        {
            if (setting.use_proxy == 0) { WebRequest.DefaultWebProxy = null; }
            if (setting.use_proxy == 2) { WebRequest.DefaultWebProxy = new WebProxy(setting.proxy, setting.proxy_port); }
            if (setting.use_proxy == 1) { WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy(); }

            btn_o_ppage.Enabled = false;
            btn_o_npage.Enabled = false;
            int i = read_DoujinSite(string_doujinsite);
            if (DSList[i].search_currect_page < DSList[i].search_all_page)
            {
                DSList[i].search_currect_page += 1;
                lv_o.Items.Clear();
                if (parIsRunning) { cancelToken.Cancel(); parIsRunning = false; }
                search(string_doujinsite);
            }
        }

        private void lv_o_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = this.lv_o.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    this.lv_o.ContextMenuStrip = cms_lv_item;
                }
                else
                {
                    this.lv_o.ContextMenuStrip = null;
                }
            }
        }

        private void lv_o_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = this.lv_o.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                System.Diagnostics.Process.Start(search_result_object_list[item.Index].download_url);
            }
        }

        private void tsmi_OpenOriPage_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lv_o.SelectedItems)
            {
                System.Diagnostics.Process.Start(search_result_object_list[lvi.Index].download_url);
            }
        }

        private void tsmi_OpenPic_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lv_o.SelectedItems)
            {
                System.Diagnostics.Process.Start(search_result_object_list[lvi.Index].cover_url);
            }
        }

        private void tsmi_Copy_Click(object sender, EventArgs e)
        {
            string dsname = "";
            foreach (ListViewItem lvi in lv_o.SelectedItems)
            {
                dsname += search_result_object_list[lvi.Index].name + System.Environment.NewLine;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Clipboard.Clear();
                Clipboard.SetText(dsname);
                this.Cursor = Cursors.Default;
            }
            catch (System.Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tsmi_Download_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lv_o.SelectedItems)
            {
                int indexd = 0;
                indexd = lvi.Index;
                Task.Factory.StartNew(o =>
                {
                    try
                    {
                        int index = (int)o;
                        search_result_object_list[index].file_url = DSList[read_DoujinSite(string_doujinsite)].GetDownLink(search_result_object_list[index].download_url);
                        search_result_object_list[index].passwd = DSList[read_DoujinSite(string_doujinsite)].GetPassword(search_result_object_list[index].download_url);
                        if (search_result_object_list[index].file_url.Count == 0) { throw new Exception("无法找到下载地址！请联系作者报告此错误！"); }
                        switch (System.Convert.ToInt32(search_result_object_list[index].file_url[0][0]))
                        {
                            case 2:

                                search_result_object_list[index].direct_download_url = DownloadSite.howfile(System.Convert.ToString(search_result_object_list[index].file_url[0][1]), setting.howfile_server, ref search_result_object_list[index].howfile_cookies);
                                break;
                            case 3:
                                search_result_object_list[index].direct_download_url = DownloadSite.jbpan(System.Convert.ToString(search_result_object_list[index].file_url[0][1]));
                                break;
                            case 4:
                                search_result_object_list[index].direct_download_url = DownloadSite.baidupan(System.Convert.ToString(search_result_object_list[index].file_url[0][1]), search_result_object_list[index].passwd);
                                break;
                        }
                        download(search_result_object_list[index]);
                    }
                    catch (Exception ed) { MessageBox.Show(ed.Message); }
                }, indexd);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Doujinshi djs = new Doujinshi();
            djs.file_url = new List<System.Collections.ArrayList>();
            djs.file_url.Add(new System.Collections.ArrayList());
            djs.file_url[0].Add(Global.downmethod.howfile);
            djs.file_url[0].Add("http://howfile.com/file/luanqibazou/d187f8f7/");
            djs.direct_download_url = DownloadSite.howfile(System.Convert.ToString(djs.file_url[0][1]), ref djs.howfile_cookies);
            djs.name = "aaaaaaa";
            download(djs);*/
            //this.tt_o_lv_lvi.SetToolTip(this.lv_o, "aaaaa");

        }

        private void tb_o_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btn_o_search_Click(sender, e); }
        }

        private void lv_o_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = this.lv_o.GetItemAt(e.X, e.Y);
            if (lvi != null)
            {
                if (!tt_active)
                {
                    Doujinshi djs = search_result_object_list[lvi.Index];
                    this.tt_o_lv_lvi.Show("名称：" + djs.name + System.Environment.NewLine +
                                          "作者：" + djs.author + System.Environment.NewLine +
                                          "首发展会：" + djs.party + System.Environment.NewLine +
                                          "汉化组：" + djs.localization_group + System.Environment.NewLine +
                                          "标签：" + string.Join(",", djs.tag.ToArray()) + System.Environment.NewLine, this.lv_o, e.X, e.Y - 47);
                    tt_active = true;
                }
            }
            else { this.tt_o_lv_lvi.RemoveAll(); tt_active = false; }
        }

        private void tabControl_MouseMove(object sender, MouseEventArgs e)
        {
            this.tt_o_lv_lvi.RemoveAll(); tt_active = false;
        }

        #endregion

        #region Download

        void refreshTimer_Tick(object sender, EventArgs e)
        {
            lv_d.BeginUpdate();
            foreach (ListViewItem lvife in lv_d.Items)
            {
                long percentl = download_object_dict[Guid.Parse(lvife.SubItems[5].Text)].down_percent;
                if (percentl != 0)
                {
                    ProgressBar pbo = lv_d.GetEmbeddedControl(2, lvife.Index) as ProgressBar;
                    if (pbo.Maximum < System.Convert.ToInt32(percentl))
                    {
                        System.Random rand = new Random();
                        rand.NextDouble();
                        pbo.Maximum += System.Convert.ToInt32(percentl) + System.Convert.ToInt32(500000d * rand.NextDouble());
                    }
                    pbo.Value = System.Convert.ToInt32(percentl);
                    float percent = System.Convert.ToSingle(percentl) / System.Convert.ToSingle(pbo.Maximum) * 100f;
                    NumberFormatInfo nfi = CultureInfo.CurrentUICulture.NumberFormat;
                    lvife.SubItems[1].Text = percent.ToString("N", nfi) + "%";
                }
            }
            lv_d.EndUpdate();
            Application.DoEvents();
        }

        private void download(Doujinshi djs)
        {
            Guid GUID = Guid.NewGuid();
            this.Invoke((Action)delegate
            {
                ProgressBar pb = new ProgressBar();
                ListViewItem lvi = new ListViewItem();
                lvi.Text = djs.name;
                lvi.SubItems.Add("正在连接");
                lvi.SubItems.Add("");
                lvi.SubItems.Add(djs.download_url);
                lvi.SubItems.Add(djs.direct_download_url);
                lvi.SubItems.Add(GUID.ToString());
                this.lv_d.Items.Add(lvi);
                this.lv_d.AddEmbeddedControl(pb, 2, lv_d.Items.Count - 1);
            });
            if (download_object_dict.TryAdd(GUID, djs))
            {
                Task.Factory.StartNew((o) =>
                {
                    Guid guido = (Guid)o;
                    Doujinshi djsd = null;
                    if (download_object_dict.TryGetValue(guido, out djsd))
                    {
                        DownLoadFile dlfile = new DownLoadFile();
                        dlfile.DownSuccessEventHandler += (sender, e) =>
                        {
                            if (System.Convert.ToBoolean(e.msg))
                            {
                                //MessageBox.Show("文件下载成功！");
                                this.Invoke((Action)delegate
                                {
                                    foreach (ListViewItem lvife in lv_d.Items)
                                    {
                                        if (lvife.SubItems[5].Text == guido.ToString()) { lvife.Remove(); break; }
                                    }
                                    if (lv_d.Items.Count == 0) { refreshTimer.Stop(); }
                                });
                            }
                            else
                            {
                                MessageBox.Show("文件下载失败！" + System.Environment.NewLine +
                                                "文件名：" + djsd.name + System.Environment.NewLine +
                                                "GUID：" + guido.ToString());
                            }
                        };
                        dlfile.FileBytesEventHandler += (sender, e) => this.Invoke((Action)delegate
                        {
                            //MessageBox.Show("开始下载！");
                            foreach (ListViewItem lvife in lv_d.Items)
                            {
                                if (lvife.SubItems[5].Text == guido.ToString())
                                {
                                    ProgressBar pbo = lv_d.GetEmbeddedControl(2, lvife.Index) as ProgressBar;
                                    pbo.Maximum = System.Convert.ToInt32(e.msg);
                                    break;
                                }
                            }
                        });
                        dlfile.PercentEventHandler += (sender, e) => download_object_dict[guido].down_percent = System.Convert.ToInt64(e.msg);
                        if (!(Directory.Exists(setting.defaultFolder)))
                        {
                            try
                            {
                                Directory.CreateDirectory(setting.defaultFolder);
                            }
                            catch
                            {
                                MessageBox.Show("无法创建文件！下载失败！");
                            }
                        }
                        string ext = "";
                        string filename = setting.defaultFolder + new Regex(@"[<>/\|:""*?]").Replace("[" + djsd.author + "]" + djsd.name + "[" + djsd.localization_group + "]", "_");
                        dlfile.DownloadFile(filename, djsd.direct_download_url, (String)djsd.file_url[0][1], null, ref ext, download_object_dict[guido].howfile_cookies);
                        File.Move(filename, filename + "." + ext);
                    }
                }, GUID);
            }
        }

        #endregion

        #region Setting

        private void InitializeSetting()
        {
            try { setting = Universe.LoadSettings("Configuration/settings.xml"); }
            catch
            {
                setting = new Settings();
            }
            tb_s_pic_load_sametime.Text = System.Convert.ToString(setting.pic_load_sametime);
            tb_s_pic_load_timeout.Text = System.Convert.ToString(setting.pic_load_timeout);
            tb_s_proxy.Text = setting.proxy;
            tb_s_proxy_port.Text = System.Convert.ToString(setting.proxy_port);
            tb_s_pic_proxy.Text = setting.pic_proxy;
            tb_s_pic_proxy_port.Text = System.Convert.ToString(setting.pic_proxy_port);
            tb_s_defaultFolder.Text = setting.defaultFolder;
            switch (setting.use_proxy)
            {
                case 0:
                    rb_s_proxy_0.Select();
                    tb_s_proxy.Enabled = false;
                    tb_s_proxy_port.Enabled = false;
                    break;
                case 1:
                    rb_s_proxy_1.Select();
                    tb_s_proxy.Enabled = false;
                    tb_s_proxy_port.Enabled = false;
                    break;
                case 2:
                    rb_s_proxy_2.Select();
                    tb_s_proxy.Enabled = true;
                    tb_s_proxy_port.Enabled = true;
                    break;
            }
            switch (setting.use_pic_proxy)
            {
                case 0:
                    rb_s_pic_proxy_0.Select();
                    tb_s_pic_proxy.Enabled = false;
                    tb_s_pic_proxy_port.Enabled = false;
                    break;
                case 1:
                    rb_s_pic_proxy_1.Select();
                    tb_s_pic_proxy.Enabled = false;
                    tb_s_pic_proxy_port.Enabled = false;
                    break;
                case 2:
                    rb_s_pic_proxy_2.Select();
                    tb_s_pic_proxy.Enabled = true;
                    tb_s_pic_proxy_port.Enabled = true;
                    break;
            }
        }

        private void rb_s_proxy_0_Click(object sender, EventArgs e)
        {
            tb_s_proxy.Enabled = false;
            tb_s_proxy_port.Enabled = false;
        }

        private void rb_s_proxy_1_Click(object sender, EventArgs e)
        {
            tb_s_proxy.Enabled = false;
            tb_s_proxy_port.Enabled = false;
        }

        private void rb_s_proxy_2_Click(object sender, EventArgs e)
        {
            tb_s_proxy.Enabled = true;
            tb_s_proxy_port.Enabled = true;
        }

        private void rb_s_pic_proxy_0_Click(object sender, EventArgs e)
        {
            tb_s_pic_proxy.Enabled = false;
            tb_s_pic_proxy_port.Enabled = false;
        }

        private void rb_s_pic_proxy_1_Click(object sender, EventArgs e)
        {
            tb_s_pic_proxy.Enabled = false;
            tb_s_pic_proxy_port.Enabled = false;
        }

        private void rb_s_pic_proxy_2_Click(object sender, EventArgs e)
        {
            tb_s_pic_proxy.Enabled = true;
            tb_s_pic_proxy_port.Enabled = true;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            setting.pic_load_sametime = System.Convert.ToInt32(tb_s_pic_load_sametime.Text);
            setting.pic_load_timeout = System.Convert.ToInt32(tb_s_pic_load_timeout.Text);
            if (rb_s_proxy_0.Checked)
            {
                tb_s_proxy.Enabled = false;
                tb_s_proxy_port.Enabled = false;
                setting.use_proxy = 0;
            }
            if (rb_s_proxy_1.Checked)
            {
                tb_s_proxy.Enabled = false;
                tb_s_proxy_port.Enabled = false;
                setting.use_proxy = 1;
            }
            if (rb_s_proxy_2.Checked)
            {
                tb_s_proxy.Enabled = true;
                tb_s_proxy_port.Enabled = true;
                setting.use_proxy = 2;
                setting.proxy = tb_s_proxy.Text;
                setting.proxy_port = System.Convert.ToInt32(tb_s_proxy_port.Text);
            }
            if (rb_s_pic_proxy_0.Checked)
            {
                tb_s_pic_proxy.Enabled = false;
                tb_s_pic_proxy_port.Enabled = false;
                setting.use_pic_proxy = 0;
            }
            if (rb_s_pic_proxy_1.Checked)
            {
                tb_s_pic_proxy.Enabled = false;
                tb_s_pic_proxy_port.Enabled = false;
                setting.use_pic_proxy = 1;
            }
            if (rb_s_pic_proxy_2.Checked)
            {
                tb_s_pic_proxy.Enabled = true;
                tb_s_pic_proxy_port.Enabled = true;
                setting.use_pic_proxy = 2;
                setting.pic_proxy = tb_s_pic_proxy.Text;
                setting.pic_proxy_port = System.Convert.ToInt32(tb_s_pic_proxy_port.Text);
            }
            string temp = tb_s_defaultFolder.Text;
            string lastchar = temp.Substring(temp.Length - 1, 1);
            if (lastchar != System.Convert.ToString(Path.DirectorySeparatorChar)) { temp += Path.DirectorySeparatorChar; }
            setting.defaultFolder = temp;
            if (!Directory.Exists("Configuration"))
            {
                Directory.CreateDirectory("Configuration");
            }
            Universe.SaveSettings(setting, "Configuration/settings.xml");
        }

        private void btn_s_defaultFolder_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd_s_defaultFolder = new OpenFileDialog();
            ofd_s_defaultFolder.FileName = "　";
            ofd_s_defaultFolder.Filter = "文件夹|*.neverseenthisfile";
            ofd_s_defaultFolder.CheckPathExists = true;
            ofd_s_defaultFolder.ShowReadOnly = false;
            ofd_s_defaultFolder.ReadOnlyChecked = true;
            ofd_s_defaultFolder.CheckFileExists = false;
            ofd_s_defaultFolder.ValidateNames = false;
            ofd_s_defaultFolder.InitialDirectory = tb_s_defaultFolder.Text;
            if (ofd_s_defaultFolder.ShowDialog() == DialogResult.OK)
            {
                if (Path.DirectorySeparatorChar == '\\') { tb_s_defaultFolder.Text = Regex.Match(ofd_s_defaultFolder.FileName, @".*\\").Value; }
                else { tb_s_defaultFolder.Text = Regex.Match(ofd_s_defaultFolder.FileName, @".*" + Path.DirectorySeparatorChar).Value; }
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            Loginwindow lw = new Loginwindow();
            lw.SendSettings += (senderd, ed) =>
            {
                setting.soulplus_enabled = ed.soulplus_enabled;
                setting.hentai_enabled = ed.hentai_enabled;
                if (!Directory.Exists("Configuration"))
                {
                    Directory.CreateDirectory("Configuration");
                }
                Universe.SaveSettings(setting, "Configuration/settings.xml");
            };
            lw.ShowDialog();
            DSList[0] = new DoujinSite(Global.urls.hentai, CookieUtility.ReadCookie("Configuration/cookie.xml"));
            DSList[1] = new DoujinSite(Global.urls.soulplus, CookieUtility.ReadCookie("Configuration/cookie.xml"));
            cb_o_siteselect.Items.Clear();
            if (setting.soulplus_enabled) { cb_o_siteselect.Items.Add("soul-plus.net"); }
            if (setting.hentai_enabled) { cb_o_siteselect.Items.Add("E-hentai.org"); }
            if (cb_o_siteselect.Items.Count != 0) { cb_o_siteselect.Text = cb_o_siteselect.Items[0] as String; }
        }

        #endregion

        

    }
}
