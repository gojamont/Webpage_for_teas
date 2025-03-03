namespace HeatingOptimizer;

public class Season
{
    public DateTime TimeFrom { get; set; } // e.x. 3/1/2024 0:00 
    public DateTime TimeTo { get; set; } // e.x. 3/1/2024 1:00
    public double HeatDemand { get; set; } // e.x. 7 (MWh)
    public decimal ElectricityPrice { get; set; } // e.x. 700.3 (DKK / Mwh(el))
}