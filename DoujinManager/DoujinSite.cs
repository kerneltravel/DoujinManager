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
        /// <returns>请求返回的HttpWebResponse</returns>
        public HttpWebResponse Login(string username, string password)
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
                    return this.Login(username, password);
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
                        Match f = new Regex("(?<=<--).+(?=-->)").Match(keyword);
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
                        if (new Regex(@"<li class=""pagesone"">").IsMatch(html))
                        {
                            Match match_page = new Regex(@"Pages: (\d+)\/(\d+)").Match(html);
                            search_currect_page = System.Convert.ToInt32(match_page.Groups[1].Value);
                            search_all_page = System.Convert.ToInt32(match_page.Groups[2].Value);
                            search_page_url = "http://bbs.soul-plus.net/" + new Regex(@"search.php\?step=2&keyword=.+?&sid=\d+?&seekfid=.+?&page=").Match(html).Value;
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

                    Regex rxd = new Regex("(?<=论坛提示.*?<tr class=\"f_one\"><td><center><br /><br /><br />).*?(?=<br /><br /></center><br /><br /></td></tr>)");
                    if (rxd.IsMatch(html)) { Messager(this, new DoujinSiteEventArgs(rxd.Match(html).Value)); break; }

                    Regex rx2 = new Regex("<tr class=\"tr3 tac\">.*?</tr>");
                    foreach (Match row in rx2.Matches(html))
                    {
                        Regex rx3 = new Regex(@"tid=\d+");
                        string tid = rx3.Match(row.Value).Value;
                        Regex rx4 = new Regex("(?<=target=\"_blank\">).*?(?=</a></th>)");
                        Regex rx5 = new Regex("<.*?>");
                        string title = rx5.Replace(rx4.Match(row.Value).Value, "");
                        result.Add(new string[] { title, "http://bbs.soul-plus.net/read.php?" + tid });
                    }
                    break;
                case 2:
                    searchurl = "http://exhentai.org/?page=" + System.Convert.ToString(page - 1) + "&f_doujinshi=on&f_manga=on&f_artistcg=on&f_gamecg=on&f_western=on&f_non-h=on&f_imageset=on&f_cosplay=on&f_asianporn=on&f_misc=on&f_search=" + keyword + "&f_apply=Apply Filter";
                    html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(searchurl, null, null, cookie, true), Encoding.UTF8);

                    if (new Regex("No hits found").IsMatch(html)) { Messager(this, new DoujinSiteEventArgs("未找到结果")); break; }
                    MatchCollection mc = new Regex(@"(?<=return false"">)\d+(?=</a></td>)").Matches(html);
                    search_all_page = System.Convert.ToInt32(mc[mc.Count - 1].Value);
                    search_currect_page = System.Convert.ToInt32(new Regex(@"(?<=ptds""><.*?>)\d+?(?=<)").Match(html).Value);
                    search_page_url = null;

                    Regex rh = new Regex("Uploader</th></tr>.*?</table>", RegexOptions.Singleline);
                    html = rh.Match(html).Value;
                    Regex rh2 = new Regex("<tr class=\"gtr\\d\">.*?</tr>");
                    foreach (Match row in rh2.Matches(html))
                    {
                        Regex rh3 = new Regex("<a href=\"http://exhentai.org/g/\\d+/.*?</a>");
                        html = rh3.Match(row.Value).Value;
                        Regex rh4 = new Regex("(?<=<a href=\"http://exhentai.org/g/).*?(?=\")");
                        string gid_addr = rh4.Match(html).Value;
                        Regex rh5 = new Regex("(?<=>).*(?=<)");
                        string title = rh5.Match(html).Value;
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
                    Regex rx = new Regex("(?<=<div class=\"tpc_content\">.*?<img src=\").*?(?=\")");
                    foreach (Match row in rx.Matches(html))
                    {
                        Regex rx2 = new Regex(@"^images/");
                        if (rx2.IsMatch(row.Value))
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
                    Regex rh = new Regex("(?<=<div id=\"gd1\"><img src=\").*?(?=\")");
                    result = rh.Match(html).Value;
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
                    if (new Regex("若发现会员采用欺骗的方法获取财富,请立刻举报,我们会对会员处以2-N倍的罚金,严重者封掉ID!").IsMatch(html))
                    {
                        string target2_url = "http://bbs.soul-plus.net/" + new Regex(@"(?<=我付钱"" class=""btn"" onclick=""location.href=').*?(?='"")").Match(html).Value;
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target2_url, null, null, cookie, true), Encoding.UTF8);
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8);
                        webcontent_cache[target_url] = html;
                        html = html.Replace("&amp;", "&").Replace("&#46;", ".");
                        html = new Regex(@"<blockquote class=""blockquote"">.*?</blockquote>").Match(html).Value;
                    }
                    else
                    {
                        html = new Regex(@"<div class=""tpc_content"">.*?</th> </tr>").Match(html).Value;
                    }
                    MatchCollection ma = new Regex(@"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?").Matches(html);
                    foreach (Match row in ma)
                    {
                        if (new Regex("jbpan").IsMatch(row.Value)) { result.Add(new ArrayList { Global.downmethod.jbpan, row.Value }); }
                        else if (new Regex("pan.hacg.me").IsMatch(row.Value)) { result.Add(new ArrayList { Global.downmethod.jbpan, row.Value }); }
                        else if (new Regex("howfile").IsMatch(row.Value)) { result.Add(new ArrayList { Global.downmethod.howfile, row.Value }); }
                        else if (new Regex("pan.baidu").IsMatch(row.Value)) { result.Add(new ArrayList { Global.downmethod.baidupan, row.Value }); }
                        else if (new Regex("kuai.xunlei").IsMatch(row.Value)) { result.Add(new ArrayList { Global.downmethod.xunleikuai, row.Value }); }
                        else if (new Regex("115.com").IsMatch(row.Value)) { result.Add(new ArrayList { Global.downmethod.pan115, row.Value }); }
                    }
                    break;
                case 2:
                    if (webcontent_cache.ContainsKey(target_url)) { html = webcontent_cache[target_url]; }
                    else { html = webcontent_cache.GetOrAdd(target_url, DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8)); }
                    Regex rx = new Regex(@"(?<=onclick=""return popUp\(').*?(?=',\d+,\d+\)"">.*? Download)");
                    foreach (Match row in rx.Matches(html))
                    {
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(HttpUtility.HtmlDecode(row.Value), null, null, cookie, true), Encoding.UTF8);
                        Regex rx2 = new Regex("archiver");
                        if (rx2.IsMatch(row.Value))
                        {
                            Regex rx3 = new Regex(@"(?<=<form action="").*?(?="" method=""post"">)");
                            result.Add(new ArrayList { Global.downmethod.HT_archive, HttpUtility.HtmlDecode(rx3.Match(html).Value) });
                        }
                        else
                        {
                            Regex rx3 = new Regex(@"(?<=<td colspan=""5""> &nbsp; <a href="").*?(?="")");
                            foreach (Match row2 in rx3.Matches(html))
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
                    if (new Regex("若发现会员采用欺骗的方法获取财富,请立刻举报,我们会对会员处以2-N倍的罚金,严重者封掉ID!").IsMatch(html))
                    {
                        string target2_url = "http://bbs.soul-plus.net/" + new Regex(@"(?<=我付钱"" class=""btn"" onclick=""location.href=').*?(?='"")").Match(html).Value;
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target2_url, null, null, cookie, true), Encoding.UTF8);
                        html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, cookie, true), Encoding.UTF8);
                        webcontent_cache[target_url] = html;
                        html = html.Replace("&amp;", "&").Replace("&#46;", ".");
                    }
                    Regex ra = new Regex("(?<=(密[码碼]|pw|passwd|password|提取[码碼]|提取)(:|：| +?)).*?(?= *?<)");
                    if (ra.IsMatch(html))
                    {
                        result = ra.Match(html).Value;
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
            Regex brackets = new Regex(@"^ *?[(\[【]|[)\]】] *?$");
            Regex space = new Regex(@"^( *)");
            title = title.Replace("&nbsp;", " ").Replace("&amp;", "&").Replace("&#46;", ".").Replace("(同人誌)", "");
            MatchCollection lgMatches = new Regex(@"( *)(\[.+?\]|【.+?】)").Matches(title);
            foreach (Match lgMatch in lgMatches)
            {
                if (new Regex("[汉漢]化|CE").IsMatch(lgMatch.Value))
                {
                    localization_group = brackets.Replace(lgMatch.Value, "");
                    title = title.Replace(lgMatch.Value, "");
                    localization_group = space.Replace(localization_group, "");
                    is_doujin = true;
                }
            }
            Match partyMatch = new Regex(@"^( *)\(.+?\)").Match(title);
            if (partyMatch.Success)
            {
                party = brackets.Replace(partyMatch.Value, "");
                title = title.Replace(partyMatch.Value, "");
                party = space.Replace(party, "");
                is_doujin = true;
            }
            Match authorMatch = new Regex(@"^( *)(\[.+?\]|\(.+?\))").Match(title);
            if (authorMatch.Success)
            {
                author = brackets.Replace(authorMatch.Value, "");
                title = title.Replace(authorMatch.Value, "");
                author = space.Replace(author, "");
                is_doujin = true;
                NameFormatter_ds(ref title, ref is_doujin, ref author);
                if (author == null || author == "") { author = party; party = ""; }
            }
            Match nameMatch = new Regex(@"^( *)[\w :"";',.~!@#$%/\\^&*_+|`=！…?—：“”：；‘’，。、-]+").Match(title);
            if (nameMatch.Success)
            {
                name = brackets.Replace(nameMatch.Value, "");
                title = title.Replace(nameMatch.Value, "");
                name = space.Replace(name, "");
                name = new Regex(@" +$").Replace(name, "");
            }
            MatchCollection tagMatches = new Regex(@"\[.+?\]|【.+?】|\(.+?\)").Matches(title);
            foreach (Match tagMatch in tagMatches) { tag.Add(brackets.Replace(tagMatch.Value, "")); }
            if (tag.Exists(o => o == "chinese") && localization_group == "未汉化") { localization_group = "已汉化，汉化组未知"; }
            return is_doujin;
        }

        private void NameFormatter_ds(ref string title, ref Boolean is_doujin, ref string author)
        {
            Regex space = new Regex(@"^( *)");
            Match authorMatch = new Regex(@"^( *)(\[.+?\]|\(.+?\))").Match(title);
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
                if (new Regex(@"<li class=""pagesone"">").IsMatch(html))
                {
                    Match match_page = new Regex(@"Pages: (\d+)\/(\d+)").Match(html);
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
            Regex rxd = new Regex("(?<=论坛提示.*?<tr class=\"f_one\"><td><center><br /><br /><br />).*?(?=<br /><br /></center><br /><br /></td></tr>)");
            if (rxd.IsMatch(html)) { Messager(this, new DoujinSiteEventArgs(rxd.Match(html).Value)); }

            if (page == 1) { html = new Regex("普通主题.*").Match(html).Value; }
            Regex rx2 = new Regex("tr3 t_one\">.*?</tr>");
            foreach (Match row in rx2.Matches(html))
            {
                Regex rx3 = new Regex(@"tid=\d+");
                string tid = rx3.Match(row.Value).Value;
                Regex rx4 = new Regex(@"(?<=id=""a_ajax_\d+"">).*?(?=</a></h3>)");
                Regex rx5 = new Regex("<.*?>");
                string title = rx5.Replace(rx4.Match(row.Value).Value, "");
                result.Add(new string[] { title, "http://bbs.soul-plus.net/read.php?" + tid });
            }
            return result;
        }
    }
}
