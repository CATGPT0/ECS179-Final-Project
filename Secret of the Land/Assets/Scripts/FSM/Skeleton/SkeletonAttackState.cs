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
        Physics2D.IgnoreLayerCollision(9, 0, true);
    }
    public override void OnExit()
    {
        Debug.Log("AttackState: OnExit");
        properties.CanAttack = true;
        Physics2D.IgnoreLayerCollision(9, 0, false);
    }
    public override void OnUpdate()
    {
        animInfo = machine.anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime >= 0.95f)
        {
            machine.ToState(State.Chase);
        }
        // if (properties.Health <= 0)
        // {
        //     machine.ToState(State.Death);
        // }
    }
}
