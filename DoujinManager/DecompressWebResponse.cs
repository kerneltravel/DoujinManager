using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace DoujinManager
{
    class DecompressWebResponse
    {
        /// <summary>
        /// 解压缩网页数据流
        /// </summary>
        /// <param name="response">需解压的HttpWebResponse</param>
        /// <param name="Encoding">网页的编码</param>
        /// <returns>网页的源代码</returns>
        public static string Decompress(HttpWebResponse response, Encoding Encoding)
        {
            string data;
            string ce = response.Headers[HttpResponseHeader.ContentEncoding];
            int ContentLength = (int)response.ContentLength;
            Stream s = response.GetResponseStream();
            if (ce == "gzip")
            {
                GZipStream g = new GZipStream(s, CompressionMode.Decompress);
                System.IO.StreamReader sr = new System.IO.StreamReader(g, Encoding);
                data = (sr.ReadToEnd());
                sr.Close();
            }
            else if (ce == "deflate")
            {
                DeflateStream g = new DeflateStream(s, CompressionMode.Decompress);
                System.IO.StreamReader sr = new System.IO.StreamReader(g, Encoding);
                data = (sr.ReadToEnd());
                sr.Close();
            }
            else
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(s, Encoding);
                data = (sr.ReadToEnd());
                sr.Close();
            }
            s.Close();
            return data;
        }
    }
}
