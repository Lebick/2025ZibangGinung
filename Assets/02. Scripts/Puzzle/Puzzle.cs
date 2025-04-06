using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public int myIndex;

    public UnityEvent clearEvent;

    public bool isClear;

    public Transform resetPos;

    protected virtual void Start()
    {
        if (GameManager.instance.clearPuzzles.Contains(myIndex))
            clearEvent?.Invoke();
    }

    public virtual void PuzzleClear()
    {
        isClear = true;
        clearEvent?.Invoke();

        GamePlayManager.instance.clearPuzzles.Add(myIndex);
    }

    public virtual void PuzzleReset()
    {
        //if (isClear)
        //{
        //    GamePlayManager.instance.AlertMessage("이미 클리어 한 퍼즐입니다.");
        //    return;
        //}
    }
}
