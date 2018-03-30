using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geneticism.Core;
using Geneticism.Core.Interface;
using Geneticism.Units;
using Console = Colorful.Console;
using StringUnit = Geneticism.Units.StringUnit;

namespace Geneticism.Managers
{
    public class StringUnitPopulationManager:PopulationManager<string>
    {
        public int TargetLength { get; set; }
        public int PopulationSize { get; set; }
        public string TargetString { get; set; }
        public int Generations { get; set; }
        public IDictionary<string, object> SeedParameters { get; set; }

        public StringUnitPopulationManager(IDictionary<string, object> seedParameters, string targetString)
        {
            Globals.DefaultFitness = targetString.Length;
            this.SeedParameters = seedParameters;
            //this.CurrentPopulation = new List<PopulationUnit<string>>();
            this.CurrentPopulation = new List<StringPopulationStruct>();
            this.EvaluateSeedParameters(seedParameters);
            this.TargetString = targetString.ToUpper();
            this.TargetLength = targetString.Trim().Length;
        }

        public void AddUnitToPopulation(PopulationUnit<string> unit)
        {
            ((List<PopulationUnit<string>>)this.CurrentPopulation).Add(unit);
        }
        public void AddUnitToPopulation(StringPopulationStruct unit)
        {
            (this.CurrentPopulation).Add(unit);
        }


        //public override void SeedPopulation()
        //{
        //    for (int i = 0; i < PopulationSize; i++)
        //    {
        //        var newUnit = new StringUnit(RandomStringGenome(TargetLength), true);
        //        newUnit.SetFitness(TargetLength);
        //        AddUnitToPopulation(newUnit);
        //    }
        //}

        public StringPopulationStruct GenerateRandomPopulationUnit(bool isRoot, string parentAId = null, string parentBId = null)
        {
            var newStruct = new StringPopulationStruct(RandomStringGenome(TargetLength), isRoot);
            if (string.IsNullOrEmpty(parentAId))
            {
                return newStruct;
            }
            newStruct.ParentAID = parentAId;
            newStruct.ParentBID = parentBId;
            return newStruct;
        }

        //public override void SeedPopulation()
        //{
        //    for (int i = 0; i < PopulationSize; i++)
        //    {
        //        var newUnit = new StringUnit(RandomStringGenome(TargetLength), true);
        //        newUnit.SetFitness(TargetLength);
        //        AddUnitToPopulation(newUnit);
        //    }
        //}

