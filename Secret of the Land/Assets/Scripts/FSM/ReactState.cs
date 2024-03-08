using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReactState : IState
{
    protected FSM machine;
    protected Properties properties;
    protected AnimatorStateInfo animInfo;
    protected float originalSpeed;

    public ReactState(FSM machine)
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
