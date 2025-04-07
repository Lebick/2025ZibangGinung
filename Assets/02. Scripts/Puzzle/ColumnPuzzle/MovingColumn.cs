using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingColumn : InteractableObject
{
    public bool switchState;

    private bool startState;

    public Vector3 onPos;
    public Vector3 offPos;

    public ColumnPuzzle myPuzzle;

    private void Start()
    {
        startState = switchState;
    }

    public override void Interaction()
    {
        base.Interaction();

        for(int i=0; i<myPuzzle.columns.Count; i++)
        {
            if(Vector3.Distance(transform.position, myPuzzle.columns[i].transform.position) <= myPuzzle.columnDistance)
            {
                myPuzzle.columns[i].switchState = !myPuzzle.columns[i].switchState;
            }
        }
    }

    private void Update()
    {
        if (switchState)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, onPos, Time.deltaTime * 10f);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, offPos, Time.deltaTime * 10f);
        }
    }

    public void ResetPos()
    {
        switchState = startState;

        if (switchState)
            transform.localPosition = onPos;
        else
            transform.localPosition = offPos;
    }
}
