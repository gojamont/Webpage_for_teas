using System.Text.Json;

namespace HeatingOptimizer;

class Init{

// static List<ProductionUnit> ProductionUnits = new List<ProductionUnit>{new ProductionUnit(ProductionUnitType.GasBoiler, "GB1", 4.0d, 0.0d, 520.0m, 175, 0.9d),
//     new ProductionUnit(ProductionUnitType.GasBoiler, "GB2", 3.0d, 0.0d, 560.0m, 130, 0.7d),
//     new ProductionUnit(ProductionUnitType.GasBoiler, "OB1", 4.0d, 0.0d, 670.0m, 330, 1.5d),
//     new ProductionUnit(ProductionUnitType.GasBoiler, "GM1", 3.5d, 2.6d, 960.0m, 650, 1.8d),
//     new ProductionUnit(ProductionUnitType.GasBoiler, "HP1", 6.0d, -6.0d, 60.0m, 0, 0)};

//     static ProductionUnit GasBoiler1 = new ProductionUnit(ProductionUnitType.GasBoiler, "GB1", 4.0d, 0.0d, 520.0m, 175, 0.9d);
//     static ProductionUnit GasBoiler2 = new ProductionUnit(ProductionUnitType.GasBoiler, "GB2", 3.0d, 0.0d, 560.0m, 130, 0.7d);
//     static ProductionUnit OilBoiler1 = new ProductionUnit(ProductionUnitType.OilBoiler, "OB1", 4.0d, 0.0d, 670.0m, 330, 1.5d);
//     static ProductionUnit GasMotor1 = new ProductionUnit(ProductionUnitType.GasMotor, "GM1", 3.5d, 2.6d, 960.0m, 650, 1.8d);
//     static ProductionUnit HeatPump1 = new ProductionUnit(ProductionUnitType.HeatPump, "HP1", 6.0d, -6.0d, 60.0m, 0, 0);
    static List<ProductionUnit> ProductionUnits = new();
    public static void Initialize()
    {
        ProductionUnits = HeatingData.GetProductionUnits(@"machines.csv");
        ProductionUnits.Sort((ProductionUnit a, ProductionUnit b) => (decimal)(a.MaxHeatOutput * 1000) / a.ProductionCosts > (decimal)(b.MaxHeatOutput * 1000) / b.ProductionCosts ? -1 : 1);
        foreach (var item in ProductionUnits)
        {
            Console.WriteLine($"{item.Name} {item.MaxHeatOutput} {item.ProductionCosts}");
        }
        HeatingData.GetData();
        // CostCalculator.CalculateCosts(ProductionUnits, HeatingData.WinterTimeFrame);
        // CostCalculator.CalculateCosts(ProductionUnits, HeatingData.SummerTimeFrame);

        //    foreach(var item in HeatingData.WinterTimeFrame)
        //    {
        //        Console.WriteLine($"Measure start: {item.TimeFrom} Measure end: {item.TimeTo} Heat demand: {item.HeatDemand} Electricity price: {item.ElectricityPrice}");
        //    }
        //    foreach(var item in HeatingData.SummerTimeFrame)
        //    {
        //        Console.WriteLine($"Summer Time from: {item.TimeFrom.TimeOfDay} Time to: {item.TimeTo.TimeOfDay} Heat demand: {item.HeatDemand} Electricity price: {item.ElectricityPrice}");
        //    }
        Console.WriteLine($"{ProductionUnitType.GasBoiler} {ProductionUnitType.OilBoiler} {ProductionUnitType.GasMotor} {ProductionUnitType.HeatPump}");
    }
}