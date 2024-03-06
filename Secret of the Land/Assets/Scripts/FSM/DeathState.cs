using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    private FSM machine;
    private Properties properties;

    public DeathState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public void OnEnter()
    {
        Debug.Log("DeathState: OnEnter");
    }
    public void OnExit()
    {
        Debug.Log("DeathState: OnExit");
    }
    public void OnUpdate()
    {
        Debug.Log("DeathState: OnUpdate");
    }
}

