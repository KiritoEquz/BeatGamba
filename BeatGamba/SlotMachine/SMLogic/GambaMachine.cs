using BeatGamba.UI;
using UnityEngine;
using VRUIControls;
using Random = UnityEngine.Random;

namespace BeatGamba.SlotMachine.SMLogic;

internal class GambaMachine : MonoBehaviour
{
    private float k;
    internal Lever lever { get; private set; } = null!;
    internal Slot[] slots { get; private set; } = new Slot[3];


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

        pointerEventsHandler.PointerDownEvent += (_,_) => Gamble();
    }

    internal void Gamble()
    {
        if (slots[0].IsRolling)
            return;
        
        lever.Pull();
        int result = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            result = Random.Range(0, 4);
            StartCoroutine(slots[i].Roll((Slot.Result)result));
            switch (slots[i].LastResult)
            {
                case 0:
                    k+=0.1f;
                    break;
                case 1:
                    k += 0.5f;
                    break;
                case 2:
                    k += 1f;
                    break;
                case 3:
                    k += 3f;
                    break;
            }
            StartCoroutine(ResultCoroutine(k));
            k = 0; // resets k
        }

    }
    IEnumerator ResultCoroutine(float k)
    {
        switch (k)
        {
            case 9f:
                Plugin.Log.Notice("115");
                //Plugin.Log.Info(k.ToString()); <- to test conflicts of adding to k
                break;
            case 1.5f:
                Plugin.Log.Notice("Badcut");
                //Plugin.Log.Info(k.ToString()); <- to test conflicts of adding to k
                break;
            case 0.3f:
                Plugin.Log.Critical("Skulls");
                //Plugin.Log.Info(k.ToString()); <- to test conflicts of adding to k
                yield return new WaitForSeconds(17f);
                Application.Quit();
                break;
            case 3:
                Plugin.Log.Notice("Miss");
                //Plugin.Log.Info(k.ToString()); <- to test conflicts of adding to k
                break;
            default: // basically everything except ^^
                Plugin.Log.Notice("Unluck");
                //Plugin.Log.Info(k.ToString()); <- to test conflicts of adding to k
                break;
        }
    }
}
