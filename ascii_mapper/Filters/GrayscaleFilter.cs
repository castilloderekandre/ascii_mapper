using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ascii_mapper.Filters
{
    internal class GrayscaleFilter : IFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            Bitmap newBitmap = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                   });

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);

                    g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                       0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);

                }
            }

            return newBitmap;
        }

    }
}
