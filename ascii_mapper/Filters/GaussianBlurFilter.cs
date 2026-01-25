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
            this._kernel = GenerateKernel(_kernelSize, standardDeviation); //Precompute the kernel
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
                        int sampleIndex = Bounce(j + k, innerLimit - 1);
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

        /// <summary>
        /// Calculates a non-negative integer that reflects the input value within the range from 0 to the specified
        /// maximum, bouncing back from the maximum if exceeded.
        /// </summary>
        /// <remarks>This method is useful for creating a 'bouncing' effect where values exceeding the
        /// maximum are reflected back into the range, rather than wrapping around or clamping. Negative input values
        /// are treated as their absolute value.</remarks>
        /// <param name="value">The integer value to be reflected within the range. Can be positive or negative.</param>
        /// <param name="max">The maximum value of the range. Must be greater than zero.</param>
        /// <returns>A non-negative integer between 0 and max, inclusive, representing the reflected value.</returns>
        private int Bounce(int value, int max)
        {
            int absValue = Math.Abs(value);

            if (absValue <= max)
                return absValue;

            return max - ((absValue + max) % max);
        }

        /// <summary>
        /// Calculates the value of the Gaussian (normal) distribution function at a specified integer position and
        /// standard deviation.
        /// </summary>
        /// <remarks>This method evaluates the unnormalized Gaussian function, which is commonly used in
        /// signal processing and image filtering. The result is not scaled to ensure the total area under the curve
        /// equals one.</remarks>
        /// <param name="x">The integer position at which to evaluate the Gaussian function.</param>
        /// <param name="sigma">The standard deviation of the Gaussian distribution. Must be greater than zero.</param>
        /// <returns>The value of the Gaussian distribution at the specified position and standard deviation.</returns>
        private double Gaussian(int x, double sigma)
        {
            double sigmaSquared2 = 2 * sigma * sigma;
            double coefficient = 1.0 / Math.Sqrt(Math.PI * sigmaSquared2);
            double exponent = -(x * x) / sigmaSquared2;
            return coefficient * Math.Exp(exponent);
        }

        /// <summary>
        /// Generates a one-dimensional, normalized Gaussian kernel with the specified size and standard deviation.
        /// </summary>
        /// <remarks>The generated kernel is centered such that the middle element corresponds to the mean
        /// of the distribution. This kernel can be used for convolution operations such as image blurring or
        /// smoothing.</remarks>
        /// <param name="size">The number of elements in the kernel. Must be a positive integer.</param>
        /// <param name="sigma">The standard deviation of the Gaussian distribution. Must be greater than 0.</param>
        /// <returns>An array of doubles representing the normalized Gaussian kernel. The sum of all elements in the array is 1.</returns>
        private double[] GenerateKernel(int size, double sigma)
        {
            double normalizationSum = 0.0;
            double[] kernel = new double[size];

            for (int i = 0; i < size; i++)
            {
                int x = i - size / 2;
                kernel[i] = Gaussian(x, sigma);
                normalizationSum += kernel[i];
            }

            // Normalize the kernel
            for (int i = 0; i < size; i++)
            {
                kernel[i] /= normalizationSum;
            }

            return kernel;
        }
    }

}
