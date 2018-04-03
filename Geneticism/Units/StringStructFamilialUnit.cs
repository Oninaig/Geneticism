using System;
using System.Collections.Generic;
using System.Linq;
using Geneticism.Core;
using Geneticism.Core.Interface;
using Console = Colorful.Console;
namespace Geneticism.Units
{
    class StringStructFamilialUnit
    {
        public StringPopulationStruct ParentA { get; set; }
        public StringPopulationStruct ParentB { get; set; }

        public string TargetString { get; set; }

        public StringStructFamilialUnit(IEnumerable<StringPopulationStruct> parents, string targetString)
        {
            this.ParentA = parents.ElementAt(0);
            this.ParentB = parents.ElementAt(1);
            this.TargetString = targetString;
        }

        public StringStructFamilialUnit(StringPopulationStruct parentA, StringPopulationStruct parentB, string targetString)
        {
            this.ParentA = parentA;
            this.ParentB = parentB;
            this.TargetString = targetString;
        }


        public IEnumerable<StringPopulationStruct> Breed()
        {
            //Get random index and split there.
            var splitIndex = ThreadRandom.Next(((StringPopulationStruct) ParentA).Genome.Length);
            var parentAFirst = ParentA.Genome.Substring(0, splitIndex);
            var parentASecond = ParentA.Genome.Substring(splitIndex);
            var parentBFirst = ParentB.Genome.Substring(0, splitIndex);
            var parentBSecond = ParentB.Genome.Substring(splitIndex);

            //AF -> BS and BF -> AS
            var firstChild = new StringPopulationStruct(string.Concat(parentAFirst, parentBSecond), ParentA.ID, ParentB.ID);
            var secondChild = new StringPopulationStruct(string.Concat(parentBFirst, parentASecond), ParentA.ID, ParentB.ID);
            var children = new List<StringPopulationStruct>() { firstChild, secondChild };

            //Attempt mutation
            //todo: make Mutate method part of IPopulationUnit to avoid type casting here?
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];

                child.Genome = child.Mutate();
                child.Fitness = child.CalculateFitness(TargetString);
                children[i] = child;
            }
            //foreach (StringPopulationStruct child in children)
            //{
            //    child.Mutate();
            //    child.CalculateFitness(TargetString);
            //}

            return children;
        }


    }
}
