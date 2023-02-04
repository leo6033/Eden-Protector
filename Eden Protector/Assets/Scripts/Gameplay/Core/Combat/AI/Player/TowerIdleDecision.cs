using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Tower/Idle")]
public class TowerIdleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return FindEnemy(controller);
    }

    public bool FindEnemy(StateController controller)
    {
        var enemies = Physics.OverlapSphere(controller.transform.position, controller.attribute.visionRange, 1 << 10);
        foreach (var enemy in enemies)
        {
            controller.attackObject = enemy;
            return true;
        }

        return false;
    }
}
