using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int resourceNum;
    public AudioClip getResource;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"trigger collider enter {other.gameObject}");
        if (other.CompareTag("Root"))
        {
            GamePlayManager.Instance.resourceNumber += resourceNum;
            AudioManager.Instance.PlayAudio(getResource);
            Destroy(gameObject);
        }
    }
}
