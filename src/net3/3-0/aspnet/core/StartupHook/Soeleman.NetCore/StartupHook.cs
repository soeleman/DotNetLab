using System;
using System.IO;
using System.Linq;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "RCS1110:Declare type inside namespace.", Justification = "<Pending>")]
internal static class StartupHook
{
    public static void Initialize()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        RunDirectory();
        ShowAssembly();
        Console.ResetColor();
    }

    private static void ShowAssembly()
    {
        AppDomain
            .CurrentDomain
            .GetAssemblies()
            .ToList()
            .ForEach(s => Console.WriteLine(s.GetName().Name));
    }

    private static void RunDirectory()
    {
        Console.WriteLine("----------------------------");
        Console.WriteLine($"Assembly :: {typeof(StartupHook).Assembly.GetName().Name}");
        Console.WriteLine($"Directory.GetCurrentDirectory() :: {Directory.GetCurrentDirectory()}");
        Console.WriteLine($"System.AppContext.BaseDirectory :: {AppContext.BaseDirectory}");
        Console.WriteLine($"AppDomain.CurrentDomain.BaseDirectory :: {AppDomain.CurrentDomain.BaseDirectory}");
        Console.WriteLine($"Environment.CurrentDirectory :: {Environment.CurrentDirectory}");
        Console.WriteLine($"this.GetType().Assembly.Location :: {typeof(StartupHook).Assembly.Location}");
        Console.WriteLine("----------------------------");
    }
}