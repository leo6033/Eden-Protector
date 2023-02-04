using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int resourceNum;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"trigger collider enter {other.gameObject}");
        if (other.CompareTag("Root"))
        {
            GamePlayManager.Instance.resourceNumber += resourceNum;
            Destroy(gameObject);
        }
    }
}
