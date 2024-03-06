using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    [SerializeField]
    protected int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    [SerializeField]
    protected int attackPower;
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    [SerializeField]
    protected int armor;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    [SerializeField]
    protected int magicResist;
    public int MagicResist
    {
        get { return magicResist; }
        set { magicResist = value; }
    }
    [SerializeField]
    protected int speed;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public EntityType.Type entityType;

    protected virtual void Init()
    {
    }

    void Awake()
    {
        Init();
    }
}

