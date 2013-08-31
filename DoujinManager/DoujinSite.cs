using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Concurrent;

namespace DoujinManager
{
    /// <summary>
    /// 同人站相关类
    /// </summary>
    class DoujinSite
    {
        /// <summary>
        /// DoujinSite事件处理类
        /// </summary>
        public class DoujinSiteEventArgs : EventArgs
        {
            public readonly string msg;
            public DoujinSiteEventArgs(string message)
            {
                msg = message;
            }
        }

        /// <summary>
        /// 网站的对应枚举类型
        /// </summary>
        public Global.urls url;

        /// <summary>
        /// 登录后的cookie
        /// </summary>
        public CookieCollection cookie;

        /// <summary>
        /// 搜索的当前页
        /// </summary>
        public int search_currect_page = 1;

        /// <summary>
        /// 搜索结果的总共页数
        /// </summary>
        public int search_all_page;

        /// <summary>
        /// 消息事件
        /// </summary>
        public event EventHandler<DoujinSiteEventArgs> Messager;

        /// <summary>
        /// 搜索分页url
        /// </summary>
        private string search_page_url;

        /// <summary>
        /// 网页内容缓存字典
        /// </summary>
        private ConcurrentDictionary<string, string> webcontent_cache = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 同人站相关类
        /// </summary>
        public DoujinSite() { }

        /// <summary>
        /// 同人站相关类
        /// </summary>
        /// <param name="url">网站的对应枚举类型</param>
        public DoujinSite(Global.urls url)
        {
            this.url = url;
        }

        /// <summary>
        /// 同人站相关类
        /// </summary>
        /// <param name="url">网站的对应枚举类型</param>
        /// <param name="cookie">登录后的cookie</param>
        public DoujinSite(Global.urls url, CookieCollection cookie)
        {
            this.url = url;
            this.cookie = cookie;
        }

        /// <summary>
        /// 登录网站
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// /// <param name="password">登录网站枚举</param>
        /// <returns>请求返回的HttpWebResponse</returns>
        public static HttpWebResponse Login(string username, string password, Global.urls url)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            string loginurl = null;
            HttpWebResponse result = null;
            switch (System.Convert.ToInt32(url))
            {
                case 0:
                    parameters.Add("ipb_login_username", username);
                    parameters.Add("ipb_login_password", password);
                    loginurl = "http://e-hentai.org/";
                    HttpWebResponse temp = HttpWebResponseUtility.CreatePostHttpResponse(loginurl, parameters, null, null, Encoding.UTF8, null, true);
                    result = HttpWebResponseUtility.CreatePostHttpResponse("http://exhentai.org/", null, null, null, Encoding.UTF8, temp.Cookies, false);
                    break;
                case 1:
                    parameters.Add("jumpurl", "http://bbs.soul-plus.net/index.php");
                    parameters.Add("step", "2");
                    parameters.Add("cktime", "31536000");
                    parameters.Add("lgt", "0");
                    parameters.Add("pwuser", username);
                    parameters.Add("pwpwd", password);
                    parameters.Add("question", "0");
                    loginurl = "http://bbs.soul-plus.net/login.php";
                    result = HttpWebResponseUtility.CreatePostHttpResponse(loginurl, parameters, null, null, Encoding.UTF8, null, true);
                    break;
                case 2:
                    url = Global.urls.hentai;
                    return Login(username, password, Global.urls.hentai);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 搜索同人志
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns>一个List中包含的string数组，数组对应标题-网址</returns>
        public List<string[]> Search(string keyword, int page = 1)
        {
            List<string[]> result = new List<string[]>();
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            string searchurl = null;
            string html = null;
            switch (System.Convert.ToInt32(url))
            {
                case 0:
                    url = Global.urls.xhentai;
                    return this.Search(keyword);
                    break;
                case 1:
                    if (page == 1)
                    {
                        parameters.Add("step", "2");
                        Match f = Regex.Match(keyword, "(?<=<--).+(?=-->)");
                        if (f.Success)
                        {
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
                                    else
                                    {
                                        Messager(this, new DoujinSiteEventArgs("搜索语法错误！"));
                                        return new List<string[]>();
                                    }
                                    break;
                            }
                            keyword = keyword.Replace("<--" + f.Value + "-->", "");
                            parameters.Add("f_fid", fid);
                        }
                        else { parameters.Add("f_fid", "all"); }
                        parameters.Add("keyword", keyword);
                        parameters.Add("method", "OR");
                        parameters.Add("pwuser", "");
                        parameters.Add("sch_area", "0");
                        parameters.Add("sch_time", "all");
                        parameters.Add("orderway", "postdate");
                        parameters.Add("asc", "DESC");
                        searchurl = "http://bbs.soul-plus.net/search.php";
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreatePostHttpResponse(searchurl, parameters, null, null, Encoding.UTF8, cookie, true), Encoding.UTF8);
                        if (Regex.IsMatch(html, @"<li class=""pagesone"">"))
                        {
                            Match match_page = Regex.Match(html, @"Pages: (?:\d+)\/(?:\d+)");
                            search_currect_page = System.Convert.ToInt32(match_page.Groups[1].Value);
                            search_all_page = System.Convert.ToInt32(match_page.Groups[2].Value);
                            search_page_url = "http://bbs.soul-plus.net/" + Regex.Match(html, @"search.php\?step=2&keyword=.+?&sid=\d+?&seekfid=.+?&page=").Value;
                        }
                        else
                        {
                            search_currect_page = 1;
                            search_all_page = 1;
                            search_page_url = null;
                        }
                    }
                    else
                    {
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(search_page_url + System.Convert.ToString(page), null, null, cookie, true), Encoding.UTF8);
                        search_currect_page = page;
                    }

