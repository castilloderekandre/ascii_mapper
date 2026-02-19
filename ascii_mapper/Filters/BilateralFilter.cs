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
        private double _rangeSigma;
        private double _spatialSigma;
        private int _rangeKernelRadius;
        private int _spatialKernelRadius;

        public struct Kernel
        {
            public Kernel(double[] values, double sigma)
            {
                Values = values;
                Sigma = sigma;
                Diameter = (int)(6 * Sigma) | 1;
                TruncatedRadius = Diameter / 2;
            }

            public double Sigma { get; }
            public double[] Values { get; }
            public int Diameter { get; }
            public int TruncatedRadius { get; }

        }

        public BilateralFilter(double rangeStandardDeviation, double spatialStandardDeviation)
        {
            _rangeSigma = rangeStandardDeviation;
            _rangeKernelRadius = CalculateKernelDiameter(_rangeSigma);

            _spatialSigma = spatialStandardDeviation;
            _spatialKernelRadius = CalculateKernelDiameter(_spatialSigma);
        }

        public int CalculateKernelDiameter(double standardDeviation)
        {
            return (int)(6 * standardDeviation) | 1; // Ensure kernel size is odd
        }

        public Bitmap Apply(Bitmap image)
        {
            Bitmap filteredImage = new Bitmap(image);
            int width = image.Width;
            int height = image.Height;

            // Precompute spatial kernel
            double[] spatialKernel = KernelMath.GenerateKernel(_spatialKernelRadius, _spatialSigma);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    for(int ky = 0; ky < _spatialKernelRadius; ky++)
                    {
                        for(int kx = 0; kx < _spatialKernelRadius; kx++)
                        {
                            int neighborX = KernelMath.Bounce(j + kx - _spatialKernelRadius / 2, width);
                            int neighborY = KernelMath.Bounce(i + ky - _spatialKernelRadius / 2, height);
                            Color neighborColor = image.GetPixel(neighborX, neighborY);
                            Color centerColor = image.GetPixel(j, i);
                            double rangeWeight = Math.Exp(-Math.Pow(neighborColor.R - centerColor.R, 2) / (2 * Math.Pow(_rangeSigma, 2))) *
                                                 Math.Exp(-Math.Pow(neighborColor.G - centerColor.G, 2) / (2 * Math.Pow(_rangeSigma, 2))) *
                                                 Math.Exp(-Math.Pow(neighborColor.B - centerColor.B, 2) / (2 * Math.Pow(_rangeSigma, 2)));
                            double spatialWeight = spatialKernel[ky] * spatialKernel[kx];
                            double weight = rangeWeight * spatialWeight;
                            // Accumulate weighted color values
                            // (This part would need to be implemented to compute the final color)
                        }
                    }
                }
            }
            
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
