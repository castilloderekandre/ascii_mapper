using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ascii_mapper.Filters
{
    internal class GaussianBlurFilter : IFilter
    {
        private readonly double _standardDeviation;
        private readonly int _kernelSize;
        private readonly double[] _kernel;
        enum Axis
        {
            Horizontal,
            Vertical
        }
        public GaussianBlurFilter(double standardDeviation)
        {
            this._standardDeviation = standardDeviation;
            this._kernelSize = (int)(6 * standardDeviation) | 1; // Ensure kernel size is odd
            this._kernel = KernelMath.GenerateKernel(_kernelSize, standardDeviation); //Precompute the kernel
        }
        public Bitmap Apply(Bitmap image)
        {
            Bitmap smoothedImage = Convolve(image, _kernel, Axis.Horizontal);
            smoothedImage = Convolve(smoothedImage, _kernel, Axis.Vertical);
            return smoothedImage;
        }

        private Bitmap Convolve(Bitmap image, double[] kernel, Axis axis)
        {
            Bitmap smoothedImage = new Bitmap(image);
            int width = smoothedImage.Width;
            int height = smoothedImage.Height;
            int kHalf = _kernelSize / 2 | 0;
            
            int outerLimit = axis == Axis.Horizontal ? height : width;
            int innerLimit = axis == Axis.Horizontal ? width : height;

            for (int i = 0; i < outerLimit; i++)
            {
                for (int j = 0; j < innerLimit; j++)
                {
                    double convolutionSum = 0.0;
                    for (int k = -kHalf; k <= kHalf; k++)
                    {
                        int sampleIndex = KernelMath.Bounce(j + k, innerLimit - 1);
                        Color sampleColor = axis == Axis.Horizontal ? smoothedImage.GetPixel(sampleIndex, i) : smoothedImage.GetPixel(i, sampleIndex);
                        double intensity = (sampleColor.R + sampleColor.G + sampleColor.B) / 3.0; //Use GrayscaleImage.Luminance property when implemented
                        convolutionSum += intensity * kernel[k + kHalf];
                    }
                    int clampedValue = Math.Min(255, Math.Max(0, (int)convolutionSum));
                    Color newColor = Color.FromArgb(clampedValue, clampedValue, clampedValue);
                    if (axis == Axis.Horizontal)
                        smoothedImage.SetPixel(j, i, newColor);
                    else
                        smoothedImage.SetPixel(i, j, newColor);
                }
            }

            return smoothedImage;
        }
    }

}
