using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemEffect
{
    none,
    speed1,
    speed2,
    finder,
    breath,
    hp,
    invisible
}

[CreateAssetMenu(fileName = "info", menuName = "ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public string myName;

    public float price;
    public float weight;

    public Sprite itemSprite;
    public ItemEffect effect;
}
