using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public float maxWeight;
    public float currentWeight;

    public int maxItemCount;

    public int currentSelectItem;

    public List<ItemInfo> items = new();

    public Indicator indicator;

    private void Update()
    {
        for (int i = 0; i < maxItemCount; i++)
        {
            if(Input.GetKeyDown((KeyCode)(i + (int)KeyCode.Alpha1)))
            {
                currentSelectItem = i;
            }
        }

        currentWeight = 0;
        for(int i=0; i < items.Count; i++)
        {
            currentWeight += items[i].weight;
        }

        if (Input.GetKeyDown(KeyCode.E) && currentSelectItem < items.Count)
        {
            UseItem();
        }
    }

    public bool IsCanGetItem()
    {
        if(items.Count >= maxItemCount)
        {
            GamePlayManager.instance.AlertMessage("아이템이 너무 많습니다.");
            return false;
        }

        return true;
    }

    public void GetItem(ItemInfo item)
    {
        items.Add(item);
    }

    private void UseItem()
    {
        switch (items[currentSelectItem].effect)
        {
            case ItemEffect.none: return;

            case ItemEffect.speed1:
                GamePlayManager.instance.player.moveSpeedBuff = 1.2f;

                if(IsInvoking(nameof(MoveSpeedReturn)))
                    CancelInvoke(nameof(MoveSpeedReturn));

                Invoke(nameof(MoveSpeedReturn), 5f);

                break;

            case ItemEffect.speed2:
                GamePlayManager.instance.player.moveSpeedBuff = 1.5f;

                if (IsInvoking(nameof(MoveSpeedReturn)))
                    CancelInvoke(nameof(MoveSpeedReturn));

                Invoke(nameof(MoveSpeedReturn), 5f);

                break;

            case ItemEffect.invisible:
                GamePlayManager.instance.player.isInvisible = true;
                break;

            case ItemEffect.finder:
                indicator.gameObject.SetActive(true);
                indicator.FindTarget();
                break;

            case ItemEffect.hp:
                GamePlayManager.instance.player.hp += GamePlayManager.instance.player.maxHP * 0.5f;
                break;

            case ItemEffect.breath:
                GamePlayManager.instance.player.breath += GamePlayManager.instance.player.maxBreath * 0.5f;
                break;
        }

        items.RemoveAt(currentSelectItem);
    }

    private void MoveSpeedReturn()
    {
        GamePlayManager.instance.player.moveSpeedBuff = 1.0f;
    }

    private void InvisibleReturn()
    {
        GamePlayManager.instance.player.isInvisible = false;
    }
}
