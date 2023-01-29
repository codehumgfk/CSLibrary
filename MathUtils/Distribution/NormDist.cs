using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.Distribution
{
    public static class NormDist
    {
        public static double PDF(double mu, double sigma, double x)
        {
            return StandardNormDist.PDF((x - mu) / sigma);
        }
        public static double CDF(double mu, double sigma, double x)
        {
            return StandardNormDist.CDF((x - mu) / sigma);
        }
    }
}
