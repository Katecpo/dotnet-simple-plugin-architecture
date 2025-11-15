using CustomPlugin.Abstractions;

namespace Plugin1;

public class Startup : IPluginStartup
{
    public string Name => "Plugin1";

    public void Start()
    {
        Console.WriteLine("Hello from Plugin1");
    }
}