using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace DoujinManager
{
    class DownloadSite
    {
        /// <summary>
        /// 获得jbpan下载地址
        /// </summary>
        /// <param name="target_url">需提取的url</param>
        /// <returns>下载地址</returns>
        public static string jbpan(string target_url)
        {
            string result = "";
            string dkcode = new Regex(@"dk\d+").Match(target_url).Value;
            target_url = "http://jbpan.tk/Download/index/code/" + dkcode;
            string html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, null, true), Encoding.UTF8);
            result = new Regex(@"(?<=""url"":"").*?(?="")").Match(html).Value.Replace(@"\/","/");
            return result;
        }

        /// <summary>
        /// 获得baidupan下载地址
        /// </summary>
        /// <param name="target_url">需提取的url</param>
        /// <param name="pwd">（可选）网盘提取密码</param>
        /// <returns>下载地址</returns>
        public static string baidupan(string target_url,string pwd)
        {
            string result = "";
            if (pwd == null) { pwd = ""; }
            HttpWebResponse try_verify = HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, null, true);
            CookieCollection baipancookie = try_verify.Cookies;
            string html = DecompressWebResponse.Decompress(try_verify, Encoding.UTF8);
            //度娘就是受，还要提取密码=_=
            if (new Regex("请输入提取密码").IsMatch(html))
            {
                DateTime Time1 = DateTime.UtcNow;
                DateTime Time2 = Convert.ToDateTime("1970-01-01");
                TimeSpan span = Time1 - Time2;
                string t = span.TotalMilliseconds.ToString("0");
                string shareid = new Regex(@"(?<=shareid=)\d+").Match(target_url).Value;
                string uk = new Regex(@"(?<=uk=)\d+").Match(target_url).Value;
                string verify_url = "http://pan.baidu.com/share/verify?shareid=" + shareid + "&uk=" + uk + "&t=" + t;
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("pwd", pwd);
                parameters.Add("vcode", "");
                HttpWebResponse verify_response = HttpWebResponseUtility.CreatePostHttpResponse(verify_url, parameters, null, null, Encoding.UTF8, baipancookie, true);
                string verify_html = DecompressWebResponse.Decompress(verify_response, Encoding.UTF8);
                if (new Regex("\"errno\":0").IsMatch(verify_html))
                {
                    baipancookie.Add(verify_response.Cookies);
                    html = DecompressWebResponse.Decompress(HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, baipancookie, true), Encoding.UTF8);
                }
                else
                {
                    return "error occured : baidupan verify failed (maybe need vcode)";
                }
            }
            result = new Regex(@"(?<=dlink\\"":\\"").*?(?=\\"")").Match(html).Value.Replace(@"\\/", "/");
            return result;
        }

        /// <summary>
        /// 获得howfile下载地址
        /// </summary>
        /// <param name="target_url">需提取的url</param>
        /// <param name="cookies">howfile下载需要的验证cookie</param>
        /// <returns>下载地址</returns>
        public static string howfile(string target_url, short server, ref CookieCollection cookies)
        {
            string result = "";
            HttpWebResponse cookie_res = (HttpWebResponse)HttpWebResponseUtility.CreateGetHttpResponse(target_url, null, null, null, true);
            string html = DecompressWebResponse.Decompress(cookie_res, Encoding.UTF8);
            cookies = CookieUtility.getResponseCookies(cookie_res, "howfile.com");
            //MatchCollection sdfsdf = new Regex(@"(?<=<a href="")http://dl\d+.howfile.com.*?(?="")").Matches(html);
            result = new Regex(@"(?<=<a href="")http://dl\d+.howfile.com.*?(?="")").Matches(html)[server].Value;
            Match match = new Regex(@"onclick='setCookie\(""(.*?)"", ""(.*?)"", 1\*60\*60\*1000\);setCookie\(""(.*?)"", ""(.*?)"", 1\*60\*60\*1000\);").Match(html);
            cookies.Add(new Cookie(match.Groups[1].Value, match.Groups[2].Value, "/", "howfile.com"));
            cookies.Add(new Cookie(match.Groups[3].Value, match.Groups[4].Value, "/", "howfile.com"));
            return result;
        }
    }
}
