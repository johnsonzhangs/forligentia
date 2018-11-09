using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage
{
    public class AreaNotExistException : Exception
    {
        public AreaNotExistException() : base() { }
        public AreaNotExistException(string message) : base(message) { }
    }
}
