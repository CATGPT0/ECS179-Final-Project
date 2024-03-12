using System.Collections;
using System.Collections.Generic;
using CardBattle;
using UnityEngine;

public class IdleState : IState
{
    protected FSM machine;
    protected Properties properties;
    protected float timer = 0;
    protected float maxIdleTime = 3f;

    public IdleState(FSM machine)
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
