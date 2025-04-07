using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomJoin : MonoBehaviour
{
    public UnityEvent joinEvent;

    private bool isJoin;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            GamePlayManager.instance.currentRoom = transform.parent.GetComponent<RoomData>();

            if (!isJoin)
            {
                isJoin = true;
                joinEvent?.Invoke();
            }
        }
    }
}
