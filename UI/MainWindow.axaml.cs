using Avalonia.Controls;

namespace HeatingOptimizer;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public void BrowseFile()
    {
        var dialog = new OpenFileDialog();
        dialog.AllowMultiple = false;
        dialog.Directory = ".";
        dialog.Filters = new List<FileDialogFilter>
        {
            new FileDialogFilter { Name = "CSV Files", Extensions = new List<string> { "csv" } }
        };
        var result = dialog.ShowAsync(this);
        // return result.Result.FirstOrDefault();
    }
}