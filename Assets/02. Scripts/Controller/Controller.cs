using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public bool isDeath;

    public float maxHP;
    public float hp
    {
        get { return _hp; }
        set { _hp = Mathf.Min(value, maxHP); }
    }

    private float _hp;

    public virtual void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        hp = maxHP;
    }

    public virtual void GetDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            isDeath = true;
            Death();
        }
    }

    public virtual void Death()
    {

    }
}
