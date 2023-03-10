using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Monster/Battle")]
public class MonsterBattleAction : Action
{
    
    public override void Act(StateController controller)
    {
        Battle(controller);
    }

    private void Battle(StateController controller)
    {
        if (controller.attackObject == null)
            return;
        
        Root root = controller.attackObject.GetComponent<Root>();
        if (root != null && root.tower != null)
            return;
        
        var position = controller.attackObject.ClosestPoint(controller.transform.position);
        if (Vector3.Distance(position, controller.transform.position) >
            controller.attribute.attackRange)
        {
            var dir = controller.attackObject.ClosestPoint(controller.transform.position) -
                      controller.transform.position;
            
            controller.spriteRenderer.flipX = dir.x < 0;

            controller.transform.position += dir.normalized * controller.attribute.moveSpeed * Time.deltaTime;
            controller.animator.SetBool("Move", true);
        }
        else if(controller.attackCoolDown <= 0)
        {
            controller.animator.SetBool("Move", false);
            controller.DoAttack();
            controller.attackCoolDown = controller.attribute.attackRate;
        }
    }
}
