using System.Reflection.Metadata;

namespace HeatingOptimizer;
enum ProductionUnitType
{
    GasBoiler,
    OilBoiler,
    GasMotor,
    HeatPump
}
class ProductionUnit
{
    protected internal string Name { get; }
    protected internal double MaxHeatOutput { get; }
    protected internal double MaxElectricity { get; }
    protected internal decimal ProductionCosts { get; }
    protected internal int CO2Emissions { get; }
    protected internal double Consumption { get; }
    
    
    protected internal ProductionUnit(string name, double? maxHeatOutput, double? maxElectricity, decimal? productionCosts, int? co2Emissions, double? consumption)
    {
        Name = name;
        MaxHeatOutput = maxHeatOutput ?? 0.0d;
        MaxElectricity = maxElectricity ?? 0.0d;
        ProductionCosts = productionCosts ?? 0.0m;
        CO2Emissions = co2Emissions ?? 0;
        Consumption = consumption ?? 0;
    }
}