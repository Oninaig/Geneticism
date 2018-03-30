using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geneticism.Core;
using Geneticism.Managers;
using Console = Colorful.Console;

namespace Geneticism.Accuracy
{
    public static class MutationProbabilityFinder
    {
        public static double FindBestMutationProbability()
        {
            var bestGeneration = 500;
            var bestProbability = 0.0;
            var _locker = new object();

            for (var i = 0.0; i < 0.02; i += 0.001)
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

            Console.WriteLine(
                $"Gen0 - {GC.CollectionCount(0)} | Gen1 - {GC.CollectionCount(1)} | Gen2- {GC.CollectionCount(2)}");
            return bestProbability;
        }
    }
}