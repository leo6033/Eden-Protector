using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;
    private float maxHp;

    public bool IsDead => hp <= 0;

    private bool _dead = false;

    public delegate void DeadCallbackDelegate();

    public DeadCallbackDelegate deadCallback;

    public HpBar hpBar;

    private void Start()
    {
        maxHp = hp;
    }

    private void Update()
    {
        if (!_dead && IsDead)
        {
            Dead();
        }

        if (hpBar != null)
        {
            hpBar.SetValue(hp / maxHp);
        }
    }


    private void Dead()
    {
        _dead = true;
        // play animation
        deadCallback?.Invoke();
        
        Destroy(gameObject);
    }
}
