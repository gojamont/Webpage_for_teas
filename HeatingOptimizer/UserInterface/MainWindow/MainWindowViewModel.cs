using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using CommunityToolkit.Mvvm.ComponentModel;
using HeatingOptimizer.SourceDataManager;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;

namespace HeatingOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        protected internal Dictionary<string, Results> ResultDictionary = new Dictionary<string, Results>();
        [ObservableProperty]
        private string _inputText= string.Empty;

        [ObservableProperty]
        private ObservableCollection<ISeries> _series = [];

        [ObservableProperty]
        private ObservableCollection<ICartesianAxis> _xAxes = [new Axis
        {
            Name = "Time Period",
            NamePaint = new SolidColorPaint(SKColor.Parse("#808080")),
            TextSize = 18,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#B0B0B0")),
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#ffffff"),
                StrokeThickness = 1,
            },
            ZeroPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#808080"),
                StrokeThickness = 2
            },
            TicksPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#B0B0B0"),
                StrokeThickness = 1.5f
            },
            MinLimit=0,
        }];
        protected internal List<TimeFrame> Frames;

        public List<ProductionUnit> AllProductionUnits { get; set; } = DataParser.ParseMachineData();
        public List<ProductionUnit> SelectedProductionUnits { get; set; } = [];

        // checking which of the options is selected by their index

        [ObservableProperty]
       private short _selectedIndex = 0; // Default to first index


        public void Generate()
        {
            // Add way to get timeframe
            // CostCalculator.CalculatePeriod(SelectedProductionUnits, , SelectedIndex);
        }

        public MainWindowViewModel()
        {
            // in the view model, here are defined the units that need to be displayed in a chart
            /*
            Series = new ISeries[]
            {
            new StackedAreaSeries<double?>{Name=ProductionUnit.CreateProductionUnit(ProductionUnitType.GasBoiler).Name, Values=[ 3, 2, 3, 3, 2, 4, 9 ], LineSmoothness = 0},
            new StackedAreaSeries<double?>{Name=ProductionUnit.CreateProductionUnit(ProductionUnitType.OilBoiler).Name, Values= [6, 5, 6, 0, 0, 5, 2 ], LineSmoothness = 0},
            new StackedAreaSeries<double?>{Name=ProductionUnit.CreateProductionUnit(ProductionUnitType.GasMotor).Name, Values=[ 4, 8, 2, 8, 9, 0, 0], LineSmoothness = 0},
            new StackedAreaSeries<double?>{Name=ProductionUnit.CreateProductionUnit(ProductionUnitType.HeatPump).Name, Values=[ 4, 8, 2, 8, 9, 0, 0], LineSmoothness = 0},
            };
            */
        }

        // for now just setting up the axes for efficiency and the time period

        // X Axis

        /*
        new Axis
        {
            Name = "Time Period",
            NamePaint = new SolidColorPaint(SKColor.Parse("#808080")),
            TextSize = 18,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#B0B0B0")),
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#ffffff"),
                StrokeThickness = 1,
            },
            ZeroPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#808080"),
                StrokeThickness = 2
            },
            TicksPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#B0B0B0"),
                StrokeThickness = 1.5f
            },
        }
        */

        // Y Axis
        public ICartesianAxis[] YAxes { get; set; } = new ICartesianAxis[]
        {
        new Axis
        {
            Name = "Efficiency",
            NamePaint = new SolidColorPaint(SKColor.Parse("#808080")),
            TextSize = 18,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#B0B0B0")),
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#ffffff"),
                StrokeThickness = 1,
            },
            ZeroPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#000"),
                StrokeThickness = 2
            },
            TicksPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#B0B0B0"),
                StrokeThickness = 1.5f
            }
        }
        };
    }
}
