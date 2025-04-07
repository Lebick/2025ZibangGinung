using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : InteractableObject
{
    public int myIndex;

    private bool isCanInteraction = true;

    public ItemInfo myItem;

    public Transform playerPos;

    public GameObject myCamera;

    public Animator anim;

    public GameObject itemImage;

    public Transform canvas;

    private void Start()
    {
        if (GameManager.instance.openChests.Contains(myIndex))
        {
            anim.SetTrigger("Open");
            isCanInteraction = false;
            gameObject.layer = 0;
        }

        canvas = GamePlayManager.instance.cutSceneCanvas;
    }

    public override void Interaction()
    {
        base.Interaction();

        if (!isCanInteraction) return;

        if (GamePlayManager.instance.player.playerItem.IsCanGetItem())
        {
            isCanInteraction = false;
            StartCoroutine(ShowGraveStone());
        }
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

        yield return new WaitForSeconds(2.5f);

        anim.SetTrigger("Open");

        GamePlayManager.instance.player.playerItem.items.Add(myItem);
        GamePlayManager.instance.openChests.Add(myIndex);

        RectTransform image = Instantiate(itemImage, canvas).transform as RectTransform;
        image.GetComponent<Image>().sprite = myItem.itemSprite;

        Vector3 pos1 = myCamera.GetComponent<Camera>().WorldToViewportPoint(transform.position);
        pos1 = new Vector2(pos1.x * Screen.width - (Screen.width / 2f), pos1.y * Screen.height - (Screen.height / 2f));

        Vector3 pos3 = GamePlayManager.instance.player.playerHUD.backpackTransform.anchoredPosition;

        Vector3 pos2 = pos1 + Quaternion.Euler(0, 0, -60f) * (pos3 - pos1);

        float progress = 0f;

        while (progress <= 1f)
        {
            progress += Time.deltaTime;
            Vector3 pos4 = Vector3.Lerp(pos1, pos2, progress);
            Vector3 pos5 = Vector3.Lerp(pos2, pos3, progress);

            image.anchoredPosition = Vector3.Lerp(pos4, pos5, progress);
            yield return null;
        }

        while(progress >= 0f)
        {
            progress -= Time.deltaTime * 2f;
            image.GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), progress);
            yield return null;
        }

        Destroy(image.gameObject);
       
        FadeManager.instance.SetFade(1f, Color.black, () =>
        {
            myCamera.SetActive(false);
        }, () =>
        {
            GamePlayManager.instance.isCutScene = false;
        });
    }
}
