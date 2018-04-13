using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geneticism.Core;
using Console = Colorful.Console;
namespace Geneticism.Units
{
    class StringFamilialUnit
    {
        public StringUnit ParentA { get; set; }

        public StringUnit ParentB { get; set; }

        public string TargetString { get; set; }

        public StringFamilialUnit(IEnumerable<StringUnit> parents, string targetString)
        {
            this.ParentA = parents.ElementAt(0);
            this.ParentB = parents.ElementAt(1);
            this.TargetString = targetString;
        }

        public StringFamilialUnit(StringUnit parentA, StringUnit parentB, string targetString)
        {
            this.ParentA = parentA;
            this.ParentB = parentB;
            this.TargetString = targetString;
        }


        public IList<StringUnit> Breed()
        {
            //Get random index and split there.
            var len = ParentA.Genome.Length;
            var splitIndex = ThreadRandom.Next(len);
            
            var parentAFirst = ParentA.Genome.ToString(0, splitIndex);
            var parentASecond = ParentA.Genome.ToString(splitIndex, len - (splitIndex));
            var parentBFirst = ParentB.Genome.ToString(0, splitIndex);
            var parentBSecond = ParentB.Genome.ToString(splitIndex, len - (splitIndex));

            //AF -> BS and BF -> AS
            var firstChild = new StringUnit(new StringBuilder(string.Concat(parentAFirst, parentBSecond)), ParentA, ParentB );
            var secondChild = new StringUnit(new StringBuilder(string.Concat(parentBFirst, parentASecond)), ParentA, ParentB);
            var children = new List<StringUnit>() { firstChild, secondChild };

            //Attempt mutation
            //todo: make Mutate method part of IPopulationUnit to avoid type casting here?
            foreach (StringUnit child in children)
            {
                child.Mutate();
                child.CalculateFitness(TargetString);
            }

            return children;
        }


    }
}
