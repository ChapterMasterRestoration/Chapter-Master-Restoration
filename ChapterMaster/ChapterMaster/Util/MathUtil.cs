﻿using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Util
{
    static class MathUtil
    {
        // https://github.com/martindevans/CasualGodComplex/blob/master/CasualGodComplex/RandomExtensions.cs
        /// <summary>
        /// Generates a single normal value clamped within min and max
        /// </summary>
        /// <remarks>
        /// Originally used inverse phi method, but this method, found here:
        /// http://arxiv.org/pdf/0907.4010.pdf
        /// is significantly faster
        /// </remarks>
        /// <param name="random">A (uniform) random number generator</param>
        /// <param name="standardDeviation">The standard deviation of the distribution</param>
        /// <param name="mean">The mean of the distribution</param>
        /// <param name="min">The minimum allowed value (does not bias)</param>
        /// <param name="max">The max allowed value (does not bias)</param>
        /// <returns>A single sample from a normal distribution, clamped to within min and max in an unbiased manner.</returns>
        public static float NormallyDistributedSingle(this Random random, float standardDeviation, float mean, float min, float max)
        {
            var nMax = (max - mean) / standardDeviation;
            var nMin = (min - mean) / standardDeviation;
            var nRange = nMax - nMin;
            var nMaxSq = nMax * nMax;
            var nMinSq = nMin * nMin;
            var subFrom = nMinSq;
            if (nMin < 0 && 0 < nMax) subFrom = 0;
            else if (nMax < 0) subFrom = nMaxSq;

            var sigma = 0.0;
            double u;
            float z;
            do
            {
                z = nRange * (float)random.NextDouble() + nMin; // uniform[normMin, normMax]
                sigma = Math.Exp((subFrom - z * z) / 2);
                u = random.NextDouble();
            } while (u > sigma);

            return z * standardDeviation + mean;
        }
        public static int ClampInt(int x, int min, int max)
        {
            return Math.Max(Math.Min(x,max),min);
        }
    }
}