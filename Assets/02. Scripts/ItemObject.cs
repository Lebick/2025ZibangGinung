using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : InteractableObject
{
    public ItemInfo item;

    public ParticleSystem particle;

    public List<ItemInfo> items = new();

    private void Start()
    {
        item = items[Random.Range(0, items.Count)];
    }

    public override void Interaction()
    {
        base.Interaction();

        if (GamePlayManager.instance.player.playerItem.IsCanGetItem())
        {
            GamePlayManager.instance.player.playerItem.GetItem(item);
        }

        gameObject.layer = 0;
        particle.Stop();
    }
}
