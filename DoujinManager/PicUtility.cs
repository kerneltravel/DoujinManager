using System.Drawing;

namespace DoujinManager
{
    class PicUtility
    {
        /// <summary>
        /// 生成缩略图(最终图片固定大小,图片按比例缩小)
        /// </summary>
        /// <param name="sourceImg">原图片=</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="backColor">如果图片按比例缩小后不能填充满缩略图,则使用此颜色填充(比如"#FFFFFF")</param>
        public static Image MakePic(Image sourceImg, int width, int height, string backColor)
        {
            System.Drawing.Image originalImage = sourceImg;
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            string mode;
            if (ow < towidth && oh < toheight)
            {
                towidth = ow;
                toheight = oh;
            }
            else
            {
                if (originalImage.Width / originalImage.Height >= width / height)
                {
                    mode = "W";
                }
                else
                {
                    mode = "H";
                }
                switch (mode)
                {
                    case "W"://指定宽，高按比例 
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case "H"://指定高，宽按比例 
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    default:
                        break;
                }
            }

            //新建一个bmp图片 
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(width, height);
            //新建一个画板 
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以指定颜色填充 
            g.Clear(ColorTranslator.FromHtml(backColor));
            //在指定位置并且按指定大小绘制原图片的指定部分 
            int top = (height - toheight) / 2;
            int left = (width - towidth) / 2;
            g.DrawImage(originalImage, new System.Drawing.Rectangle(left, top, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);
            return bitmap;
        }
    }
}
