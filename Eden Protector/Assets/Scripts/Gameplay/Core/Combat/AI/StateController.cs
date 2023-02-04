using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;
    public Attribute attribute;

    public Health health;

    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Vector3 targetPoint;
    [HideInInspector] public int nextPoint;
    [HideInInspector] public Collider attackObject;

    [HideInInspector] public float attackCoolDown;
    
    protected bool aiActive = true;

    public bool TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            //Debug.Log(transform.gameObject.name + " start" + nextState);
            currentState = nextState;
            OnExitState();
            return true;
        }
        return false;
    }

    public bool CheckifCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    public void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    void Update()
    {
        if (!aiActive || (health != null && health.IsDead))
            return;
        currentState.UpdateState(this);

        if (attackCoolDown > 0)
            attackCoolDown -= Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        
    }

    #region common action

    public virtual void DoAttack() { }
    
    

    #endregion
}
