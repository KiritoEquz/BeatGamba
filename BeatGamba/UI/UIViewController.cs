using BeatGamba.SlotMachine;
using BeatGamba.SlotMachine.SMLogic;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;
using UnityEngine;
using Zenject;

namespace BeatGamba.UI;


[HotReload(RelativePathToLayout = @".\UI.bsml")]
[ViewDefinition("BeatGamba.UI.UI.bsml")]
internal class UIViewController : BSMLAutomaticViewController
{
    [Inject] private readonly PluginConfig _config = null!;

    private AssetLoader _assetLoader = new AssetLoader();
    private GameObject _gambaMachineInstance = null!;

    private bool Enabled
    {
        get => _config.Enabled;
        set => _config.Enabled = value;
    }

    private void SpawnGamba()
    {
        if (_gambaMachineInstance)
            return;
        if (!_assetLoader._slotMachineAsset)
            _assetLoader.LoadAsset();
        
        _gambaMachineInstance = Instantiate(_assetLoader._slotMachineAsset, new Vector3(-2f, 0.5f, -1f), Quaternion.Euler(0f, -120f, 0f));
        _gambaMachineInstance.AddComponent<GambaMachine>();
        
        Plugin.Log.Info("Slot machine successfully spawned");
        
    }

    private void DestroyGamba()
    {
        if (_gambaMachineInstance)
        {
            Destroy(_gambaMachineInstance);
            Plugin.Log.Info("Slot machine successfully destroyed");
        }
    }
}