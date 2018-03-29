using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geneticism.Accuracy;
using Geneticism.Core.Interface;
using Geneticism.Managers;
using Geneticism.Units;

namespace Geneticism
{
    class Program
    {
        static void Main(string[] args)
        {
            //var seedParams = new Dictionary<string, object>();
            //seedParams.Add("populationSize", 100);
            //seedParams.Add("generations", 500);
            //var manager = new StringUnitPopulationManager(seedParams, "Hello world, my name is Steven.");
            //manager.SeedPopulation();
            //manager.Go();
            //manager.PrintPopulationStats();
            Console.WriteLine(MutationProbabilityFinder.FindBestMutationProbability());
            Console.ReadKey();
        }
    }
}
