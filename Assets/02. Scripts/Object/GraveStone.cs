using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : InteractableObject
{
    private bool isCanInteraction = true;

    public Transform playerPos;

    public CanvasGroup myText;

    public GameObject myCamera;

    public override void Interaction()
    {
        base.Interaction();

        if (!isCanInteraction) return;

        isCanInteraction = false;
        StartCoroutine(ShowGraveStone());
    }

    private IEnumerator ShowGraveStone()
    {
        Player player = GamePlayManager.instance.player;

        GamePlayManager.instance.isCutScene = true;

        FadeManager.instance.SetFade(1f, Color.black, () =>
        {
            player.transform.position = playerPos.position;
            player.transform.rotation = playerPos.rotation;
            myCamera.SetActive(true);
        });

        yield return new WaitForSeconds(1.25f);

        player.anim.SetTrigger("GetItem");

        yield return new WaitForSeconds(2f);

        float progress = 0f;

        myText.gameObject.SetActive(true);

        while(progress <= 1f)
        {
            progress += Time.deltaTime;
            myText.alpha = progress;
            yield return null;
        }

        while (!Input.anyKey)
            yield return null;

        player.anim.SetTrigger("GetItemReturn");

        while (progress >= 0f)
        {
            progress -= Time.deltaTime;
            myText.alpha = progress;
            yield return null;
        }

        myText.gameObject.SetActive(false);

        FadeManager.instance.SetFade(1f, Color.black, () =>
        {
            myCamera.SetActive(false);
        },() =>
        {
            GamePlayManager.instance.isCutScene = false;
        });
    }
}
