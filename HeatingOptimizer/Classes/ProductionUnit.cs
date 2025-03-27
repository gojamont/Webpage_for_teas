using System.Collections.Generic;

namespace HeatingOptimizer;

public enum ProductionUnitType
{
    GasBoiler,
    OilBoiler,
    GasMotor,
    HeatPump
}

public class ProductionUnit
{
    public string Name { get; set; } = "";
    public double MaxHeatOutput { get; set; }
    public double MaxElectricity { get; set; }
    public decimal ProductionCosts { get; set; }
    public int CO2Emissions { get; set; }
    public double Consumption { get; set; }

    public override string ToString() // Override for the ListBox
    {
        return $"{Name}";
    }
}