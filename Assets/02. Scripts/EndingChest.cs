using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingChest : InteractableObject
{
    public Transform playerPos;

    public GameObject myCamera;

    public Animator anim;

    public GameObject itemImage;

    public Transform canvas;

    public List<Sprite> gemSprites = new();

    private void Start()
    {
        canvas = GamePlayManager.instance.cutSceneCanvas;
    }

    public override void Interaction()
    {
        StartCoroutine(ShowGraveStone());
    }

    private IEnumerator ShowGraveStone()
    {
        Player player = GamePlayManager.instance.player;

        GamePlayManager.instance.isCutScene = true;

        FadeManager.instance.SetFade(1f, Color.black, () =>
        {
            player.transform.position = playerPos.position;
            myCamera.SetActive(true);
        });

        yield return new WaitForSeconds(2.5f);

        //anim.SetTrigger("Open");

        for(int i=0; i<30; i++)
        {
            StartCoroutine(ShowImage());
            yield return new WaitForSeconds(0.1f);
        }

        FadeManager.instance.SetFade(1f, Color.black, () =>
        {
            myCamera.SetActive(false);
        }, () =>
        {
            GamePlayManager.instance.isCutScene = false;
        });
    }

    private IEnumerator ShowImage()
    {
        RectTransform image = Instantiate(itemImage, canvas).transform as RectTransform;
        image.GetComponent<Image>().sprite = gemSprites[Random.Range(0, gemSprites.Count)];

        Vector3 pos1 = myCamera.GetComponent<Camera>().WorldToViewportPoint(transform.position);
        pos1 = new Vector2(pos1.x * Screen.width - (Screen.width / 2f), pos1.y * Screen.height - (Screen.height / 2f));

        Vector3 pos3 = GamePlayManager.instance.player.playerHUD.backpackTransform.anchoredPosition;

        Vector3 pos2 = pos1 + Quaternion.Euler(0, 0, Random.Range(-60f, 60f)) * (pos3 - pos1);

        float progress = 0f;

        while (progress <= 1f)
        {
            progress += Time.deltaTime;
            Vector3 pos4 = Vector3.Lerp(pos1, pos2, progress);
            Vector3 pos5 = Vector3.Lerp(pos2, pos3, progress);

            image.anchoredPosition = Vector3.Lerp(pos4, pos5, progress);
            yield return null;
        }

        while (progress >= 0f)
        {
            progress -= Time.deltaTime * 2f;
            image.GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), progress);
            yield return null;
        }

        Destroy(image.gameObject);
    }
}
