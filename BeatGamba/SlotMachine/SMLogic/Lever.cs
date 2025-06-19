using UnityEngine;
using System.Collections;

namespace BeatGamba.SlotMachine.SMLogic;

public class Lever : MonoBehaviour
{
    private Transform _leverTransform = null!;

    internal void Awake()
    {
        _leverTransform = GetComponent<Transform>();
    }

    internal void Pull()
    {
        StartCoroutine(PullCoroutine());
    }

    IEnumerator PullCoroutine()
    {
        for (int i = 0; i < 70; i++)
        {
            _leverTransform.Rotate(0,0,1);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < 280; i++)
        {
            _leverTransform.Rotate(0,0,-0.25f);
            yield return null;
        }
    }
}