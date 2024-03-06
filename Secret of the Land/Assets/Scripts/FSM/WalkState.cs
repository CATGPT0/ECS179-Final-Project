using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    private FSM machine;
    private Properties properties;

    public WalkState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public void OnEnter()
    {
        Debug.Log("WalkState: OnEnter");
    }
    public void OnExit()
    {
        Debug.Log("WalkState: OnExit");
    }
    public void OnUpdate()
    {
        Debug.Log("WalkState: OnUpdate");
    }
}

