using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transifition[] transitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

   private void CheckTransitions(StateController controller)
   {
       foreach (var transition in transitions)
       {
           bool decisionSucceeded = transition.decision.Decide(controller);

           State transitionState = decisionSucceeded ? transition.trueState : transition.falseState;

           if(controller.TransitionToState(transitionState))
               break;
       }
   }
}
