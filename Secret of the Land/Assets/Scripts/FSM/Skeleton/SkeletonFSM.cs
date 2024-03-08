using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonProperties : Properties
{
    public Vector2 spawnPosition;
    public Vector2 currentPos;
    public Transform player;
    public bool seePlayer = false;
    public float patrolRadius = 10f;
    public TerrainDetector.TerrainType groundType;
}
public class SkeletonFSM : FSM
{
    public new SkeletonProperties properties = new SkeletonProperties();
    protected new void Awake()
    {
        base.Awake();
        properties.patrolRadius = 5f;
    }
    protected new void Start()
    {
        properties.spawnPosition = transform.position;
        states.Add(State.Idle, new SkeletonIdleState(this));
        states.Add(State.Attack, new SkeletonAttackState(this));
        states.Add(State.Death, new SkeletonDeathState(this));
        states.Add(State.Patrol, new SkeletonPatrolState(this));
        states.Add(State.Chase, new SkeletonChaseState(this));
        currentState = states[State.Idle];
        currentState.OnEnter();
    }

    protected new void Update()
    {
        base.Update();
        properties.currentPos = transform.position;
        properties.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            ToState(State.Idle);
        }
        else if (other.CompareTag("Water"))
        {
            ToState(State.Idle);
        }

        if (other.CompareTag("Grass"))
        {
            properties.groundType = TerrainDetector.TerrainType.Grass;
        }
        else if (other.CompareTag("Road"))
        {
            properties.groundType = TerrainDetector.TerrainType.Road;
        }
        else if (other.CompareTag("Bridge"))
        {
            properties.groundType = TerrainDetector.TerrainType.Bridge;
        }
    }

    public void FindPlayer()
    {
        properties.seePlayer = true;
    }

    public void LosePlayer()
    {
        properties.seePlayer = false;
    }
}
