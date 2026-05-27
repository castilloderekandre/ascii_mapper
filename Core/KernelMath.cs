using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class KernelMath
    {
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
        public static double Gaussian1D(int x, double sigma, int u = 0)
        {
            double sigmaSquared2 = 2 * sigma * sigma;
            double coefficient = 1.0 / Math.Sqrt(Math.PI * sigmaSquared2);
            double exponent = -((x - u) * (x - u)) / sigmaSquared2;
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
        public static double[] GenerateGaussianKernel(int diameter, double sigma, int u = 0)
        {
            int size = diameter * diameter;
            double normalizationSum = 0.0;
            double[] kernel = new double[size];

            for (int i = 0; i < size; i++)
            {
                int x = i - size / 2;
                kernel[i] = Gaussian1D(x, sigma, u);
                normalizationSum += kernel[i];
            }

            // Normalize the kernel
            for (int i = 0; i < size; i++)
            {
                kernel[i] /= normalizationSum;
            }

            return kernel;
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
        public static int Bounce(int value, int max)
        {
            int absValue = Math.Abs(value);

            if (absValue <= max)
                return absValue;

            return max - ((absValue + max) % max);
        }
    }
}
