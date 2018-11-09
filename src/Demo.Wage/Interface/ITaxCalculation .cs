using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage
{
    public interface ITaxCalculation
    {
        double Cal(double grossWage);
    }
}
