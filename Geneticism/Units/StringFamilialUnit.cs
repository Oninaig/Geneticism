using System;
using System.Collections.Generic;
using System.Linq;
using Geneticism.Core;
using Geneticism.Core.Interface;
using Console = Colorful.Console;
namespace Geneticism.Units
{
    class StringFamilialUnit : FamilialUnit<string>
    {
        public new StringUnit ParentA
        {
            get { return (StringUnit) base.ParentA; }
            set { base.ParentA = value; }
        }

        public new StringUnit ParentB
        {
            get { return (StringUnit) base.ParentB; }
            set { base.ParentB = value; }
        }

        public string TargetString { get; set; }

        public StringFamilialUnit(IEnumerable<IPopulationUnit<string>> parents, string targetString)
        {
            this.ParentA = parents.ElementAt(0) as StringUnit;
            this.ParentB = parents.ElementAt(1) as StringUnit;
            this.TargetString = targetString;
        }

        public StringFamilialUnit(StringUnit parentA, StringUnit parentB, string targetString)
        {
            this.ParentA = parentA;
            this.ParentB = parentB;
            this.TargetString = targetString;
        }


        public override  IEnumerable<IPopulationUnit<string>> Breed()
        {
            //Get random index and split there.
            var splitIndex = ThreadRandom.Next(((StringUnit) ParentA).Genome.Length);
            var parentAFirst = ParentA.Genome.Substring(0, splitIndex);
            var parentASecond = ParentA.Genome.Substring(splitIndex);
            var parentBFirst = ParentB.Genome.Substring(0, splitIndex);
            var parentBSecond = ParentB.Genome.Substring(splitIndex);

            //AF -> BS and BF -> AS
            var firstChild = new StringUnit(string.Concat(parentAFirst, parentBSecond), ParentA, ParentB );
            var secondChild = new StringUnit(string.Concat(parentBFirst, parentASecond), ParentA, ParentB);
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
