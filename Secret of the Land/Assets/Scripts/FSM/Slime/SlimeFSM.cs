using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProperties : Properties
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
            slimeEvent.onMonsterHealthChanged?.Invoke();
            if (health <= 0)
            {
                Debug.Log("Slime died");
                health = 0;
                slimeEvent.onMonsterDeath?.Invoke();
            }
        }
    }

    public SlimeProperties(int level, EntityType.Type type) : base()
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

    public SlimeEvent slimeEvent;
}
public class SlimeFSM : FSM
{
    [SerializeField]
    private int spawnLevel;
    public new SlimeProperties properties;
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackRange;
    public GameObject player;
    private Coroutine hurtCoroutine;
    protected override void Awake()
    {
        properties = new SlimeProperties(spawnLevel, EntityType.Type.Slime);
        player = GameObject.FindGameObjectWithTag("Player");
        base.Awake();
    }

    protected override void Start()
    {
        properties.slimeEvent = GetComponentInChildren<SlimeEvent>();
        properties.slimeEvent.onMonsterDeath.AddListener(() => ToState(State.Death));
        properties.slimeEvent.onMonsterHealthChanged += GetHurtAnimation;
        properties.SpawnPosition = transform.position;
        states.Add(State.Idle, new SlimeIdleState(this));
        states.Add(State.Attack, new SlimeAttackState(this));
        states.Add(State.React, new SlimeReactState(this));
        states.Add(State.Death, new SlimeDeathState(this));
        states.Add(State.Patrol, new SlimePatrolState(this));
        states.Add(State.Chase, new SlimeChaseState(this));
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = properties.Speed;
        currentState = states[State.Idle];
        currentState.OnEnter();
    }

    protected override void Update()
    {
        base.Update();
        properties.CurrentPos = transform.position;
        if (player != null)
        {
            properties.Player = player.transform;
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
            transform.localScale = new Vector3(6, 6, 1);
        }
        else
        {
            transform.localScale = new Vector3(-6, 6, 1);
        }
    }

    public void GetHurtAnimation()
    {
        if (hurtCoroutine != null)
        {
            return;
        }
        hurtCoroutine = StartCoroutine(HurtAnimation());
        IEnumerator HurtAnimation()
        {
            var sr = GetComponent<SpriteRenderer>();
            var originalColor = sr.color;
            sr.color = new Color(255 / 255f, 155 / 255f, 155 / 255f, 255 / 255f);
            yield return new WaitForSeconds(0.1f);
            sr.color = originalColor;
            hurtCoroutine = null;
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public override void Flip()
    {
        if (agent.velocity.x > 0)
        {
            transform.localScale = new Vector3(6, 6, 1);
        }
        else
        {
            transform.localScale = new Vector3(-6, 6, 1);
        }

    }
}
