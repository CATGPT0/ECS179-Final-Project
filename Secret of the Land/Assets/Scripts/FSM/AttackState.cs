using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private FSM machine;
    private Properties properties;

    public AttackState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public void OnEnter()
    {
        Debug.Log("AttackState: OnEnter");
    }
    public void OnExit()
    {
        Debug.Log("AttackState: OnExit");
    }
    public void OnUpdate()
    {
        Debug.Log("AttackState: OnUpdate");
    }
}

