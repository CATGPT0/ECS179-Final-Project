using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    protected FSM machine;
    protected Properties properties;
    protected AnimatorStateInfo animInfo;

    public AttackState(FSM machine)
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

