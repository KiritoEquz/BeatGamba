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
        float downDuration = 70f / 60f;
        float downAngle = 70f;
        float downSpeed = downAngle / downDuration;

        float downRotated = 0f;
        while (downRotated < downAngle)
        {
            float delta = downSpeed * Time.deltaTime;
            _leverTransform.Rotate(0, 0, delta);
            downRotated += delta;
            yield return null;
        }
    
        yield return new WaitForSeconds(0.6f);

        float upDuration = 280f / 60f;
        float upAngle = 70f;
        float upSpeed = upAngle / upDuration;

        float upRotated = 0f;
        while (upRotated < upAngle)
        {
            float delta = upSpeed * Time.deltaTime;
            _leverTransform.Rotate(0, 0, -delta);
            upRotated += delta;
            yield return null;
        }
    }
}
