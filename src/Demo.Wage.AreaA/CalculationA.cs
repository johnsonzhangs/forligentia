using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage.AreaA
{
    public class CalculationA : ITaxCalculation
    {
        public double Cal(double grossWage)
        {
            return grossWage * (1 - 0.15);
        }
    }
}
