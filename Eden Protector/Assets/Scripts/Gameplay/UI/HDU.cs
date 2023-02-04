using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDU : MonoBehaviour
{
    public Text resourceText;

    // Update is called once per frame
    void Update()
    {
        resourceText.text = GamePlayManager.Instance.resourceNumber.ToString();
    }
}
