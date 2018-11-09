using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage
{
    public class AreaFactory
    {
        public static AreaFactory Instance = new AreaFactory();
        public static Dictionary<string, Definition> Definitions = new Dictionary<string, Definition>();
        public static Dictionary<string, IArea> AreaList = new Dictionary<string, IArea>();

        public AreaFactory()
        {

        }

        public IArea Create(string areaKey)
        {
            if (AreaList.ContainsKey(areaKey)) return AreaList[areaKey];

            IArea area = null;

            if (Definitions.ContainsKey(areaKey))
            {
                Definition definition = Definitions[areaKey];

                try
                {
                    area = ClassFactory.Create<IArea>(definition, new object[] { definition });

                }
                catch { }
            }

            if (null == area)
            {
                throw new AreaNotExistException(string.Format("Area [{0}] doesn't exist.", areaKey));
            }
            else
            {
                if (!AreaList.ContainsKey(areaKey))
                    AreaList[areaKey] = area;
            }

            return area;
        }
    }
}
