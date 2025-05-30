using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBtn : MonoBehaviour
{
    public GameObject helpUI;

    public CanvasGroup startBtnGroup;
    public CanvasGroup rankingGroup;

    public void _0_OnClickStart()
    {
        startBtnGroup.interactable = false;
        StartCoroutine(Starting());
    }

    public void _1_OnClickRanking()
    {
        startBtnGroup.interactable = false;
        StartCoroutine(ShowRanking());
    }

    public void _2_OnClickHelp()
    {
        helpUI.SetActive(true);
    }

    public void _3_OnClickExit()
    {
        Application.Quit();
    }

    public void _4_OnClickRankingBack()
    {
        rankingGroup.interactable = false;
        StartCoroutine(HideRanking());
    }

    private IEnumerator Starting()
    {
        yield return null;
    }

    private IEnumerator ShowRanking()
    {
        float progress = 0f;

        while (progress <= 1f)
        {
            progress += Time.deltaTime;
            startBtnGroup.alpha = 1 - progress;
            yield return null;
        }

        startBtnGroup.gameObject.SetActive(false);
        rankingGroup.gameObject.SetActive(true);
        rankingGroup.interactable = true;

        while (progress >= 0f)
        {
            progress -= Time.deltaTime;
            rankingGroup.alpha = 1 - progress;
            yield return null;
        }
    }

    private IEnumerator HideRanking()
    {
        float progress = 0f;

        while (progress <= 1f)
        {
            progress += Time.deltaTime;
            rankingGroup.alpha = 1 - progress;
            yield return null;
        }

        startBtnGroup.gameObject.SetActive(true);
        rankingGroup.gameObject.SetActive(false);
        startBtnGroup.interactable = true;

        while (progress >= 0f)
        {
            progress -= Time.deltaTime;
            startBtnGroup.alpha = 1 - progress;
            yield return null;
        }
    }
}
