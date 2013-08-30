using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
namespace DoujinManager
{
    public class Global
    {
        /// <summary>
        /// 可登录站点列表枚举
        /// </summary>
        public enum urls { hentai, soulplus, xhentai };
        /// <summary>
        /// 下载方式枚举
        /// </summary>
        public enum downmethod { BT, HT_archive, howfile, jbpan, baidupan, xunleikuai, pan115 };

        public static bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9') { return false; }
            }
            return true;
        }
    }

    [Serializable]
    public class Settings
    {
        public Int32 pic_load_sametime = 6;
        public Int32 pic_load_timeout = 15000;
        public Int16 use_proxy = 0;
        public String proxy = "";
        public Int32 proxy_port = 0;
        public Int32 Thumbnail_Width = 187;
        public Int32 Thumbnail_Height = 256;
        public Int16 use_pic_proxy = 0;
        public String pic_proxy = "127.0.0.1";
        public Int32 pic_proxy_port = 8087;
        public String defaultFolder = @"D:\Doujin\";
        public Int16 howfile_server = 2;
        public Boolean soulplus_enabled = false;
        public Boolean hentai_enabled = false;

    }

    public static class Universe
    {
        public static void SaveSettings(Settings settings, string SavePath)
        {
            XmlSerializer xmlformat = new XmlSerializer(typeof(Settings));
            using (Stream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fs, settings);
            }
        }
        public static Settings LoadSettings(string SavePath)
        {
            XmlSerializer xmlformat = new XmlSerializer(typeof(Settings));
            using (Stream fs = File.OpenRead(SavePath))
            {
                return (Settings)xmlformat.Deserialize(fs);
            }
        }
    }

}
