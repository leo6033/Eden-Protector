using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour
{
    private float _time = 0f;
    
    /// <summary>
    /// 消息显示时间
    /// </summary>
    public float duration = 3f;

    public Text text;
    public AudioSource audioSource;
    

    // Update is called once per frame
    void Update()
    {
        if (_time < duration)
        {
            _time += Time.deltaTime;
            return;
        }
        
        gameObject.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        _time = 0f;
        text.text = message;
        gameObject.SetActive(true);
        audioSource.Play();
    }
}
