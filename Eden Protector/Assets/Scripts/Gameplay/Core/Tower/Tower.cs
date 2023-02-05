using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : StateController
{
    public TowerType towerType;
    public Root standRoot;
    public Transform bulletPos;

    public GameObject mushRoomBulletPrefab;
    public GameObject sunFlowerBeanPrefab;

    public override void DoAttack()
    {
        animator.SetTrigger("Attack");
        if (towerType == TowerType.MushRoom)
        {
            var mushRoomBulletGameObj = Instantiate(mushRoomBulletPrefab, bulletPos.position, bulletPos.rotation);
            var mushRoomBullet = mushRoomBulletGameObj.GetComponent<MushRoomBullet>();
            mushRoomBullet.harm = attribute.physicalAttack;
            mushRoomBullet.target = attackObject.transform;
            mushRoomBullet.Shoot();
        }
        else if (towerType == TowerType.SunFlower)
        {
            var sunFlowerBeanGameObj = Instantiate(sunFlowerBeanPrefab, bulletPos.position, bulletPos.rotation);
            var sunFlowerBean = sunFlowerBeanGameObj.GetComponent<SunFlowerBean>();
            sunFlowerBean.harm = attribute.physicalAttack;
            sunFlowerBean.target = attackObject.transform;
            sunFlowerBean.Shoot();
        }
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

    public void Remove()
    {
        Destroy(gameObject);
    }
}
