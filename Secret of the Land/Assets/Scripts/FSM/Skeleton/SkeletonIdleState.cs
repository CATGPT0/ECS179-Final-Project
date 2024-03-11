using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : IdleState
{
    protected new SkeletonFSM machine;
    protected new SkeletonProperties properties;

    public SkeletonIdleState(SkeletonFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = this.machine.properties;
        this.soundClips = machine.soundClips;
    }

    public override void OnEnter()
    {
        maxIdleTime = Random.Range(3f, 5f);
        machine.agent.ResetPath();
        machine.anim.Play("idle");
    }

    public override void OnExit()
    {
        machine.agent.ResetPath();
        timer = 0;
    }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer > maxIdleTime)
        {
            machine.ToState(State.Patrol);
        }

        if (properties.SeePlayer)
        {
            machine.ToState(State.React);
        }
        // if (properties.Health <= 0)
        // {
        //     machine.ToState(State.Death);
        // }
    }
}
