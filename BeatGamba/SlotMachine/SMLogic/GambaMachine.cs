using UnityEngine;
using Random = UnityEngine.Random;

namespace BeatGamba.SlotMachine.SMLogic;

internal class GambaMachine : MonoBehaviour
{
    internal Lever lever { get; private set; } = null!;
    internal Slot[] slots { get; private set; } = null!;


    public void Awake()
    {
        var leverTransform = transform.Find("Lever");
        lever = leverTransform.gameObject.AddComponent<Lever>();
        
        var slotTransform = transform.Find("Left slot");
        slots[0] = slotTransform.gameObject.AddComponent<Slot>();
        
        slotTransform = transform.Find("Mid slot");
        slots[1] = slotTransform.gameObject.AddComponent<Slot>();
        
        slotTransform = transform.Find("Right slot");
        slots[2] = slotTransform.gameObject.AddComponent<Slot>();
    }

    internal void Gamble()
    {
        lever.Pull();
        int result = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            result = Random.Range(0, 4);
            StartCoroutine(slots[i].Roll((Slot.Result)result));
        }

    }
}