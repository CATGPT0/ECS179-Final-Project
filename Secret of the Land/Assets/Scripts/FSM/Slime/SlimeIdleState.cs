using System.Collections;
using System.Collections.Generic;
using Schema.Builtin.Nodes;
using UnityEngine;

public class SlimeIdleState : IdleState
{
    protected new SlimeFSM machine;
    protected new SlimeProperties properties;

    public SlimeIdleState(SlimeFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = this.machine.properties;
    }

    public override void OnEnter()
    {
        maxIdleTime = Random.Range(3f, 5f);
        machine.anim.Play("idle");
    }

    public override void OnExit()
    {
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
    }
}
