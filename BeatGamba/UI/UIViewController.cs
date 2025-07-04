using BeatGamba.SlotMachine;
using BeatGamba.SlotMachine.SMLogic;
using BeatGamba.SlotMachine.SoundLogic;
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
    
    internal GameObject gambaMachineInstance = null!;
    

    private bool CrashEnabled
    {
        get => _config.CrashEnabled;
        set => _config.CrashEnabled = value;
    }

    private void SpawnGamba()
    {
        if (gambaMachineInstance)
            return;
        
        gambaMachineInstance = Instantiate(AssetLoader._slotMachineAsset, new Vector3(-2f, 0.5f, -1f), Quaternion.Euler(0f, -120f, 0f));
        
        gambaMachineInstance.AddComponent<GambaMachine>();
        Plugin.Log.Info("Slot machine successfully spawned");
        
    }

    private void DestroyGamba()
    {
        if (gambaMachineInstance)
        {
            Destroy(gambaMachineInstance);
            Plugin.Log.Info("Slot machine successfully destroyed");
        }
    }
}