using System.Reflection;
using AssetBundleLoadingTools.Utilities;
using UnityEngine;


namespace BeatGamba.SlotMachine;

public class AssetLoader : MonoBehaviour
{
    private const string _SMPath = "BeatGamba.Assets.gambamachine";

    internal GameObject _slotMachineAsset { get; private set; } = null!;

    public void LoadAsset()
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_SMPath);
        var assetBundle = AssetBundle.LoadFromStream(stream);
        _slotMachineAsset = assetBundle.LoadAsset<GameObject>("gambamachine");
        if (_slotMachineAsset == null)
        {
            Plugin.Log.Info("gambamachine not found");
            return;
        }
        var replacementInfo = ShaderRepair.FixShadersOnGameObject(_slotMachineAsset);
        
        assetBundle.Unload(false);
    }
}