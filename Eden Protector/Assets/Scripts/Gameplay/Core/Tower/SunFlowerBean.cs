using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerBean : MonoBehaviour
{
    public float showTime;
    public float outTime;
    public Transform target;
    public float harm;

    public SpriteRenderer spriteRenderer;
    private float _time;
    private Color originColor;
    private Color color;

    private Coroutine _coroutine;
    
    public void Shoot()
    {
        transform.localScale = new Vector3(Vector3.Distance(transform.position, target.position), 1, 1);
        transform.right = (target.position - transform.position);

        color = spriteRenderer.color;
        originColor = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        _coroutine =  StartCoroutine(DoShoot());
    }

    private void Update()
    {
        if (target == null)
        {
            StopCoroutine(_coroutine);
            Destroy(gameObject);
        }
    }

    IEnumerator DoShoot()
    {
        while (_time < showTime)
        {
            spriteRenderer.color = Vector4.Lerp(color, originColor, _time / showTime);
            _time += Time.deltaTime;
            yield return null;
        }

        Health health = target.GetComponent<Health>();
        health.hp -= harm;

        _time = 0;
        while (_time < outTime)
        {
            spriteRenderer.color = Vector4.Lerp(originColor, color, _time / outTime);
            _time += Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
