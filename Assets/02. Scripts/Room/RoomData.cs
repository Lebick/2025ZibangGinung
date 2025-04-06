using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public Animator myLight;
    public Transform myCamera;

    public Puzzle myPuzzle;

    private void Update()
    {
        myLight.SetBool("isOn", GamePlayManager.instance.currentRoom == this);
    }
}
