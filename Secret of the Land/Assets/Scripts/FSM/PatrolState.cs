using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    protected FSM machine;
    protected Properties properties;
    protected SoundClips soundClips;
    protected Vector2 targetPos;
    protected TerrainDetector.TerrainType currentTerrainType;
    protected float stuckTimer = 0;

    public PatrolState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
        this.soundClips = machine.soundClips;
    }
    public virtual void OnEnter()
    {

    }
    public virtual void OnExit()
    {

    }
    public virtual void OnUpdate()
    {

    }

    protected void HandleStuck()
    {
        if (machine.agent.velocity.x < 0.1f && machine.agent.velocity.y < 0.1f)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer > 0.2f)
            {
                machine.ToState(State.Patrol);
            }
        }
        else
        {
            stuckTimer = 0;
        }
    }
}
