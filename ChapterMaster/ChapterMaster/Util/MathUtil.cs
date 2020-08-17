using Microsoft.Xna.Framework;
using System;


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
        public static Rectangle VectorToRectangle(Vector2 start, Vector2 end)
        {
            return new Rectangle((int)start.X, (int)start.Y, (int)end.X, (int)end.Y);
        }
        public static int RoundedDistance(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Round(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
        }
        public static int RoundedDistance(Vector2 a, Vector2 b)
        {
            return (int)Math.Round((b - a).Length());
        }
    }
}
