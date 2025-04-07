using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutScene : MonoBehaviour
{
    public GameObject myCamera;

    public GameObject roomLight;

    public UnityEvent cutSceneEvent;

    public void SetCutScene()
    {
        StartCoroutine(CutScenePlay());
    }

    private IEnumerator CutScenePlay()
    {
        GamePlayManager.instance.isCutScene = true;

        FadeManager.instance.SetFade(1f, Color.black, () =>
        {
            myCamera.SetActive(true);
            roomLight.SetActive(true);
        });

        yield return new WaitForSeconds(1f);

        cutSceneEvent?.Invoke();

        yield return new WaitForSeconds(1.5f);

        FadeManager.instance.SetFade(1f, Color.black, () =>
        {
            myCamera.SetActive(false);
            roomLight.SetActive(false);
        }, () =>
        {
            GamePlayManager.instance.isCutScene = false;
        });
    }
}
