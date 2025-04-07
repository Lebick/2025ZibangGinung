using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPuzzle : Puzzle
{
    public List<MovingColumn> columns = new();

    public float columnDistance;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (this.isClear) return;

        bool isClear = true;

        for(int i=0; i<columns.Count; i++)
        {
            if (!columns[i].switchState)
                isClear = false;
        }

        if (isClear)
        {
            PuzzleClear();
        }
    }

    public override void PuzzleClear()
    {
        base.PuzzleClear();

        CameraShake.instance.SetShake(1f, 0.05f);
    }

    public override void PuzzleReset()
    {
        base.PuzzleReset();

        if (isClear)
        {
            GamePlayManager.instance.AlertMessage("이미 클리어 한 퍼즐입니다.");
            return;
        }

        GamePlayManager.instance.isCutScene = true;

        FadeManager.instance.SetFade(1f, Color.black, () =>
        {
            for (int i = 0; i < columns.Count; i++)
                columns[i].ResetPos();
        }, () =>
        {
            GamePlayManager.instance.isCutScene = false;
        });
    }
}
