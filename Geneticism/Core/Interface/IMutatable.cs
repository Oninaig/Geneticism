using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geneticism.Core.Interface
{
    public interface IMutatable<T>
    {
        T Genome { get; set; }
        bool Mutate();
        Random rand { get; }
    }
}
