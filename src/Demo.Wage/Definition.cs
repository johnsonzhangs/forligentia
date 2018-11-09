using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Wage
{
    public class Definition
    {
        public string AssemblyName { get; set; }

        public string TypeName { get; set; }

        public string Key { get; set; }

        public Definition() { }

        public Definition(string assemblyName, string typeName)
        {
            AssemblyName = assemblyName;
            TypeName = typeName;
        }

    }
}
