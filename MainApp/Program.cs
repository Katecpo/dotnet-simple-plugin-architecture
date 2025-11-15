using System.Reflection;
using CustomPlugin.Abstractions;

namespace MainApp;

class Program
{
    private readonly List<Assembly> plugins;

    static void Main(string[] args)
    {
        var pluginFiles = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.Plugin.dll");

        var pluginAssemblies = new List<Assembly>();

        foreach (var file in pluginFiles)
        {
            var assembly = Assembly.LoadFile(file);
            pluginAssemblies.Add(assembly);
        }

        foreach (var assembly in pluginAssemblies)
        {
            var startupClasses = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IPluginStartup))).ToList();
            var assemblyFullName = assembly.FullName;

            if (startupClasses.Count > 1)
                throw new ApplicationException($"Found multiple startup classes for {assemblyFullName}");

            var startupClass = startupClasses.FirstOrDefault();

            if (startupClass is null)
                continue;

            var startupInstance = Activator.CreateInstance(startupClass) as IPluginStartup;
            if (startupInstance is null)
                throw new ApplicationException($"Could not create plugin startup instance for {assemblyFullName}");
            
            startupInstance.Start();
            Console.WriteLine($"Started {assemblyFullName}");
        }
    }
}