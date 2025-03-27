using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Platform.Storage;
using System;
using HeatingOptimizer.ViewModels;
using Avalonia.Interactivity;
using HeatingOptimizer.SourceDataManager;
using LiveChartsCore.SkiaSharpView;
using System.Linq;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;
using HeatingOptimizer.Optimizer;
using LiveChartsCore;


namespace HeatingOptimizer.UI;

public partial class MainWindow : Window
{
    private MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
    public MainWindow()
    {
        InitializeComponent();
        DataContext = mainWindowViewModel; // Set the DataContext here
    }
    public async void BrowseFile( object sender, RoutedEventArgs e )
    {
        IReadOnlyList<IStorageFile> result = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open a csv file with the timeFrames",
            // File type picker
            FileTypeFilter = new List<FilePickerFileType>
            {
                new FilePickerFileType("CSV Files(*.csv)")
                {
                    Patterns = ["*.csv"]
                },
            },
            SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory), // Open the file dialog in the current directory
            AllowMultiple = false // Allow only one file to be selected
        });

        if (result != null && result.Count > 0) 
        {
            // Get the path of the selected file
            var LocalPath = result[0].Path.AbsolutePath;
            // Set the title of the window to the name of the file, removes extensions from name
            // Load the file and replacing "%20" with spaces, determines whether the file is in binary depending on the extension
            mainWindowViewModel.InputText=LocalPath.Replace("%20", " ");
            DataParser.ParseHeatingData(mainWindowViewModel.InputText, out mainWindowViewModel.Frames);

        }
    }

    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
    }

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {   mainWindowViewModel.Series.Clear();
        CostCalculator.CalculateSeason(mainWindowViewModel.SelectedProductionUnits, mainWindowViewModel.Frames,
        mainWindowViewModel.SelectedIndex, ref mainWindowViewModel.ResultDictionary);
        mainWindowViewModel.XAxes[0].Labels = [.. mainWindowViewModel.Frames.Select(TF => TF.TimeFrom.ToString("dd/MM H:mm"))];
        mainWindowViewModel.XAxes[0].LabelsRotation = 90;
        mainWindowViewModel.XAxes[0].LabelsDensity=0;
        mainWindowViewModel.XAxes[0].TextSize=10;
        mainWindowViewModel.XAxes[0].MinStep=1;
        // ObservableCollection<ISeries> TempSeries=new ();
        // TempSeries = mainWindowViewModel.ResultDictionary.Select(PU =>
        // new StackedAreaSeries<double>{Name=PU.Key, Values=PU.Value.HeatProduced});

        // mainWindowViewModel.ResultDictionary.Select(PU =>
        // mainWindowViewModel.Series.Append(new StackedAreaSeries<double>{Name=PU.Key, Values=PU.Value.HeatProduced}));

        mainWindowViewModel.Series.Add(
           new LineSeries<double>{Name="Winter Heat Demand" ,Values=new ObservableCollection<double>(mainWindowViewModel.Frames.Select(s=> s.HeatDemand)), Fill=null, }
        );
        foreach (var PU in mainWindowViewModel.ResultDictionary)
        {
            mainWindowViewModel.Series.Add(new StackedAreaSeries<double>{Name=PU.Key, Values=PU.Value.HeatProduced});
        }

        mainWindowViewModel.XAxes[0].MinLimit = 0;
        mainWindowViewModel.YAxes[0].MinLimit = 0;
    }
}