using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geneticism.Core.Interface;

namespace Geneticism.Core
{
    public abstract class PopulationUnit<T>:IPopulationUnit<T>, IMutatable<T>
    {
        public IPopulationUnit<T> ParentA { get; set; }
        public IPopulationUnit<T> ParentB { get; set; }
        public bool IsRoot { get; }
        public abstract void IncreaseFitness();
        public abstract void DecreaseFitness();
        public abstract void ResetFitness();
        public abstract void SetFitness(object parameter, bool isDefault = false);
        public T Genome { get; set; }
        public Random rand { get; }
        public abstract bool Mutate();
    }
}
