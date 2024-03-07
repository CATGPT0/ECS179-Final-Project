using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private FSM machine;
    private Properties properties;
    private float chaseSpeed = 3.5f;
    private float originalSpeed;
    public ChaseState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }
    public void OnEnter()
    {
        originalSpeed = machine.agent.speed;
        machine.agent.speed = chaseSpeed;
        machine.agent.ResetPath();
        machine.anim.Play("walk");
    }

    public void OnExit()
    {
        machine.agent.ResetPath();
        machine.agent.speed = originalSpeed;
    }

    public void OnUpdate()
    {
        machine.agent.SetDestination(properties.player.position);
        machine.FlipTo(properties.player.position);
        if (!properties.seePlayer)
        {
            machine.ToState(State.Idle);
        }
    }
}
