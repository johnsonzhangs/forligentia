using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage
{
    public abstract class AreaBase : IArea
    {
        public string Name
        {
            get
            {
                return Definition == null ? string.Empty : Definition.Name;
            }
            set
            {
                if (Definition != null)
                {
                    Definition.Name = value;
                }
            }
        }
        public AreaDefinition Definition { get; protected set; }
        public ITaxCalculation Calculation { get; protected set; }

        public AreaBase(AreaDefinition definition)
        {
            Definition = definition;

            if (Definition != null)
            {
                Calculation = TaxCalculationFactory.Instance.Create(Definition.CalKey);
            }

            if (null == Calculation)
            {
                throw new Exception("Calculation class error.");
            }
        }
    }
}
