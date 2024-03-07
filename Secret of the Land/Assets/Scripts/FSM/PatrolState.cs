using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private FSM machine;
    private Properties properties;
    private SoundClips soundClips;
    private Vector2 targetPos;
    private TerrainDetector.TerrainType currentTerrainType;
    private float stuckTimer = 0;

    public PatrolState(FSM machine)
    {
        this.machine = machine;
        this.properties = machine.properties;
        this.soundClips = machine.soundClips;
    }
    public void OnEnter()
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
    public void OnExit()
    {
        machine.audioSource.enabled = false;
        machine.agent.ResetPath();
        stuckTimer = 0;
    }
    public void OnUpdate()
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

    private void HandleStuck()
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
