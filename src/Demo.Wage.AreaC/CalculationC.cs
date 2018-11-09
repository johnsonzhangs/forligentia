using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage.AreaC
{
    public class CalculationC : ITaxCalculation
    {
        public double Cal(double grossWage)
        {
            throw new CalculationException("Calculation process error!");
        }
    }
}
