using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geneticism.Core;
using Geneticism.Managers;

namespace Geneticism.Accuracy
{
    public static class MutationProbabilityFinder
    {

        public static double FindBestMutationProbability()
        {
            int bestGeneration = 500;
            double bestProbability = 0.0;
            object _locker = new object();

            for (double i = 0.0; i < 0.02; i += 0.001)
            {
                var options = new ParallelOptions();
                options.MaxDegreeOfParallelism = 6;
                Console.WriteLine($"Testing probability {i}");
                Parallel.For(0, 100, options, k =>
                {
                    Globals.BaseMutationChance = i;

                    var seedParams = new Dictionary<string, object>();
                    seedParams.Add("populationSize", 100);
                    seedParams.Add("generations", 500);

                    var manager = new StringUnitPopulationManager(seedParams, "Hello world, my name is Steven.");
                    manager.SeedPopulation();
                    var solutionGeneration = manager.Go();

                    lock (_locker)
                    {
                        if (solutionGeneration < bestGeneration)
                        {
                            bestGeneration = solutionGeneration;
                            bestProbability = i;
                        }
                    }
                });
            }
            return bestProbability;
        }
    }
}
