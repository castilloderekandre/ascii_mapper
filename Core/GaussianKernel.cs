using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class GaussianKernel
    {
        public double[] Kernel { get; private set; }


        private int _diameter;
        public int Diameter 
        { 
            get { return _diameter; }
            private set
            {
                _diameter = value;
                Radius = _diameter / 2;
            }
        }
        public int Radius { get; private set; }
        private double _standardDeviation;
        public double StandardDeviation { 
            get { return _standardDeviation; }
            set 
            {
                _standardDeviation = value;  
                Diameter = CalculateKernelDiameter(_standardDeviation);
            }
        }

        public GaussianKernel(double standardDeviation)
        {
            StandardDeviation = standardDeviation;
        }

        public void GenerateGaussianKernel()
        {
            Kernel = KernelMath.GenerateGaussianKernel(Diameter, StandardDeviation);
        }

        private int CalculateKernelDiameter(double standardDeviation)
        {
            return (int)(6 * standardDeviation) | 1; // Ensure kernel size is odd
        }
    }
}
