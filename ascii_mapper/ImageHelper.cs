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
    internal class ImageHelper
    {
        public Bitmap Image { get; set; }
        public ImageHelper(Bitmap image)
        {
            this.Image = image;
        }

        public void Resize(int width, int height)
        {
            Resize(new Size(width, height));
        }

        public void Resize(Size size) 
        {
            Point point = new Point(0, 0);
            var destRect = new Rectangle(point, size);
            Bitmap resizedImage = new Bitmap(this.Image, size);
            Bitmap image = this.Image;

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

            this.Image = resizedImage;
        }

        public Bitmap MakeGrayscale3()
        {
            Bitmap newBitmap = new Bitmap(this.Image.Width, this.Image.Height);
            Bitmap image = this.Image;

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                   });

                //create some image attributes
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    //set the color matrix attribute
                    attributes.SetColorMatrix(colorMatrix);

                    //draw the original image on the new image
                    //using the grayscale color matrix
                    g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                       0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);

                }
            }

            return image;
        }
    }
}
