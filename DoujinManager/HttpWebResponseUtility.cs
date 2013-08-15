using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DoujinManager
{
    /// <summary>
    /// 有关HTTP请求的辅助类
    /// </summary>
    public class HttpWebResponseUtility
    {
        private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.0 Safari/537.36";
        /// <summary>
        /// 创建GET方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <param name="AutoRedirect">是否自动对302进行跳转，false时将进行逐过程cookie合并并跳转</param>
        /// <returns></returns>
        public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies, bool AutoRedirect)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            request.CookieContainer = new CookieContainer();
            WebHeaderCollection myWebHeaderCollection = request.Headers;

            //Include English in the Accept-Langauge header. 
            myWebHeaderCollection.Add("Accept-Language", "zh-cn;q=0.8");
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer.Add(cookies);
            }
            request.AllowAutoRedirect = AutoRedirect;
            HttpWebResponse result = request.GetResponse() as HttpWebResponse;
            if (!AutoRedirect)
            {
                if (result.StatusCode == HttpStatusCode.Found)
                {
                    result.Cookies.Add(cookies);
                    CookieCollection temp_coo = result.Cookies;
                    result = HttpWebResponseUtility.CreateGetHttpResponse(result.Headers[HttpResponseHeader.Location], null, null, temp_coo, false);
                    result.Cookies.Add(temp_coo);
                }
            }
            return result;
        }
        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <param name="AutoRedirect">是否自动对302进行跳转，false时将进行逐过程cookie合并并跳转</param>
        /// <returns></returns>
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies, bool AutoRedirect)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
            request.CookieContainer = new CookieContainer();
            //

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            request.AllowAutoRedirect = AutoRedirect;
            HttpWebResponse result = request.GetResponse() as HttpWebResponse;
            if (!AutoRedirect)
            {
                if (result.StatusCode == HttpStatusCode.Found)
                {
                    result.Cookies.Add(cookies);
                    CookieCollection temp_coo = result.Cookies;
                    result = HttpWebResponseUtility.CreateGetHttpResponse(result.Headers[HttpResponseHeader.Location], null, null, temp_coo, false);
                    result.Cookies.Add(temp_coo);
                }
            }
            return result;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }
    }
}