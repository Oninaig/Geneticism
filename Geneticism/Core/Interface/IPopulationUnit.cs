using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geneticism.Core.Interface
{
    public interface IPopulationUnit<T>
    {
        IPopulationUnit<T> ParentA { get; set; }
        IPopulationUnit<T> ParentB { get; set; }
        bool IsRoot { get; }
        void IncreaseFitness();
        void DecreaseFitness();
        void ResetFitness();
        void SetFitness(object parameter, bool isDefault = false);
        
    }
}
