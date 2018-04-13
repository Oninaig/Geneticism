using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geneticism.Performance
{
    public static class PerformanceTester
    {
        public static int CalculateFitnessLoop(string source, string target)
        {
            if (source.Length != target.Length)
                throw new ArgumentException("Strings must be same legnth.");
            var fitness = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == target[i])
                    fitness++;
            }
            return fitness;
        }

        public static int CalculateFitnessByte(string source, string target)
        {
            if (source.Length != target.Length)
                throw new ArgumentException("Strings must be same legnth.");
            int distnace = source.ToCharArray().Zip(target.ToCharArray(), (c1, c2) => new {c1, c2})
                .Count(m => m.c1 != m.c2);
            return distnace;
        }
    }
}
