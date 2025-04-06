using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomJoin : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            GamePlayManager.instance.currentRoom = transform.parent.GetComponent<RoomData>();
        }
    }
}
