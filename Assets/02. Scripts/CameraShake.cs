using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    private IEnumerator currentCoroutine;

    public void SetShake(float duration, float amount)
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = Shake(duration, amount);
        StartCoroutine(currentCoroutine);
    }

    private IEnumerator Shake(float duration, float amount)
    {
        float progress = 0f;

        while(progress <= duration)
        {
            progress += Time.deltaTime;

            Vector3 pos = Random.insideUnitSphere * amount;

            transform.localPosition = pos;

            yield return null;
        }

        transform.localPosition = Vector3.zero;

        yield break;
    }
}
