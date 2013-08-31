using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace DoujinManager
{
    public class DownLoadFile
    {

        public class DownLoadFileEventArgs : EventArgs
        {
            public readonly string msg;
            public DownLoadFileEventArgs(float message)
            {
                msg = System.Convert.ToString(message);
            }
            public DownLoadFileEventArgs(long message)
            {
                msg = System.Convert.ToString(message);
            }
            public DownLoadFileEventArgs(bool message)
            {
                msg = System.Convert.ToString(message);
            }
        }

        /// <summary>
        /// 文件长度通知事件
        /// </summary>
        public event EventHandler<DownLoadFileEventArgs> FileBytesEventHandler;

        /// <summary>
        /// 下载百分比通知事件
        /// </summary>
        public event EventHandler<DownLoadFileEventArgs> PercentEventHandler;

        /// <summary>
        /// 下载成功通知事件
        /// </summary>
        public event EventHandler<DownLoadFileEventArgs> DownSuccessEventHandler;

        /// <summary>
        /// 下载文件方法
        /// </summary>
        /// <param name="strFileName">文件保存路径和文件名（不带扩展名）</param>
        /// <param name="strUrl">下载地址</param>
        ///
        public void DownloadFile(string strFileName, string strUrl, string referer, string useragent, ref string ext, CookieCollection cookies = null)
        {
            int CompletedLength = 0;//记录已完成的大小
            long sPosstion = 0;
            long count = 0;
            FileStream FStream;
            if (File.Exists(strFileName))
            {
                FStream = File.OpenWrite(strFileName);
                sPosstion = FStream.Length;
                FStream.Seek(sPosstion, SeekOrigin.Current);//移动文件流中的当前指针
            }
            else
            {
                FStream = new FileStream(strFileName, FileMode.Create);
                sPosstion = 0;
            }
            //打开网络连接
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myRequest.KeepAlive = true;
                if (sPosstion > 0) { myRequest.AddRange((int)sPosstion); }//设置Range值
                if (referer != null) { myRequest.Referer = referer; }
                if (useragent != null) { myRequest.UserAgent = useragent; }
                else { myRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1588.0 Safari/537.36"; }
                if (cookies != null)
                {
                    CookieContainer cc = new CookieContainer();
                    cc.Add(cookies);
                    myRequest.CookieContainer = cc;
                }
                //向服务器请求，获得服务器的回应数据流
                HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
                MatchCollection mcext = Regex.Matches(webResponse.Headers.Get("Content-Disposition"), @"(?<==.*\.)\w+");
                ext = mcext[mcext.Count - 1].Value;
                //ext = webResponse.Headers.Get("Content-Disposition").Split('=')[1].Split('.')[1];
                FileBytesEventHandler(this, new DownLoadFileEventArgs(webResponse.ContentLength + sPosstion));
                Stream myStream = webResponse.GetResponseStream();
                byte[] btContent = new byte[1024];
                if (count <= 0) count += sPosstion;

                while ((CompletedLength = myStream.Read(btContent, 0, 1024)) > 0)
                {
                    FStream.Write(btContent, 0, CompletedLength);
                    count += CompletedLength;
                    PercentEventHandler(this, new DownLoadFileEventArgs(count));
                }
                FStream.Close();
                myStream.Close();
                DownSuccessEventHandler(this, new DownLoadFileEventArgs(true));
            }
            catch (Exception)
            {
                FStream.Close();
                DownSuccessEventHandler(this, new DownLoadFileEventArgs(false));
            }
        }
    }
}