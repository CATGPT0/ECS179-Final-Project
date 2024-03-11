using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Properties
{
    protected int maxHealth;
    public virtual int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    protected int health;
    public virtual int Health
    {
        get { return health; }
        set { health = value; }
    }
    protected int attackPower;
    public virtual int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    protected float speed;
    public virtual float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    protected int armor;
    public virtual int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    protected int magicResist;
    public virtual int MagicResist
    {
        get { return magicResist; }
        set { magicResist = value; }
    }
    protected int level;
    public virtual int Level
    {
        get { return level; }
        set { level = value; }
    }
    protected Vector2 currentPos;
    public Vector2 CurrentPos
    {
        get { return currentPos; }
        set { currentPos = value; }
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
    React,
    Patrol,
    Chase,
    Attack,
    Death
}
public class FSM : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform thisPosition;
    public float stuckTimer;
    public AudioSource audioSource;
    public Properties properties = new Properties();
    public SoundClips soundClips = new SoundClips();
    protected IState currentState;
    protected Dictionary<State, IState> states = new Dictionary<State, IState>();

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        thisPosition = transform;
    }
    protected void Start()
    {
        states.Add(State.Idle, new IdleState(this));
        states.Add(State.Walk, new WalkState(this));
        states.Add(State.React, new ReactState(this));
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
        properties.CurrentPos = transform.position;
        currentState.OnUpdate();
    }

    public void ToState(State state)
    {
        currentState.OnExit();
        currentState = states[state];
        currentState.OnEnter();
    } 

    public void Flip()
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

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
