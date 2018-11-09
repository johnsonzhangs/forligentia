using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage
{
    public interface IArea
    {
        string Name { get; set; }
        AreaDefinition Definition { get; }
        ITaxCalculation Calculation { get; }

    }
}
