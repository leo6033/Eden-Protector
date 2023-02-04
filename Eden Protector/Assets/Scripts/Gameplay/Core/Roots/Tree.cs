using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private static Tree _instance;

    public static Tree Instance => _instance;

    public Node node;
    public GameObject rootPrefab;
    public GameObject nodePrefab;
    public GameObject sunFlowerPrefab; 

    public Transform rootGroup;

    /// <summary>
    /// 树拥有的根的数量
    /// </summary>
    public int rootNumber;

    public List<Node> emptyNode = new List<Node>();
    public List<Root> allRoot = new List<Root>();

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        rootNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewRoot()
    {
        rootNumber += 1;
    }

    public void RefreshRootNumber()
    {
        rootNumber = 0;
        for (int i = 0; i < node.OutDegree; i ++)
        {
            if(i < node.OutDegree - 1)
                AddNewRoot();
            var root = node.connectRoots[i];
            root.rootID = rootNumber;
            root.RefreshRootID();
        }
        Debug.Log($"Refresh root number, {rootNumber}");
    }
}
