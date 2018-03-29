using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geneticism.Core.Interface;

namespace Geneticism.Core
{
    public abstract class FamilialUnit<T>
    {
        public IPopulationUnit<T> ParentA { get; set; }
        public IPopulationUnit<T> ParentB { get; set; }
        public IEnumerable<IPopulationUnit<T>> Offspring { get; set; }
        public abstract IEnumerable<IPopulationUnit<T>> Breed();
    }
}
