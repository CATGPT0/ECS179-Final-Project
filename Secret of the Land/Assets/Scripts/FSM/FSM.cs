using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Properties
{
    public string name;
    public int health;
    public int damage;
    public int speed;
    public int armor;
    public Vector2 spawnPosition;
    public Vector2 currentPos;
    public Transform player;
    public bool seePlayer = false;
    public float patrolRadius = 10f;
    public TerrainDetector.TerrainType groundType;
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
    private IState currentState;
    private Dictionary<State, IState> states = new Dictionary<State, IState>();

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        properties.spawnPosition = transform.position;
        states.Add(State.Idle, new IdleState(this));
        states.Add(State.Walk, new WalkState(this));
        states.Add(State.Attack, new AttackState(this));
        states.Add(State.Death, new DeathState(this));
        states.Add(State.Patrol, new PatrolState(this));
        states.Add(State.Chase, new ChaseState(this));
        currentState = states[State.Idle];
        currentState.OnEnter();
    }
    
    void Update()
    {
        properties.currentPos = transform.position;
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
