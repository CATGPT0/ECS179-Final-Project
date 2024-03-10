using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChaseState : ChaseState
{
    protected new SkeletonFSM machine;
    protected new SkeletonProperties properties;
    public event EventHandler OnChase;
    public SkeletonChaseState(SkeletonFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }

    public override void OnEnter()
    {
        Debug.Log("ChaseState: OnEnter");
        originalSpeed = machine.agent.speed;
        machine.agent.speed *= chaseSpeedMultiplier;
        machine.agent.ResetPath();
        machine.anim.Play("walk");
    }

    public override void OnExit()
    {
        machine.agent.ResetPath();
        machine.agent.speed = originalSpeed;
        machine.stuckTimer = 0;
    }

    public override void OnUpdate()
    {
        machine.agent.SetDestination(properties.Player.position);
        machine.Flip();
        if (!properties.SeePlayer)
        {
            machine.ToState(State.Idle);
        }
        if (Physics2D.OverlapCircle(machine.attackPoint.position, machine.attackRange, machine.targetLayer) && properties.CanAttack && machine.agent.isOnNavMesh)
        {
            machine.ToState(State.Attack);
        }
        if (properties.Health <= 0)
        {
            machine.ToState(State.Death);
        }
        //HandleStuck();
    }
}

