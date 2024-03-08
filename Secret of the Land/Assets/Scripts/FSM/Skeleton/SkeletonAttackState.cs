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

    public new void OnEnter()
    {
        Debug.Log("AttackState: OnEnter");
    }
    public new void OnExit()
    {
        Debug.Log("AttackState: OnExit");
    }
    public new void OnUpdate()
    {
        Debug.Log("AttackState: OnUpdate");
    }
}
