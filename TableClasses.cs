namespace HeatingOptimizer;

class Timeframe
{
    protected internal DateTime TimeFrom { get; }
    protected internal DateTime TimeTo { get; }
    protected internal double HeatDemand { get; }
    protected internal decimal ElectricityPrice { get; }

    protected internal Timeframe(DateTime tFrom, DateTime tTo, double hDemand, decimal ePrice)
    {
        TimeFrom = tFrom;
        TimeTo = tTo;
        HeatDemand = hDemand;
        ElectricityPrice = ePrice;
    }    
}