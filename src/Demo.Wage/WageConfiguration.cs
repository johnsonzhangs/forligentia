using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Wage
{
    public class WageConfiguration
    {
        //Load areas and calculation class definition from areas.json file.For reflaction 
        public static void Configure()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("areas.json");
            var configuration = builder.Build();

            var areaSection = configuration.GetSection("areas");
            List<AreaDefinition> list = new List<AreaDefinition>();
            areaSection.Bind(list);

            foreach (AreaDefinition def in list)
            {
                AreaFactory.Definitions[def.Key] = def;
            }


            var calSection = configuration.GetSection("calculations");
            List<Definition> calList = new List<Definition>();
            calSection.Bind(calList);

            foreach (Definition def in calList)
            {
                TaxCalculationFactory.Definitions[def.Key] = def;
            }
        }
    }
}
