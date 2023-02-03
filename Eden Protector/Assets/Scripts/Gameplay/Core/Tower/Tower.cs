using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : StateController
{
    public TowerType towerType;
    public Root standRoot;

    public override void DoAttack()
    {
        
    }

    /// <summary>
    /// 塔沿树根移动
    /// </summary>
    /// <param name="needTime"></param>
    /// <param name="nodePos"></param>
    /// <param name="rootCenterPos"></param>
    public void BeginMove(float needTime, Vector3 nodePos, Vector3 rootCenterPos)
    {
        aiActive = false;
        StartCoroutine(DoMove(needTime, nodePos, rootCenterPos));
    }

    IEnumerator DoMove(float needTime, Vector3 nodePos, Vector3 rootCenterPos)
    {
        float time = 0;
        float halfNeedTime = needTime / 2f;
        var startPosition = transform.position;
        while (time < halfNeedTime)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, nodePos, time / halfNeedTime);
            yield return null;
        }

        time = 0;
        while (time < halfNeedTime)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(nodePos, rootCenterPos, time / halfNeedTime);
            yield return null;
        }

        aiActive = true;
    }
}
