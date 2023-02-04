using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : StateController
{
   public MonsterType monsterType;

   public override void DoAttack()
   {
      var health = attackObject.GetComponent<Health>();
      health.hp -= attribute.physicalAttack;
   }
}
