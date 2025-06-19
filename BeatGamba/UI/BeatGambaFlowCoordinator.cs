using System;
using HMUI;
using Zenject;

namespace BeatGamba.UI;

internal class BeatGambaFlowCoordinator : FlowCoordinator
{
    [Inject] private readonly UIViewController uiViewController = null!;

    public event Action? DidFinish;
    
    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        if (firstActivation)
        {
            showBackButton = true;
            SetTitle(nameof(BeatGamba));
        }

        if (addedToHierarchy)
        {
            ProvideInitialViewControllers(uiViewController);
        }
    }

    protected override void BackButtonWasPressed(ViewController topViewController)
    {
        DidFinish?.Invoke();
    }
}