                    if (Regex.IsMatch(html, "(?<=论坛提示.*?<tr class=\"f_one\"><td><center><br /><br /><br />).*?(?=<br /><br /></center><br /><br /></td></tr>)")) { Messager(this, new DoujinSiteEventArgs(Regex.Match(html, "(?<=论坛提示.*?<tr class=\"f_one\"><td><center><br /><br /><br />).*?(?=<br /><br /></center><br /><br /></td></tr>)").Value)); break; }

                    foreach (Match row in Regex.Matches(html, "<tr class=\"tr3 tac\">.*?</tr>"))
                    {
                        string tid = Regex.Match(row.Value, @"tid=\d+").Value;
                        string title = new Regex("<.*?>").Replace(Regex.Match(row.Value, "(?<=target=\"_blank\">).*?(?=</a></th>)").Value, "");
                        result.Add(new string[] { title, "http://bbs.soul-plus.net/read.php?" + tid });
                    }
                    break;
                case 2:
                    searchurl = "http://exhentai.org/?page=" + System.Convert.ToString(page - 1) + "&f_doujinshi=on&f_manga=on&f_artistcg=on&f_gamecg=on&f_western=on&f_non-h=on&f_imageset=on&f_cosplay=on&f_asianporn=on&f_misc=on&f_search=" + keyword + "&f_apply=Apply Filter";
                    html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(searchurl, null, null, cookie, true), Encoding.UTF8);

                    if (Regex.IsMatch(html, "No hits found")) { Messager(this, new DoujinSiteEventArgs("未找到结果")); break; }
                    MatchCollection mc = Regex.Matches(html, @"(?<=return false"">)\d+(?=</a></td>)");
                    search_all_page = System.Convert.ToInt32(mc[mc.Count - 1].Value);
                    search_currect_page = System.Convert.ToInt32(Regex.Match(html, @"(?<=ptds""><.*?>)\d+?(?=<)").Value);
                    search_page_url = null;

