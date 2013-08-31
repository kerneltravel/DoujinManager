using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoujinManager
{
    public partial class Loginwindow : Form
    {
        public class LoginWindowEventArgs : EventArgs
        {
            public readonly Boolean soulplus_enabled;
            public readonly Boolean hentai_enabled;
            public LoginWindowEventArgs(bool sp, bool ht)
            {
                soulplus_enabled = sp;
                hentai_enabled = ht;
            }
        }
        public Boolean sp_enabled = false;
        public Boolean ht_enabled = false;
        public CookieCollection cc = new CookieCollection();
        bool islogin = false;
        //public delegate void SendSettingsHandler(object sender, LoginWindowEventArgs e);
        public event EventHandler<LoginWindowEventArgs> SendSettings;

        public Loginwindow()
        {
            InitializeComponent();
        }

        private void ll_soulplus_reg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://bbs.soul-plus.net/register.php");
        }

        private void ll_hentai_reg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://e-hentai.org/");
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            btn_login.Enabled = false;
            btn_login.Text = "登录中……";
            Task.Factory.StartNew(() =>
            {
                HttpWebResponse hwr_sp = DoujinSite.Login(tb_soulplus_username.Text, tb_soulplus_password.Text, Global.urls.soulplus);
                HttpWebResponse hwr_ht = DoujinSite.Login(tb_hentai_username.Text, tb_hentai_password.Text, Global.urls.hentai);
                if (Regex.IsMatch(DecompressWebResponse.Decompress(hwr_sp, Encoding.UTF8), "您已经顺利登录"))
                {
                    sp_enabled = true;
                    cc.Add(hwr_sp.Cookies);
                    islogin = true;
                }
                if (Regex.IsMatch(DecompressWebResponse.Decompress(hwr_ht, Encoding.UTF8), "ExHentai.org - The X Makes It Sound Cool"))
                {
                    ht_enabled = true;
                    cc.Add(hwr_ht.Cookies);
                    islogin = true;
                }
                islogin = true;
                if (islogin)
                {
                    if (!Directory.Exists("Configuration"))
                    {
                        Directory.CreateDirectory("Configuration");
                    }
                    CookieUtility.SaveCookie(cc, "Configuration/cookie.xml");
                    SendSettings(this, new LoginWindowEventArgs(sp_enabled, ht_enabled));
                    this.Invoke((Action)delegate { this.Close(); });
                }
                else
                {
                    MessageBox.Show("登录失败！");
                    this.Invoke((Action)delegate
                    {
                        btn_login.Enabled = true;
                        btn_login.Text = "登录";
                    });
                }
            });
        }

        private void Loginwindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!islogin) { Application.Exit(); }
        }

        private void tb_soulplus_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(null, new EventArgs());
            }
        }

        private void tb_soulplus_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(null, new EventArgs());
            }
        }

        private void tb_hentai_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(null, new EventArgs());
            }
        }

        private void tb_hentai_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(null, new EventArgs());
            }
        }
    }
}
