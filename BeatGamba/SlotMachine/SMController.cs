using System.IO;
using AssetBundleLoadingTools.Utilities;
using SiraUtil.Zenject;
using UnityEngine;


namespace BeatGamba.SlotMachine;

public class SMController : MonoBehaviour


{
    private string _SMPath = Application.streamingAssetsPath + "/../" + "/../" + "Asset/gambamachine";

    internal GameObject _slotMachineAsset { get; private set; } = null!;

    public void LoadAsset()
    {
        var assetBundle = AssetBundle.LoadFromFile(_SMPath);
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