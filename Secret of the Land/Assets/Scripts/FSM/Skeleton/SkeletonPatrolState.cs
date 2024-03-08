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
        this.soundClips = machine.soundClips;
    }

    public override void OnEnter()
    {
        machine.agent.ResetPath();
        machine.anim.Play("walk");
        // Choose a random position within the patrol radius
        var randomPos = Random.insideUnitCircle * properties.patrolRadius;
        targetPos = new Vector2(properties.currentPos.x + randomPos.x, properties.currentPos.y + randomPos.y);
        // bool isSuccessful = false;

        while (Vector2.Distance(targetPos, properties.spawnPosition) > properties.patrolRadius)
        {
            randomPos = Random.insideUnitCircle * properties.patrolRadius;
            targetPos = new Vector2(properties.currentPos.x + randomPos.x, properties.currentPos.y + randomPos.y);
        }
        machine.agent.SetDestination(targetPos);

        // Set audio clip based on the ground type
        machine.audioSource.enabled = true;
        currentTerrainType = properties.groundType;

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
        stuckTimer = 0;
    }

    public override void OnUpdate()
    {
        if (Vector2.Distance(properties.currentPos, targetPos) < 0.1f)
        {
            machine.ToState(State.Idle);
        }
        machine.FlipTo(targetPos);
        HandleStuck();
        ChangeAudioClip();

        if (properties.seePlayer)
        {
            machine.ToState(State.Chase);
        }
    }

    public void ChangeAudioClip()
    {
        if (currentTerrainType != properties.groundType)
        {
            currentTerrainType = properties.groundType;
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
