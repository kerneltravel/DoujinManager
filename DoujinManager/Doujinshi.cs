using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;

namespace DoujinManager
{
    [Serializable]
    class Doujinshi
    {
        /// <summary>
        /// 未格式化的原始标题
        /// </summary>
        public string ori_name;
        /// <summary>
        /// 标题
        /// </summary>
        public string name;
        /// <summary>
        /// 封面网络地址
        /// </summary>
        public string cover_url;
        /// <summary>
        /// 封面本地地址
        /// </summary>
        public string local_cover_url;
        /// <summary>
        /// 文件下载地址列表
        /// </summary>
        public List<System.Collections.ArrayList> file_url;
        /// <summary>
        /// 文件本地存储地址
        /// </summary>
        public string local_file_url;
        /// <summary>
        /// 作者
        /// </summary>
        public string author;
        /// <summary>
        /// 首发展会
        /// </summary>
        public string party;
        /// <summary>
        /// 汉化组
        /// </summary>
        public string localization_group = "未汉化";
        /// <summary>
        /// 内容页
        /// </summary>
        public string download_url;
        /// <summary>
        /// 下载/解压密码
        /// </summary>
        public string passwd;
        /// <summary>
        /// 标签
        /// </summary>
        public List<String> tag = new List<string>();

        [NonSerialized]
        /// <summary>
        /// 封面
        /// </summary>
        public Image cover;
        [NonSerialized]
        /// <summary>
        /// 直链下载地址
        /// </summary>
        public string direct_download_url;
        [NonSerialized]
        /// <summary>
        /// 已下载的百分比
        /// </summary>
        public long down_percent;
        [NonSerialized]
        /// <summary>
        /// howfile的验证cookies
        /// </summary>
        public CookieCollection howfile_cookies = null;
    }
}
