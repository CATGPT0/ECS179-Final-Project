using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private FSM machine;
    private Properties properties;

    public PatrolState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public void OnEnter()
    {
        Debug.Log("PatrolState: OnEnter");
    }
    public void OnExit()
    {
        Debug.Log("PatrolState: OnExit");
    }
    public void OnUpdate()
    {
        Debug.Log("PatrolState: OnUpdate");
    }
}
