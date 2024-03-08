using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactState : IState
{
    protected FSM machine;
    protected Properties properties;

    public ReactState(FSM machine)
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
