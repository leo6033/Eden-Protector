using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

/// <summary>
/// 树根节点
/// </summary>
[Serializable]
public class Node : MonoBehaviour
{
    /// <summary>
    /// 以该节点为起点的根
    /// </summary>
    public List<Root> connectRoots = new List<Root>();

    /// <summary>
    /// 拥有该节点的根
    /// </summary>
    public Root ownerRoot;

    public bool isRoot;

    public int InDegree => ownerRoot == null ? 0 : 1;
    public int OutDegree => connectRoots.Count;

    public bool isConnectToTree;

    private void Update()
    {
        if (!isRoot && InDegree == 0 && OutDegree == 0)
        {
            Destroy(gameObject);
        }
    }

    public bool CreateRoot(Vector3 position)
    {
        if (connectRoots.Count >= 2)
        {
            Debug.Log("Create root fail, connect root number is up to 2");
            return false;
        }

        // 检查相交
        
        var newNodeGameObj = Instantiate(Tree.Instance.nodePrefab, Tree.Instance.rootGroup);
        newNodeGameObj.transform.position = position;
        var newNode = newNodeGameObj.GetComponent<Node>();
        var newRootGameObj = Instantiate(Tree.Instance.rootPrefab, Tree.Instance.rootGroup);
        var newRoot = newRootGameObj.GetComponent<Root>();
        newNode.ownerRoot = newRoot;
        newNode.isConnectToTree = true;
        
        // set position and rotation
        newRoot.transform.position = transform.position;
        newRoot.transform.forward = position - transform.position;
        newRoot.transform.localScale = new Vector3(1, 1, Vector3.Distance(position, transform.position));
        newRoot.toNode = newNode;

        // 超过一个根段连到该节点，则算作新增一条根
        if (connectRoots.Count > 0)
        {
            Tree.Instance.AddNewRoot();
        }
        newRoot.rootID = Tree.Instance.rootNumber;
        Connect(newRoot);

        return true;
    }

    public void Connect(Root root)
    {
        connectRoots.Add(root);
        root.fromNode = this;
    }

    public void DisConnect(Root root)
    {
        connectRoots.Remove(root);
        root.fromNode = null;
    }

    public void OnPointerClick(BaseEventData args)
    {
        Debug.Log("OnPointerClick");
    }

    /// <summary>
    /// 与断开连接的节点建立连接
    /// </summary>
    /// <param name="node"></param>
    public void ConnectToNode(Node node)
    {
        var newRootGameObj = Instantiate(Tree.Instance.rootPrefab, Tree.Instance.rootGroup);
        newRootGameObj.transform.position = transform.position;
        newRootGameObj.transform.forward = node.transform.position - transform.position;
        newRootGameObj.transform.localScale = new Vector3(1, 1, Vector3.Distance(transform.position, node.gameObject.transform.position));
        var newRoot = newRootGameObj.GetComponentInParent<Root>();
        newRoot.ConnectToNode(node);
        node.ownerRoot = newRoot;
        
        Connect(newRoot);
    }

    /// <summary>
    /// 断开或连上与树之间的连接
    /// </summary>
    /// <param name="value"></param>
    public void ConnectToTree(bool value)
    {
        isConnectToTree = value;
        foreach (var root in connectRoots)
        {
            root.toNode.ConnectToTree(value);
        }
    }
}