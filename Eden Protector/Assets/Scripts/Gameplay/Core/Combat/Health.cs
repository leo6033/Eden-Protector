using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;

    public bool IsDead => hp <= 0;

    private bool _dead = false;

    public delegate void DeadCallbackDelegate();

    public DeadCallbackDelegate deadCallback;

    private void Update()
    {
        if (!_dead && IsDead)
        {
            Dead();
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
