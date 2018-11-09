using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Demo.Wage
{
    public class ClassFactory
    {

        public static T Create<T>(Definition definition, object[] args) where T : class
        {
            Type type = GetType(definition.AssemblyName, definition.TypeName);

            if (type != null)
                return System.Activator.CreateInstance(type, args) as T;
            else
                return null;
        }

        public static Type GetType(string strAssemblyName, string strTypeName)
        {
            AssemblyName assemblyName = new AssemblyName(strAssemblyName);
            Assembly assembly = Assembly.Load(assemblyName);
            Type myType = assembly.GetType(strTypeName);
            return myType;
        }
    }
}
