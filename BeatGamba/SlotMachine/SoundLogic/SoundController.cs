using UnityEngine;

namespace BeatGamba.SlotMachine.SoundLogic;

public class SoundController : MonoBehaviour
{
    internal AudioSource gambaAudioSource = null!;
    
    public void Start()
    {
        gambaAudioSource = GetComponent<AudioSource>();
        gambaAudioSource.clip = AssetLoader.spawnClip;
        gambaAudioSource.Play();
    }
    
    
    internal void PlayRollClip()
    {
        gambaAudioSource.clip = AssetLoader.rollClip;
        gambaAudioSource.Play();
    }

    internal void StopRollClip()
    {
        gambaAudioSource.Stop();
    }

    internal void PlayJackpotClip()
    {
        gambaAudioSource.clip = AssetLoader.jackpotClip;
        gambaAudioSource.Play();
    }
}