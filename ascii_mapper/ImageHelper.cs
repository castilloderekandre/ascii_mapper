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

        public float AspectRatio { get; set; }

        public ImageHelper(Bitmap image)
        {
            this.Image = new Bitmap(image);
            this.AspectRatio = (float)this.Image.Width / this.Image.Height;
        }

        public void Resize(int width, int height)
        {
            Resize(new Size(width, height));
        }

        public void Resize(Size size) 
        {
            this.Image = MakeResize(this.Image, size);
        }

        public void Grayscale()
        {
            this.Image = MakeGrayscale(this.Image); 
        }

        public void Downscale(float factor)
        {
            this.Image = MakeDownscale(this.Image, factor);
        }

        public void DownscaleByWidth(int width)
        {
            this.Image = MakeDownscaleByWidth(this.Image, width);
        }

        public void DownscaleByHeight(int height)
        {
            this.Image = MakeDownscaleByHeight(this.Image, height);
        }

        public Bitmap MakeResize(Bitmap image, Size size)
        {
            Point point = new Point(0, 0);
            var destRect = new Rectangle(point, size);
            Bitmap resizedImage = new Bitmap(this.Image, size);

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

        public Bitmap MakeGrayscaleLegacy()
        {
            //make an empty bitmap the same size as original
            Bitmap original = new Bitmap(this.Image);
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                        + (originalColor.B * .11));

                    //create the color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }

        public Bitmap MakeGrayscale(Bitmap image)
        {
            Bitmap newBitmap = new Bitmap(image.Width, image.Height);

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

            return newBitmap;
        }

        public Bitmap MakeDownscaleByWidth(Bitmap image, int width)
        {
            float factor = (float)image.Width / width;
            return MakeDownscale(image, factor);
        }

        public Bitmap MakeDownscaleByHeight(Bitmap image, int height)
        {
            float factor = (float)image.Height / height;
            return MakeDownscale(image, factor);
        }

        public Bitmap MakeDownscale(Bitmap image, float factor)
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
            //return this.MakeResize(newImage, image.Size);
        }
    }
}
