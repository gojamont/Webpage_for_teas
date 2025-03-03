using System;
using System.Collections.Generic;

namespace HeatingOptimizer
{
    class CostCalculator
    {
        // private static int scenarioNumber = 0;
        public static void CalculateCosts(List<ProductionUnit> productionUnits, List<Timeframe> timeframes, string[] workingUnits)
        {
            StreamWriter textWriter = new("output.csv", true);
            // string[] currentScenario = scenarios[scenarioNumber];
            // List<ProductionUnit> unitsInScenario = productionUnits.FindAll(unit => workingUnits.Contains(unit.Name));
            
            List<ProductionUnit> unitsInScenario = productionUnits.FindAll(unit => workingUnits.Contains(unit.Name));
            foreach (var unit in unitsInScenario)
            {
                // Console.WriteLine($"{unit.Name} {unit.MaxHeatOutput} {unit.ProductionCosts}");
            }
            foreach (var timeframe in timeframes)
            {
                textWriter.WriteLine($"{timeframe.TimeFrom},{timeframe.TimeTo},{timeframe.HeatDemand},{CalculateCostsForTimeframe(unitsInScenario, timeframe)}");
            }
            textWriter.Close();
        }
        private static decimal CalculateCostsForTimeframe(List<ProductionUnit> machines, Timeframe timeframe)
        {
            decimal totalCost = 0;
            double heatDemand = timeframe.HeatDemand;
            machines.Sort((ProductionUnit a, ProductionUnit b) => 
            (decimal)(a.MaxHeatOutput * 1000) / a.ProductionCosts > (decimal)(b.MaxHeatOutput * 1000) / b.ProductionCosts ? -1 : 1);
            foreach (var machine in machines)
            {
                // Console.WriteLine($"{machine.Name} {machine.MaxHeatOutput} {machine.ProductionCosts} {heatDemand}");
                if (machine.MaxHeatOutput < heatDemand)
                {
                    totalCost += (decimal)machine.MaxHeatOutput * machine.ProductionCosts;
                    // machines.Remove(machine);
                    heatDemand -= machine.MaxHeatOutput;
                }
                else
                {
                    totalCost += (decimal)heatDemand * machine.ProductionCosts;
                    break;
                }
            }
            return totalCost;
        }
    }
}