using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public float playTime;

    public List<int> openChests = new();
    public List<int> clearPuzzles = new();

    public bool isCutScene;

    public Player player;
    public Transform cutSceneCanvas;

    public Text alertText;

    private string alert;

    public RoomData currentRoom;

    public int stageIndex;

    private void Update()
    {
        playTime += Time.deltaTime;
    }

    public void Exit()
    {
        GameManager.instance.openChests.AddRange(openChests);
        GameManager.instance.clearPuzzles.AddRange(clearPuzzles);
        GameManager.instance.playTime += playTime;

        float moneyValue = 0;

        for(int i=0; i < player.playerItem.items.Count; i++)
        {
            moneyValue += player.playerItem.items[i].price;
        }

        GameManager.instance.money += moneyValue;
    }

    public void AlertMessage(string text)
    {
        alert = text;
        StartCoroutine(nameof(ShowAlert));
    }

    private IEnumerator ShowAlert()
    {
        alertText.text = alert;
        alertText.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(1f);

        float progress = 0f;
        
        while(progress <= 1f)
        {
            progress += Time.deltaTime;
            alertText.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), progress);
            yield return null;
        }
    }
}
