using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : Singleton<CheatManager>
{
    public void Update()
    {
        if(GamePlayManager.instance == null)
        {
            if (Input.GetKeyDown(KeyCode.F2)) Cheat2();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F1)) Cheat1();
            if (Input.GetKeyDown(KeyCode.F3)) Cheat3();
            if (Input.GetKeyDown(KeyCode.F4)) Cheat4();
            if (Input.GetKeyDown(KeyCode.F5)) Cheat5();
        }
    }

    private void Cheat1()
    {

    }

    private void Cheat2()
    {

    }

    private void Cheat3()
    {

    }

    private void Cheat4()
    {

    }

    private void Cheat5()
    {

    }
}
