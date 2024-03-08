using System.Collections;
using System.Collections.Generic;
using CardBattle;
using UnityEngine;

public class IdleState : IState
{
    private FSM machine;
    private Properties properties;
    private SoundClips soundClips;
    private float timer = 0;
    private float maxIdleTime = 3f;

    public IdleState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
        this.soundClips = machine.soundClips;
    }
    public void OnEnter()
    {
        maxIdleTime = Random.Range(3f, 5f);
        machine.agent.ResetPath();
        machine.anim.Play("idle");
    }
    public void OnExit()
    {
        machine.agent.ResetPath();
        timer = 0;
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer > maxIdleTime)
        {
            machine.ToState(State.Patrol);
        }

        if (properties.seePlayer)
        {
            machine.ToState(State.Chase);
        }
    }
}
