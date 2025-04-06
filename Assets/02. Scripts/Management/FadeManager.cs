using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    public Image fadeImage;

    private bool isFading;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void SetFade(float time, Color color, Action action = null, Action endAction = null)
    {
        if (isFading) return;

        isFading = true;

        StartCoroutine(Fading(time, color, action, endAction));
    }

    private IEnumerator Fading(float time, Color color, Action action = null, Action endAction = null)
    {
        fadeImage.gameObject.SetActive(true);

        Color startColor = color;
        Color endColor = color;
        startColor.a = 0;
        endColor.a = 1;

        float progress = 0f;

        while(progress <= 1f)
        {
            progress += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        action?.Invoke();

        while (progress >= 0f)
        {
            progress -= Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        endAction?.Invoke();
        fadeImage.gameObject.SetActive(false);
        isFading = false;
    }
}
