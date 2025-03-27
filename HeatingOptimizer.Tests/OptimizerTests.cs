using HeatingOptimizer;
using HeatingOptimizer.Optimizer;

namespace heat.tests;

public class ProdUnitSorterTests
{
    [Fact]
    public void Sort_Scenario1Cheapest_ReturnCorrectList()
    {
        // Arrange
        List<ProductionUnit> inputList = [
            // ProductionUnit(string name, double maxHeatOutput, double maxElectricity, decimal productionCosts, int co2Emissions, double consumption)
            new ProductionUnit("", 1, 0, 2, 0, 0),
            new ProductionUnit("", 1, 0, 3, 0, 0),
            new ProductionUnit("", 1, 0, 4, 0, 0),
            new ProductionUnit("", 2, 0, 4, 0, 0),
        ];
        TimeFrame timeframe = new(DateTime.Now, DateTime.Now, 0, 0);
        short sortType = 0; // Cheapest
        var expectedList = inputList.OrderBy(x => x.ProductionCosts).ToList();

        // Act
        var sortedList = ProdUnitSorter.Sort(inputList, timeframe, sortType);

        // Assert
        Assert.Equal(sortedList, expectedList);
    }
}