using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public InputField nameInput;

    public List<RankData> rankData = new();

    public List<Transform> rankerUI = new();

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        rankData.Clear();
        for (int i = 0; i < 5; i++)
        {
            string name = PlayerPrefs.GetString($"{i}Name", "------");
            float time = PlayerPrefs.GetFloat($"{i}Time", 3599);
            rankData.Add(new RankData(name, time));
        }

        UpdateUI();
    }

    public void Save()
    {
        Sort();

        for(int i=0; i<rankData.Count; i++)
        {
            PlayerPrefs.SetString($"{i}Name", rankData[i].name);
            PlayerPrefs.SetFloat($"{i}Time", rankData[i].time);
        }

        UpdateUI();
    }

    public void Sort()
    {
        rankData = rankData.OrderBy(a => a.time).ToList();
        rankData = rankData.GetRange(0, Mathf.Min(rankData.Count, 5));
    }

    public void Register()
    {
        string name = nameInput.text;
        float time = GameManager.instance.playTime;

        rankData.Add(new RankData(name, time));
        Save();
    }

    public void UpdateUI()
    {
        for(int i=0; i<5; i++)
        {
            Text name = rankerUI[i].GetChild(1).GetComponent<Text>();
            Text time = rankerUI[i].GetChild(2).GetComponent<Text>();

            name.text = rankData[i].name;
            time.text = GameManager.instance.ReturnTimeFormat(rankData[i].time);
        }
    }
}

[System.Serializable]
public class RankData
{
    public string name;
    public float time;
    public RankData(string name, float time)
    {
        this.name = name;
        this.time = time;
    }
}