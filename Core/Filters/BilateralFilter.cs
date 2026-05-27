using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Filters
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

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double windowSum = 0;
                    double kernel_sum = 0;
                    int centerPixelIntensity = image.GetPixel(j, i).R; // Assuming grayscale image

                    for (int windowY = 0; windowY < _spatialKernel.Diameter; windowY++)
                    {
                        for (int windowX = 0; windowX < _spatialKernel.Diameter; windowX++)
                        {
                            int x = KernelMath.Bounce(j + windowX - _spatialKernel.Radius, width - 1);
                            int y = KernelMath.Bounce(i + windowY - _spatialKernel.Radius, height - 1);

                            int neighborPixelIntensity = image.GetPixel(x, y).R; // Assuming grayscale image

                            // Not materializing range kernel as an array, calculating inline during accumulation to reduce computational complexity
                            double rangeWeight = _spatialKernel.Kernel[windowY * (_spatialKernel.Diameter - 1) + windowX] * KernelMath.Gaussian1D(neighborPixelIntensity, _rangeKernel.StandardDeviation, centerPixelIntensity);

                            windowSum += rangeWeight * neighborPixelIntensity;
                            kernel_sum += rangeWeight;
                        }
                    }

                    windowSum /= kernel_sum;
                    windowSum = Math.Min(255, Math.Max(0, windowSum)); // Clamp to [0, 255]

                    filteredImage.SetPixel(j, i, Color.FromArgb((int)windowSum, (int)windowSum, (int)windowSum)); // Set the new pixel value
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
