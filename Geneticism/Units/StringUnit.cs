using System;
using System.Collections.Generic;
using System.Text;
using Geneticism.Core;
using Geneticism.Core.Interface;
using Console = Colorful.Console;
namespace Geneticism.Units
{
    public class StringUnit : PopulationUnit<string>, IMutatable<string>
    {
        

        public string Genome { get; set; }
        public StringUnit ParentA { get; set; }
        public StringUnit ParentB { get; set; }

        private bool _isRoot;


        //todo: make these part of the interface
        //Fitness in our case is the hamming distance, where a higher fitness is bad and a lower fitness is good.
        public int Fitness { get; set; }
        public int DefaultFitness { get; set; }


        public bool IsRoot
        {
            get { return _isRoot; }
            set { this._isRoot = value; }
        }

        public StringUnit(string genome, StringUnit parentA, StringUnit parentB)
        {
            Genome = genome;
            ParentA = parentA;
            ParentB = parentB;
            _isRoot = false;
            DefaultFitness = Globals.DefaultFitness;
        }

        public StringUnit(string genome, bool isRoot)
        {
            Genome = genome;
            _isRoot = isRoot;
            DefaultFitness = Globals.DefaultFitness;
        }


        public override void IncreaseFitness()
        {
            //we increase fitness by "lowering" our fitness score because a lower hamming distance is good
            this.Fitness--;
        }

        public override void DecreaseFitness()
        {
            this.Fitness++;
        }

        public override void ResetFitness()
        {
            this.Fitness = DefaultFitness;
        }

        public override void SetFitness(object parameter, bool isDefault= false)
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

        public void ReplaceProperties(string genome, StringUnit parentA, StringUnit parentB, bool isRoot)
        {
            this.Genome = genome;
            this.ParentA = parentA;
            this.ParentB = parentB;
            this._isRoot = isRoot;
            ResetFitness();
        }

        public override bool Mutate()
        {
            //Slow mutations over time
            var probability = (double)Globals.BaseMutationChance * (double)1001;
            if (ThreadRandom.Next(1, 1001) <= probability)
            {
                //Console.WriteLine("Mutating");

                var b = new StringBuilder(Genome);
                b[ThreadRandom.Next(b.Length)] = Globals.RandomChar();
                Genome = b.ToString();
                
                return true;
            }
            return false;
        }
    }
}
