using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一组怪物生成信息
/// </summary>
[Serializable]
public class LevelMonster
{
    public GameObject monsterPrefab;
    public Transform monsterCreatePosition;
    public int number;
}


[Serializable]
public class OneWaveMonsters
{
    public List<LevelMonster> LevelMonsters;
    public float createTime;
}