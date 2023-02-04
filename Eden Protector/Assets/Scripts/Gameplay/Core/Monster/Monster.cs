using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : StateController
{
   public MonsterType monsterType;
   public int rewardResourceNumber;

   private void Awake()
   {
      health.deadCallback += () => { GamePlayManager.Instance.resourceNumber += rewardResourceNumber; };
   }

   public override void DoAttack()
   {
      var health = attackObject.GetComponent<Health>();
      health.hp -= attribute.physicalAttack;
   }
}
