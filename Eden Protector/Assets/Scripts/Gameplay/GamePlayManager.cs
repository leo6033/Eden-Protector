using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    private static GamePlayManager _instance;

    public static GamePlayManager Instance => _instance;
    
    public InputState currentState = InputState.None;

    public bool updateClick = true;

    private Node _currentSelectNode;
    private Root _currentSelectRoot;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    #region Player Input

    public void SelectRoot(Root root)
    {
        _currentSelectRoot = root;
        currentState = InputState.SelectRoot;
        UIManager.Instance.rootOperationPanel.gameObject.SetActive(true);
        Debug.Log("Enter select root mode");
    }

    public void SelectNode(Node node)
    {
        _currentSelectNode = node;
        currentState = InputState.SelectNode;
        Debug.Log("Enter select node mode");
    }

    public void Unselect()
    {
        _currentSelectNode = null;
        _currentSelectRoot = null;
        currentState = InputState.None;
    }

    public bool CreateRoot(Vector3 position)
    {
        position.y = 0.01f;
        Debug.Log($"create root, node position {position}");
        var value =  _currentSelectNode.CreateRoot(position);
        Tree.Instance.RefreshRootNumber();
        return value;
    }

    public void ConnectToNode(Node node)
    {
        _currentSelectNode.ConnectToNode(node);
        Tree.Instance.RefreshRootNumber();
        Unselect();
    }

    public void DestroyRoot()
    {
        _currentSelectRoot.RootDestroy();
        Tree.Instance.RefreshRootNumber();
    }

    public void BuildTower()
    {
        _currentSelectRoot.BuildTower(Tree.Instance.sunFlowerPrefab);
    }

    #endregion

}
