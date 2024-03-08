using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Properties
{
    public int health;
    public int attackPower;
    public int speed;
    public int armor;
    public int magicResist;
    public int level;
    public AttackType attackType;
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
    public Properties properties = new Properties();
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

    public void FlipTo(Vector2 target)
    {
        if (target.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
