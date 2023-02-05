using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootOperationPanel : MonoBehaviour
{
    public Text sunflowerCost;
    public Text mushroomCost;
    
    private void Update()
    {
        if (gameObject.activeInHierarchy && GamePlayManager.Instance.currentState != InputState.SelectRoot)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            sunflowerCost.text = Tree.Instance.sunflowerNeedResourceNumber.ToString();
            mushroomCost.text = Tree.Instance.mushroomNeedResourceNumber.ToString();
            Time.timeScale = 0;
        }
    }

    public void OnDestroyRootClick()
    {
        GamePlayManager.Instance.updateClick = false;
        GamePlayManager.Instance.DestroyRoot();
        GamePlayManager.Instance.Unselect();
    }

    public void OnMushroomBuild()
    {
        if (GamePlayManager.Instance.resourceNumber < Tree.Instance.mushroomNeedResourceNumber)
        {
            UIManager.Instance.ShowUIMessage("建造蘑菇失败，资源不足");
            return;
        }
        GamePlayManager.Instance.updateClick = false;
        GamePlayManager.Instance.resourceNumber -= Tree.Instance.mushroomNeedResourceNumber;
        GamePlayManager.Instance.BuildTower(Tree.Instance.mushRoomPrefab);
        GamePlayManager.Instance.Unselect();
    }


    public void OnSunflowerBuild()
    {
        if (GamePlayManager.Instance.resourceNumber < Tree.Instance.sunflowerNeedResourceNumber)
        {
            UIManager.Instance.ShowUIMessage("建造向日葵失败，资源不足");
            return;
        }
        GamePlayManager.Instance.updateClick = false;
        GamePlayManager.Instance.resourceNumber -= Tree.Instance.sunflowerNeedResourceNumber;
        GamePlayManager.Instance.BuildTower(Tree.Instance.sunFlowerPrefab);
        GamePlayManager.Instance.Unselect();
    }
}
