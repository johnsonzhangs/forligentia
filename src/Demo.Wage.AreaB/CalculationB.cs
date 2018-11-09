using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage.AreaB
{
    public class CalculationB : ITaxCalculation
    {
        public double Cal(double grossWage)
        {
            return grossWage - 2;
        }
    }
}
