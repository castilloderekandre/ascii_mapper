using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ascii_mapper
{
    internal class ImagePatch
    {
        unsafe struct FeatureVector
        {
            public float mean;
            public float std;
            public float edgeDensity;
            public float gradientMean;
            public float gradientStd;

            public fixed float orientationHistogram[8];

            public float fill;
            public byte dominant;
        }
        public ImagePatch()
        {

        }
    }
}
