using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlayManager : MonoBehaviour
{

    private static GamePlayManager _instance;

    public static GamePlayManager Instance => _instance;
    
    public InputState currentState = InputState.None;

    public bool updateClick = true;

    public Transform monsterGroup;

    private Node _currentSelectNode;
    private Root _currentSelectRoot;

    public static float DurationTime;
    private Queue<OneWaveMonsters> monsterCreateInfos;
    public List<OneWaveMonsters> monsters;

    public int resourceNumber = 0;

    public int currentMonsterNum;
    public Tree tree;
    private bool win;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
        }

        DurationTime = 0;
        currentMonsterNum = 0;
        monsterCreateInfos = new Queue<OneWaveMonsters>(monsters);

        StartCoroutine(OnGamePlay());
    }

    IEnumerator OnGamePlay()
    {
        while (monsterCreateInfos.Count > 0 && !tree.health.IsDead)
        {
            if (currentMonsterNum == 0)
            {
                DurationTime += Time.deltaTime;
                if (monsterCreateInfos.Peek().createTime <= DurationTime)
                {
                    var monsterCreateInfo = monsterCreateInfos.Dequeue();
                    StartCoroutine(CreateMonster(monsterCreateInfo));
                }
            }
            else
            {
                DurationTime = 0;
            }
            
            yield return null;
        }

        yield return OnGameEnd();
    }

    IEnumerator OnGameEnd()
    {
        while (currentMonsterNum > 0 && !Tree.Instance.health.IsDead)
        {
            yield return null;
        }

        win = currentMonsterNum == 0;
        string meassge = win ? "胜利" : "游戏失败";
        UIManager.Instance.ShowUIMessage(meassge);
    }

    IEnumerator CreateMonster(OneWaveMonsters monsterCreateInfo)
    {
        foreach (var levelMonster in monsterCreateInfo.LevelMonsters)
        {
            for (int i = 0; i < levelMonster.number; i++)
            {
                Vector3 randomOffset = Random.insideUnitSphere * 2f;
                var monster = Instantiate(levelMonster.monsterPrefab, monsterGroup);
                Health health = monster.GetComponent<Health>();
                health.deadCallback += () => { currentMonsterNum -= 1; };
                currentMonsterNum += 1;
                yield return null;
            }
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
        node.Select(true);
        currentState = InputState.SelectNode;
        Debug.Log("Enter select node mode");
    }

    public void Unselect()
    {
        if(_currentSelectNode != null)
            _currentSelectNode.Select(false);
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

    public void BuildTower(GameObject prefab)
    {
        _currentSelectRoot.BuildTower(prefab);
    }

    #endregion

}
