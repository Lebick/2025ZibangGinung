using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressPuzzle : Puzzle
{
    public List<Pressure> pressures = new();
    public List<MovingBarrel> barrels = new();

    protected override void Start()
    {
        base.Start();
        pressures = GetComponentsInChildren<Pressure>().ToList();
        barrels = GetComponentsInChildren<MovingBarrel>().ToList();
    }

    private void Update()
    {
        if (this.isClear) return;

        bool isClear = true;

        for(int i=0; i<pressures.Count; i++)
        {
            if (!pressures[i].isPress)
                isClear = false;
        }

        if (isClear)
            PuzzleClear();
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
            GamePlayManager.instance.player.transform.position = resetPos.position;
            for(int i=0; i<barrels.Count; i++)
            {
                barrels[i].ResetPos();
            }
        }, () =>
        {
            GamePlayManager.instance.isCutScene = false;
        });
    }
}
