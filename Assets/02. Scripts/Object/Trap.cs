using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private float coolTime;

    private void FixedUpdate()
    {
        if(coolTime > 0)
            coolTime -= Time.fixedDeltaTime;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player) && coolTime <= 0)
        {
            player.GetDamage(10f);
            GetComponent<Animator>().SetTrigger("Trigger");
            coolTime = 1f;
        }
    }
}
