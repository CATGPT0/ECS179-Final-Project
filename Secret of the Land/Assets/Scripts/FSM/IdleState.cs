using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FSM machine;
    private Properties properties;

    public IdleState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public void OnEnter()
    {
        Debug.Log("IdleState: OnEnter");
        machine.anim.Play("idle");
    }
    public void OnExit()
    {
        Debug.Log("IdleState: OnExit");
    }
    public void OnUpdate()
    {
        Debug.Log("IdleState: OnUpdate");
    }
}
