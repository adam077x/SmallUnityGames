using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    public float strengthModifier = 1.5f;

    void Update()
    {
        if(start)
        {
            start = false;
            StartCoroutine(Shaking());
        }    
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration)  * strengthModifier;
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

    public static void StartShaking()
    {
        start = true;
    }
}
