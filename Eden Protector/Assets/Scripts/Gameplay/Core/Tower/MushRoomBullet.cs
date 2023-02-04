using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomBullet : MonoBehaviour
{
    public float flyTime;
    public float flySpeed;
    public float downTime;
    public Transform target;

    public Animator animator;
    public float harm;

    private float _time;

    public void Shoot()
    {
        StartCoroutine(DoShoot());
    }
    
    IEnumerator DoShoot()
    {
        _time = 0;
        while (_time < flyTime)
        {
            _time += Time.deltaTime;
            transform.position += flySpeed * Vector3.forward * Time.deltaTime;
            yield return null;
        }

        _time = 0;
        var position = transform.position;
        position.x = target.position.x;
        animator.SetBool("Down", true);

        while (_time < downTime)
        {
            transform.position = Vector3.Lerp(position, target.position, _time / downTime);
            _time += Time.deltaTime;
            yield return null;
        }

        Health health = target.GetComponent<Health>();
        health.hp -= harm;
        
        Destroy(gameObject);
    }
}
