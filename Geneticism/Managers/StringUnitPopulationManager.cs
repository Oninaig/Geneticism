using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geneticism.Core;
using Geneticism.Units;
using Console = Colorful.Console;
using StringUnit = Geneticism.Units.StringUnit;

namespace Geneticism.Managers
{
    public class StringUnitPopulationManager
    {
        public int TargetLength { get; set; }
        public int PopulationSize { get; set; }
        public string TargetString { get; set; }
        public StringBuilder TargetStringAsBuilder { get; set; }
        public int Generations { get; set; }
        public IDictionary<string, object> SeedParameters { get; set; }
        public IList<StringUnit> CurrentPopulation { get; set; }

        public StringUnitPopulationManager(IDictionary<string, object> seedParameters, string targetString)
        {
            Globals.DefaultFitness = targetString.Length;
            this.SeedParameters = seedParameters;
            this.CurrentPopulation = new List<StringUnit>();
            this.EvaluateSeedParameters(seedParameters);
            this.TargetString = targetString.ToUpper();
            this.TargetLength = targetString.Trim().Length;
            this.TargetStringAsBuilder = new StringBuilder(TargetString);
        }

        public void AddUnitToPopulation(StringUnit unit)
        {
            this.CurrentPopulation.Add(unit);
        }

        public StringUnit GenerateRandomPopulationUnit(bool isRoot, StringUnit parentA, StringUnit parentB)
        {
            var newUnit = new StringUnit(RandomStringGenome(TargetLength), isRoot);
            if (parentA == null)
            {
                return newUnit;
            }
            newUnit.ParentA = parentA;
            newUnit.ParentB = parentB;
            return newUnit;
        }



        public void SeedPopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                var newUnit = new StringUnit(RandomStringGenome(TargetLength), true);
                newUnit.SetFitness(TargetLength);
                AddUnitToPopulation(newUnit);
            }
        }


        public void InsertRandomUnits(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newUnit = new StringUnit(RandomStringGenome(TargetLength), true);
                newUnit.SetFitness(TargetLength);
                AddUnitToPopulation(newUnit);
            }
        }



      

        public void EvaluateSeedParameters(IDictionary<string, object> seedParams)
        {
            foreach (KeyValuePair<string, object> kvp in seedParams)
            {
                switch (kvp.Key.ToLower())
                {
                    case "populationsize":
                        this.PopulationSize = (int) kvp.Value;
                        break;
                    case "generations":
                        this.Generations = (int) kvp.Value;
                        break;
                }
            }
        }

        public StringBuilder RandomStringGenome(int length)
        {
            var b = Globals.GetStringBuilder();

            for (int i = 0; i < length; i++)
                b.Append(Globals.RandomChar());

            return b;
        }

        //public string RandomStringGenome(int length)
        //{
        //    var b = Globals.GetStringBuilder();

        //    for (int i = 0; i < length; i++)
        //        b.Append(Globals.RandomChar());

        //    return b.ToString();
        //}



        public void CalculateHammingDistance()
        {
            foreach (var unit in CurrentPopulation)
            {
                unit.CalculateFitness(TargetString);
            }
        }
    

        public void PrintPopulationStats()
        {
            //get highest fitness
            var bestFitness = TargetLength;
            foreach (var unit in this.CurrentPopulation)
            {
                if ((unit).Fitness < bestFitness)
                    bestFitness = (unit).Fitness;
            }

            Console.WriteLine($"Best Fitness: {bestFitness}. TargetLength is: {TargetLength}.");
        }




        public int Go()
        {
            // Console.WriteLine("Beginning genetic experiments...");
            for (int i = 0; i < Generations; i++)
            {
                if (this.CurrentPopulation.Any(x =>
                    x.Genome.Equals(TargetStringAsBuilder)))
                {
                    //Console.WriteLine($"Target string reached in generation {i}.", Color.LawnGreen);
                    var winningChild = this.CurrentPopulation.First(x =>
                        x.Genome.Equals(TargetStringAsBuilder));

                    //Console.WriteLine($"Winning Child Genome: {((StringUnit)winningChild).Genome}. ParentA: {((StringUnit)winningChild).ParentA.Genome} | ParentB: {((StringUnit)winningChild).ParentB.Genome}");
                    return i;
                }
                //Console.WriteLine($"---GENERATION {i + 1}---");
                CalculateHammingDistance();
                var bestChildren = SelectBest();
                ReplacePopulation(bestChildren);
                //Console.WriteLine(new string('\n', 3));
            }
            Console.WriteLine("No winning child found after max generations :(", Color.Red);
            return Generations;
        }


        private void ReplacePopulation(IEnumerable<StringUnit> initialPop)
        {
            var initPopCount = initialPop.Count();
            InsertRandomUnitsToPopulation(initialPop, initPopCount);
        }


        public void InsertRandomUnitsToPopulation(IEnumerable<StringUnit> initialPop, int startAt)
        {
            for (int i = 0; i < startAt; i++)
            {
                CurrentPopulation[i] = initialPop.ElementAt(i);
            }
            for (int i = startAt; i < PopulationSize; i++)
            {
                //var newUnit = new StringUnit(RandomStringGenome(TargetLength), true);
                //newUnit.SetFitness(TargetLength);
                CurrentPopulation[i].ReplaceUnit(RandomStringGenome(TargetLength), true);
                CurrentPopulation[i].SetFitness(TargetLength);
            }
        }


        public List<StringUnit> SelectBest()
        {
            //var sorted = this.CurrentPopulation.OrderBy(x => ((StringUnit)x).Fitness);
            ((List<StringUnit>) this.CurrentPopulation).Sort((a, b) => (a.Fitness.CompareTo(b.Fitness)));
            //Console.WriteLine("---ALL FITNESS---");
            //foreach (StringPopulationStruct unit in sorted)
            //{
            //    Console.WriteLine(unit.Fitness);
            //}

            //for now we just select top 20
            var top20 = CurrentPopulation.Take(20);

            //Console.WriteLine("---BEST FITNESS---");
            //Console.WriteLine(((StringPopulationStruct)top20.OrderBy(x => ((StringPopulationStruct)x).Fitness).First()).Fitness);

            //Console.WriteLine("---TOP 20---");
            //foreach (StringUnit unit in top20)
            //{
            //    Console.WriteLine(unit.Fitness);
            //}

            var children = new List<StringUnit>();
            for (int i = 0; i < top20.Count(); i += 2)
            {
                var parents = top20.Skip(i).Take(2);
                var newFamily = new StringFamilialUnit(parents, TargetString);
                children.AddRange(newFamily.Breed());
            }

            //Console.WriteLine("---CHILD FITNESS---");
            //foreach (StringPopulationStruct child in children.OrderBy(x => x.Fitness))
            //{
            //    Console.WriteLine(child.Fitness);
            //}

            //Console.WriteLine("---BEST CHILD---");
            return children;
        }
    }
}
