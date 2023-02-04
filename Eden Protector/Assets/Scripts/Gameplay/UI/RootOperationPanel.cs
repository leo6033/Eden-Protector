using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootOperationPanel : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.activeInHierarchy && GamePlayManager.Instance.currentState != InputState.SelectRoot)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnDestroyRootClick()
    {
        GamePlayManager.Instance.updateClick = false;
        GamePlayManager.Instance.DestroyRoot();
        GamePlayManager.Instance.Unselect();
    }

    public void OnBuildClick()
    {
        GamePlayManager.Instance.updateClick = false;
        GamePlayManager.Instance.BuildTower();
    }
}
