using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Tower/Attack")]
public class TowerAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return EnemyDead(controller);
    }

    private bool EnemyDead(StateController controller)
    {
        if (controller.attackObject == null || controller.attackObject.GetComponent<Monster>().health.IsDead)
        {
            return true;
        }

        return false;
    }
}
