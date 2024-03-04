using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

public class Entity : MonoBehaviour
{
    
    protected HealthComponent health;
    public HealthComponent Health
    {
        get { return health; }
        set { health = value; }
    }
    protected AttackPowerComponent attackPower;
    public AttackPowerComponent AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    protected ArmorComponent armor;
    public ArmorComponent Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    protected MagicResistComponent magicResist;
    public MagicResistComponent MagicResist
    {
        get { return magicResist; }
        set { magicResist = value; }
    }

    protected SpeedComponent speed;
    public SpeedComponent Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    protected virtual void Init()
    {
        health = GetComponentInChildren<HealthComponent>();
        attackPower = GetComponentInChildren<AttackPowerComponent>();
        armor = GetComponentInChildren<ArmorComponent>();
        magicResist = GetComponentInChildren<MagicResistComponent>();
        speed = GetComponentInChildren<SpeedComponent>();
    }

    void Awake()
    {
        Init();
    }
}

