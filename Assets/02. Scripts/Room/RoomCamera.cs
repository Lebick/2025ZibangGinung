using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public void Update()
    {
        if (GamePlayManager.instance.currentRoom == null) return;

        Transform target = GamePlayManager.instance.currentRoom.myCamera;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 20f);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * 20f);
    }
}
