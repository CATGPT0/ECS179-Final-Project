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
    protected float patrolRadius;
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

    private bool canReact = true;
    public bool CanReact
    {
        get { return canReact; }
        set { canReact = value; }
    }

    private bool canAttack = true;
    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public override int Health
    {
        get { return health; }
        set
        {
            health = value;
            skeletonEvent.onMonsterHealthChanged?.Invoke();
            if (health <= 0)
            {
                Debug.Log("Skeleton died");
                health = 0;
                skeletonEvent.onMonsterDeath?.Invoke();
            }
        }
    }

    public SkeletonEvent skeletonEvent;
    
    public SkeletonProperties(int level, EntityType.Type type) : base()
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
        patrolRadius = 8f;
    }
}
public class SkeletonFSM : FSM
{
    [SerializeField]
    private int spawnLevel;
    public new SkeletonProperties properties;
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackRange;
    public GameObject player;
    protected new void Awake()
    {
        properties = new SkeletonProperties(spawnLevel, EntityType.Type.Skeleton);
        player = GameObject.FindGameObjectWithTag("Player");
        base.Awake();
    }
    protected new void Start()
    {
        properties.skeletonEvent = GetComponentInChildren<SkeletonEvent>();
        properties.skeletonEvent.onMonsterDeath.AddListener(() => ToState(State.Death));
        properties.skeletonEvent.onMonsterHealthChanged += GetHurtAnimation;
        properties.SpawnPosition = transform.position;
        states.Add(State.Idle, new SkeletonIdleState(this));
        states.Add(State.Attack, new SkeletonAttackState(this));
        states.Add(State.React, new SkeletonReactState(this));
        states.Add(State.Death, new SkeletonDeathState(this));
        states.Add(State.Patrol, new SkeletonPatrolState(this));
        states.Add(State.Chase, new SkeletonChaseState(this));
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = properties.Speed;
        currentState = states[State.Idle];
        currentState.OnEnter();
    }

    protected new void Update()
    {
        base.Update();
        if (player != null)
        {
            properties.Player = player.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
        properties.CanReact = true;
    }

    public void FlipToPlayer()
    {
        if (properties.Player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void GetHurtAnimation()
    {
        IEnumerator HurtAnimation()
        {
            var sr = GetComponent<SpriteRenderer>();
            var originalColor = sr.color;
            sr.color = new Color(255 / 255f, 155 / 255f, 155 / 255f, 255 / 255f);
            yield return new WaitForSeconds(0.1f);
            sr.color = originalColor;
        }
        StartCoroutine(HurtAnimation());
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
