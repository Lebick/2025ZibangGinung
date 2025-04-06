using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float playTime;
    public float money;

    public int breathState;
    public int backpackState;
    public int weaponState;

    public List<int> openChests = new();
    public List<int> clearPuzzles = new();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public string ReturnTimeFormat(float time)
    {
        string mm = ((int)(time / 60)).ToString("D2");
        string ss = ((int)(time % 60)).ToString("D2");

        return $"{mm} : {ss}";
    }
}
