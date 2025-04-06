using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure : MonoBehaviour
{
    public bool isPress;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovingBarrel>())
            isPress = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovingBarrel>())
            isPress = false;
    }
}
