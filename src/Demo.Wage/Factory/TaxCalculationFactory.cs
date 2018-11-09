using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage
{
    public class TaxCalculationFactory
    {
        public static TaxCalculationFactory Instance = new TaxCalculationFactory();
        public static Dictionary<string, Definition> Definitions = new Dictionary<string, Definition>();

        public TaxCalculationFactory()
        {

        }

        public ITaxCalculation Create(string calKey)
        {
            ITaxCalculation calculation = null;

            Definition definition = Definitions.ContainsKey(calKey) ? Definitions[calKey] : null;

            try
            {
                calculation = ClassFactory.Create<ITaxCalculation>(definition, null);
            }
            catch { }

            return calculation;
        }
    }
}
