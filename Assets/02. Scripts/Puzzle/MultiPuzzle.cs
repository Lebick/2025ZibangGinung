using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPuzzle : Puzzle
{
    public List<Puzzle> needPuzzles = new();

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (this.isClear) return;

        bool isClear = true;

        for(int i=0; i<needPuzzles.Count; i++)
        {
            if (!needPuzzles[i].isClear)
                isClear = false;
        }

        if (isClear)
            PuzzleClear();
    }

    public override void PuzzleClear()
    {
        base.PuzzleClear();
    }

    public override void PuzzleReset()
    {
        base.PuzzleReset();
    }
}
