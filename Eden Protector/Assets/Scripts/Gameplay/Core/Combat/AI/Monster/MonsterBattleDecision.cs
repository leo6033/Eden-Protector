using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Monster/Battle")]
public class MonsterBattleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return Battle(controller);
    }

    private bool Battle(StateController controller)
    {
        if (controller.attackObject == null)
            return true;
        else
        {
            if (controller.CompareTag("Root"))
            {
                Root root = controller.GetComponent<Root>();
                if (root.tower != null)
                    return true;
            }
            
            Health health = controller.GetComponent<Health>();
            if (health.IsDead)
                return true;
        }
        return false;
    }
}
