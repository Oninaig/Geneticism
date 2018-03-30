using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Geneticism.Core
{
    [StructLayout(LayoutKind.Auto)]
    public struct StringPopulationStruct
    {
        public string Genome;
        public string ID;
        public string ParentAID;
        public string ParentBID;
        public bool IsRoot;
        public int Fitness;
        public int DefaultFitness;

        public StringPopulationStruct(string genome, bool isRoot = false) : this()
        {
            Genome = genome;
            IsRoot = isRoot;
            this.ID = Guid.NewGuid().ToString();
        }

        public StringPopulationStruct(string genome, bool isRoot, int defaultFitness) : this(genome, isRoot)
        {
            DefaultFitness = defaultFitness;
        }

        public StringPopulationStruct(string genome, string parentAID, string parentBID) : this(genome)
        {
            ParentAID = parentAID;
            ParentBID = parentBID;
        }

        public void SetParents(string parentAId, string parentBId)
        {
            this.ParentAID = parentAId;
            this.ParentBID = parentBId;
        }

        public void SetFitness(int fitness, bool isDefault = false)
        {
            this.Fitness = fitness;
            if (isDefault)
                this.DefaultFitness = fitness;
        }

        public void ResetFitness()
        {
            this.Fitness = this.DefaultFitness;
        }

        public void IncreaseFitness()
        {
            this.Fitness--;
        }

        public void DecreaseFitness()
        {
            this.Fitness++;
        }

        public void CalculateFitness(string target)
        {
            ResetFitness();
            for (int i = 0; i < Genome.Length; i++)
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
