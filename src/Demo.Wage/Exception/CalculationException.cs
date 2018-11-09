using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage
{
    public class CalculationException : Exception
    {
        public CalculationException() : base() { }
        public CalculationException(string message) : base(message) { }
    }
}
