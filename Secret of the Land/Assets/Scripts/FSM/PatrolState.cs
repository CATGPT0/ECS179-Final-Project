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
}
