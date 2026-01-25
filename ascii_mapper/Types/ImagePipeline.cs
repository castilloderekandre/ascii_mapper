using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ascii_mapper
{
    internal class ImagePipeline
    {
        public List<IFilter> Filters { get; } = new List<IFilter>();

        public Bitmap Process(Bitmap image)
        {
            Bitmap currentImage = image;
            foreach (var filter in Filters)
            {
                currentImage = filter.Apply(currentImage);
            }
            return currentImage;
        }
    }
}
