using System;
using BeatSaberMarkupLanguage.MenuButtons;
using Zenject;

namespace BeatGamba.UI;

internal class MenuButtonManager : IInitializable, IDisposable
{
    private readonly MainFlowCoordinator mainFlowCoordinator;
    private readonly BeatGambaFlowCoordinator beatGambaFlowCoordinator;
    private readonly MenuButtons menuButtons;
    private readonly MenuButton menuButton;
    
    public MenuButtonManager(
        MainFlowCoordinator mainFlowCoordinator,
        BeatGambaFlowCoordinator beatGambaFlowCoordinator,
        MenuButtons menuButtons)
    {
        this.mainFlowCoordinator = mainFlowCoordinator;
        this.beatGambaFlowCoordinator = beatGambaFlowCoordinator;
        this.menuButtons = menuButtons;
        menuButton = new(nameof(BeatGamba), PresentFlowCoordinator);
    }
    
    public void Initialize()
    {
        menuButtons.RegisterButton(menuButton);
        beatGambaFlowCoordinator.DidFinish += DismissFlowCoordinator;
    }

    public void Dispose()
    {
        beatGambaFlowCoordinator.DidFinish -= DismissFlowCoordinator;
    }

    private void PresentFlowCoordinator() =>
        mainFlowCoordinator.PresentFlowCoordinator(beatGambaFlowCoordinator);

    private void DismissFlowCoordinator() =>
        mainFlowCoordinator.DismissFlowCoordinator(beatGambaFlowCoordinator);
}