using System;
using System.IO;
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
        public int pic_load_sametime = 6;
        public int pic_load_timeout = 15000;
        public short use_proxy = 0;
        public string proxy = "";
        public int proxy_port = 0;
        public int Thumbnail_Width = 187;
        public int Thumbnail_Height = 256;
        public short use_pic_proxy = 2;
        public string pic_proxy = "127.0.0.1";
        public int pic_proxy_port = 8087;
        public string defaultFolder = "D:/Doujin/";

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
