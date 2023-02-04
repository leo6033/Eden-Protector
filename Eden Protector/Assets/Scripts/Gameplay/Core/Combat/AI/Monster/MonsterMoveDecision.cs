using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Monster/Move")]
public class MonsterMoveDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        return FindEnemy(controller);
    }

    private bool FindEnemy(StateController controller)
    {
        var enemies = Physics.OverlapSphere(controller.transform.position, controller.attribute.visionRange, 1 << 9);
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Root"))
            {
                var root = enemy.GetComponent<Root>();
                if (root.tower == null)
                {
                    controller.attackObject = enemy;
                    return true;
                }
            }
            else if (enemy.CompareTag("Tree"))
            {
                controller.attackObject = enemy;
                return true;
            }
        }

        return false;
    }
}
