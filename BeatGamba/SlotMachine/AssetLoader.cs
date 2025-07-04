using System.Reflection;
using AssetBundleLoadingTools.Utilities;
using UnityEngine;


namespace BeatGamba.SlotMachine;

public static class AssetLoader
{
    private const string _SMPath = "BeatGamba.Assets.gambamachine";
    private static bool ready = false;
    public static GameObject _slotMachineAsset { get; private set; } = null!;
    public static AudioClip rollClip { get; private set; } = null!;
    public static AudioClip jackpotClip { get; private set; } = null!;
    public static AudioClip spawnClip { get; private set; } = null!;
    
    public static void Initialize()
    {
        if (ready) return;
        
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_SMPath);
        var assetBundle = AssetBundle.LoadFromStream(stream);
        
        _slotMachineAsset = assetBundle.LoadAsset<GameObject>("GambaMachine");
        jackpotClip = assetBundle.LoadAsset<AudioClip>("jackpotClip");
        rollClip = assetBundle.LoadAsset<AudioClip>("rollClip");
        spawnClip = assetBundle.LoadAsset<AudioClip>("spawnClip");

        if (rollClip != null)
            Plugin.Log.Info("Roll clip loaded");
        if (jackpotClip != null)
            Plugin.Log.Info("Jackpot clip loaded");
        if (spawnClip != null)
            Plugin.Log.Info("Spawn clip loaded");
        
        if (_slotMachineAsset == null)
        {
            Plugin.Log.Info("gambamachine not found");
            return;
        }
        var replacementInfo = ShaderRepair.FixShadersOnGameObject(_slotMachineAsset);
        
        assetBundle.Unload(false);
        ready = true;
    }
}