using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonReactState : ReactState
{
    private new SkeletonFSM machine;
    private new SkeletonProperties properties;
    public SkeletonReactState(SkeletonFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }

    public new void OnEnter()
    {
        Debug.Log("DeathState: OnEnter");
    }
    public new void OnExit()
    {
        Debug.Log("DeathState: OnExit");
    }
    public new void OnUpdate()
    {
        Debug.Log("DeathState: OnUpdate");
    }
}
