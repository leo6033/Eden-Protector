using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Monster/Move")]
public class MonsterMoveAction : Action
{
    public override void Act(StateController controller)
    {
        DoMove(controller);
    }

    private void DoMove(StateController controller)
    {
        var dirction = (Tree.Instance.transform.position - controller.transform.position).normalized;
        controller.transform.position += dirction * controller.attribute.moveSpeed * Time.deltaTime;
    }
}