                    html = Regex.Match(html, "Uploader</th></tr>.*?</table>", RegexOptions.Singleline).Value;
                    foreach (Match row in Regex.Matches(html, "<tr class=\"gtr\\d\">.*?</tr>"))
                    {
                        html = Regex.Match(row.Value, "<a href=\"http://exhentai.org/g/\\d+/.*?</a>").Value;
                        string gid_addr = Regex.Match(html, "(?<=<a href=\"http://exhentai.org/g/).*?(?=\")").Value;
                        string title = Regex.Match(html, "(?<=>).*(?=<)").Value;
                        result.Add(new string[] { title, "http://exhentai.org/g/" + gid_addr });
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// 提取封面图片
        /// </summary>
        /// <param name="target_url">需提取的url</param>
        /// <returns>图片地址</returns>
        public string Extract_pic(string target_url)
        {
            string result = null;
            string html = null;
            switch (System.Convert.ToInt32(url))
            {
                case 0:
                    url = Global.urls.xhentai;
                    return this.Extract_pic(target_url);
                    break;
                case 1:
                    if (webcontent_cache.ContainsKey(target_url)) { html = webcontent_cache[target_url]; }
                    else { html = webcontent_cache.GetOrAdd(target_url, DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8)); }
                    foreach (Match row in Regex.Matches(html, "(?<=<div class=\"tpc_content\">.*?<img src=\").*?(?=\")"))
                    {
                        if (Regex.IsMatch(row.Value, @"^images/"))
                        {
                            continue;
                        }
                        else
                        {
                            result = row.Value;
                            break;
                        }
                    }
                    break;
                case 2:
                    if (webcontent_cache.ContainsKey(target_url)) { html = webcontent_cache[target_url]; }
                    else { html = webcontent_cache.GetOrAdd(target_url, DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8)); }
                    result = Regex.Match(html, "(?<=<div id=\"gd1\"><img src=\").*?(?=\")").Value;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 提取下载地址
        /// </summary>
        /// <param name="target_url">需提取的url</param>
        /// <returns>下载地址</returns>
        public List<ArrayList> GetDownLink(string target_url)
        {
            List<ArrayList> result = new List<ArrayList>();
            string html = null;
            switch (System.Convert.ToInt32(url))
            {
                case 0:
                    url = Global.urls.xhentai;
                    return this.GetDownLink(target_url);
                    break;
                case 1:
                    if (webcontent_cache.ContainsKey(target_url)) { html = webcontent_cache[target_url]; }
                    else { html = webcontent_cache.GetOrAdd(target_url, DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8)); }
                    html = html.Replace("&amp;", "&").Replace("&#46;", ".");
                    if (Regex.IsMatch(html, "若发现会员采用欺骗的方法获取财富,请立刻举报,我们会对会员处以2-N倍的罚金,严重者封掉ID!"))
                    {
                        string target2_url = "http://bbs.soul-plus.net/" + Regex.Match(html, @"(?<=我付钱"" class=""btn"" onclick=""location.href=').*?(?='"")").Value;
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target2_url, null, null, cookie, true), Encoding.UTF8);
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8);
                        webcontent_cache[target_url] = html;
                        html = html.Replace("&amp;", "&").Replace("&#46;", ".");
                        html = Regex.Match(html, @"<blockquote class=""blockquote"">.*?</blockquote>").Value;
                    }
                    else
                    {
                        html = Regex.Match(html, @"<div class=""tpc_content"">.*?</th> </tr>").Value;
                    }
                    MatchCollection ma = Regex.Matches(html, @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?");
                    foreach (Match row in ma)
                    {
                        if (Regex.IsMatch(row.Value, "jbpan")) { result.Add(new ArrayList { Global.downmethod.jbpan, row.Value }); }
                        else if (Regex.IsMatch(row.Value, "pan.hacg.me")) { result.Add(new ArrayList { Global.downmethod.jbpan, row.Value }); }
                        else if (Regex.IsMatch(row.Value, "howfile")) { result.Add(new ArrayList { Global.downmethod.howfile, row.Value }); }
                        else if (Regex.IsMatch(row.Value, "pan.baidu")) { result.Add(new ArrayList { Global.downmethod.baidupan, row.Value }); }
                        else if (Regex.IsMatch(row.Value, "kuai.xunlei")) { result.Add(new ArrayList { Global.downmethod.xunleikuai, row.Value }); }
                        else if (Regex.IsMatch(row.Value, "115.com")) { result.Add(new ArrayList { Global.downmethod.pan115, row.Value }); }
                    }
                    break;
                case 2:
                    if (webcontent_cache.ContainsKey(target_url)) { html = webcontent_cache[target_url]; }
                    else { html = webcontent_cache.GetOrAdd(target_url, DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8)); }
                    foreach (Match row in Regex.Matches(html, @"(?<=onclick=""return popUp\(').*?(?=',\d+,\d+\)"">.*? Download)"))
                    {
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(HttpUtility.HtmlDecode(row.Value), null, null, cookie, true), Encoding.UTF8);
                        if (Regex.IsMatch(row.Value, "archiver"))
                        {
                            result.Add(new ArrayList { Global.downmethod.HT_archive, HttpUtility.HtmlDecode(Regex.Match(html, @"(?<=<form action="").*?(?="" method=""post"">)").Value) });
                        }
                        else
                        {
                            foreach (Match row2 in Regex.Matches(html, @"(?<=<td colspan=""5""> &nbsp; <a href="").*?(?="")"))
                            {
                                result.Add(new ArrayList { Global.downmethod.BT, HttpUtility.HtmlDecode(row2.Value) });
                            }
                        }
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// 提取压缩包密码/网盘访问密码
        /// </summary>
        /// <param name="target_url">需提取的url</param>
        /// <returns>压缩包密码/网盘访问密码</returns>
        public String GetPassword(string target_url)
        {
            string result = "";
            string html = null;
            switch (System.Convert.ToInt32(url))
            {
                case 0:
                    result = "";
                    break;
                case 1:
                    if (webcontent_cache.ContainsKey(target_url)) { html = webcontent_cache[target_url]; }
                    else { html = webcontent_cache.GetOrAdd(target_url, DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8)); }
                    html = html.Replace("&amp;", "&").Replace("&#46;", ".");
                    if (Regex.IsMatch(html, "若发现会员采用欺骗的方法获取财富,请立刻举报,我们会对会员处以2-N倍的罚金,严重者封掉ID!"))
                    {
                        string target2_url = "http://bbs.soul-plus.net/" + Regex.Match(html, @"(?<=我付钱"" class=""btn"" onclick=""location.href=').*?(?='"")").Value;
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target2_url, null, null, cookie, true), Encoding.UTF8);
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8);
                        webcontent_cache[target_url] = html;
                        html = html.Replace("&amp;", "&").Replace("&#46;", ".");
                    }
                    if (Regex.IsMatch(html, @"(?<=(密[码碼]|pw|passwd|password|提取[码碼]|提取)(|<(|/)\w+>)(:|：| +?)).*?(?=[  ]*?<)"))
                    {
                        result = Regex.Match(html, @"(?<=(密[码碼]|pw|passwd|password|提取[码碼]|提取)(|<(|/)\w+>)(:|：| +?)).*?(?=[  ]*?<)").Value;
                    }
                    break;
                case 2:
                    result = "";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 名称格式化
        /// </summary>
        /// <param name="title">原始标题</param>
        /// <param name="name">标题</param>
        /// <param name="author">作者</param>
        /// <param name="party">首发展会</param>
        /// <param name="localization_group">汉化组</param>
        /// <param name="tag">标签</param>
        /// <returns>判断是否为同人志</returns>
        public Boolean NameFormatter(string title, ref string name, ref string author, ref string party, ref string localization_group, ref List<String> tag)
        {
            bool is_doujin = false;
            Regex brackets = new Regex(@"^[  ]*?[(\[【]|[)\]】] *?$");
            Regex space = new Regex(@"^([  ]*)");
            title = title.Replace("&nbsp;", " ").Replace("&amp;", "&").Replace("&#46;", ".").Replace("(同人誌)", "").Replace("(自扫)", "").Replace("[自扫]", "");
            MatchCollection lgMatches = Regex.Matches(title, @"([  ]*)(\[.+?\]|【.+?】|\(.+?\))");
            foreach (Match lgMatch in lgMatches)
            {
                if (Regex.IsMatch(lgMatch.Value, "[汉漢]化|CE|中文"))
                {
                    localization_group = brackets.Replace(lgMatch.Value, "");
                    title = title.Replace(lgMatch.Value, "");
                    localization_group = space.Replace(localization_group, "");
                    is_doujin = true;
                }
            }
            Match partyMatch = Regex.Match(title, @"^([  ]*)\(.+?\)");
            if (partyMatch.Success)
            {
                party = brackets.Replace(partyMatch.Value, "");
                title = title.Replace(partyMatch.Value, "");
                party = space.Replace(party, "");
                is_doujin = true;
            }
            Match authorMatch = Regex.Match(title, @"^([  ]*)(\[.+?\]|\(.+?\))");
            if (authorMatch.Success)
            {
                author = brackets.Replace(authorMatch.Value, "");
                title = title.Replace(authorMatch.Value, "");
                author = space.Replace(author, "");
                is_doujin = true;
                NameFormatter_ds(ref title, ref is_doujin, ref author);
                if (author == null || author == "") { author = party; party = ""; }
            }
            Match nameMatch = Regex.Match(title, @"^([  ]*)[\p{P}\p{S}\p{Zs}\w-[\[\]()【】（）]]+");
            if (nameMatch.Success)
            {
                name = brackets.Replace(nameMatch.Value, "");
                title = title.Replace(nameMatch.Value, "");
                name = space.Replace(name, "");
                name = new Regex(@"[  ]+$").Replace(name, "");
            }
            MatchCollection tagMatches = Regex.Matches(title, @"\[.+?\]|【.+?】|\(.+?\)");
            foreach (Match tagMatch in tagMatches) { tag.Add(brackets.Replace(tagMatch.Value, "")); }
            if (tag.Exists(o => o == "chinese") && localization_group == "未汉化") { localization_group = "已汉化，汉化组未知"; }
            return is_doujin;
        }

        private void NameFormatter_ds(ref string title, ref Boolean is_doujin, ref string author)
        {
            Regex space = new Regex(@"^([  ]*)");
            Match authorMatch = Regex.Match(title, @"^([  ]*)(\[.+?\]|\(.+?\))");
            if (authorMatch.Success)
            {
                author = author + authorMatch.Value;
                title = title.Replace(authorMatch.Value, "");
                is_doujin = true;
                NameFormatter_ds(ref title, ref is_doujin, ref author);
            }
        }

        /// <summary>
        /// 获取某版的同人志（soulplus用）
        /// </summary>
        public List<string[]> List(string fid, int page = 1)
        {
            List<string[]> result = new List<string[]>();
            string html = null;
            string Threadurl = "http://bbs.soul-plus.net/thread.php?fid=" + fid + "&page=" + System.Convert.ToString(page);
            html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(Threadurl, null, null, cookie, true), Encoding.UTF8);
            if (page == 1)
            {
                if (Regex.IsMatch(html, @"<li class=""pagesone"">"))
                {
                    Match match_page = Regex.Match(html, @"Pages: (\d+)\/(\d+)");
                    search_currect_page = System.Convert.ToInt32(match_page.Groups[1].Value);
                    search_all_page = System.Convert.ToInt32(match_page.Groups[2].Value);
                }
                else
                {
                    search_currect_page = 1;
                    search_all_page = 1;
                }
            }
            else
            {
                search_currect_page = page;
            }
            if (Regex.IsMatch(html, "(?<=论坛提示.*?<tr class=\"f_one\"><td><center><br /><br /><br />).*?(?=<br /><br /></center><br /><br /></td></tr>)"))
            {
                Messager(this, new DoujinSiteEventArgs(Regex.Match(html, "(?<=论坛提示.*?<tr class=\"f_one\"><td><center><br /><br /><br />).*?(?=<br /><br /></center><br /><br /></td></tr>)").Value));
            }

            if (page == 1) { html = Regex.Match(html, "普通主题.*").Value; }
            foreach (Match row in Regex.Matches(html, "tr3 t_one\">.*?</tr>"))
            {
                string tid = Regex.Match(row.Value, @"tid=\d+").Value;
                Regex rx5 = new Regex("<.*?>");
                string title = rx5.Replace(Regex.Match(row.Value, @"(?<=id=""a_ajax_\d+"">).*?(?=</a></h3>)").Value, "");
                result.Add(new string[] { title, "http://bbs.soul-plus.net/read.php?" + tid });
            }
            return result;
        }
    }
}
