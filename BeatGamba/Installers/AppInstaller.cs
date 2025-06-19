using Zenject;

namespace BeatGamba.Installers;

internal class AppInstaller : Installer
{
    private readonly PluginConfig _config = null!;
    
    public AppInstaller(PluginConfig config)
    {
        _config = config;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_config).AsSingle();
    }
}