using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChaseState : ChaseState
{
    protected new SlimeFSM machine;
    protected new SlimeProperties properties;
    public SlimeChaseState(SlimeFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
    }

    public override void OnEnter()
    {
        Debug.Log("ChaseState: OnEnter");
        originalSpeed = machine.agent.speed;
        machine.agent.speed *= chaseSpeedMultiplier;
        //machine.agent.ResetPath();
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
        if (Physics2D.OverlapCircle(machine.attackPoint.position, machine.attackRange, machine.targetLayer) && properties.CanAttack)
        {
            machine.ToState(State.Attack);
        }
        HandleStuck();
    }

    public void HandleStuck()
    {
        if (Vector2.Distance(lastPos, properties.CurrentPos) < 0.02f)
        {
            machine.stuckTimer += Time.deltaTime;
            if (machine.stuckTimer > 2f)
            {
                Debug.Log("Stuck");
                machine.agent.Warp(properties.CurrentPos + UnityEngine.Random.insideUnitCircle * 2f);
                machine.ToState(State.Idle);
            }
        }
        else
        {
            lastPos = properties.CurrentPos;
            machine.stuckTimer = 0;
        }
    }
}
