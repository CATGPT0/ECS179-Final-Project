using System.Collections;
using System.Collections.Generic;
using Schema.Builtin.Nodes;
using UnityEngine;

public class SkeletonPatrolState : PatrolState
{
    protected new SkeletonFSM machine;
    protected new SkeletonProperties properties;

    public SkeletonPatrolState(SkeletonFSM machine) : base(machine)
    {
        this.machine = machine;
        this.properties = this.machine.properties;
        this.soundClips = machine.soundClips;
    }

    public override void OnEnter()
    {
        machine.agent.ResetPath();
        machine.anim.Play("walk");
        // Choose a random position within the patrol radius
        var randomPos = Random.insideUnitCircle * properties.PatrolRadius;
        targetPos = new Vector2(properties.CurrentPos.x + randomPos.x, properties.CurrentPos.y + randomPos.y);
        // targetPos = FindWalkablePoint(targetPos, properties.PatrolRadius);
        // bool isSuccessful = false;

        // while (Vector2.Distance(targetPos, properties.SpawnPosition) > properties.PatrolRadius)
        // {
        //     randomPos = Random.insideUnitCircle * properties.PatrolRadius;
        //     targetPos = new Vector2(properties.CurrentPos.x + randomPos.x, properties.CurrentPos.y + randomPos.y);
        //     // targetPos = FindWalkablePoint(targetPos, properties.PatrolRadius);
        // }
        machine.agent.SetDestination(targetPos);

        // Set audio clip based on the ground type
        machine.audioSource.enabled = true;
        currentTerrainType = properties.GroundType;

        if (currentTerrainType == TerrainDetector.TerrainType.Grass)
        {
            machine.audioSource.clip = soundClips.grass;
        }
        else if (currentTerrainType == TerrainDetector.TerrainType.Road)
        {
            machine.audioSource.clip = soundClips.road;
        }
        else if (currentTerrainType == TerrainDetector.TerrainType.Bridge)
        {
            machine.audioSource.clip = soundClips.wood;
        }
    }

    public override void OnExit()
    {
        machine.audioSource.enabled = false;
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
        ChangeAudioClip();

        if (properties.SeePlayer)
        {
            machine.ToState(State.React);
        }
        if (properties.Health <= 0)
        {
            machine.ToState(State.Death);
        }
    }

    public void ChangeAudioClip()
    {
        if (currentTerrainType != properties.GroundType)
        {
            currentTerrainType = properties.GroundType;
            if (currentTerrainType == TerrainDetector.TerrainType.Grass)
            {
                machine.audioSource.clip = soundClips.grass;
            }
            else if (currentTerrainType == TerrainDetector.TerrainType.Road)
            {
                machine.audioSource.clip = soundClips.road;
            }
            else if (currentTerrainType == TerrainDetector.TerrainType.Bridge)
            {
                machine.audioSource.clip = soundClips.wood;
            }

            if (machine.audioSource.isPlaying)
            {
                machine.audioSource.Stop();
            }
                machine.audioSource.Play();
        }
    }
}
