using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeathState : DeathState
{
    private new SlimeFSM machine;
    private new SlimeProperties properties;
    public SlimeDeathState(SlimeFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }

    public override void OnEnter()
    {
        machine.anim.Play("death");
        machine.agent.speed = 0;
    }
    public override void OnExit()
    {
        Debug.Log("DeathState: OnExit");
    }
    public override void OnUpdate()
    {
        stateInfo = machine.anim.GetCurrentAnimatorStateInfo(0);
    }
}
