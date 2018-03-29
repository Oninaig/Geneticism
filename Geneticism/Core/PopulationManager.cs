using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geneticism.Core.Interface;

namespace Geneticism.Core
{
    public abstract class PopulationManager<T> : IPopulationManager<T>
    {
        public IEnumerable<PopulationUnit<T>> CurrentPopulation { get; protected set; }
        //public abstract IEnumerable<PopulationUnit<T>> Breed(IPopulationUnit<T> parentA, IPopulationUnit<T> parentB);
        public abstract void SeedPopulation();
        public abstract void EvaluateSeedParameters(IDictionary<string, object> seedParams);
    }
}
