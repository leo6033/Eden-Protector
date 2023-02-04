using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Tower/Attack")]
public class TowerAttackAction : Action
{
    public override void Act(StateController controller)
    {
        DoAttack(controller);
    }

    private void DoAttack(StateController controller)
    {
        if (controller.attackObject == null)
            return;
        
        if (controller.attackCoolDown <= 0)
        {
            controller.DoAttack();
            controller.attackCoolDown = controller.attribute.attackRate;
        }
    }
}
