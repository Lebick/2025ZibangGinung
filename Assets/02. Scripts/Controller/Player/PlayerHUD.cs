using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image hpFill;
    public Text hpText;

    public Image breathFill1;
    public Image breathFill2;
    public Text breathText;

    public Image backpackFill;
    public RectTransform backpackTransform;
    public Text backpackText;

    public Gradient backpackGradient;

    public List<Image> itemUIs = new();

    public void UpdateHUD(Player player)
    {
        UpdateHP(player);
        UpdateBreath(player);
        UpdateBackpack(player.playerItem);
    }

    private void UpdateHP(Player player)
    {
        float value = player.hp / player.maxHP;

        hpFill.fillAmount = value;
        hpText.text = $"{(int)(value * 100)}%";
    }

    private void UpdateBreath(Player player)
    {
        float value = player.breath / player.maxBreath;

        breathFill1.fillAmount = value;
        breathFill2.fillAmount = value;

        hpText.text = $"{(int)(value * 100)}%";
    }

    private void UpdateBackpack(PlayerItem player)
    {
        float value = player.currentWeight / player.maxWeight;

        backpackFill.fillAmount = value;
        backpackText.text = $"{(int)player.currentWeight} / {(int)player.maxWeight}";
        backpackFill.color = backpackGradient.Evaluate(value / 2f);

        for(int i=0; i<itemUIs.Count; i++)
        {
            itemUIs[i].gameObject.SetActive(i < player.maxItemCount);

            if (i < player.items.Count)
            {
                itemUIs[i].transform.Find("Icon").gameObject.SetActive(true);
                itemUIs[i].transform.Find("Icon").GetComponent<Image>().sprite = player.items[i].itemSprite;
            }
            else
            {
                itemUIs[i].transform.Find("Icon").gameObject.SetActive(false);
            }

            itemUIs[i].transform.Find("OutLine").gameObject.SetActive(false);
        }

        itemUIs[player.currentSelectItem].transform.Find("OutLine").gameObject.SetActive(true);
    }
}
