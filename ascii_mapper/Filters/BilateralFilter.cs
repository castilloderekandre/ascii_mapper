using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ascii_mapper.Filters
{
    internal class BilateralFilter: IFilter
    {
        GaussianKernel _rangeKernel;
        GaussianKernel _spatialKernel;

        public BilateralFilter(double rangeStandardDeviation, double spatialStandardDeviation)
        {
            _spatialKernel = new GaussianKernel(spatialStandardDeviation);
            _spatialKernel.GenerateGaussianKernel();
            _rangeKernel = new GaussianKernel(rangeStandardDeviation);
        }


        public Bitmap Apply(Bitmap image)
        {
            Bitmap filteredImage = new Bitmap(image);
            int width = image.Width;
            int height = image.Height;

            return filteredImage;
        }

        //private float[] MakeRangeKernel()
        //{
            
        //}

        //private float[] MakeSpatialKernel()
        //{

        //}
    }
}
