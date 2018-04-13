using System;
using System.Collections.Generic;
using System.Text;
using Geneticism.Core;
using Console = Colorful.Console;
namespace Geneticism.Units
{
    public class StringUnit 
    {
        

        public StringBuilder Genome { get; set; }
        public StringUnit ParentA { get; set; }
        public StringUnit ParentB { get; set; }
        private bool _isRoot;
        //Fitness in our case is the hamming distance, where a higher fitness is bad and a lower fitness is good.
        public int Fitness { get; set; }
        public int DefaultFitness { get; set; }


        public bool IsRoot
        {
            get { return _isRoot; }
            set { this._isRoot = value; }
        }

        public StringUnit(StringBuilder genome, StringUnit parentA, StringUnit parentB)
        {
            ParentA = parentA;
            ParentB = parentB;
            _isRoot = false;
            DefaultFitness = Globals.DefaultFitness;
            SetupBuilder(genome);
        }

        public StringUnit(StringBuilder genome, bool isRoot)
        {
            _isRoot = isRoot;
            DefaultFitness = Globals.DefaultFitness;
            SetupBuilder(genome);
        }

        public void SetupBuilder(StringBuilder sourceBuilder)
        {
            if(this.Genome == null)
                this.Genome = new StringBuilder(DefaultFitness);
            this.Genome.Clear();
            for (int i = 0; i < sourceBuilder.Length; i++)
            {
                this.Genome.Append(sourceBuilder[i]);
            }
        }

        //Reusal
        public void ReplaceUnit(string newGenome, StringUnit parentA, StringUnit parentB, bool isRoot, int fitness,
            int defaultFitness)
        {
            this.Genome.Clear().Append(newGenome);
            this.ParentA = parentA;
            this.ParentB = parentB;
            this.IsRoot = isRoot;
            this.Fitness = fitness;
            this.DefaultFitness = defaultFitness;
        }

        public void ReplaceUnit(StringUnit otherUnit)
        {
            this.Genome.Clear().Append(otherUnit.Genome);
            this.ParentA = otherUnit.ParentA;
            this.ParentB = otherUnit.ParentB;
            this.IsRoot = otherUnit.IsRoot;
            this.Fitness = otherUnit.Fitness;
            this.DefaultFitness = otherUnit.DefaultFitness;
        }

        public void ReplaceUnit(StringBuilder sourceBuilder, bool isRoot)
        {
            this.ParentA = null;
            this.ParentB = null;
            SetupBuilder(sourceBuilder);
            this.IsRoot = isRoot;
        }
        

        public void IncreaseFitness()
        {
            //we increase fitness by "lowering" our fitness score because a lower hamming distance is good
            this.Fitness--;
        }

        public void DecreaseFitness()
        {
            this.Fitness++;
        }

        public void ResetFitness()
        {
            this.Fitness = DefaultFitness;
        }

        public void SetFitness(object parameter, bool isDefault= false)
        {
            this.Fitness = (int) parameter;
        }

        public void CalculateFitness(string target)
        {
            ResetFitness();
            for (int i = 0; i <Genome.Length; i++)
            {
                if (Genome[i] == target[i])
                    this.IncreaseFitness();
            }
        }

        public bool Mutate()
        {
            //Slow mutations over time
            var probability = (double)Globals.BaseMutationChance * (double)1001;
            if (ThreadRandom.Next(1, 1001) <= probability)
            {
                //todo:extract this builder into a threatstatic builder
                var b = Globals.GetStringBuilder(Genome.ToString());
                b[ThreadRandom.Next(b.Length)] = Globals.RandomChar();
                SetupBuilder(b);
                return true;
            }
            return false;
        }
    }
}
