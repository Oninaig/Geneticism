using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geneticism.Core.Interface
{
    public interface IPopulationManager<T>
    {
        
        //IEnumerable<PopulationUnit<T>> Breed(IPopulationUnit<T> parentA, IPopulationUnit<T> parentB);
        IEnumerable<PopulationUnit<T>> CurrentPopulation { get; }
        void SeedPopulation();
        void EvaluateSeedParameters(IDictionary<string, object> seedParams);
    }
}
