using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DaechingPuzzle : Puzzle
{
    public Transform leftPuzzle;
    public Transform rightPuzzle;

    public List<MovingBarrel> leftBarrels = new();
    public List<MovingBarrel> rightBarrels = new();

    public CutScene cutScene;

    protected override void Start()
    {
        base.Start();

        leftBarrels = leftPuzzle.GetComponentsInChildren<MovingBarrel>().ToList();
        rightBarrels = rightPuzzle.GetComponentsInChildren<MovingBarrel>().ToList();
    }

    private void Update()
    {
        if (this.isClear) return;

        bool isClear = true;

        for(int i=0; i<leftBarrels.Count; i++)
        {
            bool isDaeching = false;

            for(int j=0; j<rightBarrels.Count; j++)
            {
                if (Vector3.Distance(leftPuzzle.position - leftBarrels[i].transform.position, rightPuzzle.position - rightBarrels[j].transform.position) <= 0.1f)
                    isDaeching = true;
            }

            if (!isDaeching) isClear = false;
        }

        if (isClear)
            PuzzleClear();
    }

    public override void PuzzleClear()
    {
        isClear = true;
        cutScene.SetCutScene();
        CameraShake.instance.SetShake(1f, 0.05f);
        GamePlayManager.instance.clearPuzzles.Add(myIndex);
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
            for (int i = 0; i < leftBarrels.Count; i++)
                leftBarrels[i].ResetPos();

            for (int i = 0; i < leftBarrels.Count; i++)
                rightBarrels[i].ResetPos();
        }, () =>
        {
            GamePlayManager.instance.isCutScene = false;
        });
    }
}
