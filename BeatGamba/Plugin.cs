using BeatGamba.Installers;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using IPA.Logging;
using SiraUtil.Zenject;


namespace BeatGamba;

[Plugin(RuntimeOptions.DynamicInit)]
internal class Plugin
{
    internal static Plugin Instance { get; private set; } = null!;
    internal static Logger Log { get; private set; } = null!;
    
    
    [Init]
    public Plugin(Logger log, Config config, PluginMetadata metadata, Zenjector zenjector)
    {
        log.Info($"{metadata.Name} {metadata.HVersion} initialized.");

        Instance = this;
        Log = log;
        
        var pluginConfig = config.Generated<PluginConfig>();
        
        zenjector.UseLogger(log);
        
        //Installers
        zenjector.Install<AppInstaller>(Location.App, pluginConfig);
        zenjector.Install<MenuInstaller>(Location.Menu);
        
    }
}