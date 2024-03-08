using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonProperties : Properties
{
    protected Vector2 spawnPosition;
    public Vector2 SpawnPosition
    {
        get { return spawnPosition; }
        set { spawnPosition = value; }
    }
    protected Vector2 currentPos;
    public Vector2 CurrentPos
    {
        get { return currentPos; }
        set { currentPos = value; }
    }
    protected Transform player;
    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }
    protected bool seePlayer = false;
    public bool SeePlayer
    {
        get { return seePlayer; }
        set { seePlayer = value; }
    }
    protected float patrolRadius = 10f;
    public float PatrolRadius
    {
        get { return patrolRadius; }
        set { patrolRadius = value; }
    }
    protected TerrainDetector.TerrainType groundType;

    public TerrainDetector.TerrainType GroundType
    {
        get { return groundType; }
        set { groundType = value; }
    }
    public SkeletonProperties(int level, EntityType.Type type) : base(level, type)
    {
        this.level = level;
        thisType = type;
        maxHealth = Table.healthTable[type][level];
        health = maxHealth;
        attackPower = Table.attackPowerTable[type][level];
        speed = Table.speedTable[type][level];
        armor = Table.armorTable[type][level];
        magicResist = Table.magicResistTable[type][level];
        attackType = Table.attackTypeTable[type];
    }
}
public class SkeletonFSM : FSM
{
    public new SkeletonProperties properties = new SkeletonProperties(1, EntityType.Type.Skeleton);
    protected new void Awake()
    {
        base.Awake();
    }
    protected new void Start()
    {
        properties.SpawnPosition = transform.position;
        states.Add(State.Idle, new SkeletonIdleState(this));
        states.Add(State.Attack, new SkeletonAttackState(this));
        states.Add(State.Death, new SkeletonDeathState(this));
        states.Add(State.Patrol, new SkeletonPatrolState(this));
        states.Add(State.Chase, new SkeletonChaseState(this));
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = states[State.Idle];
        currentState.OnEnter();
    }

    protected new void Update()
    {
        base.Update();
        properties.CurrentPos = transform.position;
        properties.Player = GameObject.FindGameObjectWithTag("Player").transform;
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
            properties.GroundType = TerrainDetector.TerrainType.Grass;
        }
        else if (other.CompareTag("Road"))
        {
            properties.GroundType = TerrainDetector.TerrainType.Road;
        }
        else if (other.CompareTag("Bridge"))
        {
            properties.GroundType = TerrainDetector.TerrainType.Bridge;
        }
    }

    public void FindPlayer()
    {
        properties.SeePlayer = true;
    }

    public void LosePlayer()
    {
        properties.SeePlayer = false;
    }
}