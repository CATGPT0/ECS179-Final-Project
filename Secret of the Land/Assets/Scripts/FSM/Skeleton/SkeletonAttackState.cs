using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : AttackState
{
    protected new SkeletonFSM machine;
    protected new SkeletonProperties properties;

    public SkeletonAttackState(SkeletonFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }

    public override void OnEnter()
    {
        properties.CanAttack = false;
        machine.anim.Play("attack");
    }
    public override void OnExit()
    {
        Debug.Log("AttackState: OnExit");
    }
    public override void OnUpdate()
    {
        animInfo = machine.anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime >= 0.99f)
        {
            properties.CanAttack = true;
            machine.ToState(State.Chase);
        }
    }
}
