using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 树根类
/// </summary>
public class Root : MonoBehaviour
{
    public Node fromNode;
    public Node toNode;

    public int rootID;

    public void RootDestroy()
    {
        toNode.ownerRoot = null;
        toNode.ConnectToTree(false);
        fromNode.DisConnect(this);

        var tmpNode = toNode;
        // 每一个根的出度与入度只差不能超过 1
        while (tmpNode.OutDegree - tmpNode.InDegree > 1)
        {
            var needChangeRoot = tmpNode.connectRoots[0];
            // tmpNode.DisConnect(needChangeRoot);
            // tmpNode.ownerRoot = needChangeRoot;
            needChangeRoot.ChangeConnectDirection();
            
            tmpNode = needChangeRoot.fromNode;
        }

        tmpNode.ownerRoot = null;
        Tree.Instance.emptyNode.Add(tmpNode);
        
        Destroy(gameObject);
    }

    public void ConnectToNode(Node node)
    {
        toNode = node;
        var tmpNode = node;
        while (tmpNode.ownerRoot != null)
        {
            var needChangeRoot = tmpNode.ownerRoot;
            needChangeRoot.ChangeConnectDirection();
            tmpNode = needChangeRoot.toNode;
        }

        node.ConnectToTree(true);
    }
    
    /// <summary>
    /// 修改根的生长方向
    /// </summary>
    public void ChangeConnectDirection()
    {
        var tmp = fromNode;
        fromNode.DisConnect(this);
        fromNode = toNode;
        fromNode.Connect(this);
        toNode = tmp;
        toNode.ownerRoot = this;
    }
}
