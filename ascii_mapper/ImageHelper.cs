using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ascii_mapper
{
    internal static class ImageHelper
    {
        public static Bitmap MakeResize(Bitmap image, int width, int height)
        {
            return MakeResize(image, new Size(width, height));
        }

        public static Bitmap MakeResize(Bitmap image, Size size)
        {
            Point point = new Point(0, 0);
            var destRect = new Rectangle(point, size);
            Bitmap resizedImage = new Bitmap(image, size);

            resizedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return resizedImage;
        }

        public static Bitmap MakeDownscaleByWidth(Bitmap image, int width)
        {
            float factor = (float)image.Width / width;
            return MakeDownscale(image, factor);
        }

        public static Bitmap MakeDownscaleByHeight(Bitmap image, int height)
        {
            float factor = (float)image.Height / height;
            return MakeDownscale(image, factor);
        }

        public static Bitmap MakeDownscale(Bitmap image, float factor)
        {
            Bitmap newImage = new Bitmap((int)(image.Width / factor), (int)(image.Height / factor));

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(image, new Rectangle(0, 0, newImage.Width, newImage.Height),
                   0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            }

            return newImage;
        }
    }
}
