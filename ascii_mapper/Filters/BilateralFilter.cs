using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ascii_mapper.Filters
{
    internal class BilateralFilter: IFilter
    {
        private double _sigma;

        public BilateralFilter(double standardDeviation)
        {
            _sigma = standardDeviation;
        }

        public Bitmap Apply(Bitmap image)
        {
            // Placeholder implementation for Bilateral Filter
            // Actual implementation would involve edge-preserving smoothing
            return image;
        }

        //private float[] MakeRangeKernel()
        //{
            
        //}

        //private float[] MakeSpatialKernel()
        //{

        //}



    }
}
