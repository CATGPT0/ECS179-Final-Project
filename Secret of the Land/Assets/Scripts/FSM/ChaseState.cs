using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    protected FSM machine;
    protected Properties properties;
    protected float chaseSpeed = 3.5f;
    protected float originalSpeed;
    public ChaseState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public virtual void OnEnter()
    {
        
    }

    public virtual void OnExit()
    {
        
    }

    public virtual void OnUpdate()
    {
        
    }
}