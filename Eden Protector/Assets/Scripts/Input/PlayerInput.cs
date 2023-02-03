﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            LeftMouseDown();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            RightMouseDown();
        }
    }

    private void LeftMouseDown()
    {
        if (!GamePlayManager.Instance.updateClick)
        {
            GamePlayManager.Instance.updateClick = true;
            return;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (GamePlayManager.Instance.currentState == InputState.None)
        {
            if (Physics.Raycast(ray, out var hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag("Root"))
                {
                    var root = obj.GetComponentInParent<Root>();
                    GamePlayManager.Instance.SelectRoot(root);
                    Debug.Log("hit root");
                }
                else if (obj.CompareTag("Node"))
                {
                    var node = obj.GetComponentInParent<Node>();
                    if (!node.isConnectToTree)
                    {
                        UIManager.Instance.ShowUIMessage("节点未连接到树");
                        return;
                    }
                    GamePlayManager.Instance.SelectNode(node);
                    Debug.Log("hit node");
                }
            }
        }
        else if (GamePlayManager.Instance.currentState == InputState.SelectNode)
        {
            if(Physics.Raycast(ray, out var hit, 1000f, 1 << 8))
            {
                if (hit.collider.CompareTag("Node"))
                {
                    var node = hit.collider.GetComponentInParent<Node>();
                    if (node.isConnectToTree)
                    {
                        UIManager.Instance.ShowUIMessage("无法连接到未断开的节点");
                        return;
                    }
                    else if (node.InDegree == 0 || node.OutDegree == 0)
                    {
                        GamePlayManager.Instance.ConnectToNode(node);
                    }
                    else
                    {
                        UIManager.Instance.ShowUIMessage("只能连接到边缘节点");
                        return;
                    }
                }
                else if (GamePlayManager.Instance.CreateRoot(hit.point))
                {
                    GamePlayManager.Instance.Unselect();
                }
            }
        }
    }

    private void RightMouseDown()
    {
        GamePlayManager.Instance.Unselect();
    }
}