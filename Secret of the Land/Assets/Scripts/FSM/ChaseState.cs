using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private FSM machine;
    private Properties properties;
    public ChaseState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public void OnEnter()
    {
        machine.anim.Play("walk");
    }

    public void OnExit()
    {
        machine.agent.ResetPath();
    }

    public void OnUpdate()
    {
        machine.agent.SetDestination(properties.player.position);
        machine.FlipTo(properties.player.position);
        if (!properties.seePlayer)
        {
            machine.ToState(State.Patrol);
        }
    }
}
