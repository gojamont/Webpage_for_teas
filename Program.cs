using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;

namespace HeatingOptimizer;

class Program
{
    
    // [STAThread]
    // public static void Main(string[] args) => BuildAvaloniaApp()
    //     .StartWithClassicDesktopLifetime(args);
    // // Avalonia configuration, don't remove; also used by visual designer.

    // public static AppBuilder BuildAvaloniaApp()
    //     => AppBuilder.Configure<App>()
    //         .UsePlatformDetect()
    //         .WithInterFont()
    //         .LogToTrace();

    public static void Main(string[] args) => Init.Initialize();
}

