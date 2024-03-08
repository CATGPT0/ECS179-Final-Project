using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChaseState : ChaseState
{
    protected new SkeletonFSM machine;
    protected new SkeletonProperties properties;
    public SkeletonChaseState(SkeletonFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }

    public override void OnEnter()
    {
        originalSpeed = machine.agent.speed;
        machine.agent.speed = chaseSpeed;
        machine.agent.ResetPath();
        machine.anim.Play("walk");
    }

    public override void OnExit()
    {
        machine.agent.ResetPath();
        machine.agent.speed = originalSpeed;
    }

    public override void OnUpdate()
    {
        machine.agent.SetDestination(properties.player.position);
        machine.FlipTo(properties.player.position);
        if (!properties.seePlayer)
        {
            machine.ToState(State.Idle);
        }
    }
}

