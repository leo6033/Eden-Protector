using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomBullet : MonoBehaviour
{
    public float flyTime;
    public float flySpeed;
    public float downTime;
    public float outTime;
    public Transform target;

    public Animator animator;
    public float harm;
    public AudioClip boom;

    private float _time;

    private Coroutine _coroutine;

    public void Shoot()
    {
        _coroutine = StartCoroutine(DoShoot());
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

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = boom;
        audioSource.Play();

        yield return CoDestroy();
    }

    IEnumerator CoDestroy()
    {
        _time = 0;
        animator.SetBool("Boom", true);
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        var color = spriteRenderer.color;
        var beginColor = color;
        color.a = 0;
        while (_time < outTime)
        {
            _time += Time.deltaTime;
            spriteRenderer.color = Vector4.Lerp(beginColor, color, _time / outTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
