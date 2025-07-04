using BeatGamba.Installers;
using BeatGamba.SlotMachine;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using IPA.Logging;
using JetBrains.Annotations;
using SiraUtil.Zenject;


namespace BeatGamba;

[Plugin(RuntimeOptions.DynamicInit)]
internal class Plugin
{
    internal static Plugin Instance { get; private set; } = null!;
    internal static Logger Log { get; private set; } = null!;
    internal static PluginConfig Config { get; private set; } = null!;
    
    
    [Init]
    public Plugin(Logger log, Config config, PluginMetadata metadata, Zenjector zenjector)
    {
        log.Info($"{metadata.Name} {metadata.HVersion} initialized.");

        Instance = this;
        Log = log;
        
        Config = config.Generated<PluginConfig>();
        
        zenjector.UseLogger(log);
        
        //Installers
        zenjector.Install<AppInstaller>(Location.App, Config);
        zenjector.Install<MenuInstaller>(Location.Menu);
        
    }

    [OnStart]
    [UsedImplicitly]
    public void OnApplicationStart()
    {
        AssetLoader.Initialize();
    }
}