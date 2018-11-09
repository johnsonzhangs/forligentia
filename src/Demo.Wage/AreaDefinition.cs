using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Wage
{
    public class AreaDefinition : Definition
    {
        public string Name { get; set; }

        public string CalKey { get; set; }

        public AreaDefinition() { }

        public AreaDefinition(string assemblyName, string typeName)
            : base(assemblyName, typeName)
        {

        }
    }
}
