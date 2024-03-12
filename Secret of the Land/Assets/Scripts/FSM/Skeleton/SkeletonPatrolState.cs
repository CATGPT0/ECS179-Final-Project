using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrolState : PatrolState
{
    protected new SkeletonFSM machine;
    protected new SkeletonProperties properties;

    public SkeletonPatrolState(SkeletonFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = this.machine.properties;
    }

    public override void OnEnter()
    {
        machine.agent.ResetPath();
        machine.anim.Play("walk");
        // Choose a random position within the patrol radius
        var randomPos = Random.insideUnitCircle * properties.PatrolRadius;
        targetPos = new Vector2(properties.CurrentPos.x + randomPos.x, properties.CurrentPos.y + randomPos.y);
        machine.agent.SetDestination(targetPos);
    }

    public override void OnExit()
    {
        machine.agent.ResetPath();
        machine.stuckTimer = 0;
    }

    public override void OnUpdate()
    {
        if (Vector2.Distance(properties.CurrentPos, targetPos) < 0.1f)
        {
            machine.ToState(State.Idle);
        }
        machine.Flip();

        if (properties.SeePlayer)
        {
            machine.ToState(State.React);
        }
    }
}
