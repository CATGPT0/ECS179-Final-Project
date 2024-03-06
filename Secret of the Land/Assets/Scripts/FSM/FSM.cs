using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Properties
{
    public string name;
    public int health;
    public int damage;
    public int speed;
    public int armor;
}

public enum State
{
    Idle,
    Walk,
    Patrol,
    Attack,
    Death
}
public class FSM : MonoBehaviour
{
    public Animator anim;
    public Properties properties = new Properties();
    private IState currentState;
    private Dictionary<State, IState> states = new Dictionary<State, IState>();

    void Start()
    {
        states.Add(State.Idle, new IdleState(this));
        states.Add(State.Walk, new WalkState(this));
        states.Add(State.Attack, new AttackState(this));
        states.Add(State.Death, new DeathState(this));
        states.Add(State.Patrol, new PatrolState(this));
        currentState = states[State.Idle];
        currentState.OnEnter();
    }

    public void ToState(State state)
    {
        currentState.OnExit();
        currentState = states[state];
        currentState.OnEnter();
    }
    
}
