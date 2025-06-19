using BeatGamba.SlotMachine;
using BeatGamba.UI;
using Zenject;


namespace BeatGamba.Installers;

internal class MenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<MenuButtonManager>().AsSingle();
        Container.Bind<BeatGambaFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<UIViewController>().FromNewComponentAsViewController().AsSingle();
    }
}