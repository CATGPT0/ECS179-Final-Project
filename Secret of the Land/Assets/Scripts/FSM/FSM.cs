using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Properties
{
    protected int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    protected int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    protected int attackPower;
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    protected float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    protected int armor;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    protected int magicResist;
    public int MagicResist
    {
        get { return magicResist; }
        set { magicResist = value; }
    }
    protected int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    protected AttackType attackType;
    public AttackType AttackType
    {
        get { return attackType; }
        set { attackType = value; }
    }
    protected EntityType.Type thisType;
    public EntityType.Type ThisType
    {
        get { return thisType; }
        set { thisType = value; }
    }

    public Properties(int level, EntityType.Type type)
    {
        thisType = type;
        this.level = level;
        maxHealth = Table.healthTable[type][level];
        health = maxHealth;
        attackPower = Table.attackPowerTable[type][level];
        armor = Table.armorTable[type][level];
        magicResist = Table.magicResistTable[type][level];
        speed = Table.speedTable[type][level];
        attackType = Table.attackTypeTable[type];
    }
}

[Serializable]
public class SoundClips
{
    public AudioClip attack;
    public AudioClip death;
    public AudioClip hit;
    public AudioClip wood;
    public AudioClip grass;
    public AudioClip road;
}

public enum State
{
    Idle,
    Walk,
    Patrol,
    Chase,
    Attack,
    Death
}
public class FSM : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public AudioSource audioSource;
    public Properties properties = new Properties(1, EntityType.Type.Player);
    public SoundClips soundClips = new SoundClips();
    protected IState currentState;
    protected Dictionary<State, IState> states = new Dictionary<State, IState>();

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }
    protected void Start()
    {
        states.Add(State.Idle, new IdleState(this));
        states.Add(State.Walk, new WalkState(this));
        states.Add(State.Attack, new AttackState(this));
        states.Add(State.Death, new DeathState(this));
        states.Add(State.Patrol, new PatrolState(this));
        states.Add(State.Chase, new ChaseState(this));
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = states[State.Idle];
        currentState.OnEnter();
    }
    
    protected void Update()
    {
        currentState.OnUpdate();
    }

    public void ToState(State state)
    {
        currentState.OnExit();
        currentState = states[state];
        currentState.OnEnter();
    } 

    public void FlipTo()
    {
        if (agent.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
}