        public override void SeedPopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                var newUnit = new StringPopulationStruct(RandomStringGenome(TargetLength), true);
                newUnit.SetFitness(TargetLength);
                AddUnitToPopulation(newUnit);
            }
        }
        //public void InsertRandomUnits(int count)
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        var newUnit = new StringUnit(RandomStringGenome(TargetLength), true);
        //        newUnit.SetFitness(TargetLength);
        //        AddUnitToPopulation(newUnit);
        //    }
        //}

        public void InsertRandomUnits(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newUnit = new StringPopulationStruct(RandomStringGenome(TargetLength), true);
                newUnit.SetFitness(TargetLength);
                AddUnitToPopulation(newUnit);
            }
        }

        ////Specifies a specific population to add random ppl to
        //public void InsertRandomUnitsToPopulation(int count, List<StringUnit> population)
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        var newUnit = new StringUnit(RandomStringGenome(TargetLength), true);
        //        newUnit.SetFitness(TargetLength);
        //        population.Add(newUnit);
        //    }
        //}

        public void InsertRandomUnitsToPopulation(int count, List<StringPopulationStruct> population)
        {
            for (int i = 0; i < count; i++)
            {
                var newUnit = new StringPopulationStruct(RandomStringGenome(TargetLength), true);
                newUnit.SetFitness(TargetLength);
                population.Add(newUnit);
            }
        }

        public override void EvaluateSeedParameters(IDictionary<string, object> seedParams)
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


        public string RandomStringGenome(int length)
        {
            var b = new StringBuilder();

            for (int i = 0; i < length; i++)
                b.Append(Globals.RandomChar());

            return b.ToString();
        }

        //public void CalculateHammingDistance()
        //{
        //    foreach (StringUnit unit in this.CurrentPopulation)
        //    {
        //        unit.ResetFitness();
        //        for (int i = 0; i < unit.Genome.Length; i++)
        //        {
        //            if(unit.Genome[i] == TargetString[i])
        //                unit.IncreaseFitness();
        //        }
        //    }
        //}

        public void CalculateHammingDistance()
        {
            foreach (StringPopulationStruct unit in this.CurrentPopulation)
            {
                unit.ResetFitness();
                for (int i = 0; i < unit.Genome.Length; i++)
                {
                    if (unit.Genome[i] == TargetString[i])
                        unit.IncreaseFitness();
                }
            }
        }
        //public void PrintPopulationStats()
        //{
        //    //get highest fitness
        //    var bestFitness = TargetLength;
        //    foreach (var unit in this.CurrentPopulation)
        //    {
        //        if (((StringUnit) unit).Fitness < bestFitness)
        //            bestFitness = ((StringUnit) unit).Fitness;
        //    }

        //    Console.WriteLine($"Best Fitness: {bestFitness}. TargetLength is: {TargetLength}.");
        //}

        public void PrintPopulationStats()
        {
            //get highest fitness
            var bestFitness = TargetLength;
            foreach (var unit in this.CurrentPopulation)
            {
                if (((StringPopulationStruct)unit).Fitness < bestFitness)
                    bestFitness = ((StringPopulationStruct)unit).Fitness;
            }

            Console.WriteLine($"Best Fitness: {bestFitness}. TargetLength is: {TargetLength}.");
        }

        //public int Go()
        //{
        //   // Console.WriteLine("Beginning genetic experiments...");
        //    for (int i = 0; i < Generations; i++)
        //    {
        //        if (this.CurrentPopulation.Any(x =>
        //            ((StringUnit)x).Genome.Equals(TargetString, StringComparison.CurrentCultureIgnoreCase)))
        //        {
        //            //Console.WriteLine($"Target string reached in generation {i}.");
        //            var winningChild = this.CurrentPopulation.First(x =>
        //                ((StringUnit)x).Genome.Equals(TargetString, StringComparison.InvariantCultureIgnoreCase));

        //            //Console.WriteLine($"Winning Child Genome: {((StringUnit)winningChild).Genome}. ParentA: {((StringUnit)winningChild).ParentA.Genome} | ParentB: {((StringUnit)winningChild).ParentB.Genome}");
        //            return i;
        //        }
        //        //Console.WriteLine($"---GENERATION {i + 1}---");
        //        CalculateHammingDistance();
        //        var bestChildren = SelectBest();
        //        ReplacePopulation(bestChildren);
        //        //Console.WriteLine(new string('\n', 3));
        //    }
        //    Console.WriteLine("No winning child found after max generations :(", Color.Red);
        //    return Generations;
        //}


        public int Go()
        {
            // Console.WriteLine("Beginning genetic experiments...");
            for (int i = 0; i < Generations; i++)
            {
                if (this.CurrentPopulation.Any(x =>
                    ((StringPopulationStruct)x).Genome.Equals(TargetString, StringComparison.CurrentCultureIgnoreCase)))
                {
                    //Console.WriteLine($"Target string reached in generation {i}.");
                    var winningChild = this.CurrentPopulation.First(x =>
                        ((StringPopulationStruct)x).Genome.Equals(TargetString, StringComparison.InvariantCultureIgnoreCase));

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
        //private void ReplacePopulation(IEnumerable<StringUnit> initialPop)
        //{
        //    var currentPop = new List<StringUnit>();
        //    currentPop.AddRange(this.CurrentPopulation.Cast<StringUnit>());
        //    currentPop.Clear();
        //    var replacements = PopulationSize - initialPop.Count();
        //    currentPop.AddRange(initialPop);
        //    InsertRandomUnitsToPopulation(replacements, currentPop);
        //    this.CurrentPopulation = currentPop;
        //}

        private void ReplacePopulation(IEnumerable<StringPopulationStruct> initialPop)
        {
            var currentPop = new List<StringPopulationStruct>();
            currentPop.AddRange(this.CurrentPopulation.Cast<StringPopulationStruct>());
            currentPop.Clear();
            var replacements = PopulationSize - initialPop.Count();
            currentPop.AddRange(initialPop);
            InsertRandomUnitsToPopulation(replacements, currentPop);
            this.CurrentPopulation = currentPop;
        }

        //public List<StringUnit> SelectBest()
        //{
        //    var sorted = this.CurrentPopulation.OrderBy(x => ((StringUnit) x).Fitness);
        //    //Console.WriteLine("---ALL FITNESS---");
        //    //foreach (StringUnit unit in sorted)
        //    //{
        //    //    Console.WriteLine(unit.Fitness);
        //    //}

        //    //for now we just select top 20
        //    var top20 = sorted.Take(20);
            
        //    //Console.WriteLine("---BEST FITNESS---");
        //    //Console.WriteLine(((StringUnit)top20.OrderBy(x=>((StringUnit)x).Fitness).First()).Fitness);
            
        //    //Console.WriteLine("---TOP 20---");
        //    //foreach (StringUnit unit in top20)
        //    //{
        //    //    Console.WriteLine(unit.Fitness);
        //    //}

        //    var families = new List<StringFamilialUnit>();
        //    var children = new List<StringUnit>();
        //    for (int i = 0; i < top20.Count(); i += 2)
        //    {
        //        var parents = top20.Skip(i).Take(2);
        //        var newFamily = new StringFamilialUnit(parents, TargetString);
        //        children.AddRange((IEnumerable<StringUnit>) newFamily.Breed());
        //    }

        //    //Console.WriteLine("---CHILD FITNESS---");
        //    //foreach (StringUnit child in children.OrderBy(x=>x.Fitness))
        //    //{
        //    //    Console.WriteLine(child.Fitness);
        //    //}

        //    //Console.WriteLine("---BEST CHILD---");
        //    return children;
        //}
        public List<StringPopulationStruct> SelectBest()
        {
            var sorted = this.CurrentPopulation.OrderBy(x => ((StringPopulationStruct)x).Fitness);
            //Console.WriteLine("---ALL FITNESS---");
            //foreach (StringUnit unit in sorted)
            //{
            //    Console.WriteLine(unit.Fitness);
            //}

            //for now we just select top 20
            var top20 = sorted.Take(20);

            //Console.WriteLine("---BEST FITNESS---");
            //Console.WriteLine(((StringUnit)top20.OrderBy(x=>((StringUnit)x).Fitness).First()).Fitness);

            //Console.WriteLine("---TOP 20---");
            //foreach (StringUnit unit in top20)
            //{
            //    Console.WriteLine(unit.Fitness);
            //}

            var families = new List<StringFamilialUnit>();
            var children = new List<StringPopulationStruct>();
            for (int i = 0; i < top20.Count(); i += 2)
            {
                var parents = top20.Skip(i).Take(2);
                var newFamily = new StringStructFamilialUnit(parents, TargetString);
                children.AddRange((IEnumerable<StringPopulationStruct>)newFamily.Breed());
            }

            //Console.WriteLine("---CHILD FITNESS---");
            //foreach (StringUnit child in children.OrderBy(x=>x.Fitness))
            //{
            //    Console.WriteLine(child.Fitness);
            //}

            //Console.WriteLine("---BEST CHILD---");
            return children;
        }
    }
}
