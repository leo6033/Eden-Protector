using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private bool isTranslate = false;
    public Transform cameraTrans;
    public bool stop = false;

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
        
        if (Input.GetMouseButtonDown(2))
        {
            isTranslate = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            isTranslate = false;
        }
        if (isTranslate)
        {
            var mouse_x = Input.GetAxis("Mouse X");
            var mouse_y = Input.GetAxis("Mouse Y");
            Vector3 right = mouse_x * cameraTrans.right;
            Vector3 up = mouse_y * cameraTrans.up;
            cameraTrans.Translate(-right - up, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stop = !stop;
            if (stop)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = GamePlayManager.Instance.currentState == InputState.SelectRoot ? 0 : 1;
            }
            
            UIManager.Instance.stopPanel.SetActive(stop);

        }

        float  mouseCenter = Input.GetAxis("Mouse ScrollWheel");
        var position = cameraTrans.position;
        var value = position.y - mouseCenter * 40 * Time.deltaTime;
        value = Math.Max(7, value);
        value = Math.Min(value, 10);
        position.y = value;
        cameraTrans.position = position;
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

                    if (!root.complete)
                    {
                        UIManager.Instance.ShowUIMessage("当前根正在延伸");
                        return;
                    }

                    if (!root.toNode.isConnectToTree)
                    {
                        UIManager.Instance.ShowUIMessage("选择根未与树连通");
                        return;
                    }
                    
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
                    else if (node.connectRoots.Count == 2) // TODO: 资源判断
                    {
                        UIManager.Instance.ShowUIMessage("该节点已有2根连接");
                        return;
                    }
                    else if (Tree.Instance.rootCost > GamePlayManager.Instance.resourceNumber)
                    {
                        UIManager.Instance.ShowUIMessage("资源不足，无法延伸根");
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
