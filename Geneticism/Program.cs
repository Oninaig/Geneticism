using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Geneticism.Accuracy;
using Geneticism.Managers;
using Geneticism.Performance;
using Geneticism.Units;
using Console = Colorful.Console;
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
            //TestHammingPerformance();
            Console.ReadKey();
        }

        public static void TestHammingPerformance()
        {
            Console.WriteLine("Starting first test.");
            Stopwatch t1 = new Stopwatch();
            t1.Start();
            for (int i = 0; i < 1000000; i++)
            {

                PerformanceTester.CalculateFitnessLoop("HELLO WORLD, MY NAME IS STEVEN.",
                    "JFHHE WOERDL MS ERQE IS OEEVTN.");
            }
            t1.Stop();

            Console.WriteLine("Starting second test.");
            Stopwatch t2 = new Stopwatch();
            t2.Start();
            for (int i = 0; i < 1000000; i++)
            {
                PerformanceTester.CalculateFitnessByte("HELLO WORLD, MY NAME IS STEVEN.",
                    "JFHHE WOERDL MS ERQE IS OEEVTN.");
            }
            t2.Stop();
            Console.WriteLine($"First test: {t1.ElapsedMilliseconds}. Second test: {t2.ElapsedMilliseconds}");

        }
    }
}
