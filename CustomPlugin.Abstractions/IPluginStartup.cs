namespace CustomPlugin.Abstractions;

/// <summary>
///     Main plugin startup interface.
/// </summary>
public interface IPluginStartup
{
    /// <summary>
    ///     Plugin name.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    ///     Function to init at startup time.
    /// </summary>
    void Start();
}