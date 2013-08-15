using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DoujinManager
{
    class CookieUtility
    {
        /// <summary>
        /// 获得请求返回的Cookie
        /// </summary>
        /// <param name="res">请求返回的HttpWebResponse</param>
        /// <param name="url">目标网站</param>
        /// <returns>请求返回的Cookie</returns>
        public static CookieCollection getResponseCookies(HttpWebResponse response, string url)
        {
            CookieCollection cookies = new CookieCollection();
            string[] ary = response.Headers[HttpResponseHeader.SetCookie].Replace(", ","").Split(',');
            foreach (string coo in ary)
            {
                string ary2 = coo.Split(';')[0];
                Match match = new Regex(@"(.+?)=(.+)").Match(ary2);
                cookies.Add(new Cookie(match.Groups[1].Value, match.Groups[2].Value, "/", url));
            }
            return cookies;
        }

        private static Cookie GetCookieFromString(string cookieString, string defaultDomain)
        {
            string[] ary = cookieString.Split(',');
            Hashtable hs = new Hashtable();
            for (int i = 0; i < ary.Length; i++)
            {
                string s = ary[i].Trim();
                int index = s.IndexOf("=");
                if (index > 0)
                {
                    hs.Add(s.Substring(0, index), s.Substring(index + 1));
                }
            }
            Cookie ck = new Cookie();
            foreach (object Key in hs.Keys)
            {
                if (Key.ToString() == "path") ck.Path = hs[Key].ToString();

                else if (Key.ToString() == "expires")
                {
                    //ck.Expires=DateTime.Parse(hs[Key].ToString();
                }
                else if (Key.ToString() == "domain") ck.Domain = hs[Key].ToString();
                else
                {
                    ck.Name = Key.ToString();
                    ck.Value = hs[Key].ToString();
                }
            }
            if (ck.Name == "") return null;
            if (ck.Domain == "") ck.Domain = defaultDomain;
            return ck;
        }


        /// <summary>
        /// 保存XML格式的Cookie到指定文件
        /// </summary>
        /// <param name="cookies">需保存的cookie</param>
        /// <param name="savepath">保存路径</param>
        public static void SaveCookie(CookieCollection cookies, string savepath)
        {
            XElement CookieCollectionXml = new XElement("CookieCollection");
            foreach (Cookie cookie in cookies)
            {
                XElement CookieXml =
                    new XElement("cookie",
                        new XElement("Name", new XCData(cookie.Name)),
                        new XElement("Value", new XCData(cookie.Value)),
                        new XElement("Path", new XCData(cookie.Path)),
                        new XElement("Domain", new XCData(cookie.Domain))
                    );
                CookieCollectionXml.Add(CookieXml);
            }
            CookieCollectionXml.Save(savepath);
        }

        /// <summary>
        /// 从XML格式的Cookie文件中读取Cookie
        /// </summary>
        /// <param name="savepath">文件路径</param>
        public static CookieCollection ReadCookie(string savepath)
        {
            CookieCollection cookies = new CookieCollection();
            XElement CookieCollectionXml = XElement.Load(savepath);
            foreach (XElement CookieXml in CookieCollectionXml.Elements("cookie"))
            {
                Cookie cookie = new Cookie(CookieXml.Element("Name").Value, CookieXml.Element("Value").Value, CookieXml.Element("Path").Value, CookieXml.Element("Domain").Value);
                cookies.Add(cookie);
            }
            return cookies;
        }
    }
}
