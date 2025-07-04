using System.Collections;
using System.Collections.Generic;
using BeatGamba.UI;
using UnityEngine;
using Random = UnityEngine.Random;
using BeatGamba.SlotMachine.SoundLogic;


namespace BeatGamba.SlotMachine.SMLogic;

internal class GambaMachine : MonoBehaviour
{
    internal Lever lever { get; private set; } = null!;
    internal Slot[] slots { get; private set; } = new Slot[3];
    
    internal SoundController soundController { get; private set; } = null!;


    public void Start()
    {
        var leverTransform = transform.Find("Lever");
        lever = leverTransform.gameObject.AddComponent<Lever>();
        var pointerEventsHandler = leverTransform.gameObject.AddComponent<VRPointerEventsHandler>();
        
        var slotTransform = transform.Find("Left slot");
        slots[0] = slotTransform.gameObject.AddComponent<Slot>();
        
        slotTransform = transform.Find("Mid slot");
        slots[1] = slotTransform.gameObject.AddComponent<Slot>();
        
        slotTransform = transform.Find("Right slot");
        slots[2] = slotTransform.gameObject.AddComponent<Slot>();
        
        gameObject.AddComponent<AudioSource>();
        soundController = gameObject.AddComponent<SoundController>();
        pointerEventsHandler.PointerDownEvent += (_,_) => Gamble();
    }

    internal void Gamble()
    {
        if (slots[0].IsRolling)
            return;
        
        soundController.PlayRollClip();
        int[] result = [0,0,0];
        for (int i = 0; i < 3; i++)
            result[i] = Random.Range(0, 4);

        StartCoroutine(HandleRoll(result));
    }

    IEnumerator HandleRoll(int[] result)
    {
        lever.Pull();
        
        List<YieldInstruction> slotRollAnimations = new List<YieldInstruction>();
        for (int i = 0; i < slots.Length; i++)
        {
            YieldInstruction rollAnimation = StartCoroutine(slots[i].Roll((Slot.Result)result[i]));
            slotRollAnimations.Add(rollAnimation);
        }
        
        foreach (var anim in slotRollAnimations)
            yield return anim;
        
        soundController.StopRollClip();

        if (result[0] == result[1] && result[1] == result[2])
        {
            soundController.PlayJackpotClip();
            StartCoroutine(LogJackpot(result[0]));
        }
    }

    IEnumerator LogJackpot(int result)
    {
        switch (result)
        {
            case 0:
                Plugin.Log.Notice("You lost!)");
                if (Plugin.Config.CrashEnabled)
                {
                    yield return new WaitForSeconds(1f);
                    Application.Quit();
                }
                break;
            case 1:
                Plugin.Log.Notice("Badcut jackpot");
                break;
            case 2:
                Plugin.Log.Notice("Miss jackpot");
                break;
            case 3:
                Plugin.Log.Notice("115 jackpot");
                break;
            default:
                Plugin.Log.Notice("Unluck");
                break;
        }
    }
}
