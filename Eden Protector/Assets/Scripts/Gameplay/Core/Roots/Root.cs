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
    public Tower tower;

    private void Awake()
    {
        Health health = GetComponent<Health>();
        health.deadCallback += RootDestroy;
    }

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
            tmpNode.ownerRoot = null;
        }

        // tmpNode.ownerRoot = null;
        Tree.Instance.emptyNode.Add(tmpNode);
        Tree.Instance.allRoot.Remove(this);
        Debug.Log($"destroy root, empty node count {Tree.Instance.emptyNode.Count}");
        Destroy(gameObject);
    }

    public void ConnectToNode(Node node)
    {
        toNode = node;
        var tmpNode = node;
        while (tmpNode.ownerRoot != null && tmpNode.connectRoots.Count != 0)
        {
            var needChangeRoot = tmpNode.ownerRoot;
            needChangeRoot.ChangeConnectDirection();
            tmpNode = needChangeRoot.toNode;
        }

        Tree.Instance.emptyNode.Remove(tmpNode);
        Debug.Log($"connect to node, empty node count {Tree.Instance.emptyNode.Count}");

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

    /// <summary>
    /// 刷新根节点编号
    /// </summary>
    public void RefreshRootID()
    {
        for (int i = 0; i < toNode.OutDegree; i ++)
        {
            var root = toNode.connectRoots[i];
            root.rootID = Tree.Instance.rootNumber;
            root.RefreshRootID();
            if(i < toNode.OutDegree - 1)
                Tree.Instance.AddNewRoot();
        }
    }
    
    public bool Cross(Vector3 a, Vector3 b)
    {
        Vector3 c = fromNode.transform.position;
        Vector3 d = toNode.transform.position;

        if (Vector3.Distance(a, c) <= 0.0001 || Vector3.Distance(a, d) <= 0.0001 || Vector3.Distance(b, c) <= 0.0001 || Vector3.Distance(b, d) <= 0.0001)
            return false;

        Vector2 ac = new Vector2(c.x - a.x, c.z - a.z);
        Vector2 ad = new Vector2(d.x - a.x, d.z - a.z);
        Vector2 bc = new Vector2(c.x - b.x, c.z - b.z);
        Vector2 bd = new Vector2(d.x - b.x, d.z - b.z);

        return (Product(ac, ad) * Product(bc, bd) <= 0.0001) && (Product(ac, bc) * Product(ad, bd) <= 0.0001);
    }

    private float Product(Vector2 a, Vector2 b)
    {
        return a.x * b.y - a.y * b.x;
    }

    public void BuildTower(GameObject towerPrefab)
    {
        var towerGameObj = Instantiate(towerPrefab, Tree.Instance.rootGroup);
        towerGameObj.transform.position = (fromNode.transform.position + toNode.transform.position) / 2;

        tower = towerGameObj.GetComponent<Tower>();
        // tower.transform.right = -Camera.main.transform.forward;
    }

    /// <summary>
    /// 根生长
    /// </summary>
    public void Grow()
    {
        var root1 = fromNode.ownerRoot;
        var root2 = this;
        while (root1 != null && root1.rootID == root2.rootID)
        {
            if (root1.tower != null)
            {
                root2.tower = root1.tower;
                root1.tower = null;
                root2.tower.BeginMove(2f, root2.fromNode.transform.position, (root2.fromNode.transform.position + root2.toNode.transform.position) / 2);
            }

            root2 = root1;
            root1 = root1.fromNode.ownerRoot;
        }
    }
}
