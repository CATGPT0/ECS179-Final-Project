using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReactState : ReactState
{
    protected new SlimeFSM machine;
    protected new SlimeProperties properties;
    public SlimeReactState(SlimeFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }

    public override void OnEnter()
    {
        machine.anim.Play("react");
        originalSpeed = machine.agent.speed;
        machine.agent.speed = 0;
        properties.CanReact = false;
    }
    public override void OnExit()
    {
        machine.agent.speed = originalSpeed;
        properties.CanReact = true;
    }
    public override void OnUpdate()
    {
        animInfo = machine.anim.GetCurrentAnimatorStateInfo(0);
        machine.FlipToPlayer();
        if (animInfo.normalizedTime >= 0.99f)
        {
            machine.ToState(State.Chase);
        }
    }
}
